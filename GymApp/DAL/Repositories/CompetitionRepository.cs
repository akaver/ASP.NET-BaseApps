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
    public class CompetitionRepository : EFRepository<Competition>, ICompetitionRepository
    {
        public CompetitionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
