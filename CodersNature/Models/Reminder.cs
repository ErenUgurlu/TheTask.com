using System.ComponentModel.DataAnnotations.Schema;

namespace CodersNature.Models
{
    [NotMapped]
    public class Reminder
    {
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public CodersNatureUser? User { get; set; }
        public string Text { get; set; }
        public string? ImgPath { get; set; }
    }
}
