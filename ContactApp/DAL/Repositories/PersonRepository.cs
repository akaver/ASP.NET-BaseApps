using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class PersonRepository : EFRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<Person> GetAllForUser(int userId)
        {
            return DbSet.Where(p => p.UserId == userId).OrderBy(o => o.Lastname).ThenBy(o => o.Firstname).Include(c =>c.Contacts).ToList();
        }

        public Person GetForUser(int personId, int userId)
        {
            return DbSet.FirstOrDefault(a => a.PersonId == personId && a.UserId == userId);
        }

    }
}
