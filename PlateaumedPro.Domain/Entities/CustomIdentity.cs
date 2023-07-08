using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Domain.Entities
{
    public class CustomIdentity
    {
        public long Id { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Digits { get; set; }
        public string Separator { get; set; }
        public string Activity { get; set; }
        public long LastDigit { get; set; }
    }
}
