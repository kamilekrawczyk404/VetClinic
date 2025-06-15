using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.ViewModel;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    public class ViewOpinionsViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;

        private Doctor _selectedDoctor;
        private ObservableCollection<DetailedOpinion> _opinions;
        private DetailedOpinion _userOpinion;
        private bool _isAddingOpinion;
        private bool _isEditingOpinion;
        private string _newOpinionComment;
        private int _newOpinionRating = 5;

        public ViewOpinionsViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService userSessionService, INavigationService navigationService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;

            Opinions = new ObservableCollection<DetailedOpinion>();

            BackToDoctorsCommand = new RelayCommand(BackToDoctors);
            AddOpinionCommand = new RelayCommand(AddOpinion, _ => CanAddOpinion);
            EditOpinionCommand = new RelayCommand(EditOpinion, _ => CanEditOpinion);
            SaveOpinionCommand = new RelayCommand(SaveOpinion, _ => CanSaveOpinion);
            CancelOpinionCommand = new RelayCommand(CancelOpinion);
            DeleteOpinionCommand = new RelayCommand(DeleteOpinion, _ => CanDeleteOpinion);
        }

        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DoctorDisplayName));
                if (value != null)
                {
                    _ = LoadOpinionsAsync();
                }
            }
        }

        public string DoctorDisplayName => SelectedDoctor != null ? $"Dr {SelectedDoctor.Name} {SelectedDoctor.Surname}" : string.Empty;

        public ObservableCollection<DetailedOpinion> Opinions
        {
            get => _opinions;
            set
            {
                _opinions = value;
                OnPropertyChanged();
            }
        }

        public DetailedOpinion UserOpinion
        {
            get => _userOpinion;
            set
            {
                _userOpinion = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasUserOpinion));
            }
        }

        public bool HasUserOpinion => UserOpinion != null;

        public bool IsAddingOpinion
        {
            get => _isAddingOpinion;
            set
            {
                _isAddingOpinion = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotAddingOrEditingOpinion));
            }
        }

        public bool IsEditingOpinion
        {
            get => _isEditingOpinion;
            set
            {
                _isEditingOpinion = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsNotAddingOrEditingOpinion));
            }
        }

        public bool IsNotAddingOrEditingOpinion => !IsAddingOpinion && !IsEditingOpinion;

        public string NewOpinionComment
        {
            get => _newOpinionComment;
            set
            {
                _newOpinionComment = value;
                OnPropertyChanged();
            }
        }

        public int NewOpinionRating
        {
            get => _newOpinionRating;
            set
            {
                _newOpinionRating = value;
                OnPropertyChanged();
            }
        }

        // Sprawdzenie czy zalogowany użytkownik to klient (role != admin, itp.)
        public bool IsClient => _userSessionService.IsClient;

        public bool CanAddOpinion => IsClient && !HasUserOpinion && IsNotAddingOrEditingOpinion;
        public bool CanEditOpinion => IsClient && HasUserOpinion && IsNotAddingOrEditingOpinion;
        public bool CanDeleteOpinion => IsClient && HasUserOpinion && IsNotAddingOrEditingOpinion;
        public bool CanSaveOpinion => !string.IsNullOrWhiteSpace(NewOpinionComment) && NewOpinionRating > 0;

        public RelayCommand BackToDoctorsCommand { get; }
        public RelayCommand AddOpinionCommand { get; }
        public RelayCommand EditOpinionCommand { get; }
        public RelayCommand SaveOpinionCommand { get; }
        public RelayCommand CancelOpinionCommand { get; }
        public RelayCommand DeleteOpinionCommand { get; }

        public async Task LoadOpinionsAsync()
        {
            if (SelectedDoctor == null)
            {
                return;
            }

            using var context = _contextFactory.CreateDbContext();

            try
            {
                // Ładowanie opinii dla wybranego lekarza z dołączeniem danych użytkownika (klienta)
                var opinions = await context.Opinion
                    .Include(o => o.User) // Zamiast o.Client teraz o.User
                    .Where(o => o.DoctorId == SelectedDoctor.Id)
                    .OrderByDescending(o => o.CreatedAt)
                    .ToListAsync();

                var detailedOpinions = opinions.Select(o => new DetailedOpinion
                {
                    Opinion = o,
                    ClientName = $"{o.User.Name} {o.User.Surname}", // Zmiana z o.Client na o.User
                    DoctorName = $"{SelectedDoctor.Name} {SelectedDoctor.Surname}",
                    CommentPreview = o.Comment.Length > 100 ? o.Comment.Substring(0, 100) + "..." : o.Comment,
                    TimeAgoText = GetTimeAgoText(o.CreatedAt)
                }).ToList();

                Opinions = new ObservableCollection<DetailedOpinion>(detailedOpinions);

                // Znajdowanie opinii zalogowanego użytkownika
                if (IsClient && _userSessionService.LoggedInUser != null)
                {
                    UserOpinion = detailedOpinions.FirstOrDefault(o => o.Opinion.ClientId == _userSessionService.LoggedInUser.Id); 
                }

                OnPropertyChanged(nameof(CanAddOpinion));
                OnPropertyChanged(nameof(CanEditOpinion));
                OnPropertyChanged(nameof(CanDeleteOpinion));
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error loading opinions: {ex.Message}");
                Opinions = new ObservableCollection<DetailedOpinion>();
            }
        }

        private void BackToDoctors(object obj)
        {
            _navigationService.NavigateTo<DoctorListViewModel>();
        }

        private void AddOpinion(object obj)
        {
            if (!CanAddOpinion) return;

            NewOpinionComment = string.Empty;
            NewOpinionRating = 5;
            IsAddingOpinion = true;
        }

        private void EditOpinion(object obj)
        {
            if (!CanEditOpinion || UserOpinion == null) return;

            NewOpinionComment = UserOpinion.Opinion.Comment;
            NewOpinionRating = UserOpinion.Opinion.Rating;
            IsEditingOpinion = true;
        }

        private async void SaveOpinion(object obj)
        {
            if (!CanSaveOpinion || _userSessionService.LoggedInUser == null) return;

            using var context = _contextFactory.CreateDbContext();

            try
            {
                if (IsAddingOpinion)
                {
                    var newOpinion = new Opinion
                    {
                        DoctorId = SelectedDoctor.Id,
                        ClientId = _userSessionService.LoggedInUser.Id, // Zmiana z ClientId na UserId
                        Comment = NewOpinionComment.Trim(),
                        Rating = NewOpinionRating,
                        CreatedAt = DateTime.Now
                    };

                    context.Opinion.Add(newOpinion); // Zmiana z Opinion na Opinions (nazwa tabeli)
                }
                else if (IsEditingOpinion && UserOpinion != null)
                {
                    var existingOpinion = await context.Opinion
                        .FirstOrDefaultAsync(o => o.Id == UserOpinion.Opinion.Id);

                    if (existingOpinion != null)
                    {
                        existingOpinion.Comment = NewOpinionComment.Trim();
                        existingOpinion.Rating = NewOpinionRating;
                        existingOpinion.CreatedAt = DateTime.Now; // Update timestamp
                    }
                }

                await context.SaveChangesAsync();

                IsAddingOpinion = false;
                IsEditingOpinion = false;
                await LoadOpinionsAsync();

                Trace.WriteLine("Opinion saved successfully");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error saving opinion: {ex.Message}");
            }
        }

        private void CancelOpinion(object obj)
        {
            IsAddingOpinion = false;
            IsEditingOpinion = false;
            NewOpinionComment = string.Empty;
            NewOpinionRating = 5;
        }

        private async void DeleteOpinion(object obj)
        {
            if (!CanDeleteOpinion || UserOpinion == null) return;

            using var context = _contextFactory.CreateDbContext();

            try
            {
                var opinionToDelete = await context.Opinion
                    .FirstOrDefaultAsync(o => o.Id == UserOpinion.Opinion.Id);

                if (opinionToDelete != null)
                {
                    context.Opinion.Remove(opinionToDelete);
                    await context.SaveChangesAsync();

                    await LoadOpinionsAsync();
                    Trace.WriteLine("Opinion deleted successfully");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error deleting opinion: {ex.Message}");
            }
        }

        private string GetTimeAgoText(DateTime createdAt)
        {
            var timeSpan = DateTime.Now - createdAt;

            if (timeSpan.TotalMinutes < 1)
                return "przed chwilą";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} min temu";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} godz. temu";
            if (timeSpan.TotalDays < 30)
                return $"{(int)timeSpan.TotalDays} dni temu";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} mies. temu";

            return $"{(int)(timeSpan.TotalDays / 365)} lat temu";
        }
    }

    public class DetailedOpinion
    {
        public Opinion Opinion { get; set; }
        public string ClientName { get; set; }
        public string DoctorName { get; set; }
        public string CommentPreview { get; set; }
        public string TimeAgoText { get; set; }
    }
}