using Newtonsoft.Json;

namespace XamarinAzureChallenge.Shared.Models
{
    public class User
    {
        [JsonProperty("fullname")]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonProperty("acceptTermsOfService")]
        public bool AcceptTermsOfService { get; set; } = false;

        [JsonProperty("allowsComercialUse")]
        public bool AllowsComercialUse { get; set; } = false;
    }
}
