using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using VetClinic.Database;
using VetClinic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Windows.Media;
using System.Windows;
using VetClinic.Models;

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    // This view model handling information about most popular services for each doctor.
    // Doctor can change time range from single week, month or a year.
    // All data is displayed in the charts from LiveCharts library.
    class ServiceCount
    {
        public string ServiceName { get; set; }
        public int Count { get; set; }
    }
    public class MostPopularServicesViewModel : ViewModel
    {
        private ObservableCollection<ISeries> _mostPopularServices;
        public ObservableCollection<ISeries> MostPopularServices
        {
            get => _mostPopularServices;
                set
            {
                _mostPopularServices = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ICartesianAxis> _yAxis;
        public ObservableCollection<ICartesianAxis> YAxis
        {
            get => _yAxis;
            set 
            {
                _yAxis = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ICartesianAxis> _xAxis;
        public ObservableCollection<ICartesianAxis> XAxis
        {
            get => _xAxis;
            set
            {
                _xAxis = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _timeIntervals;
        public ObservableCollection<string> TimeIntervals
        {
            get => _timeIntervals;
            set
            {
                _timeIntervals = value;
                OnPropertyChanged();
            }
        }

        private string _selectedTimeInterval;
        public string SelectedTimeInterval
        {
            get => _selectedTimeInterval;
            set 
            {
                _selectedTimeInterval = value;
                OnPropertyChanged();
                _ = GetMostPopularServices();
            }
        }

        private ObservableCollection<int> _servicesCount;
        public ObservableCollection<int> ServicesCount
        {
            get => _servicesCount;
            set
            {
                _servicesCount = value;
                OnPropertyChanged();
            }
        }

        private int _selectedServicesCount;
        public int SelectedServicesCount
        {
            get => _selectedServicesCount;
            set
            {
                if (value != null)
                {
                    _selectedServicesCount = value;
                    OnPropertyChanged();

                    if (SelectedServicesCount <= _allServicesCount.Count())
                    {
                        // only cut current collection of those services
                        _allServicesCount = _allServicesCount.Take(SelectedServicesCount).ToList();
                        FitGraphData();
                    }
                    else
                    {
                        // we need to fetch new ones
                        _ = GetMostPopularServices();
                    }
                }   
            }
        }

        private IUserSessionService _userSessionService;

        private IDbContextFactory<VeterinaryClinicContext> _contextFactory;

        private List<ServiceCount> _allServicesCount { get; set; } = new();

        public MostPopularServicesViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService sessionService)
        {
            _contextFactory = contextFactory;
            _userSessionService = sessionService;

            TimeIntervals = new ObservableCollection<string>()
            {
                "Week",
                "Month",
                "Year"
            };

            SelectedTimeInterval = TimeIntervals[0];

            ServicesCount = new ObservableCollection<int>()
            {
                2,
                4,
                6
            };

            SelectedServicesCount = ServicesCount.Last();

            MostPopularServices = new();
            YAxis = new();
            XAxis = new();

            GetMostPopularServices();
        }

        private async Task GetMostPopularServices()
        {
            using var context = _contextFactory.CreateDbContext();
            // Based on the selected time interval, we generate the interval

            DateTime startingDate;
            var today = DateTime.Today;
            int firstDayOfWeek = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;

            switch (SelectedTimeInterval.ToLower())
            {
                case "week":
                    // Get the first day of the current week (assuming week starts on Monday)
                    startingDate = today.AddDays(-1 * firstDayOfWeek);
                    break;
                case "month":
                    startingDate = new DateTime(today.Year, today.Month, 1);
                    break;
                case "year":
                    startingDate = new DateTime(today.Year, 1, 1);
                    break;
                default:
                    startingDate = today;
                    break;
            }

            var doctor = _userSessionService?.LoggedInDoctor;

            // Zapytanie dla administratora (wszystkie usługi) lub lekarza (tylko jego usługi)
            IQueryable<Appointment> appointmentsQuery = context.Appointment
                .Where(a => a.AppointmentDate >= startingDate)
                .Include(a => a.AppointmentServices)
                .ThenInclude(asv => asv.Service);

            // Jeśli to lekarz, filtruj po jego ID
            if (doctor != null)
            {
                appointmentsQuery = appointmentsQuery.Where(a => a.DoctorId == doctor.Id);
            }

            var appointments = await appointmentsQuery.ToListAsync();

            _allServicesCount = appointments
                .SelectMany(a => a.AppointmentServices)
                .Where(asv => asv.Service != null) // Dodaj sprawdzenie null
                .GroupBy(asv => asv.Service.Name)
                .Select(g => new ServiceCount { ServiceName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(SelectedServicesCount)
                .ToList();

            FitGraphData();
        }

        private void FitGraphData()
        {
            if (_allServicesCount.Count() == 0)
            {
                return;
            }

            var serviceNames = _allServicesCount.Select(x => x.ServiceName).ToArray();
            var counts = _allServicesCount.Select(x => x.Count).ToArray();
            int maxCount = counts.Max();

            int[] grayColumnsSeries = new int[serviceNames.Count()];
            Array.Fill(grayColumnsSeries, maxCount);

            MostPopularServices = new ObservableCollection<ISeries>
            {
                new ColumnSeries<int>
                { 
                    Values = grayColumnsSeries,
                    Fill = new SolidColorPaint(GetSkColorFromSolidColorBrush("LightGray")),
                    Stroke = null,
                    IgnoresBarPosition = true
                },
                new ColumnSeries<int>
                {
                    Values = counts,
                    Fill = new SolidColorPaint(GetSkColorFromSolidColorBrush("Turquise")),
                    Stroke = null,
                    IgnoresBarPosition = true
                }
            };

            XAxis = new ObservableCollection<ICartesianAxis>
            {
                new Axis
                {
                    Labels = serviceNames,
                    TextSize = 10,
                    ForceStepToMin = true
                }
            };

            YAxis = new ObservableCollection<ICartesianAxis>
            {
                new Axis { MinLimit = 0 }
            };
        }

        private static SKColor GetSkColorFromSolidColorBrush(string solidColorBrushName)
        {
            try
            {
                var brush = (SolidColorBrush)Application.Current.Resources[solidColorBrushName];
                var color = brush.Color;
                var skColor = new SKColor(color.R, color.G, color.B, color.A);

                return skColor;
            }
            catch (Exception e)
            {
                Trace.Fail("Caanot parse color brush to skColor: " + e.Message);
            }

            return new SKColor(0, 0, 0, 1);
        }
    }
}
