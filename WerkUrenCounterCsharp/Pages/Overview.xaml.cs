using WerkUrenCounterCsharp.ViewModel;

namespace WerkUrenCounterCsharp.Pages;
public partial class Overview : ContentPage
{
    OverviewViewModel vm;
    public Overview()
    {
        

        InitializeComponent();
        BindingContext = this.vm = new OverviewViewModel();
    }
}