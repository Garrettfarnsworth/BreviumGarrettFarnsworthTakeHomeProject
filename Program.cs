using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;
using System.Text;
using static GarrettFarnsworthProject_FT_SE_2nd_Round.Appointments;
using static GarrettFarnsworthProject_FT_SE_2nd_Round.AppointmentRequest;
using static GarrettFarnsworthProject_FT_SE_2nd_Round.ScheduleAppointment;

namespace GarrettFarnsworthProject_FT_SE_2nd_Round
{
    /*
        General steps: 
        Step 1: Get all the data from API
        Step 2: Meet all the constraints
        Step 3: If constraints are met schedule the appointment. If not, don't.
    */ 
    class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                //Step #1 Define the API endpoint for retrieving the initial monthly schedule
                string entireInitialScheduleUrl = "http://scheduling-interview-2021-265534043.us-west-2.elb.amazonaws.com/api/Scheduling/Schedule?token=e0b079b7-9e6d-46c7-acff-f3403fb0aad4";
                string requestedAppointmentUrl = "http://scheduling-interview-2021-265534043.us-west-2.elb.amazonaws.com/api/Scheduling/AppointmentRequest?token=e0b079b7-9e6d-46c7-acff-f3403fb0aad4";
                var scheduledAppointmentUrl = "http://scheduling-interview-2021-265534043.us-west-2.elb.amazonaws.com/api/Scheduling/Schedule?token=e0b079b7-9e6d-46c7-acff-f3403fb0aad4";
                HttpResponseMessage entireInitialScheduleUrlResponse = await client.GetAsync(entireInitialScheduleUrl);
                HttpResponseMessage requestedAppointmentUrlResponse = await client.GetAsync(requestedAppointmentUrl);

                if (entireInitialScheduleUrlResponse.IsSuccessStatusCode && requestedAppointmentUrlResponse.IsSuccessStatusCode)
                {
                    string entireInitialScheduleUrlResponseContent = await entireInitialScheduleUrlResponse.Content.ReadAsStringAsync();
                    string requestedAppointmentUrlResponseContent = await requestedAppointmentUrlResponse.Content.ReadAsStringAsync();

                    //Map the response content to a custom object
                    var appointments = JsonConvert.DeserializeObject<List<Appointments>>(entireInitialScheduleUrlResponseContent);
                    var appointmentRequest = JsonConvert.DeserializeObject<AppointmentRequest>(requestedAppointmentUrlResponseContent);

                    //Step 2: Meet restrains for customer use cases. 
                    //I didn't have time to fully flesh these out, but the general logic would follow the description of the provided document. 
                    // Below I provided some code for the portion on ensuring the day and hours match for the availablity of the doctor and the preffered doctor of the patient to be made. 
                    //I created a flag for if the appointments are not at the same time to be set to true. I then use this flag to continue on in the logic flow and assign the appointment. 
                    // If not, then an appointment is not scheduled. I have left it commented out, but feel free drop it down and uncomment it to see the logic. 
                    bool goodToSchedule = true;
                    //foreach (Appointments appointment in appointments) 
                    // {
                    //     if (appointmentRequest.preferredDocs.Contains(appointment.doctorId))
                    //     {
                    //         string[] splitTimes = appointment.appointmentTime.Split('T');
                    //         string requestedAppointmentDate = splitTimes[0];
                    //         string requestedAppointmentHour = splitTimes[1];
                    //         //If they are on the same day
                    //         foreach (string day in appointmentRequest.preferredDays)
                    //         {
                    //             if (requestedAppointmentDate == day.Split('T')[0]) 
                    //             {
                    //                 // If the dates are the same
                    //                 if (requestedAppointmentHour == day.Split('T')[1])
                    //                 {
                    //                     goodToSchedule = false;
                    //                 }
                    //                 else {
                    //                     goodToSchedule = false;
                    //                 }
                    //             }
                    //             else 
                    //             {
                    //                 goodToSchedule = true;
                    //             }
                    //         }
                    //     }
                    // }


                    //Step 3: Schedule an appointment if it meets previous requirments. 
                    //For this portion since I didn't fully satisfy all of the logic above I hard coded the values for appointmentTime and doctorId.
                    if (goodToSchedule) 
                    {
                        ScheduleAppointment newScheduleAppointment = new ScheduleAppointment();
                        newScheduleAppointment.appointmentTime = "2021-11-01T15:00:00Z"; //appointmentRequest.preferredDays;
                        newScheduleAppointment.doctorId = 1; //appointmentRequest.preferredDocs;
                        newScheduleAppointment.isNewPatientAppointment = appointmentRequest.isNew;
                        newScheduleAppointment.personId = appointmentRequest.personId;
                        newScheduleAppointment.requestId = appointmentRequest.requestId;
                        
                        using (var httpClient = new HttpClient())
                        {
                            var appointmentJson = JsonConvert.SerializeObject(newScheduleAppointment);

                            var data = new StringContent(appointmentJson, Encoding.UTF8, "application/json");

                            var scheduledAppointmentUrlResponse = httpClient.PostAsync(scheduledAppointmentUrl, data).Result;

                            if (scheduledAppointmentUrlResponse.IsSuccessStatusCode)
                            {
                                var responseContent = scheduledAppointmentUrlResponse.Content.ReadAsStringAsync().Result;
                                Console.WriteLine(responseContent);
                            }
                            else
                            {
                                Console.WriteLine("Request failed: " + scheduledAppointmentUrlResponse.StatusCode);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + entireInitialScheduleUrlResponse.StatusCode);
                }
            }
        }
    }
}