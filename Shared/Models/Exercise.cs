﻿
using System.Text.Json.Serialization;

namespace Shared.Models;

public class Exercise
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    public Exercise(string title, DateTime date, int idUser)
    {
        
        Title = title;
        Date = date.Date; // Set only the date component, without the time
        UserId = idUser;
    }

    public Exercise(int id, string title, DateTime date, int userId)
    {
        Id = id;
        Title = title;
        Date = date;
        UserId = userId;
    }


    public Exercise()
    {
        
    }
    
}