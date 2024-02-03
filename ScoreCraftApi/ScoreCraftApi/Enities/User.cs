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

        public int? RefTeam { get; set; }
        public bool isTeamCaptain { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        [ForeignKey("RefTeam")]
        public virtual Team Team { get; set; }

        // Navigation Properties
        public ICollection<Match> Matches { get; set; }
    }

    public class UsersBLL {
        private readonly DataContext _context;

        public UsersBLL(DataContext context) // Create Constuctor for Database context
        {
            _context = context;
        }

       public async Task<List<User>> GetUserCollection() // Return list of Users
       {
            return await _context.Users.ToListAsync();
       }

        public async Task<User> GetUser(Guid RefUser) // Return User by RefUser
        {
            var user = await _context.Users.FindAsync(RefUser);

            if(user is null) return null;
          

            return user;
        }

        public async Task<User> Insert(User model) // Return User by RefUser
        {
            _context.Users.Add(model);
            await _context.SaveChangesAsync();
            
            return await GetUser(model.RefUser);
        }

        public async Task<User> Update(User model) // Return User by RefUser
        {
            var dbUser = await _context.Users.FindAsync(model.RefUser);

            if(dbUser is null) return null;




            dbUser.RefTeam = model.RefTeam;
            dbUser.Name = model.Name;
            dbUser.Surname = model.Surname;
            dbUser.Email = model.Email;
            await _context.SaveChangesAsync();

            return await GetUser(model.RefUser);
        }

        public async Task<bool?> Delete(Guid RefUser) // Return User by RefUser
        {
            var dbUser = await _context.Users.FindAsync(RefUser);

            if (dbUser is null) return null;


            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return true;
        }

    }

}
