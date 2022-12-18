using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerkUrenCounterCsharp.Models;
using WerkUrenCounterCsharp.Pages;
using WerkUrenCounterCsharp.Services;
using System.Collections.ObjectModel;

namespace WerkUrenCounterCsharp.ViewModel
{
    public class OverviewViewModel
    {
        private IDataStore _IDataStore => DependencyService.Get<IDataStore>();


        public ObservableCollection<WorkDayEvent> workdayevents { get; set; } = new ObservableCollection<WorkDayEvent>();

        public OverviewViewModel() {
            this.getAllDataAsync();
        }

        private async void getAllDataAsync() {

            updatelist(await this._IDataStore.GetAllWorkDayEventsAsync());

        }


        private void updatelist(IEnumerable<WorkDayEvent>wde) {
            this.workdayevents.Clear();

            foreach (WorkDayEvent w in wde) {
                this.workdayevents.Add(w);
            }
        }

    }
}
