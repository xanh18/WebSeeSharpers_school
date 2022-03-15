using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSeeSharpers.Models;

public class Theatre
{
  

    public int Id { get; set; }

    public int Number { get; set; }

    public int AmountOfRows { get; set; }

    public int AmountOfSeats { get; set; }

    public ICollection<Viewing> Viewings { get; set; }

}