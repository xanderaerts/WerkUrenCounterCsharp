using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkUrenCounterCsharp.Models
{

    public enum WorkDayAction { 
        StartDay = 1,
        WorkToRest,
        RestToWork,
        EndDay
    }
     public class WorkDay
    {
        public String Name { get; set; }

        public DateTime StartEvent { get; set; }
        public DateTime EndEvent { get; set; }

        public WorkDayAction Action { get; set; }


    }
}
