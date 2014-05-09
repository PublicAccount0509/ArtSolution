using Art.Data.Database;
using Art.Data.Domain;
using Art.Data.Domain.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebExpress.Core;
using WebExpress.Core.Guards;

namespace Art.BussinessLogic
{
    public class ArtistBussinessLogic
    {
        public static readonly ArtistBussinessLogic Instance = new ArtistBussinessLogic();

        private readonly IRepository<Artist> _artistRepository;
        private readonly IRepository<Profession> _professionRepository;
        private readonly IRepository<Genre> _genreRepository;

        private ArtistBussinessLogic()
        {
            _artistRepository = new EfRepository<Artist>();
            _professionRepository = new EfRepository<Profession>();
            _genreRepository = new EfRepository<Genre>();
        }

        public Artist GetArtist(int artistId)
        {
            var artist = _artistRepository.GetById(artistId);
            return artist;
        }

        public PagedList<Artist> SearchArtists(string namePart, int? professionId, PagingRequest paging)
        {
            Guard.IsNotNull<ArgumentNullException>(paging, "paging");

            var query = _artistRepository.Table;
            if (!string.IsNullOrEmpty(namePart))
            {
                query = query.Where(i => i.Name.Contains(namePart));
            }

            if (professionId.HasValue)
            {
                query.Where(i => i.Professions.Any(p => p.Id == professionId.Value));
            }

            query = query.OrderBy(i => i.Id);

            var result = new PagedList<Artist>(query, paging.PageIndex, paging.PageSize);

            return result;
        }

        public Profession[] GetArtistProfessions(int artistId)
        {
            var profs = from p in _professionRepository.Table
                        where p.Id == 1 || p.Id == 2
                        select p;
            return profs.ToArray();
        }

        public Profession[] GetProfessions()
        {
            var professions = _professionRepository.Table.ToArray();
            return professions;
        }

        public Genre[] GetGenres()
        {
            var genres = _genreRepository.Table.ToArray();
            return genres;
        }

        public void Add(Artist artist)
        {
            _artistRepository.Insert(artist);
        }

        public void Update(Artist artist)
        {
            _artistRepository.Update(artist);
        }

        public void Delete(Artist artist)
        {
            _artistRepository.Delete(artist);
        }

        public bool CanUnpublish(Artist artist)
        {
            List<string> reasons;
            return CanUnpublish(artist, out reasons);
        }

        public bool CanUnpublish(Artist artist, out List<string> reasons)
        {
            reasons = new List<string>();
            if (artist.Name == "齐白石")
            {
                reasons.Add("齐白石老人不能取消发布");

                return false;
            }
            return true;
        }

        public ICollection<Profession> GetProfessions(List<int> ids)
        {
            if (ids == null)
            {
                return null;
            }

            var query = from p in _professionRepository.Table
                        where ids.Contains(p.Id)
                        select p;
            return query.ToList();
        }

        public ICollection<Genre> GetSkilledGenres(List<int> ids)
        {
            if (ids == null)
            {
                return null;
            }
            var query = from p in _genreRepository.Table
                        where ids.Contains(p.Id)
                        select p;
            return query.ToList();
        }

    }
}
