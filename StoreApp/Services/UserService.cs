using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.DTOs.User;
using StoreApp.Exceptions;

namespace StoreApp.Services;

public class UserService
{
    private readonly StoreAppDbContext _context;

    public UserService(StoreAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserResponseDto>> GetUsersAsync()
    {
        var users = await _context.Users
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            })
            .ToListAsync();

        if (!users.Any())
        {
            throw new NotFoundException("No users found.");
        }

        return users;
    }

    public async Task<UserResponseDto> GetUserByIdAsync(string id)
    {
        var user = await _context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            })
            .FirstOrDefaultAsync();
        if (user is null)
        {
            throw new NotFoundException("User not found.");
        }

        return user;
    }
}