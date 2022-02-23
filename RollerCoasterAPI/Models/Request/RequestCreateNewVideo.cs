using System.Text.Json.Serialization;

namespace RollerCoasterAPI.Models.Request
{
    public class RequestCreateNewVideo
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
