using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Dtos;

namespace TaskManager.UI.Services
{
    public class TaskApiService
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };
        public TaskApiService(HttpClient http)
        {
            _http = http;
        }

        // 1. Get all tasks
        public async Task<PagedResult<ReadTaskItemDto>?> GetTasksAsync(int page = 1, int pageSize = 10)
        {
            var url = $"https://localhost:7191/api/task?page={page}&pageSize={pageSize}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PagedResult<ReadTaskItemDto>>(_jsonSerializerOptions) 
                ?? throw new InvalidOperationException("Response body was empty.");
            return result;
        }

        // 2. Create task
        public async Task<ReadTaskItemDto?> CreateTaskAsync(CreateTaskItemDto dto)
        {
            var url = $"https://localhost:7191/api/task";
            var response = await _http.PostAsJsonAsync(url, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>(_jsonSerializerOptions)
                ?? throw new InvalidOperationException("Response body was empty.");
        }

        // 3. Get Task by Id
        public async Task<ReadTaskItemDto?> GetTaskByIdAsync(int id)
        {
            var url = $"https://localhost:7191/api/task/{id}";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>(_jsonSerializerOptions)
                ?? throw new InvalidOperationException("Response body was empty.");
        }

        // 4. Update task
        public async Task<ReadTaskItemDto?> UpdateTaskAsync(ReadTaskItemDto dto)
        {
            var url = $"https://localhost:7191/api/task/{dto.Id}";
            var response = await _http.PutAsJsonAsync(url, dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>(_jsonSerializerOptions)
                ?? throw new InvalidOperationException("Response body was empty.");
        }

        // 5. Soft Delete task
        public async Task DeleteTaskAsync(int id)
        {
            var url = $"https://localhost:7191/api/task/{id}";
            var response = await _http.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }

        // 6. Get all deleted tasks
        public async Task<PagedResult<ReadTaskItemDto>?> GetDeletedTasksAsync()
        {
            var url = $"https://localhost:7191/api/task/deleted";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PagedResult<ReadTaskItemDto>>(_jsonSerializerOptions)
                ?? throw new InvalidOperationException("Response body was empty.");
            return result;
        }

        // 7. Complete task
        public async Task<ReadTaskItemDto?> CompleteTaskAsync(int id)
        {
            var url = $"https://localhost:7191/api/task/{id}/complete";
            var response = await _http.PostAsync(url, null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>(_jsonSerializerOptions)
                ?? throw new InvalidOperationException("Response body was empty.");
        }


        // 8. Restore task
        public async Task<ReadTaskItemDto?> RestoreTaskAsync(int id)
        {
            var url = $"https://localhost:7191/api/task/{id}/restore";
            var response = await _http.PostAsync(url, null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReadTaskItemDto>(_jsonSerializerOptions)
                ?? throw new InvalidOperationException("Response body was empty.");
        }

        // 9. Permanently delete task
        public async Task DeleteTaskPermanentlyAsync(int id)
        {
            var url = $"https://localhost:7191/api/task/{id}/permanently";
            var response = await _http.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
