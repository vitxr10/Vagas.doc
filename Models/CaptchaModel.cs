using System.Text.Json.Serialization;

namespace VagasDoc.Models
{
    public class CaptchaModel
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
