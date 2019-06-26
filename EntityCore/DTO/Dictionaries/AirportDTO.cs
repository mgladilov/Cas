using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using EntityCore.Attributte;

namespace EntityCore.DTO.Dictionaries
{
	[Table("Airports", Schema = "Dictionaries")]
	
	[Condition("IsDeleted", 0)]
	public class AirportDTO : BaseEntity
	{
		
		[Column("ShortName"), MaxLength(256)]
		public string ShortName { get; set; }

		
		[Column("FullName"), MaxLength(256)]
		public string FullName { get; set; }

		
		[Column("Locality")]
		public int? LocalityId { get; set; }

		
		[Column("Altitude")]
		public int? Altitude { get; set; }

		
		[Column("TimeBeginning")]
		public int? TimeBeginning { get; set; }

		
		[Column("TimeEnd")]
		public int? TimeEnd { get; set; }
	}
	
}
