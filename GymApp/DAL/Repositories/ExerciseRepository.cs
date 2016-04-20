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
    public class ExerciseRepository : EFRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(IDbContext dbContext) : base(dbContext)
        {
        }


        public List<Exercise> GetAllWithFilter(string filter, string sortProperty, int pageNumber, int pageSize, out int totalExerciseCount,
            out string realSortProperty)
        {
            //???
            sortProperty = sortProperty?.ToLower() ?? "";
            realSortProperty = sortProperty;
            filter = filter?.ToLower() ?? "";

            //var res = DbSet.Where(n => n.ExerciseId > 0);
            var res = Enumerable.Empty<Exercise>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                res = res.Where(e => e.ExerciseName.ToLower().Contains(filter));
            }
            switch (sortProperty)
            {
                case "_exercisename":
                    res = res.OrderByDescending(p => p.ExerciseName);
                    break;
                default:
                case "exercisename":
                    res = res.OrderBy(p => p.ExerciseName);
                    break;
            }
            totalExerciseCount = res.Count();

            var reslist = res.Skip(pageNumber*pageSize).Take(pageSize).ToList();

            return reslist;
        }
    }
}
