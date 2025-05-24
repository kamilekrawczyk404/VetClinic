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

        public async Task<User> CreateClientAsync(string name, string surname, string email, string phoneNumber, string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            User newUser = new()
            {
                RoleId = 2, // Client Role
                Email = email,
                PasswordHash = passwordHash,
                Telephone = phoneNumber,
            };

            Client client = new()
            {
                User = newUser,
                Name = name,
                Surname = surname,
            };

            try
            {
                await _context.User.AddAsync(newUser);
                await _context.Client.AddAsync(client);
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

        public async Task<User> LoginUserAsync(string email, string password)
        {
            try
            {
                var user = await _context.User
                .FirstOrDefaultAsync(u => u.Email == email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
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

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _context.User.AnyAsync(u => u.Email == email);
        }
    }
}
