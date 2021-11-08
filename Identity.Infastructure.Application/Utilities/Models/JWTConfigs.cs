namespace Identity.Infastructure.Application.Utilities.Models
{
    public class JWTConfigs
    {
        public bool RequireHttpsMetadata { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public string Key { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int LifeTime { get; set; }
    }
}
