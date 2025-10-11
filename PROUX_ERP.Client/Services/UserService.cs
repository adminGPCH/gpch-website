// D:\GPCH\APP\PROUX_ERP.Client\Services\UserService.cs

using PROUX_ERP.Shared.Models;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http; // necesario para SetBrowserRequestCredentials

namespace PROUX_ERP.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserInfo?> GetUserAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/user");
                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include); // 👈 clave: enviar cookies

                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                // Caso sin sesión → 401 limpio
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    return null;

                // Cualquier otro error (403, 500, etc.) → null
                if (!response.IsSuccessStatusCode)
                    return null;

                // Caso con sesión → deserializar JSON
                return await response.Content.ReadFromJsonAsync<UserInfo>();
            }
            catch
            {
                return null;
            }
        }
    }
}
