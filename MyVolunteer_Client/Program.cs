using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyVolunteer_Client;
using MyVolunteer_Client.Service;
using MyVolunteer_Client.Service.IService;
using MyVolunteer_Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddScoped<IProjectService,ProjectService>();
builder.Services.AddScoped<IProjectSignUpService, ProjectSignUpService>();
builder.Services.AddScoped<IProjectDateService, ProjectDateService>();
builder.Services.AddScoped<IVolunteerService,VolunteerService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IFileUpload, FileUpload>();

await builder.Build().RunAsync();
