namespace GarrettFarnsworthProject_FT_SE_2nd_Round;

public class Appointments
{
    public int doctorId { get; set; }

    public int personId { get; set; }

    public string? appointmentTime { get; set; }

    public bool isNewPatientAppointment { get; set; }

    public Appointments()
    {
    }

    public Appointments(int doctorId, int personId, string appointmentTime, bool isNewPatientAppointment)
    {
        this.doctorId = doctorId;
        this.personId = personId;
        this.appointmentTime = appointmentTime;
        this.isNewPatientAppointment = isNewPatientAppointment;
    }
}
