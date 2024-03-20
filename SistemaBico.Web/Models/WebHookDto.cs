namespace SistemaBico.Web.Models
{
    public class WebHookDto
    {
        public string action { get; set; }  
        public string api_version { get; set; }  
        public string date_created { get; set; }  
        public long id { get; set; }  
        public bool live_mode { get; set; }  
        public string type { get; set; }  
        public string user_id { get; set; }  
        public WebHookId data { get; set; }  
    }

    public class WebHookId
    {
        public string id { get; set; }
    }
}
