using BlazorWasm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSideBlazor.Pages;

public class ExerciseModel : PageModel
{
    private readonly IExerciseService _exerciseService;
    [BindProperty]
    public string ErrorMessage2 { get; set; }

    [BindProperty]
    public bool IsSuccess { get; set; }

    public ExerciseModel(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    public async Task<IActionResult> OnPostAsync(string title, DateTime date)
    {
            
        try
        {

                var isSuccess = await _exerciseService.RegisterExerciseAsync(title, date);
                if (isSuccess)
                {
                    IsSuccess = true;
                }
                else
                {
                    ErrorMessage2 = "Failed to register exercise.";
                }
            
                    
        }
        catch (Exception ex)
        {
            ErrorMessage2 = "An error occurred: " + ex.Message;
        }
                
        return Page();
            
    }
}