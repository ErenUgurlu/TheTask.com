using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodersNature.Models
{
    public class MyTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime StartDate { get; set; }
        public int Priority { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public CodersNatureUser User { get; set; }
    }
}
