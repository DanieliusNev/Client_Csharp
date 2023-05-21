using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Calendar;
using System;
using System.Collections.Generic;

namespace ClientSideBlazor.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<CalendarEvent> Events { get; set; }
        public Dictionary<DateTime, List<CalendarEvent>> EventsByDay { get; set; }
        public Dictionary<DateTime, List<string>> ToDoBoxes { get; set; }
        public DateTime WeekStart { get; set; }

        public CalendarModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            ToDoBoxes = GetToDoBoxesFromSession();
        }

        public void OnGet()
        {
            // Sample data
            Events = new List<CalendarEvent>
            {
                new CalendarEvent { Title = "Event 1", Description = "Event 1 Description", StartDate = DateTime.Now.Date },
                new CalendarEvent { Title = "Event 2", Description = "Event 2 Description", StartDate = DateTime.Now.Date.AddDays(1) },
                new CalendarEvent { Title = "Event 3", Description = "Event 3 Description", StartDate = DateTime.Now.Date.AddDays(2) }
            };

            WeekStart = DateTime.Now.Date;

            EventsByDay = new Dictionary<DateTime, List<CalendarEvent>>();

            if (Events != null)
            {
                foreach (var evt in Events)
                {
                    var startDate = evt.StartDate.Date;

                    if (!EventsByDay.ContainsKey(startDate))
                    {
                        EventsByDay[startDate] = new List<CalendarEvent>();
                    }

                    EventsByDay[startDate].Add(evt);
                }
            }
        }

        public IActionResult OnPost(string date, string newToDo)
        {
            var selectedDate = DateTime.Parse(date);

            if (!ToDoBoxes.ContainsKey(selectedDate))
            {
                ToDoBoxes[selectedDate] = new List<string>();
            }

            ToDoBoxes[selectedDate].Add(newToDo);

            SaveToDoBoxesToSession(ToDoBoxes);

            // Redirect to the same page after adding the to-do
            return RedirectToPage("/Calendar");
        }

        private Dictionary<DateTime, List<string>> GetToDoBoxesFromSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var toDoBoxesJson = session.GetString("ToDoBoxes");

            if (!string.IsNullOrEmpty(toDoBoxesJson))
            {
                return System.Text.Json.JsonSerializer.Deserialize<Dictionary<DateTime, List<string>>>(toDoBoxesJson);
            }

            return new Dictionary<DateTime, List<string>>();
        }

        private void SaveToDoBoxesToSession(Dictionary<DateTime, List<string>> toDoBoxes)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var toDoBoxesJson = System.Text.Json.JsonSerializer.Serialize(toDoBoxes);
            session.SetString("ToDoBoxes", toDoBoxesJson);
        }
    }
}
