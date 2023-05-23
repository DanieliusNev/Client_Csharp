using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Shared.Models;
using Shared.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWasm.Services;

namespace ClientSideBlazor.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExerciseService _exerciseService;

        public List<CalendarEvent> Events { get; set; }
        public Dictionary<DateTime, List<CalendarEvent>> EventsByDay { get; set; }
        public Dictionary<DateTime, List<string>> ToDoBoxes { get; set; }
        public DateTime WeekStart { get; set; }
        public List<Exercise> UserExercises { get; set; }

        public CalendarModel(IHttpContextAccessor httpContextAccessor, IExerciseService exerciseService)
        {
            _httpContextAccessor = httpContextAccessor;
            _exerciseService = exerciseService;
            ToDoBoxes = GetToDoBoxesFromSession();
        }

        public async Task OnGet()
        {
            WeekStart = DateTime.Now.Date;
            EventsByDay = new Dictionary<DateTime, List<CalendarEvent>>();

            UserState userState = _exerciseService.GetUserState();
            int userId = userState.Id; // Get the logged-in user's ID
            List<Exercise> userExercises = await _exerciseService.GetUserExercisesAsync(userId);
    
            foreach (var exercise in userExercises)
            {
                Console.WriteLine($"Exercise: Title={exercise.Title}, Date={exercise.Date}, UserId={exercise.UserId}");
            }
            foreach (var exercise in userExercises)
            {
                DateTime exerciseDate = DateTime.Parse(exercise.Date.ToString()); // Convert DateOnly to DateTime

                if (!EventsByDay.ContainsKey(exerciseDate))
                {
                    EventsByDay[exerciseDate] = new List<CalendarEvent>();
                }

                EventsByDay[exerciseDate].Add(new CalendarEvent
                {
                    Title = exercise.Title,
                    StartDate = exerciseDate
                });
            }
        }




        public IActionResult OnPostAddToDo(string date, string newToDo)
        {
            var selectedDate = DateTime.Parse(date);

            if (!ToDoBoxes.ContainsKey(selectedDate))
            {
                ToDoBoxes[selectedDate] = new List<string>();
            }

            ToDoBoxes[selectedDate].Add(newToDo);

            SaveToDoBoxesToSession(ToDoBoxes);
            
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
        public IExerciseService ReturningExerciseService()
        {
            return _exerciseService;
        }
    }
}
