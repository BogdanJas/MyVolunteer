using MyVolunteer_Client.Service.IService;
using MyVolunteer_Models;
using Newtonsoft.Json;
using System.Text;

namespace MyVolunteer_Client.Service
{
    public class ProjectSignUpService : IProjectSignUpService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        public ProjectSignUpService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<ProjectSignUpDTO> Add(ProjectSignUpDTO projectSignUp)
        {
            var content = JsonConvert.SerializeObject(projectSignUp);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/projectSignUp/add", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ProjectSignUpDTO>(contentTemp);
            return result;
        }
        public async Task<IEnumerable<ProjectSignUpDTO>> GetAll(string? userId)
        {
            var response = await _httpClient.GetAsync("/api/projectSignUp");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var projectSignUps = JsonConvert.DeserializeObject<IEnumerable<ProjectSignUpDTO>>(content);
                return projectSignUps;
            }

            return new List<ProjectSignUpDTO>();
        }
        public async Task<ProjectSignUpDTO> Get(int projectSignUpHeaderId)
        {
            var response = await _httpClient.GetAsync($"/api/projectSignUp/{projectSignUpHeaderId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var projectSignUp = JsonConvert.DeserializeObject<ProjectSignUpDTO>(content);
                return projectSignUp;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

        }
    }
}
