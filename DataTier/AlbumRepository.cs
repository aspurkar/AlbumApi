
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using DataTier.DTO;
using System.Configuration;


namespace DataTier
{
    public class AlbumRepository : IAlbumRepository
    {
        private string _conString;
        public AlbumRepository()
        {
            _conString = ConfigurationManager.ConnectionStrings["albumConString"].ConnectionString;
        }
        public AlbumRepository(string conString)
        {
            _conString = conString;
        }
        public int AddAlbum(Album album)
        {
            try
            {
                using (var con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("AddAlbum",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AlbumName", album.AlbumName);
                    cmd.Parameters.AddWithValue("Artist", album.Artist);
                    var dt = DateTime.Parse(album.Year);
                    cmd.Parameters.AddWithValue("Year", dt);
                    cmd.Parameters.AddWithValue("Price", album.Price);
                    decimal res = (decimal)cmd.ExecuteScalar();
                    return (int)res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DTO.Album> GetAllAlbums()
        {
            List<Album> albumList = new List<Album>();
            try
            {
                using (var con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from albums", con);
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Album album = new Album();
                        album.AlbumId = (int)dr["AlbumId"];
                        album.AlbumName = (string)dr["AlbumName"];
                        album.Artist = (string)dr["Artist"];
                        album.Price = (int)dr["Price"];
                        var yr = (DateTime)dr["Year"];
                        album.Year = yr.ToShortDateString();
                        albumList.Add(album);
                    }
                }
                return albumList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DTO.Album> SearchAlbum(object searchTerm, SearchBy searchBy)
        {
            List<Album> albumList = new List<Album>();
            try
            {
                string procName = "GetAlbumById";
                string searchParam = "AlbumId";
                switch (searchBy)
                {
                    case SearchBy.Id:
                        procName = "GetAlbumById";
                        searchParam = "AlbumId";
                        break;
                    case SearchBy.Name:
                        procName = "GetAlbumByName";
                        searchParam = "AlbumByName";
                        break;
                    case SearchBy.Artist:
                        procName = "GetAlbumByArtist";
                        searchParam = "Artist";
                        break;
                    case SearchBy.Year:
                        procName = "GetAlbumByYear";
                        searchParam = "Year";
                        break;
                }        
                using (var con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(procName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(searchParam, searchTerm);
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Album album = new Album();
                        album.AlbumId = (int)dr["AlbumId"];
                        album.AlbumName = (string)dr["AlbumName"];
                        album.Artist = (string)dr["Artist"];
                        album.Price = (int)dr["Price"];
                        var yr = (DateTime)dr["Year"];
                        album.Year=yr.ToShortDateString();
                        albumList.Add(album);
                    }
                }
                return albumList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateAlbum(DTO.Album album)
        {
            try
            {
                using (var con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateAlbum", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AlbumId", album.AlbumId);
                    cmd.Parameters.AddWithValue("AlbumName", album.AlbumName);
                    cmd.Parameters.AddWithValue("Artist", album.Artist);
                    var dt = DateTime.Parse(album.Year);
                    cmd.Parameters.AddWithValue("Year", dt);
                    cmd.Parameters.AddWithValue("Price", album.Price);
                    return (int)cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAlbum(int albumId)
        {
            try
            {
                using (var con = new SqlConnection(_conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DeleteAlbum", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("AlbumId", albumId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
