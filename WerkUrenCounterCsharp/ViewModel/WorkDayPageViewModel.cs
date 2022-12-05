using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkUrenCounterCsharp.ViewModel
{
    public class WorkDayPageViewModel
    {
        public int dayStarted {get;set;}

        public WorkDayPageViewModel() {
            this.dayStarted = 0;
        }
    }
}
