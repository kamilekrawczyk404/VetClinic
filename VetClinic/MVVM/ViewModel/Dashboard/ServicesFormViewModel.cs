using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    public class ServicesFormViewModel : ViewModel
    {
        private IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        
        private ObservableCollection<Service> _services;
        public ObservableCollection<Service> Services
        {
            get => _services;
            set
            {
                _services = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Service> _allServices;
        public ObservableCollection<Service> AllServices
        {
            get => _allServices;
            set
            {
                _allServices = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddServiceCommand { get; }
        public RelayCommand RemoveServiceCommand { get; }

        public ServicesFormViewModel(ObservableCollection<Service> services, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _contextFactory = contextFactory;

            AddServiceCommand = new RelayCommand(AddService);
            RemoveServiceCommand = new RelayCommand(RemoveService);

            Services = services;

            _ = GetAllServices();
        }

        private void RemoveService(object obj)
        {
            if (obj is Service service)
            {
                Services.Remove(service);
            }
        }

        private void AddService(object obj)
        {
            if (obj is Service service)
            {
                Services.Add(new Service()
                {
                    Id = service.Id,
                    Name = service.Name,
                    Description = service.Description,
                    Price = service.Price
                });
            }
        }

        private async Task GetAllServices()
        {
            using var context = _contextFactory.CreateDbContext();

            var services = await context.Service.ToArrayAsync();

            AllServices = new(services);
        }
    }
}
