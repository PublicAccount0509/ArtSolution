using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art.Data.Domain
{
    public class Profession :BaseEntity
    {
        public Profession()
        {
            Artists = new HashSet<Artist>();
        } 
        [StringLength(30)]
        public string Name { get; set; }

        public  ICollection<Artist> Artists { get; set; }
    }
}
