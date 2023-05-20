using MyVolunteer_Client.Service.IService;
using MyVolunteer_Models;
using Newtonsoft.Json;

namespace MyVolunteer_Client.Service
{
    public class ProjectService:IProjectService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public ProjectService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }
        public async Task<IEnumerable<ProjectDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/project");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var projects = JsonConvert.DeserializeObject<IEnumerable<ProjectDTO>>(content);
                foreach (var proj in projects)
                {
                    proj.ImageUrl = BaseServerUrl + proj.ImageUrl;
                }
                return projects;
            }

            return new List<ProjectDTO>();
        }
        public async Task<ProjectDTO> Get(int projectId)
        {
            var response = await _httpClient.GetAsync($"/api/project/{projectId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var project = JsonConvert.DeserializeObject<ProjectDTO>(content);
                project.ImageUrl = BaseServerUrl + project.ImageUrl;
                return project;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

        }
    }
}
