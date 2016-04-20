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
            return DbSet.Where(p => p.UserId == userId).OrderBy(o => o.LastName).ThenBy(o => o.FirstName).Include(c => c.Contacts).ToList();
        }

        public Person GetForUser(int personId, int userId)
        {
            return DbSet.FirstOrDefault(a => a.PersonId == personId && a.UserId == userId);
        }

        public List<Person> GetAllForUser(int userId, string filter, string sortProperty, int pageNumber, int pageSize, out int totalUserCount, out string realSortProperty)
        {
            sortProperty = sortProperty?.ToLower() ?? "";
            realSortProperty = sortProperty;
            filter = filter?.ToLower() ?? "";


            //start building up the query
            var res = DbSet
                .Where(p => p.UserId == userId);

            // filter records
            if (!string.IsNullOrWhiteSpace(filter))
            {
                res = res.Where(p => p.FirstName.ToLower().Contains(filter) || p.LastName.ToLower().Contains(filter));
            }

            // set up sorting
            switch (sortProperty)
            {
                case "_firstname":
                    res = res
                        .OrderByDescending(o => o.FirstName).ThenBy(o => o.LastName);
                    break;
                case "firstname":
                    res = res
                        .OrderBy(o => o.FirstName).ThenBy(o => o.LastName);
                    break;

                case "_lastname":
                    res = res
                        .OrderByDescending(o => o.LastName).ThenBy(o => o.FirstName);
                    break;

                default:
                case "lastname":
                    res = res
                        .OrderBy(o => o.LastName).ThenBy(o => o.FirstName);
                    realSortProperty = "lastname";
                    break;
            }

            // join entities to avoid 1+n
            res = res
                .Include(c => c.Contacts);

            // get the count before any skip and take - this will generate sql like: SELECT COUNT(*) FROM Person WHERE UserId=xxx
            totalUserCount = res.Count();

            // skip x first records, then take y records - this will generate full sql, with joins
            var reslist = res
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return reslist;
        }

        
    }
}
