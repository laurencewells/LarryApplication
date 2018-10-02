using System;

namespace LarryApplication.API.Dtos
{
    public class PhotosForDetailedDto
    {
         public int id { get; set; }
        public string Url { get; set; }
        public string description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
    }
}