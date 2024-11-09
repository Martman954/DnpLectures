using System.Security.Claims;
using System.Text.Json;
using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.JSInterop;

namespace BlazorApp.Components.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient client;
    private readonly IJSRuntime jsRuntime;

    private ClaimsPrincipal currentClaimsPrincipal;

    public SimpleAuthProvider(HttpClient client, IJSRuntime jsRuntime)
    {
        this.client = client;
        this.jsRuntime = jsRuntime;
    }

    public async Task Login(string username, string password)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("auth/login", new CreateUserDto
        {
            Username = username,
            Password = password
        });
        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("CONTENT: " + content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        UserDto userDto = JsonSerializer.Deserialize<UserDto>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        string serialisedData = JsonSerializer.Serialize(userDto);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString())
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        currentClaimsPrincipal = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(currentClaimsPrincipal))));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string userAsJson = "";
        try
        {
            userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        }
        catch (InvalidOperationException e)
        {
            return new AuthenticationState(new());
        }

        if (string.IsNullOrEmpty(userAsJson))
        {
            return new AuthenticationState(new());
        }

        UserDto userDto = JsonSerializer.Deserialize<UserDto>(userAsJson)!;
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDto.Username), new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
        return new AuthenticationState(claimsPrincipal);
    }

    public async Task Logout()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", ""); 
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }
    
}