namespace WebSeeSharpers.Models;

public class ViewingSeat
{
    public int Id { get; set; }
    public Viewing Viewing { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}