using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAPI.DAL;
using WebAPI.Data;
using WebAPI.Models;

namespace Book_Lending_System.Data
{
    public static class ContextSeed
    {
        //Seed Roles
        public static readonly string Admin = "ADMIN";
        public static readonly string Manager = "MANAGER";
        public static readonly string Staff = "STAFF";

        public static async Task SeedRolesAsync(ApplicationContext context)
        {
            List<ApplicationRole> roles = await context.Roles.AsNoTracking().ToListAsync();

            await using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (roles.Find(r => r.Name == Admin) is null)
                        await context.Roles.AddAsync(new ApplicationRole { Name = Admin });

                    if (roles.Find(r => r.Name == Staff) is null)
                        await context.Roles.AddAsync(new ApplicationRole { Name = Staff });

                    if (roles.Find(r => r.Name == Manager) is null)
                        await context.Roles.AddAsync(new ApplicationRole { Name = Manager });

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public static async Task SeedUsersAsync(ApplicationContext context)
        {
            await using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Admin
                    if (await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.ToUpper() == Admin) is null)
                    {
                        var adminUser = new ApplicationUser
                        {
                            Username = "admin",
                            Login = Admin,
                            EmailAddress = "bluewash.admin@example.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("b1u3w45h_4dm1n"),
                        };

                        await context.Users.AddAsync(adminUser);
                        await context.SaveChangesAsync();

                        List<ApplicationRole> roles = await context.Roles.AsNoTracking().ToListAsync();
                        List<ApplicationUserRole> userRoles = new(roles.Count);
                        foreach (var role in roles)
                        {
                            userRoles.Add(new ApplicationUserRole
                            {
                                ApplicationUserId = adminUser.Id,
                                ApplicationRoleId = role.Id
                            });
                        }

                        await context.UserRoles.AddRangeAsync(userRoles);
                        await context.SaveChangesAsync();
                    }

                    // Manager
                    if (await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.ToUpper() == Manager) is null)
                    {
                        var managerUser = new ApplicationUser
                        {
                            Username = "manager",
                            Login = Manager,
                            EmailAddress = "bluewash.manager@example.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("b1u3w45h_m4n493r"),
                        };

                        await context.Users.AddAsync(managerUser);
                        await context.SaveChangesAsync();

                        ApplicationRole? managerRole = await context.Roles.AsNoTracking().FirstOrDefaultAsync(u => u.Name.ToUpper() == Manager);

                        if (managerRole is not null)
                        {
                            await context.UserRoles.AddAsync(new ApplicationUserRole
                            {
                                ApplicationUserId = managerUser.Id,
                                ApplicationRoleId = managerRole.Id
                            });
                            await context.SaveChangesAsync();
                        }
                    }

                    // Staff
                    if (await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.ToUpper() == Staff) is null)
                    {
                        var staffUser = new ApplicationUser
                        {
                            Username = "staff",
                            Login = Staff,
                            EmailAddress = "bluewash.user@example.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("b1u3w45h_5t4ff"),
                        };

                        await context.Users.AddAsync(staffUser);
                        await context.SaveChangesAsync();

                        ApplicationRole? staffRole = await context.Roles.AsNoTracking().FirstOrDefaultAsync(u => u.Name.ToUpper() == Staff);

                        if (staffRole is not null)
                        {
                            await context.UserRoles.AddAsync(new ApplicationUserRole
                            {
                                ApplicationUserId = staffUser.Id,
                                ApplicationRoleId = staffRole.Id
                            });
                            await context.SaveChangesAsync();
                        }
                    }



                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}