using DocsoftBack.Registration;

namespace DocsoftBack.Doctor.Examination
{
	public interface IExaminationModels : IRegisterModels
	{
		string Ap_ID { get; set; }
		DateTime Date_ { get; set; }
		string Examination { get; set; }
	}
}