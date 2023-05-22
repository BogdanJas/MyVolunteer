using MyVolunteer_Client.Service.IService;
using MyVolunteer_Models;
using Newtonsoft.Json;

namespace MyVolunteer_Client.Service
{
    public class ProjectDateService:IProjectDateService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public ProjectDateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }
        public async Task<IEnumerable<ProjectDateDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/projectdate");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var projectDates = JsonConvert.DeserializeObject<IEnumerable<ProjectDateDTO>>(content);
                return projectDates;
            }

            return new List<ProjectDateDTO>();
        }
        public async Task<ProjectDateDTO> Get(int projectId)
        {
            var response = await _httpClient.GetAsync($"/api/projectdate/{projectId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var projectDate = JsonConvert.DeserializeObject<ProjectDateDTO>(content);
                return projectDate;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

        }
    }
}
