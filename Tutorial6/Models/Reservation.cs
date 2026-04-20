using System.Runtime.InteropServices.JavaScript;

namespace Tutorial6.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; } =  string.Empty;
    public string Topic { get; set; } =  string.Empty;
    public string Date { get; set; } =  string.Empty;
    public string StartTime { get; set; } =  string.Empty;
    public string EndTime { get; set; } =  string.Empty;
    public string Status { get; set; } = string.Empty;
}