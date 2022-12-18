//using WerkurenCounterCsharpapi;
using WerkUrenCounterCsharp;
using WerkUrenCounterCsharp.ViewModel;
namespace UnitTest
{
    public class UnitTest
    {
        [Fact]
        public void Test1()
        {
            var vm = new WorkDayPageViewModel();

            vm.setStartState();
            vm.DriveOrRestState.Equals(true);
        }

        [Fact]
        public void Test2() {
            var vm = new WorkDayPageViewModel();
            vm.changeStateButtonText.Equals("Start Rusten");

        }
    }

}