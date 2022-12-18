

using System.ComponentModel;
using WerkUrenCounterCsharp.Models;
using WerkUrenCounterCsharp.Services;

namespace WerkUrenCounterCsharp.ViewModel
{
    public class WorkDayPageViewModel : INotifyPropertyChanged
    {
        private IDataStore _IDataStore => DependencyService.Get<IDataStore>();

        public String _changeStateButtonText = "Start Rusten";
        public Boolean ButtonTextState = true;

        public String _driveHoursLeftLabel;

        private const int maxDriveHours = 9*60;
        private const int maxDayLenght = 15*60;


        



        public event PropertyChangedEventHandler PropertyChanged;

        public String changeStateButtonText
        {
            get { return this._changeStateButtonText; }
            set
            {
                this._changeStateButtonText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(changeStateButtonText)));
            }
        }

        public String driveHoursLeftLabel
        {
            get { return this._driveHoursLeftLabel; }
            set
            {
                this._driveHoursLeftLabel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(driveHoursLeftLabel)));
            }
        }

        public Command changeStateButton { get; }



        public WorkDayPageViewModel() {
            this.changeStateButton = new Command(changeStateButtonPushed);
        }

        public async void changeStateButtonPushed() {
            if (this.ButtonTextState)
            {
                this.changeStateButtonText = "Start Rijden";
                this.ButtonTextState = false;


                DateTime date = DateTime.Now;
                WorkDayEvent wde = new WorkDayEvent(date, WorkDayEventAction.DriveToRest);

                await this._IDataStore.AddWorkDayEventAsync(wde);

                this.CalcHoursLeft();

            }
            else {
                this.changeStateButtonText = "Start Rusten";


                DateTime date = DateTime.Now;
                WorkDayEvent wde = new WorkDayEvent(date, WorkDayEventAction.RestToDrive);

                await this._IDataStore.AddWorkDayEventAsync(wde);



                this.ButtonTextState = true;
            }
        }


        public async void CalcHoursLeft() {
            DateTime now = DateTime.Now;

            WorkDayEvent latestWde = await this._IDataStore.GetLatestWorkDayEventAsync();

            if (now < latestWde.StartEvent) {
                throw new Exception("I think you live in future :)");
            }

            //var dif = (now - latestWde.StartEvent).TotalSeconds;

            TimeSpan dif = now.Subtract(latestWde.StartEvent);

            this.driveHoursLeftLabel = dif.TotalMinutes.ToString();
         
        }
    }
}
