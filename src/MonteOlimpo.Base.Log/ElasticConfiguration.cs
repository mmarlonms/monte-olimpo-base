namespace MonteOlimpo.Base.Log
{
    public class ElasticConfiguration
    {
        public bool UseAuthentication { get; set; }
        public string UrlHost { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string IndexName { get; set; }
    }
}