﻿using System.ComponentModel.DataAnnotations.Schema;
using EFCore.DTO.Dictionaries;

namespace EFCore.DTO.Maps
{
	public class NonRoutineJobMap : BaseMap<NonRoutineJobDTO>
	{
		public NonRoutineJobMap() : base()
		{
			ToTable("Dictionaries.NonRoutineJobs");

			Property(i => i.ATAChapterId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("ATAChapterId");

			Property(i => i.Title)
				.HasMaxLength(50)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("Title");

			Property(i => i.Description)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("Description");

			Property(i => i.ManHours)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("ManHours");

			Property(i => i.Cost)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("Cost");

			Property(i => i.KitRequired)
				.HasMaxLength(50)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("KitRequired");

			
			#region MyRegion

			HasRequired(i => i.ATAChapter)
				.WithMany(i => i.NonRoutineJobDtos)
				.HasForeignKey(i => i.ATAChapterId);

			#endregion
		}
	}
}