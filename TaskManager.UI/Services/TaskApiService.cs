using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Dtos;

namespace TaskManager.UI.Services
{
    public class TaskApiService
    {
        private readonly HttpClient _http;

        public TaskApiService(HttpClient http)
        {
            _http = http;
        }

        // Получить все задачи
        public async Task<PagedResult<ReadTaskItemDto>> GetTasksAsync(int page = 1, int pageSize = 10)
        {
            var url = $"https://localhost:7191/api/task?page={page}&pageSize={pageSize}";
            return await _http.GetFromJsonAsync<PagedResult<ReadTaskItemDto>>(url, new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            });
        }

        // Создать задачу
        public async Task<ReadTaskItemDto> CreateTaskAsync(CreateTaskItemDto dto)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:7191/api/task", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>();
        }

        // Обновить задачу
        public async Task<ReadTaskItemDto> UpdateTaskAsync(ReadTaskItemDto dto)
        {
            var response = await _http.PutAsJsonAsync($"https://localhost:7191/api/task/{dto.Id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>();
        }

        // Пометить как выполненную
        public async Task<ReadTaskItemDto> CompleteTaskAsync(int id)
        {
            var response = await _http.PostAsync($"https://localhost:7191/api/task/{id}/complete", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>();
        }

        // Soft Delete
        public async Task<ReadTaskItemDto> DeleteTaskAsync(int id)
        {
            var response = await _http.DeleteAsync($"https://localhost:7191/api/task/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>();
        }
    }
}
