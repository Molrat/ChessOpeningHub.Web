using System.Net.Http.Json;
using System.Net;
using DataAccess.Models;

namespace ChessOpeningHub.Client.ApiCalls
{
    internal static class ChessOpeningHubAPI
    {
        static HttpClient client = new HttpClient();

        internal static async Task<OpeningModel[]?> GetOpenings()
        {
            OpeningModel[]? openings = null;
            HttpResponseMessage response = await client.GetAsync("https://localhost:7204/Openings");
            if (response.IsSuccessStatusCode)
            {
                openings = await response.Content.ReadFromJsonAsync<OpeningModel[]>();
            }
            return openings;
        }

        internal static async Task<OpeningModel?> GetOpening(int id)
        {
            OpeningModel? opening = null;
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7204/Openings/{id}");
            if (response.IsSuccessStatusCode)
            {
                opening = await response.Content.ReadFromJsonAsync<OpeningModel>();
            }
            return opening;
        }

        internal static async Task<Uri> PostOpening(OpeningModel opening)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:7204/Openings", opening);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        internal static async Task<HttpStatusCode> PutOpening(OpeningModel opening)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"https://localhost:7204/Openings/{opening.Id}", opening);
            response.EnsureSuccessStatusCode();
            return response.StatusCode;

        }

        internal static async Task<HttpStatusCode> DeleteOpening(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"https://localhost:7204/Openings/{id}");
            return response.StatusCode;
        }
    }
}
