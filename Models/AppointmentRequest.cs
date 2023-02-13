namespace GarrettFarnsworthProject_FT_SE_2nd_Round;

public class AppointmentRequest
{
    public int requestId { get; set; }

    public int personId { get; set; }

    public List<string>? preferredDays { get; set; }

    public List<int>? preferredDocs { get; set; }

    public bool isNew { get; set; }

    public AppointmentRequest()
    {
    }

    public AppointmentRequest(int requestId, int personId, List<string> preferredDays, List<int> preferredDocs, bool isNew)
    {
        this.requestId = requestId;
        this.personId = personId;
        this.preferredDays = preferredDays;
        this.preferredDocs = preferredDocs;
        this.isNew = isNew;
    }
}
