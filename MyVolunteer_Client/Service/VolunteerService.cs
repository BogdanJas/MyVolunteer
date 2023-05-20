using MyVolunteer_Client.Service.IService;
using MyVolunteer_Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace MyVolunteer_Client.Service
{
    public class VolunteerService : IVolunteerService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        public VolunteerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<VolunteerDTO> Add(VolunteerDTO volunteer)
        {
            var content = JsonConvert.SerializeObject(volunteer);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsJsonAsync("api/volunteer/add", content);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<VolunteerDTO>(contentTemp);

            return result;
        }

        public async Task<VolunteerDTO> Get(int volunteerId)
        {
            var response = await _httpClient.GetAsync($"/api/volunteer/get/{volunteerId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var volunteer = JsonConvert.DeserializeObject<VolunteerDTO>(content);
                //volunteer.ImageUrl = BaseServerUrl + volunteer.ImageUrl;
                return volunteer;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

        }

        public async Task<VolunteerDTO> Update(VolunteerDTO volunteer)
        {
            var content = JsonConvert.SerializeObject(volunteer);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/volunteer/update", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<VolunteerDTO>(contentTemp);

            return result;
        }
    }
}
