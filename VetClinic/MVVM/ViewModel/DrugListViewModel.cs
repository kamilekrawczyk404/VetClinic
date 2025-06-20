using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    public class DrugListViewModel : ViewModel
    {
        private IUserSessionService _userSessionService;
        private IDbContextFactory<VeterinaryClinicContext> _contextFactory;

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _nameErrorMessage;
        public string NameErrorMessage
        {
            get => _nameErrorMessage;
            set
            {
                _nameErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private string _dosageForm;
        public string DosageForm
        {
            get => _dosageForm;
            set
            {
                _dosageForm = value;
                OnPropertyChanged();
            }
        }

        private string _strength;
        public string Strength
        {
            get => _strength;
            set
            {
                _strength = value;
                OnPropertyChanged();
            }
        }

        private string _unitOfMeasure;
        public string UnitOfMeasure
        {
            get => _unitOfMeasure;
            set
            {
                _unitOfMeasure = value;
                OnPropertyChanged();
            }
        }
        
        private string _manufacturer;
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnPropertyChanged();
            }
        } 
        
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _formModeMessage;
        public string FormModeMessage
        {
            get => _formModeMessage;
            set
            {
                _formModeMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _isAddingDrug = true;
        public bool IsAddingDrug
        {
            get => _isAddingDrug;
            set
            {
                _isAddingDrug = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Drug> _drugs;
        public ObservableCollection<Drug> Drugs
        {
            get => _drugs;
            set
            {
                _drugs = value;
                OnPropertyChanged();
            }
        }

        public AsyncRelayCommand EditDrugCommand { get; }
        public AsyncRelayCommand RemoveDrugCommand { get; }
        public AsyncRelayCommand SaveDrugCommand { get; }
        public RelayCommand BackCommand { get; }

        private Drug _selectedDrug = null;

        public DrugListViewModel(IUserSessionService userSessionService, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _userSessionService = userSessionService;
            _contextFactory = contextFactory;

            RemoveDrugCommand = new AsyncRelayCommand(RemoveDrug);
            SaveDrugCommand = new AsyncRelayCommand(SaveDrug);
            EditDrugCommand = new AsyncRelayCommand(EditDrug);
            BackCommand = new RelayCommand(BackToCreatingDrug);

            Drugs = new();

            ResetFormFields();
            ResetErrorMessages();

            FormModeMessage = "Creating a new drug";

            _ = LoadDrugs();
        }

        private void BackToCreatingDrug(object obj)
        {
            IsAddingDrug = true;
            ResetFormFields();
            ResetErrorMessages();
            FormModeMessage = "Creating a new drug";
        }

        private void ResetFormFields()
        {
            Id = Name = Description = DosageForm = Manufacturer = UnitOfMeasure = Strength = string.Empty;
        }

        private void ResetErrorMessages()
        {
           NameErrorMessage = string.Empty;
        }

        private bool UserMadeChanges()
        {
            if (_selectedDrug == null)
                return false;

            if(
                Name != _selectedDrug.Name ||
                DosageForm != _selectedDrug.DosageForm ||
                Description != _selectedDrug.Description ||
                Manufacturer != _selectedDrug.Manufacturer ||
                UnitOfMeasure != _selectedDrug.UnitOfMeasure ||
                Strength != _selectedDrug.Strength)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> Validate() 
        {
            ResetErrorMessages();

            if (string.IsNullOrEmpty(Name))
            {
                NameErrorMessage = "| Cannot be empty";
            }

            if (NameErrorMessage.Length > 0)
            {
                return false;
            }

            if (_isAddingDrug)
            {
                using var context = _contextFactory.CreateDbContext();

                // check if some drug has the same name with the new one
                var drug = await context.Drug.FirstOrDefaultAsync(d => d.Name == Name);

                if (drug != null)
                {
                    NameErrorMessage = "| This drug already exists";
                    return false;
                }
            }

            return true;
        }

        private async Task SaveDrug(object obj)
        {
            bool isValidated = await Validate();
            if (!isValidated)
                return;

            using var context = _contextFactory.CreateDbContext();

            Drug newDrug = new Drug()
            {
                Name = Name,
                Description = Description,
                UnitOfMeasure = UnitOfMeasure,
                Manufacturer = Manufacturer,
                Strength = Strength,
                DosageForm = DosageForm
            };

            if (IsAddingDrug)
            {
                // store a new drug in the database
                context.Drug.Add(newDrug);
                await context.SaveChangesAsync();

                Drug drugFromDatabase = await context.Drug.FirstOrDefaultAsync(d => d.Name == Name);
                Drugs.Add(drugFromDatabase);
            }
            else
            {
                // udpate stored drug
                newDrug.Id = int.Parse(Id);
                context.Drug.Update(newDrug);
                await context.SaveChangesAsync();

                Drug drugFromDatabase = await context.Drug.FirstOrDefaultAsync(d => d.Name == Name);
                Drugs[Drugs.IndexOf(Drugs.First(d => d.Id == int.Parse(Id)))] = drugFromDatabase;

                IsAddingDrug = true;
                FormModeMessage = "Create a new drug";
            }

            ResetFormFields();
        }

        private async Task EditDrug(object obj)
        {
            if (IsAddingDrug && (Name.Length > 0 || Description.Length > 0 || DosageForm.Length > 0 || UnitOfMeasure.Length > 0 || Strength.Length > 0 || Manufacturer.Length > 0))
            {
                var userChoice = MessageBox.Show("You are currently creating a new drug. Do you still want to cancel this process?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (userChoice != MessageBoxResult.Yes)
                    return;

                ResetFormFields();
            }

            // User has not saved the current edited drug
            if (Id != string.Empty)
            {
                _selectedDrug = Drugs.First(d => d.Id == int.Parse(Id));

                if (UserMadeChanges() == true)
                {
                    var userChoice = MessageBox.Show("You have made some changed with currently selected drug. Do you want to save changes?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    
                    if (userChoice == MessageBoxResult.Yes)
                    {
                        await SaveDrug(_selectedDrug);
                    }
                }
            }

            // Assign values to the properties
            if (obj is Drug drug && drug != null)
            {
                FormModeMessage = $"Editing drug - #{drug.Id}";

                Id = drug.Id.ToString();
                Name = drug.Name;
                DosageForm = drug?.DosageForm ?? "";
                Description = drug?.Description ?? "";
                Manufacturer = drug?.Manufacturer ?? "";
                UnitOfMeasure = drug?.UnitOfMeasure ?? "";
                Strength = drug?.Strength ?? "";
            }

            IsAddingDrug = false;
        }

        private async Task RemoveDrug(object arg)
        {
            if (arg is Drug drug && drug != null)
            {
                var acceptation = MessageBox.Show($"Are you sure to remove this drug? [Id: {drug.Id}, {drug.Name}]", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (acceptation == MessageBoxResult.Yes)
                {
                    using var context = _contextFactory.CreateDbContext();

                    var isAlreadyUsed = await context.PrescriptionDrugs.FirstOrDefaultAsync(pd => pd.DrugId == drug.Id) != null;
                    if (isAlreadyUsed)
                    {
                        MessageBox.Show("This drug is already used in prescriptions! You cannot remove it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    context.Drug.Remove(drug);
                    await context.SaveChangesAsync();

                    Drugs.Remove(drug);
                }
            }
        }
        private async Task LoadDrugs()
        {
            using var context = _contextFactory.CreateDbContext();

            try
            {
                var drugList = await context.Drug.ToListAsync();

                Drugs.Clear();

                foreach(var drug in drugList)
                {
                    Drugs.Add(new Drug()
                    {
                        Id = drug.Id,
                        Name = drug.Name,
                        Description = drug.Description,
                        DosageForm = drug.DosageForm,
                        Manufacturer = drug.Manufacturer,
                        UnitOfMeasure = drug.UnitOfMeasure,
                        Strength = drug.Strength,
                    });
                }
            }catch(Exception e)
            {
                Trace.TraceError("Error occured while trying to get the drug list. " + e.Message);
            }
            throw new NotImplementedException();
        }
    }
}
