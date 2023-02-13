namespace GarrettFarnsworthProject_FT_SE_2nd_Round;

public class ScheduleAppointment
{
    public int doctorId { get; set; }

    public int personId { get; set; }

    public string? appointmentTime { get; set; }

    public bool isNewPatientAppointment { get; set; }

    public int requestId { get; set; }

    public ScheduleAppointment()
    {
    }

    public ScheduleAppointment(int doctorId, int personId, string appointmentTime, bool isNewPatientAppointment, int requestId)
    {
        this.doctorId = requestId;
        this.personId = personId;
        this.appointmentTime = appointmentTime;
        this.isNewPatientAppointment = isNewPatientAppointment;
        this.requestId = requestId;
    }
}
