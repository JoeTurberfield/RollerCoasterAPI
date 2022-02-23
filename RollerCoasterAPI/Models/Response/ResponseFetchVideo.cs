using System.Text.Json.Serialization;

namespace RollerCoasterAPI.Models.Response
{
    public class ResponseFetchVideo
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
    }
}
