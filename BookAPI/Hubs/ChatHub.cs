using DAL.DTO;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using BLL.Interface;

namespace BookAPI.Hubs
{
    public class ChatHub:Hub
    {
        private readonly HttpClient _httpClient;
        private readonly IChatService _chatService;
        public ChatHub(HttpClient httpClient, IChatService chatService)
        {
            _httpClient = httpClient;
            _chatService = chatService;
        }
        public async Task SendMessage(SendMessageModel sendMessage, string token, string senderid)
        {
            var json = JsonSerializer.Serialize(sendMessage);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Set the bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            // Make the POST request
            var response = await _httpClient.PostAsync("https://localhost:7159/api/Chat/SendMessage", content);

            if(response.IsSuccessStatusCode)
            {
                await Clients.All.SendAsync("ReceiveMessage", senderid, sendMessage.Content);
            }
        }
    }
}
