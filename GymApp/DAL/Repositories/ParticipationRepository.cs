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
    public class ParticipationRepository : EFRepository<Participation>, IParticipationRepository
    {
        public ParticipationRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
