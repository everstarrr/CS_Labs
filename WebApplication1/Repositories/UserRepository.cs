using Microsoft.EntityFrameworkCore;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class UserRepository:IUserRepository
{
    private readonly WAContext _context;

    public UserRepository(WAContext context)
    {
        _context = context;
    }

    public Task Add(User user)
    {
        _context.Users.Add(user);
        return _context.SaveChangesAsync();
    }

    public Task<List<string>> GetAllNames()
    {
        return _context.Users.Select(x => x.Name).ToListAsync();
    }
}