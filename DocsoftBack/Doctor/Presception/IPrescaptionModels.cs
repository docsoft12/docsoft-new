using DocsoftBack.Doctor.Examination;

namespace DocsoftBack.Doctor.Presception
{
	public interface IPrescaptionModels:IExaminationModels
	{
		string Appointment_ID { get; set; }
		string CheckupID { get; set; }
		string Dosage { get; set; }
		string Duration { get; set; }
		string Instruction { get; set; }
		string Medicine { get; set; }
		string Quantity { get; set; }
		string Sr_No { get; set; }
		string UHID { get; set; }
	}
}