using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.Model;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    public class PrescriptionFormViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;

        private Prescription _prescription;
        public Prescription Prescription
        {
            get => _prescription;
            set
            {
                 _prescription = value;
                OnPrescriptionChanged();
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

        private DateTime _expityDate;
        public DateTime ExpiryDate
        {
            get => _expityDate;
            set
            {
                _expityDate = value;
                OnPropertyChanged();
            }
        }

        // Errors messages
        private string _expiryDateErrorMessage;
        public string ExpiryDateErrorMessage
        {
            get => _expiryDateErrorMessage;
            set
            {
                _expiryDateErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _hasPrescription;
        public bool HasPrescription
        {
            get => _hasPrescription;
            set
            {
                _hasPrescription = value;
                OnPropertyChanged();
            }
        }


        private bool _isAppointmentCompleted;
        public bool IsAppointmentCompleted
        {
            get => _isAppointmentCompleted;
            set
            {
                _isAppointmentCompleted = value;
                OnPropertyChanged();
            }
        }

        private bool _isPrescriptionDisplayed;
        public bool IsPrescriptionDisplayed
        {
            get => _isPrescriptionDisplayed;
            set
            {
                _isPrescriptionDisplayed = value;
                OnPropertyChanged();
            }
        }

        // For storing client and doctor names
        private DetailedPrescription _detailedPrescription;
        public DetailedPrescription DetailedPrescription {
            get => _detailedPrescription;
            set
            {
                _detailedPrescription = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddDrugToPrescriptionCommand { get; }
        public RelayCommand AddPrescriptionCommand { get; }
        public RelayCommand RemoveDrugFromPrescriptionCommand { get; }

        public PrescriptionFormViewModel(Prescription prescription, DetailedPrescription detailedPrescription, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _contextFactory = contextFactory;

            ExpiryDateErrorMessage = string.Empty;
            PrescriptionDrugs = new();

            DetailedPrescription = detailedPrescription;
            Prescription = prescription;

            AddDrugToPrescriptionCommand = new RelayCommand(AddDrugToPrescription);
            RemoveDrugFromPrescriptionCommand = new RelayCommand(RemoveDrugFromPrescription);
            AddPrescriptionCommand = new RelayCommand(AddPrescription);

            _ = GetDrugs();
        }

        private void AddPrescription(object obj)
        {
            HasPrescription = true;
        }

        private void OnPrescriptionChanged()
        {
            ExpiryDate = Prescription?.ExpiryDate ?? DateTime.Now;

            // Appointment doesn't have prescription
            if (Prescription == null)
                return;

            // If doctors wants to look on completed prescirption, add to the array drugs, that are already tied with this prescription
            if (Prescription?.PrescriptionDrugs.Count() > 0)
            {
                foreach (var prescriptionDrug in Prescription.PrescriptionDrugs)
                {
                    PrescriptionDrugs.Add(new PrescriptionDrug()
                    {
                        Quantity = prescriptionDrug.Quantity,
                        Dosage = prescriptionDrug?.Dosage ?? "",
                        Id = prescriptionDrug.DrugId,
                        Name = prescriptionDrug.Drug.Name,
                        DosageForm = prescriptionDrug.Drug?.DosageForm ?? "",
                        Manufacturer = prescriptionDrug.Drug.Manufacturer,
                    });
                }
            }
        }

        private void RemoveDrugFromPrescription(object obj)
        {
            if (IsAppointmentCompleted)
                return;

            if (obj is PrescriptionDrug drug)
            {
                PrescriptionDrugs.Remove(drug);
            }

            if (PrescriptionDrugs.Count() == 0)
            {
                HasPrescription = false;
            }
        }
        public async Task<bool> CheckPrescription()
        {
            if (IsAppointmentCompleted || !HasPrescription)
                return true;

            ExpiryDateErrorMessage = string.Empty;

            // first validate the fields
            if (ExpiryDate == DateTime.MinValue)
            {
                ExpiryDateErrorMessage = "| Cannot be empty";
            }
            if (ExpiryDate < DateTime.Now)
            {
                ExpiryDateErrorMessage = "| Select proper date";
            }

            if (ExpiryDateErrorMessage.Length != 0)
                return false;

            if (PrescriptionDrugs.Count() == 0)
            {
                MessageBox.Show("You have not added any drugs yet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            foreach (var drug in PrescriptionDrugs)
            {
                if (drug.Quantity <= 0)
                {
                    MessageBox.Show($"Quantity of drug ({drug.Name}) has improper quantity", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                var matchinDrug = Drugs.Where(d => d.Id == drug.Id).FirstOrDefault();
                if (string.IsNullOrEmpty(drug.Dosage))
                {
                    MessageBox.Show($"Dosage instructions of drug ({drug.Name}) cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            // Saving changes in the database
            using var context = _contextFactory.CreateDbContext();

            Prescription prescriptionEntity = new Prescription
            {
                AppointmentId = DetailedPrescription.Appointment.Id,
                CreatedAt = DateTime.Now,
            };

            context.Prescription.Add(prescriptionEntity);

            prescriptionEntity.ExpiryDate = ExpiryDate;

            foreach (var drug in PrescriptionDrugs)
            {
                context.PrescriptionDrugs.Add(new PrescriptionDrugs
                {
                    Prescription = prescriptionEntity,
                    DrugId = drug.Id,
                    Quantity = drug.Quantity,
                    Dosage = drug.Dosage,
                });
            }

            await context.SaveChangesAsync();

            // Get the created prescription from the database (including drug)
            Prescription updatedPrescription = await context.Prescription
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Doctor)
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Pet)
                        .ThenInclude(p => p.User)
                .Include(p => p.PrescriptionDrugs)
                    .ThenInclude(pd => pd.Drug)
                .FirstOrDefaultAsync(p => p.AppointmentId == DetailedPrescription.Appointment.Id);

            return true;
        }

        private void AddDrugToPrescription(object obj)
        {
            if (IsAppointmentCompleted)
                return;

            if (obj is Drug drug)
            {
                if (PrescriptionDrugs.Any(pd => pd.Id == drug.Id))
                    return;

                PrescriptionDrugs.Add(
                    new PrescriptionDrug()
                    {
                        Id = drug.Id,
                        Manufacturer = drug.Manufacturer,
                        Name = drug.Name,
                        DosageForm = drug.DosageForm,
                        Dosage = "",
                        Quantity = 1,
                    });

                return;
            }
        }

        private async Task GetDrugs()
        {
            if (IsAppointmentCompleted)
                return;

            using var context = _contextFactory.CreateDbContext();

            try
            {
                var drugs = await context.Drug.ToListAsync();

                if (drugs.Count() > 0)
                {
                    Drugs = new(drugs);

                }
            }
            catch (Exception e)
            {
                Trace.TraceWarning("Cannot fetch drugs while creating/editing a prescription");
            }
        }
    }
}
