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

        [NotMapped] // Not mapped to the database
        public virtual string? TeamNames { get; set; }

        [NotMapped]
        public virtual List<int>? RefTeams { get; set; }

        // Navigation Properties
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
            return await _context.Users
             .Select(u => new User() 
             {
                 RefUser = u.RefUser,
                 Name = u.Name,
                 Surname = u.Surname,
                 Email = u.Email,
                 IsTeamCaptain = u.IsTeamCaptain,
                 TeamNames = string.Join(", ", u.UserTeams!.Select(ut => ut.Team!.TeamName ?? "No Teams Assigned")),
                 RefTeams = u.UserTeams!.Select(ut => ut.RefTeam).ToList()
             })
             .ToListAsync(); 
        }

        public async Task<User> GetUser(Guid RefUser) 
        {
            var user = await _context.Users
             .Select(u => new User()
             {
                 RefUser = u.RefUser,
                 Name = u.Name,
                 Surname = u.Surname,
                 Email = u.Email,
                 IsTeamCaptain = u.IsTeamCaptain,
                 TeamNames = string.Join(", ", u.UserTeams!.Select(ut => ut.Team!.TeamName ?? "No Teams Assigned")),
                 RefTeams = u.UserTeams!.Select(ut => ut.RefTeam).ToList()
             }).FirstOrDefaultAsync(u => u.RefUser == RefUser);

            if (user is null) return null;
          
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
