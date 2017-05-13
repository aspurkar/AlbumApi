using DataTier;
using DataTier.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlbumApi.Controllers
{
    public class AlbumsController : ApiController
    {
        private IAlbumRepository _repo;
        public AlbumsController()
        {
            _repo = new AlbumRepository();
        }
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var album = _repo.SearchAlbum(id, SearchBy.Id).FirstOrDefault();
                if (album != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, album);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex);
            }
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public List<Album> Search(string by, string value)
        {
            SearchBy searchBy = (SearchBy)Enum.Parse(typeof(SearchBy),by,true);
            return _repo.SearchAlbum(value, searchBy).ToList();
        }
        public HttpResponseMessage PostAlbums(Album album)
        {
           int ambumId=  _repo.AddAlbum(album);
           HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, album);
           response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = ambumId }));
           return response;
        }
    }
}
