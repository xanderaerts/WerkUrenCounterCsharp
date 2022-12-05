
using WerkUrenCounterCsharp.ViewModel;

namespace WerkUrenCounterCsharp;


public partial class MainPage : ContentPage
{
	MainPageViewModel vm;

	public MainPage()
	{
		InitializeComponent();
		BindingContext = this.vm = new MainPageViewModel(Navigation);
	}
}

