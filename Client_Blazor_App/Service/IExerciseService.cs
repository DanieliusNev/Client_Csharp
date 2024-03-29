﻿using Shared.Models;

namespace Client_Blazor_App.Service;

public interface IExerciseService
{
    public Task RegisterExerciseAsync(string title, DateTime date, string weights, string amount, int categoryId,
        int userId);
    public Task<List<Exercise>> GetUserExercisesByDateAsync(int userId, DateTime startDate, DateTime endDate);

    public Task UpdateExerciseAsync(int id, string title, DateTime date, string weights, string amount, int categoryId,
        int userId);
    public Task DeleteExerciseAsync(int exerciseId);
}