namespace CharchoobApi.Host.Options;

public class AuthOption
{
    public string SecureKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiresInMinutes { get; set; }
    public int AccessTokenLife { get; set; }
    public int RefreshTokenLife { get; set; }
    public int HttpOnlyCookieLife { get; set; }
}
