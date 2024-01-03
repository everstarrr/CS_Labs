using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    
    public Task<List<string>> GetAllNames();
}