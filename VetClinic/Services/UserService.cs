using VetClinic.Database;
using VetClinic.Models;
using BCrypt.Net;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Services
{
    public class UserService
    {
        private IDbContextFactory<VeterinaryClinicContext> _contextFactory;

        public UserService(IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<User> CreateClientAsync(string name, string surname, string email, string phoneNumber, string password, string gender, DateTime dateOfBirth)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            User newUser = new()
            {
                Role = "Client",
                Email = email,
                PasswordHash = passwordHash,
                TelephoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth,
                IsActive = true,
                Name = name,
                Surname = surname,
            };

            try
            {
                using var context = _contextFactory.CreateDbContext();
                await context.User.AddAsync(newUser);
                await context.SaveChangesAsync();

                return newUser;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error creating user: {ex.Message}");
                
                if (ex.InnerException?.Message.Contains("UNIQUE constraint failed", StringComparison.OrdinalIgnoreCase) == true)
                {
                    if (ex.Message.Contains("Email"))
                    {
                        throw new InvalidOperationException("This email is already taken.", ex);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<Doctor> LoginDoctorAsync(string email, string password)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var doctor = await context.Doctor
                .FirstOrDefaultAsync(u => u.Email == email);

                if (doctor == null || !BCrypt.Net.BCrypt.Verify(password, doctor.PasswordHash))
                {
                    return null; // Invalid email or password
                }
                else
                {
                    doctor.LastLogin = DateTime.Now;
                    await context.SaveChangesAsync();

                    return doctor;
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error during login: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during login: {ex.Message}");
                throw;
            }
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var user = await context.User
                .FirstOrDefaultAsync(u => u.Email == email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return null; // Invalid email or password
                }
                else
                {
                    user.LastLogin = DateTime.Now;
                    await context.SaveChangesAsync();

                    return user;
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error during login: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during login: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> IsUserEmailUniqueAsync(string email)
        {
            using var _context = _contextFactory.CreateDbContext();
            return !await _context.User.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsDoctorEmailUniqueAsync(string email)
        {
            using var _context = _contextFactory.CreateDbContext();
            return !await _context.Doctor.AnyAsync(u => u.Email == email);
        }
    }
}
