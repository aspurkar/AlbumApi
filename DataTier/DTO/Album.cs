using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.DTO
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string Artist { get; set; }
        public string Year { get; set; }
        public int Price { get; set; }
    }
}
