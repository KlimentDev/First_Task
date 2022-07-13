using System.ComponentModel.DataAnnotations;

namespace First_Task.Models
{
    public class FileLine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Number is required")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Label is required")]
        public string Label { get; set; }

        public int TextFileId { get; set; }
    }
}
