
using System.ComponentModel;
using WerkUrenCounterCsharp.Models;
using WerkUrenCounterCsharp.Pages;
using WerkUrenCounterCsharp.Services;


namespace WerkUrenCounterCsharp.ViewModel
{
    public class MainPageViewModel: INotifyPropertyChanged
    {
        INavigation nav;
        private IDataStore _IDataStore => DependencyService.Get<IDataStore>();

        public String _startDayBtnText = "Start dag";

        public Boolean _dayStarted = false;
        public Boolean dayStarted {
            get { return this._dayStarted; }
            set {
                this._dayStarted = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(dayStarted)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public String startDayBtnText { 
            get { return this._startDayBtnText; } 
            set {
                this._startDayBtnText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(startDayBtnText)));
            } }


        public Command StartWorkDay { get; }
        public Command showOverview { get; }
        
        public Command EndWorkDayCommand { get; }


        public MainPageViewModel(INavigation nav) {
            //this.startDayBtnText = "Start Dag";

            this.dayStarted = false;

            StartWorkDay = new Command(goToWorkDayPage);
            showOverview = new Command(goToOverview);
            EndWorkDayCommand = new Command(EndWorkDay);
            this.nav = nav;
        }

        public async void goToOverview()
        {
            await this.nav.PushAsync(new Overview());
        }

        public async void goToWorkDayPage() {
            this.startDayBtnText = "Huidige dag";

            if (!dayStarted)
            {
                this.dayStarted = true;
                DateTime date = DateTime.Now;
                date = date.AddHours(1);

                WorkDayEvent newWde = new WorkDayEvent(date, WorkDayEventAction.StartDay);

                await this._IDataStore.AddWorkDayEventAsync(newWde);
            }

            await this.nav.PushAsync(new WorkDayPage());
        }

        public async void EndWorkDay() {
            DateTime date = DateTime.Now;

            date = date.AddHours(1);

            WorkDayEvent wde = new WorkDayEvent(date, WorkDayEventAction.EndDay);
            await this._IDataStore.AddWorkDayEventAsync(wde);

            this.dayStarted = false;
            this.startDayBtnText = "Start Dag";
        }
    }
}
