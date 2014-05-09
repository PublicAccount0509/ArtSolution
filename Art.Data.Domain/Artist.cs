using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art.Data.Domain
{
    public class Artist : BaseEntity
    {
        public Artist()
        {
            Professions = new HashSet<Profession>();
            SkilledGenres = new List<Genre>();
        }

        [StringLength(30)]
        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? Deathday { get; set; }

        [StringLength(30)]
        public string School { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsPublic { get; set; }

        [StringLength(50)]
        public string AvatarFileName { get; set; }

        [StringLength(30)]
        public string Masterpiece { get; set; }

        [StringLength(30)]
        public string PrizeItems { get; set; }

        public Genders Gender { get; set; }

        public Degree? Degree { get; set; }

        public virtual ICollection<Profession> Professions { get; set; }

        public virtual ICollection<Genre> SkilledGenres { get; set; }
    }


    //public class Question:BaseEntity
    //{
    //    public Question()
    //    {
    //        Keywords = new HashSet<Keyword>();
    //    }

    //    //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    //public int QuestionId { get; set; }

    //    public string QuestionText { get; set; }
    //    public string PhotoUrl { get; set; }
    //    public int SortOrder { get; set; }
    //    public string PdfUrl { get; set; }
    //    public string VideoUrl { get; set; }

    //    public ICollection<Keyword> Keywords { get; set; }
    //}

    //public class Keyword : BaseEntity
    //{
    //    public Keyword()
    //    {
    //        Questions = new HashSet<Question>();
    //    }

    //    //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    //public int KeywordId { get; set; }

    //    public string Description { get; set; }
    //    public ICollection<Question> Questions { get; set; }



    //}
}
