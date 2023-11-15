namespace HomeOrganizer.Models
{
    public class FeatureData
    {
        public string PageName { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string UserGivenName { get; set; } = "UserGivenName";
        public string UserGivenDescription { get; set; } = "My feature about this and that, very helpful!";
        public bool IsUsed { get; set; }
        public bool IsReusable { get; set; }


    }
}
