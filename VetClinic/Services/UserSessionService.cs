using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.Utils;

namespace VetClinic.Services
{
    public interface IUserSessionService
    {
        User LoggedInUser { get; }
        Doctor LoggedInDoctor { get; }

        event Action UserChanged;
        void SetUser(User user);
        void SetDoctor(Doctor doctor);
        void ClearUser();

        bool IsClient { get; }
        bool IsDoctor { get; }
        bool IsAdmin { get; }
    }
    public class UserSessionService : ObservableObject, IUserSessionService
    {
        private User _loggedInUser;
        public User LoggedInUser
        {
            get => _loggedInUser;
            private set
            {
                _loggedInUser = value;
                OnPropertyChanged();
            }
        }

        private Doctor _loggedInDoctor;
        public Doctor LoggedInDoctor
        {
            get => _loggedInDoctor;
            private set
            {
                _loggedInDoctor = value;
                OnPropertyChanged();
            }
        }

        private bool _isClient;
        public bool IsClient
        {
            get => _isClient;
            private set
            {
                _isClient = value;
                OnPropertyChanged();
            }
        }

        private bool _isDoctor;
        public bool IsDoctor
        {
            get => _isDoctor;
            private set
            {
                _isDoctor = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            private set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        public event Action UserChanged;
        public void SetUser(User user)
        {
            LoggedInUser = user;
            CheckUserRole();
            UserChanged?.Invoke();
        }

        public void SetDoctor(Doctor doctor)
        {
            LoggedInDoctor = doctor;
            CheckUserRole();
            UserChanged?.Invoke();
            Trace.WriteLine("Doctor " + doctor.Name);
        }
        public void ClearUser()
        {
            if (LoggedInDoctor != null)
            {
                LoggedInDoctor = null;
            } else if (LoggedInUser != null)
            {
                LoggedInUser = null;
            }

            CheckUserRole();
            UserChanged?.Invoke();
        }

        void CheckUserRole()
        {
            if (LoggedInUser == null && LoggedInDoctor == null)
            {
                return;
            }

            Trace.WriteLine("roles");
            if (LoggedInUser != null)
            {
                IsClient = LoggedInUser.Role.ToLower() == "client";
                IsAdmin = LoggedInUser.Role.ToLower() == "admin";
            } else
            {
                IsDoctor = LoggedInDoctor != null;
            }
        }
    }
}
