using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Interfaces
{
    public interface IExerciseRepository : IEFRepository<Exercise>
    {
        List<Exercise> GetAllWithFilter(string filter, string sortProperty, int pageNumber, int pageSize,
            out int totalExerciseCount, out string realSortProperty);
    }
}
