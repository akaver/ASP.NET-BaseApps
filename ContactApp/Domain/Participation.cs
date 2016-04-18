using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Participation
    {
        public int ParticipationId { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }

        public int? Score { get; set; }
    }
}
