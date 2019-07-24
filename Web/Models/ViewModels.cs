using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Url
    {
        public string ShortUrl { get; set; } 
    }
    public class SearchSet
    {
        public string ShortUrl { get; set; }
        public string[] RawUrls { get; set; }
    }
    public class AddSearchSet
    {
        public string Key { get; set; }
        public string ShortUrl { get; set; }
    }
    public class AddUrl
    {
        public string ShortUrl { get; set; }
        public string RawUrl { get; set; }
    }

    public class AddUrlSet : AddUrl
    {
        public string Key { get; set; }
    }
}
