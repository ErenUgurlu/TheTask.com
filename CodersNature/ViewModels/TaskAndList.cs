using CodersNature.Models;

namespace CodersNature.ViewModels
{
    public class TaskAndList
    {
        public MyTask MyTask { get; set; }
        public List<MyTask> List { get; set; }
        public Reminder Reminder { get; set; }
    }
}
