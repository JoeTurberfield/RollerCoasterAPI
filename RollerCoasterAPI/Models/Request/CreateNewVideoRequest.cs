using System.Text.Json.Serialization;

namespace RollerCoasterAPI.Models.Request
{
    public class CreateNewVideoRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
