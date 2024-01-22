namespace inSync.Infrastructure.Minotar;

public class MinotarClient
{
    private const int DefaultWidth = 128;
    private const string BaseUrl = "https://minotar.net/";

    public static string GetFace(string username, int width = DefaultWidth) => $"{BaseUrl}/avatar/{username}/{width}.png";
    public static string GetFaceWithHelm(string username, int width = DefaultWidth) => $"{BaseUrl}/helm/{username}/{width}.png";
    public static string GetHead(string username, int width = DefaultWidth) => $"{BaseUrl}/head/{username}/{width}.png";
    public static string GetBody(string username, int width = DefaultWidth) => $"{BaseUrl}/body/{username}/{width}.png";
    public static string GetBodyWithHelm(string username, int width = DefaultWidth) => $"{BaseUrl}/armor/body/{username}/{width}.png";
    public static string GetBust(string username, int width = DefaultWidth) => $"{BaseUrl}/bust/{username}/{width}.png";
    public static string GetBustWithHelm(string username, int width = DefaultWidth) => $"{BaseUrl}/armor/bust/{username}/{width}.png";
}