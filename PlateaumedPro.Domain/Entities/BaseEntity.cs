using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Domain.Entities
{
    public class BaseEntity<TPrimaryKey>
    {
        public BaseEntity()
        {
            IsDeleted = false;
            CreatedOn = DateTimeOffset.Now;
        }
        public TPrimaryKey Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
        public bool IsDeleted { get; set; }
    }
}
