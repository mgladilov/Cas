﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EFCore.DTO.General.Maps
{
	public class RequestForQuotationMap : EntityTypeConfiguration<RequestForQuotationDTO>
	{
		public RequestForQuotationMap()
		{
			ToTable("dbo.RequestsForQuotation");

			HasKey(i => i.ItemId);
			Property(i => i.ItemId)
				.HasColumnName("ItemId");

			Property(i => i.IsDeleted)
				.IsRequired()
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("IsDeleted");

			Property(i => i.Title)
				.HasMaxLength(256)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("Title");

			Property(i => i.Description)
				.HasMaxLength(256)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("Description");

			Property(i => i.ParentId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("ParentId");

			Property(i => i.Status)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("Status");

			Property(i => i.OpeningDate)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("OpeningDate");

			Property(i => i.PublishingDate)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("PublishingDate");

			Property(i => i.ClosingDate)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("ClosingDate");

			Property(i => i.Author)
				.HasMaxLength(256)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("Author");

			Property(i => i.Remarks)
				.HasMaxLength(256)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("Remarks");

		Property(i => i.ParentTypeId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("ParentTypeId");

			Property(i => i.ToSupplierId)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("ToSupplier");

			Property(i => i.PublishedById)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("PublishedById");

			Property(i => i.ClosedById)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("ClosedById");

			Property(i => i.PublishedByUser)
				.HasMaxLength(128)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("PublishedByUser ");

			Property(i => i.CloseByUser)
				.HasMaxLength(128)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
				.HasColumnName("CloseByUser");


			HasMany(i => i.Files).WithRequired(i => i.RequestForQuotation).HasForeignKey(i => i.ParentId);
			HasMany(i => i.PackageRecords).WithRequired(i => i.RequestForQuotationDto).HasForeignKey(i => i.ParentPackageId);

		}
	}
}