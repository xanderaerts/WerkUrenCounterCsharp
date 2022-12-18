
using WerkUrenCounterCsharp.Services;
using WerkUrenCounterCsharp.ViewModel;

namespace WerkUrenCounterCsharp;


public partial class MainPage : ContentPage
{
	public MainPageViewModel vm;

	public MainPage()
	{
		InitializeComponent();

		DependencyService.Register<ApiDataStore>();

		BindingContext = this.vm = new MainPageViewModel(Navigation);
	}
}

