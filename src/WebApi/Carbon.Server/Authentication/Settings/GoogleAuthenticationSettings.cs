namespace Carbon.Server.Authentication.Settings;

public sealed class GoogleAuthenticationSettings
{
    public const string SectionName = "GoogleAuthentication";
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string AuthorizationEndpoint { get; set; }
    public required string TokenEndpoint { get; set; }
    public required string CallbackPath { get; set; }
    public required string UserInformationEndpoint { get; set; }
    public required string IdJsonKey { get; set; }
    public required string NameJsonKey { get; set; }
    public required string EmailJsonKey { get; set; }
    public required string[] Scopes { get; set; }
}