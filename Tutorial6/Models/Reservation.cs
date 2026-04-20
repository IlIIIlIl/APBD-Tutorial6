using System.Runtime.InteropServices.JavaScript;

namespace Tutorial6.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; } =  string.Empty;
    public string Topic { get; set; } =  string.Empty;
    //These 4 types probably need to be changed
    public int Date { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public string Status { get; set; } =  string.Empty;
}