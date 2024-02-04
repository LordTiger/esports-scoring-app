using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ScoreCraftApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScoreCraftApi.Enities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid RefUser { get; set; }
        public bool IsTeamCaptain { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }

        // Navigation Properties
        public ICollection<Match>? Matches { get; set; }
        public ICollection<UserTeam>? UserTeams { get; set; }
    }

    public class UsersBLL {
        private readonly DataContext _context;

        public UsersBLL(DataContext context) // Create Constuctor for Database context
        {
            _context = context;
        }

       public async Task<List<User>> GetUserCollection() 
       {
            return await _context.Users.AsNoTracking().ToListAsync();
       }

        public async Task<User> GetUser(Guid RefUser) 
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(w => w.RefUser == RefUser);

            if(user is null) return null;
          

            return user;
        }

        public async Task<User> Insert(User model) 
        {
            _context.Users.Add(model);
            await _context.SaveChangesAsync();
            
            return await GetUser(model.RefUser);
        }

        public async Task<User> Update(User model) 
        {

            await _context.Users.Where(u => u.RefUser == model.RefUser).ExecuteUpdateAsync(n =>
                n.SetProperty(u => u.Name, model.Name)
                .SetProperty(u => u.Surname, model.Surname)
                .SetProperty(u => u.Email, model.Email)
                .SetProperty(u => u.IsTeamCaptain, model.IsTeamCaptain));

            return await GetUser(model.RefUser);
        }

        public async Task<bool?> Delete(Guid RefUser) 
        {
            await _context.Users.Where(u => u.RefUser == RefUser).ExecuteDeleteAsync();

            return true;
        }

    }

}
