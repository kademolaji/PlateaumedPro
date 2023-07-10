
using System.ComponentModel.DataAnnotations;


namespace PlateaumedPro.Domain
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        [StringLength(255)]
        public string ApiKey { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}