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
        public AsyncRelayCommand SavePrescriptionCommand { get; }
        public RelayCommand RemoveDrugFromPrescriptionCommand { get; }


        public Action<Prescription> PrescriptionUpdated;
        public PrescriptionFormViewModel(Prescription prescription, Action<Prescription> prescriptionUpdated, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _contextFactory = contextFactory;

            ExpiryDateErrorMessage = string.Empty;
            IsEditing = false;
            PrescriptionDrugs = new();

            Prescription = prescription;
            PrescriptionUpdated = prescriptionUpdated;

            AddDrugToPrescriptionCommand = new RelayCommand(AddDrugToPrescription);
            SavePrescriptionCommand = new AsyncRelayCommand(SavePrescription);
            RemoveDrugFromPrescriptionCommand = new RelayCommand(RemoveDrugFromPrescription);

            _ = GetDrugs();
        }

        private void OnPrescriptionChanged()
        {            
            IsEditing = Prescription?.Id != null;

            if (IsEditing)
            {
                ExpiryDate = Prescription?.ExpiryDate ?? DateTime.Now;

                // Add to the array drugs, that are already tied with this prescription
                //if (Prescription?.PrescriptionDrugs.Count() > 0)
                //{
                //    foreach (var prescriptionDrug in Prescription.PrescriptionDrugs)
                //    { 
                //        PrescriptionDrugs.Add(new PrescriptionDrug()
                //        {
                //            Quantity = prescriptionDrug.Quantity,
                //            DosageInstructions = prescriptionDrug?.DosageInstructions ?? "",
                //            Id = prescriptionDrug.DrugId,
                //            Name = prescriptionDrug.Drug.Name,
                //            DosageForm = prescriptionDrug.Drug?.DosageForm ?? "",
                //            Manufacturer = prescriptionDrug.Drug.Manufacturer,
                //        });
                //    }
                //}
            }
        }

        private void RemoveDrugFromPrescription(object obj)
        {
            if (obj is PrescriptionDrug drug)
            {
                PrescriptionDrugs.Remove(drug);
            }
        }

        private async Task SavePrescription(object obj)
        {
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
                return;

            if (PrescriptionDrugs.Count() == 0)
            {
                MessageBox.Show("You have not added any drugs yet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var drug in PrescriptionDrugs)
            {
                if (drug.Quantity == 0)
                {
                    MessageBox.Show($"Quantity of drug ({drug.Name}) has improper quantity", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var matchinDrug = Drugs.Where(d => d.Id == drug.Id).FirstOrDefault();
                if (matchinDrug == null || drug.Quantity > matchinDrug.Quantity)
                {
                    MessageBox.Show($"Quantity of drug ({drug.Name}) has improper quantity - selected too many products.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(drug.DosageInstructions))
                {
                    MessageBox.Show($"Dosage instructions of drug ({drug.Name}) cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Saving changes in the database
            using var context = _contextFactory.CreateDbContext();

            Prescription prescriptionEntity;

            if (IsEditing)
            {
                prescriptionEntity = await context.Prescription
                    .Include(p => p.PrescriptionDrugs)
                        .ThenInclude(pd => pd.Drug)
                    .FirstOrDefaultAsync(p => p.Id == Prescription.Id);

                if (prescriptionEntity == null)
                {
                    MessageBox.Show("Cannot update prescription (not found)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                prescriptionEntity = new Prescription
                {
                    AppointmentId = Prescription.AppointmentId,
                    CreatedAt = DateTime.Now,
                };
                context.Prescription.Add(prescriptionEntity);
            }

            prescriptionEntity.ExpiryDate = ExpiryDate;

            context.PrescriptionDrugs.RemoveRange(prescriptionEntity.PrescriptionDrugs);

            foreach (var drug in PrescriptionDrugs)
            {
                context.PrescriptionDrugs.Add(new PrescriptionDrugs
                {
                    Prescription = prescriptionEntity,
                    DrugId = drug.Id,
                    Quantity = drug.Quantity,
                    DosageInstructions = drug.DosageInstructions,
                });
            }

            await context.SaveChangesAsync();

            // Get the updated prescription from the database (including drug)
            Prescription updatedPrescription = await context.Prescription
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Doctor)
                .Include(p => p.Appointment)
                    .ThenInclude(a => a.Pet)
                        .ThenInclude(p => p.User)
                .Include(p => p.PrescriptionDrugs)
                    .ThenInclude(pd => pd.Drug)
                .FirstOrDefaultAsync(p => p.Id == Prescription.Id);

            PrescriptionUpdated?.Invoke(updatedPrescription);
        }

        private void AddDrugToPrescription(object obj)
        {
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
                        DosageInstructions = "",
                        Quantity = 1,
                    });

                return;
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
            }
            catch (Exception e)
            {
                Trace.TraceWarning("Cannot fetch drugs while creating/editing a prescription");
            }
        }
    }
}
