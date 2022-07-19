using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DemoApp6.Models
{
    public class MovieGenreViewModel
    {
        public List<Movies> Movies { get; set; }
        public SelectList Genres { get; set; }
        public string MovieGenre { set; get; }
        public string SearchString { set; get; }
    }
}
