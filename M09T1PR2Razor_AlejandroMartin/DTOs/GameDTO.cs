using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace M09T1PR2Razor_AlejandroMartin.DTOs
{
    public class GameDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public string? Dev { get; set; }
        public string? Img { get; set; }
    }
}
