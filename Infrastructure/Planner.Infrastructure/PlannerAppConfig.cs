namespace Planner.Infrastructure
{
    public class PlannerAppConfig
    {
        public static string Configuration = "config";

        public string ConnectionString { get; set; }

        public string  DataBase { get; set; }
    }
}
