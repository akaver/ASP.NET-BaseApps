using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class PersonInPlanRepository : EFRepository<PersonInPlan>, IPersonInPlanRepository
    {
        public PersonInPlanRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
