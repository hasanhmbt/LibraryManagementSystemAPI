using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Entities
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }

    }
}
