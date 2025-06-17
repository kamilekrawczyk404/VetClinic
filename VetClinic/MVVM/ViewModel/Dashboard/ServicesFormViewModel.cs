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
                if (value != null)
                {
                    _services = value;
                    OnPropertyChanged();
                    OnServicesLoaded();
                }
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

        public RelayCommand AddServiceCommand { get; }
        public RelayCommand RemoveServiceCommand { get; }

        public ServicesFormViewModel(List<Service> services, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _contextFactory = contextFactory;

            AddServiceCommand = new RelayCommand(AddService);
            RemoveServiceCommand = new RelayCommand(RemoveService);

            AllServices = new();
            Services = new(services);

            _ = GetAllServices();
        }

        public async Task<bool> CheckServices(Appointment appointment)
        {
            if (IsAppointmentCompleted)
                return true;

            using var context = _contextFactory.CreateDbContext();

            var existingServices = context.AppointmentServices
                .Where(x => x.AppointmentId == appointment.Id);
            context.AppointmentServices.RemoveRange(existingServices);

            if (Services != null)
            {
                foreach (var service in Services)
                {
                    var appointmentService = new AppointmentServices
                    {
                        AppointmentId = appointment.Id,
                        ServiceId = service.Id
                    };
                    context.AppointmentServices.Add(appointmentService);
                }
            } 
            else
            {
                return false;
            }

            await context.SaveChangesAsync();

            return true;

        }
        private void RemoveService(object obj)
        {
            if (IsAppointmentCompleted)
                return;

            if (obj is Service service)
            {
                // Prevent from removing the first service
                if (service.Id == 1)
                    return;

                Services.Remove(service);
            }
        }

        private void AddService(object obj)
        {
            if (IsAppointmentCompleted)
                return;

            if (obj is Service service)
            {
                if (Services.Any(s => s.Name == service.Name))
                    return;

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
            if (IsAppointmentCompleted)
                return;

            using var context = _contextFactory.CreateDbContext();

            var services = await context.Service.ToArrayAsync();

            AllServices = new(services);

            OnServicesLoaded();    
        }

        private void OnServicesLoaded()
        {
            if (Services?.Count() == 0 && AllServices?.Count() != 0)
            {
                // Add initial service
                Services.Add(AllServices[0]);
            }
        }
    }
}
