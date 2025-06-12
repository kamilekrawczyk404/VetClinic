using System.Data.Entity.Infrastructure;
using VetClinic.Database;
using VetClinic.Models;
using BCrypt.Net;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Services
{
    public class UserService
    {
        private readonly VeterinaryClinicContext _context;

        public UserService(VeterinaryClinicContext context)
        {
            _context = context;
        }

        public async Task<User> CreateClientAsync(string name, string surname, string email, string phoneNumber, string password, string gender, DateTime dateOfBirth)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            User newUser = new()
            {
                Role = "Client",
                Email = email,
                PasswordHash = passwordHash,
                TelephoneNumber = int.Parse(phoneNumber),
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Name = name,
                Surname = surname,
            };

            try
            {
                await _context.User.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return newUser;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
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
                var doctor = await _context.Doctor
                .FirstOrDefaultAsync(u => u.Email == email);

                //if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                if (doctor == null)
                {
                    return null; // Invalid email or password
                }
                else
                {
                    doctor.LastLogin = DateTime.Now;
                    await _context.SaveChangesAsync();

                    return doctor;
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
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
                var user = await _context.User
                .FirstOrDefaultAsync(u => u.Email == email);

                //if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                if (user == null)
                {
                    return null; // Invalid email or password
                }
                else
                {
                    user.LastLogin = DateTime.Now;
                    await _context.SaveChangesAsync();

                    return user;
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
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
            return !await _context.User.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsDoctorEmailUniqueAsync(string email)
        {
            return !await _context.Doctor.AnyAsync(u => u.Email == email);
        }
    }
}
