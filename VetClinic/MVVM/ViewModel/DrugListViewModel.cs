using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        // for admin role
        public AsyncRelayCommand AddNewDrugCommand { get; }
        public AsyncRelayCommand EditDrugCommand { get; }
        public AsyncRelayCommand RemoveDrugCommand { get; }
        public AsyncRelayCommand SaveDrugListCommand { get; }

        public DrugListViewModel(IUserSessionService userSessionService, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _userSessionService = userSessionService;
            _contextFactory = contextFactory;

            AddNewDrugCommand = new AsyncRelayCommand(AddNewDrug);
            RemoveDrugCommand = new AsyncRelayCommand(RemoveDrug);
            SaveDrugListCommand = new AsyncRelayCommand(SaveDrugList);
            EditDrugCommand = new AsyncRelayCommand(EditDrug);


            Drugs = new();

            ResetFormFields();

            _ = LoadDrugs();
        }

        private void ResetFormFields()
        {
            Id = Name = Description = DosageForm = Manufacturer = UnitOfMeasure = Strength = string.Empty;
        }

        private async Task SaveDrug(object obj)
        {
            Trace.WriteLine("save drug");
        }

        private async Task EditDrug(object obj)
        {
            if (Id != string.Empty)
            {
                Drug selectedDrug = Drugs.First(d => d.Id == int.Parse(Id));
                if (selectedDrug != null)
                {
                    if (
                        Name != selectedDrug.Name || 
                        DosageForm != selectedDrug.DosageForm || 
                        Description != selectedDrug.Description || 
                        Manufacturer != selectedDrug.Manufacturer || 
                        UnitOfMeasure != selectedDrug.UnitOfMeasure ||
                        Strength != selectedDrug.Strength
                        )
                    {
                        var userChoice = MessageBox.Show("You have made some changed with currently selected drug. Do you want to save changes?", "Information", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    
                        if (userChoice == MessageBoxResult.Yes)
                        {
                            await SaveDrug(selectedDrug);
                        }
                    }
                }
            }

            if (obj is Drug drug && drug != null)
            {
                Id = drug.Id.ToString();
                Name = drug.Name;
                DosageForm = drug.DosageForm;
                Description = drug.Description;
                Manufacturer = drug.Manufacturer;
                UnitOfMeasure = drug.UnitOfMeasure;
                Strength = drug.Strength;
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

        private async Task AddNewDrug(object arg)
        {
            throw new NotImplementedException();
        }

        private async Task RemoveDrug(object arg)
        {
            if (arg is Drug drug && drug != null)
            {
                var acceptation = MessageBox.Show("Are you sure to remove this drug?", "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (acceptation == MessageBoxResult.Yes)
                {
                    using var context = _contextFactory.CreateDbContext();

                    //context.Drug.Remove(drug);
                    //await context.SaveChangesAsync();

                    // update ui
                    //Drugs.Remove(drug);
                }

            }
        }

        private async Task SaveDrugList(object arg)
        {
            throw new NotImplementedException();
        }
    }
}
