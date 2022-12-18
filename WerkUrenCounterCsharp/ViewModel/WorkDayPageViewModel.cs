

using System.ComponentModel;
using WerkUrenCounterCsharp.Models;
using WerkUrenCounterCsharp.Services;

namespace WerkUrenCounterCsharp.ViewModel
{
    public class WorkDayPageViewModel : INotifyPropertyChanged
    {
        private IDataStore _IDataStore => DependencyService.Get<IDataStore>();

        public Boolean DriveOrRestState; // drive = true & rest = false


        private const int maxDriveHours = 9;
        private const int maxDayLenght = 15;
        private TimeSpan TotalDriveHours;
        private TimeSpan TotRestHours;
        public Command changeStateButton { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        public String _changeStateButtonText;
        public String changeStateButtonText
        {
            get { return this._changeStateButtonText; }
            set
            {
                this._changeStateButtonText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(changeStateButtonText)));
            }
        }

        public String _currentHours;
        public String currentHours
        {
            get { return this._currentHours; }
            set
            {
                this._currentHours = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(currentHours)));
            }
        }

        public String _totalDriveHoursLabel;
        public String totalDriveHoursLabel
        {
            get { return this._totalDriveHoursLabel; }
            set
            {
                this._totalDriveHoursLabel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(totalDriveHoursLabel)));
            }
        }
        public String _totalRestHoursLabel;
        public String totalRestHoursLabel
        {
            get { return this._totalRestHoursLabel; }
            set
            {
                this._totalRestHoursLabel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(totalRestHoursLabel)));
            }
        }
        public WorkDayPageViewModel() {
            this.changeStateButton = new Command(changeStateButtonPushed);
            this.setStartState();
            this.CalcHours();
        }

        public async void changeStateButtonPushed() {
            if (this.DriveOrRestState)
            {
                this.changeStateButtonText = "Start Rijden";

                //Calculate hours before adding the new event
                this.CalcHours();


                DateTime date = DateTime.Now;
                date = date.AddHours(1);
                WorkDayEvent wde = new WorkDayEvent(date, WorkDayEventAction.DriveToRest);

                await this._IDataStore.AddWorkDayEventAsync(wde);

                this.DriveOrRestState = false;

            }
            else {
                this.changeStateButtonText = "Start Rusten";
                this.CalcHours();

                DateTime date = DateTime.Now;
                date = date.AddHours(1);

                WorkDayEvent wde = new WorkDayEvent(date, WorkDayEventAction.RestToDrive);

                await this._IDataStore.AddWorkDayEventAsync(wde);

                this.DriveOrRestState = true;
            }
        }


        public async void CalcHours() {
            DateTime now = DateTime.Now;

            WorkDayEvent latestWde = await this._IDataStore.GetLatestWorkDayEventAsync();
           

            TimeSpan dif = now.Subtract(latestWde.StartEvent);

            if (this.DriveOrRestState)
            {
                this.currentHours = "Aantal uren/minuten gereden:  " + dif.ToString("h\\:mm\\:ss");
            }
            else
            {
                this.currentHours = "Aantal uren/minuten gerust: " + dif.ToString("h\\:mm\\:ss");
            }

            List<WorkDayEvent> alleventsToday = await this._IDataStore.GetAllWorkDayEventsTodayAsync();

            WorkDayEvent prev = null;
            Boolean first = true;

            foreach (WorkDayEvent wde in alleventsToday) {
                if (first) {
                    prev = wde; 
                }
                if (wde.Action != WorkDayEventAction.StartDay) {

                    if (wde.Action == WorkDayEventAction.DriveToRest)
                    {
                        TimeSpan diff = wde.StartEvent.Subtract(prev.StartEvent);
                        this.TotalDriveHours += diff;
                    }
                    else if (wde.Action == WorkDayEventAction.RestToDrive) { 
                        TimeSpan difRest = wde.StartEvent.Subtract(prev.StartEvent);
                        this.TotRestHours += difRest;
                    }
                }
                prev = wde;
                first = false;
            }

            this.totalDriveHoursLabel = "Totaal aantal uren gereden vandaag: " + this.TotalDriveHours.ToString("h\\:mm\\:ss");
            this.totalRestHoursLabel = "Totaal aantal uren gerust vandaag: " + this.TotRestHours.ToString("h\\:mm\\:ss");


        }
        public async void setStartState() {
            WorkDayEvent latestWde = await this._IDataStore.GetLatestWorkDayEventAsync();

            if (latestWde.Action == WorkDayEventAction.StartDay || latestWde.Action == WorkDayEventAction.RestToDrive)
            {
                this.DriveOrRestState = true;
                this.changeStateButtonText = "Start Rusten";
            }
            else if (latestWde.Action == WorkDayEventAction.DriveToRest) {
                this.DriveOrRestState = false;
                this.changeStateButtonText = "Start Rijden";
            }


        }
    }

}
