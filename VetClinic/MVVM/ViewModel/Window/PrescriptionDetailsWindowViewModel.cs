using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Database;
using VetClinic.Models;

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

        public PrescriptionDetailsWindowViewModel(Prescription prescription, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _prescription = prescription;
            _contextFactory = contextFactory;

            IsEditing = prescription?.Id != null;
        }
    }
}
