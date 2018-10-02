using System;

namespace LarryApplication.API.Models
{
    public class Photo
    {
        public int id { get; set; }
        public string Url { get; set; }
        public string description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }


    }
}