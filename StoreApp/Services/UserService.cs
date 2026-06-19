using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.DTOs.User;

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
        return await _context.Users
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            })
            .ToListAsync();
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(string id)
    {
        return await _context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            })
            .FirstOrDefaultAsync();
    }
}