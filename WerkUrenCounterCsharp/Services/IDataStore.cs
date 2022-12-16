using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerkUrenCounterCsharp.Models;
using System.Collections.ObjectModel;

namespace WerkUrenCounterCsharp.Services
{
    internal interface IDataStore
    {
        List<WorkDayEvent> WorkDayEventList { get; set; }

        Task<List<WorkDayEvent>> GetAllWorkDayEventsAsync();
        Task<String> AddTotoDoAsync();
        Task<String> DeleteToDoAsync();
    }
}
