using API.Entities;
using API.Interfaces;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly Datacontext _context;
    public UserRepository(Datacontext context)
    {
            _context = context;
        
    }
    public async Task<AppUser> GetUserByIdAsync(int Id)
    {
        return await _context.Users.FindAsync(Id);
    }

    public async Task<AppUser> GetUserbyUsernameAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
        _context.Entry(user).State = EntryState.Modified;
    }
}