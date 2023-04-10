using DocsoftBack.Doctor.Presception;

namespace DocsoftBack.Doctor
{
	public interface IDoctorCheckupModels:IPrescaptionModels
	{
		string Appointment_ID { get; set; }
		string BP { get; set; }
		string CheckupID { get; set; }
		string Chief_Complaint { get; set; }
		string Consultant { get; set; }
		DateTime Date_ { get; set; }
		string Fever { get; set; }
		string DocAdd { get; set; }
		DateTime NextFollowUp { get; set; }
		string Note { get; set; }
		string PA { get; set; }
		string Pulse { get; set; }
		string Spo2 { get; set; }
		string UHID { get; set; }
	}
}