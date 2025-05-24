using System;
using System.Collections.Generic;
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

        event Action UserChanged;
        void SetUser(User user);
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
        public void ClearUser()
        {
            LoggedInUser = null;
            CheckUserRole();
            UserChanged?.Invoke();
        }

        void CheckUserRole()
        {
            if (LoggedInUser == null)
            {
                IsClient = false;
                IsDoctor = false;
                IsAdmin = false;
                return;
            }
            IsClient = LoggedInUser.RoleId == 2;
            IsDoctor = LoggedInUser.RoleId == 3;
            IsAdmin = LoggedInUser.RoleId == 1;
        }
    }
}
