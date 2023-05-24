namespace DocsoftBack.Registration
{
	public interface IRegisterModels
	{
		string Aadhar_No { get; set; }
		string Address { get; set; }
		DateTime? Birthdate { get; set; }
		string City { get; set; }
		string Height { get; set; }
		int? ID { get; set; }
		string Mobile_No { get; set; }
		string Occupation { get; set; }
		string P_Image { get; set; }
		string Parent_Mobile { get; set; }
		string Parent_Name { get; set; }
		string Parent_Relation { get; set; }
		string Patient_Name { get; set; }
		string Pincode { get; set; }
		DateTime? Reg_By { get; set; }
		DateTime Reg_Date { get; set; }
		string Sex { get; set; }
		string UHID { get; set; }
		DateTime Updated { get; set; }
		decimal? Weight { get; set; }
	}
}