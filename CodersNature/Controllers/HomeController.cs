using CodersNature.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.HttpOverrides;
using IpData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CodersNature.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using CodersNature.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CodersNature.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<CodersNatureUser> _userManager;
        private readonly CodersNatureContext _context;
        public HomeController(ILogger<HomeController> logger, UserManager<CodersNatureUser> userManager, CodersNatureContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }
        public Reminder GetReminder() { 
            Reminder reminder = new();

            string[] TextList =
            {
                "It is a lovely day today for penguins but you might get cold, don't forget to put your scarf on.",
                "I  guess clouds just watched titanic again they look really sad, don't forget your umbrella.",
                "Sun is really generous today you should enjoy the sunlight",
                "It is smelling snow outside , if we are lucky enough we can even play snowball",
                "The sun is rising, birds are singing, bell is ringing for the day. Don't forget to eat your breakfast",
                "It's late, you look very sleepy, go to bed, you'll continue tomorrow.",
                "Don't forget to check your tasks"
            };
            if (Int32.Parse(DateTime.Now.ToString("HH")) <= 5)
            {
                reminder.Text = TextList[5];
                
            }
            else if (Int32.Parse(DateTime.Now.ToString("HH")) > 5 && Int32.Parse(DateTime.Now.ToString("HH")) <= 10)
            {
                reminder.Text = TextList[4];
            }
            else if (ViewBag.weatherImg  == "snow" || ViewBag.weatherImg == "shower rain")
            {
                reminder.Text = TextList[3];

            }
            else if (ViewBag.weatherImg == "rain" || ViewBag.weatherImg == "thunderstorm")
            {
                reminder.Text = TextList[1]; 
            }
            else if (ViewBag.weather > 20)
            {
                reminder.Text = TextList[2];
            }
            else if (ViewBag.weather < 10)
            {
                reminder.Text = TextList[0];
            }
            else
            {
                reminder.Text = TextList[6];
            }

            return reminder;
        }
        public async Task<List<MyTask>> GetTaskListAsync()
        {
            List<MyTask> taskList = new();
            CodersNatureUser user = await _userManager.GetUserAsync(User);
            if(user != null)
                taskList = _context.Tasks.Where(x=>x.UserId == user.Id).OrderBy(task => task.Priority).ToList();

            return taskList;
        }
        public async Task<IActionResult> Index()
        {
            TaskAndList taskAndList = new()
            {
                List = await GetTaskListAsync()
            };

            CodersNatureUser user = await _userManager.GetUserAsync(User);

            string city = "istanbul";
            if (user != null)
                city = user.Hometown;
            string weatherConnection = $"https://api.openweathermap.org/data/2.5/weather?q={city}&mode=xml&units=metric&appid=b74293d1b68ac659b7f08bdc1897881b";
            try
            {
                XDocument document = XDocument.Load(weatherConnection);

                ViewBag.weather = Math.Round(Convert.ToDouble(document.Descendants("temperature").ElementAt(0).Attribute("value").Value));

                if (document.Descendants("precipitation").ElementAt(0).Attribute("mode").Value == "no")
                {
                    ViewBag.weatherImg = document.Descendants("weather").ElementAt(0).Attribute("value").Value;
                }
                else
                {
                    ViewBag.weatherImg = document.Descendants("precipitation").ElementAt(0).Attribute("mode").Value;
                }

                taskAndList.Reminder = GetReminder();
            }
            catch (Exception)
            {
                ViewBag.weatherImg = "";
                return View(taskAndList);
            }

            if (Int32.Parse(DateTime.Now.ToString("HH")) > 20 || Int32.Parse(DateTime.Now.ToString("HH")) < 6)
                ViewBag.DayOrNight = "night";
            else
                ViewBag.DayOrNight = "day";

            return View(taskAndList);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Index(TaskAndList taskAndList)
        {
            CodersNatureUser user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Index");
            if (taskAndList.MyTask.StartDate == DateTime.MinValue)
            {
                taskAndList.MyTask.StartDate = DateTime.Now.Date;
            }
            taskAndList.MyTask.UserId = user.Id; 
            _context.Add(taskAndList.MyTask);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}