using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerkUrenCounterCsharp.Pages;


namespace WerkUrenCounterCsharp.ViewModel
{
    class MainPageViewModel: INotifyPropertyChanged
    {
        INavigation nav;

        private String _startDayBtnText = "Start dag";

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


        public MainPageViewModel(INavigation nav) {
            //this.startDayBtnText = "Start Dag";

            this.dayStarted = false;

            StartWorkDay = new Command(goToWorkDayPage);
            showOverview = new Command(goToOverview);
            this.nav = nav;
        }

        public async void goToOverview()
        {
            await this.nav.PushAsync(new Overview());
        }

        public async void goToWorkDayPage() {
            this.startDayBtnText = "Huidige dag";
            this.dayStarted = true;
            await this.nav.PushAsync(new WorkDayPage());
        }
    }
}
