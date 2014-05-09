using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art.Data.Domain
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Contact { get; set; }
    }
}
