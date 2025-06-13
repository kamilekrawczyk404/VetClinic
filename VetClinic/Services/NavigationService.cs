using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Interfaces;
using VetClinic.MVVM.ViewModel;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<TViewModel>(params object[] parameters) where TViewModel : ViewModel;
    }

    public class NavigationService : ObservableObject, INavigationService
    {
        private Func<Type, ViewModel> _viewModelFactory;

        private ViewModel _currentView;
        public ViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public void NavigateTo<TViewModel>(params object[] parameters) where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            if (viewModel is ViewOpinionsViewModel opinionsViewModel && parameters.Length > 0 && parameters[0] is Models.Doctor doctor)
            {
                opinionsViewModel.SelectedDoctor = doctor;
            }
            CurrentView = viewModel;
        }

        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }
    }
}
