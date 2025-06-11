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

        private string _instructions;
        public string Instructions
        {
            get => _instructions;
            set
            {
                _instructions = value;
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

        private string _instructionsErrorMessage;
        public string InstructionsErrorMessage
        {
            get => _instructionsErrorMessage;
            set
            {
                _instructionsErrorMessage = value;
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
        public RelayCommand RemoveDrugFromPrescriptionCommand{ get; }

        public Action<Prescription> PrescriptionUpdated;
        public PrescriptionDetailsWindowViewModel(Prescription prescription, Action<Prescription> prescriptionUpdated, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _prescription = prescription;
            _contextFactory = contextFactory;
            PrescriptionUpdated = prescriptionUpdated;

            AddDrugToPrescriptionCommand = new RelayCommand(AddDrugToPrescription);
            SavePrescriptionCommand = new AsyncRelayCommand(SavePrescription);
            RemoveDrugFromPrescriptionCommand = new RelayCommand(RemoveDrugFromPrescription);

            IsEditing = prescription?.Id != null;

            // Initialize errors messages with empty string
            ExpiryDateErrorMessage = InstructionsErrorMessage = string.Empty;

            PrescriptionDrugs = new();

            if (IsEditing)
            {
                Instructions = prescription?.Instructions ?? "";
                ExpiryDate = prescription?.ExpiryDate ?? DateTime.Now;

                // Add to the array drugs, that are already tied with this prescription
                if (prescription?.PrescriptionDrugs.Count() > 0)
                {
                    foreach (var prescriptionDrug in prescription.PrescriptionDrugs)
                    {
                        // Check for prescriptionDrug itself
                        if (prescriptionDrug == null)
                        {
                            Trace.WriteLine("ERROR: Found a null 'prescriptionDrug' entity in the collection.");
                            continue; // Skip this iteration if the entity itself is null
                        }

                        // Check direct properties of prescriptionDrug
                        if (prescriptionDrug.Quantity == null) // Assuming 0 for quantity might indicate an issue or default
                        {
                            Trace.WriteLine($"WARNING: prescriptionDrug.Quantity is 0 for Prescription ID: {prescription?.Id}.");
                        }
                        if (prescriptionDrug.DosageInstructions == null)
                        {
                            Trace.WriteLine($"WARNING: prescriptionDrug.DosageInstructions is NULL for Prescription ID: {prescription?.Id}, Drug ID (if accessible): {prescriptionDrug.Drug?.Id}.");
                        }

                        // Check the nested Drug property
                        if (prescriptionDrug.Drug == null)
                        {
                            Trace.WriteLine($"ERROR: prescriptionDrug.Drug is NULL for Prescription ID: {prescription?.Id}, PrescriptionDrug ID: {prescriptionDrug.Id}. Cannot map drug details.");
                        }

                        // Now check properties of the nested Drug object
                        if (prescriptionDrug.Drug.Id == null) // Assuming 0 for ID might indicate an issue or default
                        {
                            Trace.WriteLine($"WARNING: prescriptionDrug.Drug.Id is 0 for Prescription ID: {prescription?.Id}.");
                        }
                        if (prescriptionDrug.Drug.Name == null)
                        {
                            Trace.WriteLine($"WARNING: prescriptionDrug.Drug.Name is NULL for Drug ID: {prescriptionDrug.Drug.Id}, Prescription ID: {prescription?.Id}.");
                        }
                        if (prescriptionDrug.Drug.DosageForm == null)
                        {
                            Trace.WriteLine($"WARNING: prescriptionDrug.Drug.DosageForm is NULL for Drug ID: {prescriptionDrug.Drug.Id}, Name: {prescriptionDrug.Drug.Name}, Prescription ID: {prescription?.Id}.");
                        }
                        if (prescriptionDrug.Drug.Manufacturer == null)
                        {
                            Trace.WriteLine($"WARNING: prescriptionDrug.Drug.Manufacturer is NULL for Drug ID: {prescriptionDrug.Drug.Id}, Name: {prescriptionDrug.Drug.Name}, Prescription ID: {prescription?.Id}.");
                        }


                        PrescriptionDrugs.Add(new PrescriptionDrug()
                        {
                            Quantity = prescriptionDrug.Quantity,
                            DosageInstructions = prescriptionDrug?.DosageInstructions ?? "",
                            Id = prescriptionDrug.DrugId,
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
            if (obj is PrescriptionDrug drug)
            {
                PrescriptionDrugs.Remove(drug);
            }
            Trace.WriteLine("Remove item");
        }

        private async Task SavePrescription(object obj)
        {
            ExpiryDateErrorMessage = InstructionsErrorMessage = string.Empty;

            Trace.WriteLine("Date: " + ExpiryDate.ToString());
            // first validate the fields
            if (ExpiryDate == DateTime.MinValue) 
            {
                ExpiryDateErrorMessage = "| Cannot be empty";
            }
            if (ExpiryDate < DateTime.Now)
            {
                ExpiryDateErrorMessage = "| Select proper date";
            }
            if (string.IsNullOrEmpty(Instructions))
            {
                InstructionsErrorMessage = "| Cannot be empty";
            }

            if (InstructionsErrorMessage.Length != 0 || ExpiryDateErrorMessage.Length != 0)
                return;

            if (PrescriptionDrugs.Count() == 0)
            {
                MessageBox.Show("You have not added any drugs yet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach(var drug in PrescriptionDrugs)
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

            prescriptionEntity.Instructions = Instructions;
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
                        .ThenInclude(p => p.Client)
                .Include(p => p.PrescriptionDrugs)
                    .ThenInclude(pd => pd.Drug)
                .FirstOrDefaultAsync(p => p.Id == Prescription.Id);


            PrescriptionUpdated?.Invoke(updatedPrescription);
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
