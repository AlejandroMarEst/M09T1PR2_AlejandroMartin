using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace M09T1PR2API_AlejandroMartin.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public string? Surname { get; set; }
    }
}
