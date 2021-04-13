using GillowWebApp.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class SearchResult
    {
        public List<Searches> searches { get; set; }
        public List<SearchItem> searchResult { get; set; }
        public String message { get; set; }
    }
}
