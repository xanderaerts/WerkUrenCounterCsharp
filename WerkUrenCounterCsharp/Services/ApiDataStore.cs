using WerkUrenCounterCsharp.Models;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

namespace WerkUrenCounterCsharp.Services
{
    public class ApiDataStore : IDataStore
    {
        public List<WorkDayEvent> WorkDayEventList { get ; set ; }

       // private string apiUrl = "http://10.0.2.2:8080/api/WorkDayEvents/";
        private string apiUrl = "http://localhost:8080/api/WorkDayEvents/";

        public ApiDataStore()
        {
            this.WorkDayEventList = new List<WorkDayEvent>();
        }

       public  async Task<HttpResponseMessage> AddWorkDayEventAsync(WorkDayEvent wde)
        {
            HttpClient client = new HttpClient();

            var json = System.Text.Json.JsonSerializer.Serialize<WorkDayEvent>(wde);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


            var res = await client.PostAsync(apiUrl, content);


           return res;

        }

        public async Task<WorkDayEvent> GetLatestWorkDayEventAsync() {
            HttpClient client = new HttpClient();

            String json = await client.GetStringAsync(this.apiUrl + "latest");

            return JsonConvert.DeserializeObject<WorkDayEvent>(json);
        }

        public async Task<List<WorkDayEvent>> GetAllWorkDayEventsTodayAsync() {
            HttpClient client = new HttpClient();

            String json = await client.GetStringAsync(this.apiUrl + "today");


            return JsonConvert.DeserializeObject<List<WorkDayEvent>>(json);
        }

        public Task<string> DeleteWorkDayEventAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<WorkDayEvent>> GetAllWorkDayEventsAsync()
        {
            HttpClient client = new HttpClient();

            String json = await client.GetStringAsync(this.apiUrl);

            //this.WorkDayEventList = JsonSerializer.Deserialize<List<WorkDayEvent>>(json);

            this.WorkDayEventList = JsonConvert.DeserializeObject<List<WorkDayEvent>>(json);

            return this.WorkDayEventList;

        }

    }
}
