namespace Planner.Domain
{
    public class JwtSettings
    {
        public static string Configuration = "jwtConfig";

        public string Key { get; set; }

        public int ExpiryTime { get; set; }
    }
}
