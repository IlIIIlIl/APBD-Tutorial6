namespace Tutorial6.DTOs;

public class CreateReservationDto
{
    public int RoomId { get; set; }
    public string OrganizerName { get; set; } =  string.Empty;
    public string Topic { get; set; } =  string.Empty;
    public string Date { get; set; } =  string.Empty;
    public string StartTime { get; set; } =  string.Empty;
    public string EndTime { get; set; } =  string.Empty;
    public string Status { get; set; } =  string.Empty;
}