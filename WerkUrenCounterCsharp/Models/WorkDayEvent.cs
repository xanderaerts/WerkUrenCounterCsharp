using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkUrenCounterCsharp.Models
{

    public enum WorkDayEventAction { 
        StartDay = 1,
        DriveToRest,
        RestToDrive,
        EndDay
    }
     public class WorkDayEvent
    {

        public WorkDayEvent(DateTime d1,WorkDayEventAction action) {
            this.StartEvent = d1;
            this.Action = action;
        }
        //public int id { get; set; }     
        public String Name { get; set; }

        public DateTime StartEvent { get; set; }

        public WorkDayEventAction Action { get; set; }


    }
}
