using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Entities
{
    public class Operation: BaseEntity
    {
        public int ReaderId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }

        public bool EmailStatus  { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
