using System;
namespace JewelleryStore.Models
{
	/// <summary>
	/// This Users class is used to deserialize an XML file and display the resulting data in a list.
	/// </summary>
	/// <remarks>
	/// The code used to load the XML into this class is shown here:
	/// <code><![CDATA[
	/// List<Users> user;
	/// 	using (var reader = new System.IO.StreamReader (stream)) {
	/// 	var serializer = new XmlSerializer(typeof(List<Users>));
	/// 	users = (List<Users>)serializer.Deserialize(reader);
	/// }
	/// ]]></code>
	/// </remarks>
	public class Users
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string UserType { get; set; }
	}
}
