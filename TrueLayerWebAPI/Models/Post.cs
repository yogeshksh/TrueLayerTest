using System;
using System.ComponentModel.DataAnnotations;

namespace TrueLayerWebAPI.Models
{
    /// <summary>
    /// Model class for Post object
    /// </summary>
    public class Post
    {
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        [Required]
        [StringLength(256)]
        public string Author { get; set; }
        public string Uri { get; set; }
        public int Points { get; set; }
        public int Comments { get; set; }
        public int Rank { get; set; }

        public Post(string _title, string _author, string _uri, int _points, int _comments, int _rank)
        {
            Title = _title;
            Author = _author;
            Uri = _uri;
            Points = _points;
            Comments = _comments;
            Rank = _rank;
        }

        public Post(string _title, string _author)
        {
            Title = _title;
            Author = _author;
        }
    }
}
