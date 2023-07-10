using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Domain
{
    public class BaseEntity<TPrimaryKey>
    {
        public BaseEntity()
        {
            IsDeleted = false;
            CreatedOn = DateTime.UtcNow;
        }
        public TPrimaryKey Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
    }
}
