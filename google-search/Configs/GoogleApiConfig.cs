
namespace google_search.Configs
{
    public class GoogleApiConfig
    {
        public string URL { get; set; }
        public string Key { get; set; }
        public string CX { get; set; }
        public string Fields { get; set; }
        public string SearchParameterName { get; set; }
        public string NumberParameterName { get; set; }
        public string StartParameterName { get; set; }
        public int MaxResultsPerRequest { get; set; }
    }
}
