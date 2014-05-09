using Art.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebExpress.Core.TypeExtensions;

namespace Art.Website.Models
{
    public class ArtistEditModel
    {
        public ArtistModel Artist { get; set; }

        public ProfessionModel[] SourceProfessions { get; set; }

        public GenreModel[] SourceGenres { get; set; }

        public ValueText[] Degrees { get; set; }
    }

}