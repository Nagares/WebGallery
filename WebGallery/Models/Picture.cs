using System;

namespace WebGallery.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public DateTime LoadDate { get; set; }
        public string FilePath { get; set; }

    }

}
