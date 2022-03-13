using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSeeSharpers.Models;


public class Movie
{
    public int Id { get; set; }
    public String Title { get; set; }
    public TimeSpan Duration { get; set; }
    public Boolean Movie3d { get; set; }
    [Display(Name = "Starttijd")]
    public DateTime BeginTime { get; set; }
    public Int16 AgeRequirement { get; set; }
    public String Thumbnail { get; set; }
    public List<Language> Language { get; set; }
    public String Description { get; set; }
    public String Genre { get; set; }
    public String Room { get; set; }    
}