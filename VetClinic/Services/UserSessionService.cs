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
        User LoggedInUser { get; set; }
        Doctor LoggedInDoctor { get; set; }

        event Action UserChanged;
        void SetUser(User user);
        void SetDoctor(Doctor doctor);
        void ClearUser();

        bool IsClient { get; set; }
        bool IsDoctor { get; set; }
        bool IsAdmin { get; set; }
    }
    public class UserSessionService : ObservableObject, IUserSessionService
    {
        public User LoggedInUser { get; set;  }
        public Doctor LoggedInDoctor { get; set; }
        public bool IsClient { get; set; }
        public bool IsDoctor { get; set; }
        public bool IsAdmin { get; set;  }

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
