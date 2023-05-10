using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using DtoLibrary;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace VkApiTests;

[TestCaseOrderer(
    ordererTypeName: "XUnit.Project.PriorityOrderer",
    ordererAssemblyName: "XUnit.Project")]
public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;
    private readonly string _url = "https://localhost:7287/";
    private readonly UserDto DefaultUser;
    private readonly UserDto AdminUser;
    
    public IntegrationTests(WebApplicationFactory<Program> fixture, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _client = fixture.CreateClient();
        DefaultUser = new UserDto() { Login = "User", Password = "Password", Role = Role.User};
        AdminUser = new UserDto() { Login = "Admin", Password = "Admin", Role = Role.Admin};
    }
    
    
    [Fact, TestPriority(1)]
    public async Task Registration_Same_User_Twice1()
    {
        var registrationUrl = _url + "api/auth/register";
        
        var user = DefaultUser;

        var resp = await _client.PostAsJsonAsync(registrationUrl, user);
        Assert.True(resp.IsSuccessStatusCode);

        var secondResp = await _client.PostAsJsonAsync(registrationUrl, user);
        Assert.False(secondResp.IsSuccessStatusCode);
    }
    
    [Fact, TestPriority(2)]
    public async Task Registration_Admin_Twice2()
    {
        
        var registrationUrl = _url + "api/auth/register";

        var user = AdminUser;
        
        var secondUser = new UserDto()
        {
            Password = "asdsad",
            Login = "qwerty",
            Role = Role.Admin
        };

        var resp = await _client.PostAsJsonAsync(registrationUrl, user);
        Assert.True(resp.IsSuccessStatusCode);

        var secondResp = await _client.PostAsJsonAsync(registrationUrl, secondUser);
        Assert.False(secondResp.IsSuccessStatusCode);
    }
    
    [Fact, TestPriority(3)]
    public async Task GetUsersInfo3()
    {
        var usersUrl = _url + "api/user/users";

        var pagination = new GetUsersDto()
        {
            Page = 1,
            PageSize = 10
        };

        var basic = Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(AdminUser.Login + ":" + AdminUser.Password));
        var request = new HttpRequestMessage(HttpMethod.Post, usersUrl);

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Authorization = 
            new AuthenticationHeaderValue("Bearer", basic);
        request.Content = JsonContent.Create(pagination);
        request.Version = _client.DefaultRequestVersion;

        var resp = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
        Assert.True(resp.IsSuccessStatusCode);
        if (resp.IsSuccessStatusCode)
        {
            using var payload = await JsonDocument.ParseAsync(await resp.Content.ReadAsStreamAsync());
            _testOutputHelper.WriteLine(payload.RootElement.ToString());
        }
    }
}