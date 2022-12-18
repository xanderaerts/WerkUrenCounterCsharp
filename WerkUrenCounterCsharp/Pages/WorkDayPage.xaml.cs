using WerkUrenCounterCsharp.ViewModel;

namespace WerkUrenCounterCsharp.Pages;

public partial class WorkDayPage : ContentPage
{
	WorkDayPageViewModel vm;
	public WorkDayPage()
	{
		InitializeComponent();

		BindingContext = this.vm = new WorkDayPageViewModel();


	}
}