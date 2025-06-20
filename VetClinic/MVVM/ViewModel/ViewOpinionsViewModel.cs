using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.ViewModel;
using VetClinic.MVVM.ViewModel.Dashboard;
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
            BackToDoctorDashboardCommand = new RelayCommand(BackToDoctorDashboard);
            AddOpinionCommand = new RelayCommand(AddOpinion, _ => CanAddOpinion);
            EditOpinionCommand = new RelayCommand(EditOpinion, _ => CanEditOpinion);
            SaveOpinionCommand = new RelayCommand(SaveOpinion, _ => CanSaveOpinion);
            CancelOpinionCommand = new RelayCommand(CancelOpinion);
            DeleteOpinionCommand = new RelayCommand(DeleteOpinion, _ => CanDeleteOpinion);

            IsDoctor = _userSessionService.IsDoctor;
            IsAdmin = _userSessionService.IsAdmin;
            IsClient = _userSessionService.IsClient;

            _ = LoadOpinionsAsync();
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

        public string DoctorDisplayName => SelectedDoctor != null ? $" {SelectedDoctor.Name} {SelectedDoctor.Surname}" : string.Empty;

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
        private bool _isClient;
        public bool IsClient
        {
            get => _isClient;
            set
            {
                _isClient = value;
                OnPropertyChanged();
            }
        }

        private bool _isDoctor;
        public bool IsDoctor
        {
            get => _isDoctor;
            set
            {
                _isDoctor = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        public bool CanAddOpinion => IsClient && !HasUserOpinion && IsNotAddingOrEditingOpinion;
        public bool CanEditOpinion => IsClient && HasUserOpinion && IsNotAddingOrEditingOpinion;
        public bool CanDeleteOpinion => IsClient && HasUserOpinion && IsNotAddingOrEditingOpinion;
        public bool CanSaveOpinion => !string.IsNullOrWhiteSpace(NewOpinionComment) && NewOpinionRating > 0;

        public RelayCommand BackToDoctorsCommand { get; }
        public RelayCommand BackToDoctorDashboardCommand { get; }
        public RelayCommand BackToAdminDashboardCommand { get; }
        public RelayCommand AddOpinionCommand { get; }
        public RelayCommand EditOpinionCommand { get; }
        public RelayCommand SaveOpinionCommand { get; }
        public RelayCommand CancelOpinionCommand { get; }
        public RelayCommand DeleteOpinionCommand { get; }

        public async Task LoadOpinionsAsync()
        {
            if (IsClient && SelectedDoctor == null)
                return;

            using var context = _contextFactory.CreateDbContext();

            try
            {
                List<Opinion> opinions = new();

                // Pobieranie danych jako klient
                if (IsClient)
                {
                    // Ładowanie opinii dla wybranego lekarza z dołączeniem danych użytkownika (klienta)
                    opinions = await context.Opinion
                        .Include(o => o.User)
                        .Include(o => o.Doctor)
                        .Where(o => o.DoctorId == SelectedDoctor.Id)
                        .OrderByDescending(o => o.CreatedAt)
                        .ToListAsync();
                } 
                else if (IsDoctor)
                {
                    Trace.WriteLine("Loading opinions!!");
                    opinions = await context.Opinion
                        .Include(o => o.User)
                        .Include(o => o.Doctor)
                        .Where(o => o.DoctorId == _userSessionService.LoggedInDoctor.Id)
                        .OrderByDescending(o => o.CreatedAt)
                        .ToListAsync();
                } 
                else if (IsAdmin)
                {
                    opinions = await context.Opinion
                        .Include(o => o.User)
                        .Include(o => o.Doctor)
                        .OrderByDescending(o => o.CreatedAt)
                        .ToListAsync();
                }

                var detailedOpinions = opinions.Select(o => new DetailedOpinion
                {
                    Opinion = o,
                    ClientName = $"{o.User.Name} {o.User.Surname}", 
                    DoctorName = $"{o.Doctor.Name} {o.Doctor.Surname}",
                    CommentPreview = o.Comment.Length > 100 ? o.Comment.Substring(0, 100) + "..." : o.Comment,
                    TimeAgoText = GetTimeAgoText(o.CreatedAt)
                }).ToList();

                Opinions = new ObservableCollection<DetailedOpinion>(detailedOpinions);

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

        private void BackToDoctorDashboard(object obj)
        {
            _navigationService.NavigateTo<DoctorDashboardViewModel>();
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
                        ClientId = _userSessionService.LoggedInUser.Id, 
                        Comment = NewOpinionComment.Trim(),
                        Rating = NewOpinionRating,
                        CreatedAt = DateTime.Now
                    };

                    context.Opinion.Add(newOpinion); 
                }
                else if (IsEditingOpinion && UserOpinion != null)
                {
                    var existingOpinion = await context.Opinion
                        .FirstOrDefaultAsync(o => o.Id == UserOpinion.Opinion.Id);

                    if (existingOpinion != null)
                    {
                        existingOpinion.Comment = NewOpinionComment.Trim();
                        existingOpinion.Rating = NewOpinionRating;
                        existingOpinion.CreatedAt = DateTime.Now; 
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
                return "just added";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} min ago";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hours ago";
            if (timeSpan.TotalDays < 30)
                return $"{(int)timeSpan.TotalDays} days ago";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} months ago";

            return $"{(int)(timeSpan.TotalDays / 365)} years ago";
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