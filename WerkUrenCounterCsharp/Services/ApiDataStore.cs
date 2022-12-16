using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WerkUrenCounterCsharp.Models;

namespace WerkUrenCounterCsharp.Services
{
    public class ApiDataStore : IDataStore
    {
        public List<WorkDayEvent> WorkDayEventList { get ; set ; }

        public ApiDataStore()
        {
            this.WorkDayEventList = new List<WorkDayEvent>();
        }

        public async Task<string> AddTotoDoAsync()
        {
            throw new NotImplementedException();
            
        }

        public Task<string> DeleteToDoAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<WorkDayEvent>> GetAllWorkDayEventsAsync()
        {
            HttpClient client = new HttpClient();

            String json = await client.GetStringAsync("http://localhost:8080/api/workdayevents/");

            this.WorkDayEventList = JsonSerializer.Deserialize<List<WorkDayEvent>>(json);

            return this.WorkDayEventList;

        }
    }
}
