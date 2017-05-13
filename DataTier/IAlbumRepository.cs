using DataTier.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier
{
    public interface IAlbumRepository
    {
        int AddAlbum(Album album);
        IEnumerable<Album> GetAllAlbums();
        IEnumerable<Album> SearchAlbum(object searchTerm,SearchBy searchBy);
        int UpdateAlbum(Album album);
        void DeleteAlbum(int albumId);
    }
    public enum SearchBy
    {
        Id,Name,Artist,Year
    }
}
