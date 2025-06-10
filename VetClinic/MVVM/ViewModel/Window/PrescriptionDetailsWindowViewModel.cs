using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.Model;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Window
{
    public class PrescriptionDetailsWindowViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private Prescription _prescription;
        public Prescription Prescription
        {
            get => _prescription;
            set
            {
                _prescription = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PrescriptionDrug> _prescriptionDrugs;
        public ObservableCollection<PrescriptionDrug> PrescriptionDrugs
        {
            get => _prescriptionDrugs;
            set
            {
                _prescriptionDrugs = value;
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

        private Drug _selectedDrug;
        public Drug SelectedDrug
        {
            get => _selectedDrug;
            set
            {
                _selectedDrug = value;
                OnPropertyChanged();
            }
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddDrugToPrescriptionCommand { get; }
        public RelayCommand SavePrescriptionCommand { get; }
        public RelayCommand RemoveDrugFromPrescriptionCommand{ get; }
        public PrescriptionDetailsWindowViewModel(Prescription prescription, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _prescription = prescription;
            _contextFactory = contextFactory;

            AddDrugToPrescriptionCommand = new RelayCommand(AddDrugToPrescription);
            SavePrescriptionCommand = new RelayCommand(SavePrescription);
            RemoveDrugFromPrescriptionCommand = new RelayCommand(RemoveDrugFromPrescription);

            IsEditing = prescription?.Id != null;

            PrescriptionDrugs = new();

            if (IsEditing)
            {
                // Add to the array drugs, that are already tied with this prescription
                if (prescription?.PrescriptionDrugs.Count() > 0)
                {
                    Trace.WriteLine("Ilosc lekow " + prescription.PrescriptionDrugs.Count());
                    foreach (var prescriptionDrug in prescription.PrescriptionDrugs)
                    {
                        PrescriptionDrugs.Add(new PrescriptionDrug()
                        {
                            Id = prescriptionDrug.Id,
                            Quantity = prescriptionDrug.Quantity,
                            DosageInstructions = prescriptionDrug?.DosageInstructions ?? "",
                            Name = prescriptionDrug.Drug.Name,
                            DosageForm = prescriptionDrug.Drug?.DosageForm ?? "",
                            Manufacturer = prescriptionDrug.Drug.Manufacturer,
                        });
                    }
                } 
            }
            _ = GetDrugs();
        }

        private void RemoveDrugFromPrescription(object obj)
        {
            throw new NotImplementedException();
        }

        private void SavePrescription(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddDrugToPrescription(object obj)
        {
            if(SelectedDrug != null)
            {
                PrescriptionDrugs.Add(
                    new PrescriptionDrug()
                    {
                        Id = SelectedDrug.Id,
                        Manufacturer = SelectedDrug.Manufacturer,
                        Name = SelectedDrug.Name,
                        DosageForm = SelectedDrug.DosageForm,
                        DosageInstructions = "",
                        Quantity = 1,
                    });
            }
        }

        private async Task GetDrugs()
        {
            using var context = _contextFactory.CreateDbContext();

            try
            {
                var drugs = await context.Drug.ToListAsync();

                if (drugs.Count() > 0)
                {
                    Drugs = new(drugs);

                }
            } catch(Exception e)
            {
                Trace.TraceWarning("Cannot fetch drugs while creating/editing a prescription");
            }
            
        }
    }
}
