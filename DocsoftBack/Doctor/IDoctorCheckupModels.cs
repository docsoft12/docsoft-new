using DocsoftBack.Doctor.Presception;
namespace DocsoftBack.Doctor
{
	public interface IDoctorCheckupModels : IPrescaptionModels
	{
		string Ap_ID { get; set; }
		string Appointment_ID { get; set; }
		string BP { get; set; }
		string CheckupID { get; set; }
		string Chief_Complaint { get; set; }
		string Consultant { get; set; }
		DateTime Date_ { get; set; }
		string DocAdd { get; set; }
		string Dosage { get; set; }
		string Duration { get; set; }
		string EatTime { get; set; }
		string Examination { get; set; }
		string Fever { get; set; }
		string Instruction { get; set; }
		string Medicine { get; set; }
		DateTime NextFollowUp { get; set; }
		string Note { get; set; }
		string PA { get; set; }
		string Pulse { get; set; }
		string QtyPer { get; set; }
		string Quantity { get; set; }
		string Spo2 { get; set; }
		string Sr_No { get; set; }
		string TimeTab { get; set; }
		string UHID { get; set; }
	}
}