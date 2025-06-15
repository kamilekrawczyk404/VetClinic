using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public AsyncRelayCommand RemoveDrugCommand { get; }
        public AsyncRelayCommand SaveDrugListCommand { get; }

        public DrugListViewModel(IUserSessionService userSessionService, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _userSessionService = userSessionService;
            _contextFactory = contextFactory;

            AddNewDrugCommand = new AsyncRelayCommand(AddNewDrug);
            RemoveDrugCommand = new AsyncRelayCommand(RemoveDrug);
            SaveDrugListCommand = new AsyncRelayCommand(SaveDrugList);

            Drugs = new();

            _ = LoadDrugs();
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
                        Quantity = drug.Quantity,
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
            throw new NotImplementedException();
        }

        private async Task SaveDrugList(object arg)
        {
            throw new NotImplementedException();
        }
    }
}
