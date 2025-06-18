using Estimatz.UI.Entities.Room;
using Estimatz.UI.Entities.Story;
using Estimatz.UI.ExternalServices.EstimatzApi.AddStory;
using Estimatz.UI.ExternalServices.EstimatzApi.CreateRoom;
using Estimatz.UI.ExternalServices.EstimatzApi.GetAllSimpleRooms;
using Estimatz.UI.ExternalServices.EstimatzApi.GetIndicators;
using Estimatz.UI.ExternalServices.EstimatzApi.GetRoom;
using Estimatz.UI.ExternalServices.EstimatzApi.GetStory;
using Estimatz.UI.ExternalServices.EstimatzApi.RemoveStory;
using Estimatz.UI.ExternalServices.EstimatzApi.UpdateStoryStatus;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;
using Newtonsoft.Json;
using System.Text;

namespace Estimatz.UI.ExternalServices.EstimatzApi
{
    public class EstimatzApiClient : IEstimatzApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;

        public EstimatzApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("EstimatzApi");
        }

        public async Task<Guid> CreateRoom(Room request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/Room/create-room", content);
            var responseString = await response.Content?.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<CreateRoomResponse>(responseString);

            return string.IsNullOrEmpty(responseString) ? Guid.Empty : responseObj.Data;
        }

        public async Task<GetAllSimpleRoomsResponse> GetAllSimpleRooms(Guid userId)
        {            
            var response = await _client.GetAsync($"/api/v1/Room/get-simple-rooms?userId={userId}");
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new GetAllSimpleRoomsResponse() : JsonConvert.DeserializeObject<GetAllSimpleRoomsResponse>(responseString);
        }

        public async Task<CommonResponse> DeleteRoom(Guid roomId, Guid userId)
        {
            var response = await _client.DeleteAsync($"/api/v1/Room/delete-rooms?roomId={roomId}&userId={userId}");
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }

        public async Task<GetRoomResponse> GetRoom(Guid roomId, Guid userId)
        {
            var response = await _client.GetAsync($"/api/v1/Room/get-room?roomId={roomId}&userId={userId}");
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new GetRoomResponse() : JsonConvert.DeserializeObject<GetRoomResponse>(responseString);
        }

        public async Task<CommonResponse> AddStory(AddStoryRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/Story/add-story", content);
            var responseString = await response.Content?.ReadAsStringAsync();
            
            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }

        public async Task<CommonResponse> RemoveStory(RemoveStoryRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/Story/remove-story", content);
            var responseString = await response.Content?.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }

        public async Task<CommonResponse> UpdateStoryStatus(UpdateStoryStatusRequest request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/v1/Story/update-status-story", content);
            var responseString = await response.Content?.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new CommonResponse() : JsonConvert.DeserializeObject<CommonResponse>(responseString);
        }

        public async Task<GetStoryResponse> GetStory(Guid roomId, Guid storyId)
        {
            var response = await _client.GetAsync($"/api/v1/Story/get-story?roomId={roomId}&storyId={storyId}");
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new GetStoryResponse() : JsonConvert.DeserializeObject<GetStoryResponse>(responseString);
        }

        public async Task<GetIndicatorsResponse> GetIndicators(Guid userId)
        {
            var response = await _client.GetAsync($"/api/v1/Room/get-indicators?userId={userId}");
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ? new GetIndicatorsResponse() : JsonConvert.DeserializeObject<GetIndicatorsResponse>(responseString);
        }
    }
}
