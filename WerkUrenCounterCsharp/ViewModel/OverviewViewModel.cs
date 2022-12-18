using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerkUrenCounterCsharp.Models;
using WerkUrenCounterCsharp.Pages;
using WerkUrenCounterCsharp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WerkUrenCounterCsharp.ViewModel
{
    public class OverviewViewModel
    {
        private IDataStore _IDataStore => DependencyService.Get<IDataStore>();


   
        public ObservableCollection<DayTotals> daytotals { get; set; } = new ObservableCollection<DayTotals>();



        public OverviewViewModel() {
            this.CalcHours();
        }



        public async void CalcHours() {

            List<WorkDayEvent> allwdes = await this._IDataStore.GetAllWorkDayEventsAsync();

            WorkDayEvent prev = null;
            Boolean first = true;

            DayTotals dt = null;
            TimeSpan twd = new TimeSpan();
            TimeSpan trh = new TimeSpan();

            foreach (WorkDayEvent wde in allwdes)
            {
                if (wde.Action == WorkDayEventAction.StartDay)
                {
                    dt = new DayTotals();
                    dt.date = wde.StartEvent.ToString("dd/MMMM/yyyy HH:mm");
                }

                if (first)
                {
                    prev = wde;
                }
                if (wde.Action != WorkDayEventAction.StartDay)
                {
                    if (wde.Action == WorkDayEventAction.DriveToRest)
                    {
                        TimeSpan diff = wde.StartEvent.Subtract(prev.StartEvent);
                        twd += diff;
                    }
                    else if (wde.Action == WorkDayEventAction.RestToDrive)
                    {
                        TimeSpan difRest = wde.StartEvent.Subtract(prev.StartEvent);
                        trh += difRest;
                    }
                }

                if (wde.Action == WorkDayEventAction.EndDay)
                {
                    dt.totalDriveHours = "Totaal uren gereden: " +  twd.ToString("h\\:mm\\:ss");
                    dt.totalRestHours = "Totaal uren gerust: " + trh.ToString("h\\:mm\\:ss");
                    this.daytotals.Add(dt);

                    twd = new TimeSpan();
                    trh = new TimeSpan();
                }

                prev = wde;
                first = false;
            }

        }

    }
}
