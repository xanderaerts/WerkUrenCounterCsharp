using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkUrenCounterCsharp.Models
{

    public enum WorkDayEventAction { 
        StartDay = 1,
        WorkToRest,
        RestToWork,
        EndDay
    }
     public class WorkDayEvent
    {

        public WorkDayEvent(string name, DateTime d1,DateTime d2,WorkDayEventAction action) {
            this.Name = name;
            this.StartEvent = d1;
            this.EndEvent = d2;
            this.Action = action;
        }
        //public int id { get; set; }     
        public String Name { get; set; }

        public DateTime StartEvent { get; set; }
        public DateTime EndEvent { get; set; }

        public WorkDayEventAction Action { get; set; }


    }
}
