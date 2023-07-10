using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Contracts
{
 
    public class BaseDto<TPrimaryKey>
    { 
        public TPrimaryKey Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
