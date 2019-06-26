using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using EntityCore.Attributte;
using EntityCore.DTO.General;

namespace EntityCore.DTO.Dictionaries
{
	[Table("Nomenclatures", Schema = "Dictionaries")]
	
	[Condition("IsDeleted", 0)]
	public class NomenclatureDTO : BaseEntity
	{
		
		[Column("DepartmentId")]
		public int? DepartmentId { get; set; }

	    
	    [Column("Name"), MaxLength(50)]
		public string Name { get; set; }

	    
	    [Column("FullName"), MaxLength(256), Required]
		public string FullName { get; set; }

	    
		[Child]
	    public DepartmentDTO Department { get; set; }

		#region Navigation Property

	    
		public ICollection<DocumentDTO> DocumentDtos { get; set; }

	    #endregion
	}
}
