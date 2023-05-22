using System.Net.Http.Json;
using System.Net;
using DataAccess.Models;

namespace ChessOpeningHub.Client.ApiCalls
{
    internal static class ChessOpeningHubAPI
    {
        static HttpClient client = new HttpClient();

        static string baseAdress = "https://8213-84-105-129-218.ngrok-free.app/openings/";

        internal static async Task<OpeningModel[]?> GetOpenings()
        {
            client.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");
            OpeningModel[]? openings = null;
            HttpResponseMessage response = await client.GetAsync(baseAdress);
            if (response.IsSuccessStatusCode)
            {
                openings = await response.Content.ReadFromJsonAsync<OpeningModel[]>();
            }
            return openings;
        }

        internal static async Task<OpeningModel?> GetOpening(int id)
        {
            OpeningModel? opening = null;
            HttpResponseMessage response = await client.GetAsync($"{baseAdress}{id}");
            if (response.IsSuccessStatusCode)
            {
                opening = await response.Content.ReadFromJsonAsync<OpeningModel>();
            }
            return opening;
        }

        internal static async Task<Uri> PostOpening(OpeningModel opening)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                baseAdress, opening);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        internal static async Task<HttpStatusCode> PutOpening(OpeningModel opening)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"{baseAdress}{opening.Id}", opening);
            response.EnsureSuccessStatusCode();
            return response.StatusCode;

        }

        internal static async Task<HttpStatusCode> DeleteOpening(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"{baseAdress}{id}");
            return response.StatusCode;
        }
    }
}
