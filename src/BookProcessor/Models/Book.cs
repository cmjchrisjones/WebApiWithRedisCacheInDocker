namespace BookProcessor.Models
{
    using Newtonsoft.Json;

    public class Book
    {
        public string Publisher { get; set; }

        public string Language { get; set; }

        public string Image { get; set; }

        [JsonProperty("title_long")]
        public string TitleLong { get; set; }

        public string Dimensions { get; set; }

        public int Pages { get; set; }

        [JsonProperty("date_published")]
        public string DatePublished { get; set; }

        public string[] Authors { get; set; }

        public string Title { get; set; }

        public string ISBN13 { get; set; }

        public string MSRP { get; set; }

        public string Binding { get; set; }

        [JsonProperty("publish_date")]
        public string PublishDate { get; set; }

        public string ISBN { get; set; }
    }
}