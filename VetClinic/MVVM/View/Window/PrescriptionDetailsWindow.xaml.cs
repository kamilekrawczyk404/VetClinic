using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.ViewModel.Window;

namespace VetClinic.MVVM.View.Window
{
    /// <summary>
    /// Logika interakcji dla klasy PrescriptionDetailsWindow.xaml
    /// </summary>
    public partial class PrescriptionDetailsWindow : System.Windows.Window
    {
        public PrescriptionDetailsWindow(Prescription prescription, Action<Prescription> prescriptionUpdated, IDbContextFactory<VeterinaryClinicContext> context)
        {
            InitializeComponent();
        }

        private void OnPrescriptionUpdated(Prescription _)
        {
            this.Close();
        }
    }
}
