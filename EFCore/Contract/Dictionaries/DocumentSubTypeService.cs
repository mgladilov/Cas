﻿using EFCore.DTO;
using EFCore.DTO.Dictionaries;
using EFCore.Repository;

namespace EFCore.Contract.Dictionaries
{
	public class DocumentSubTypeService : Repository<DocumentSubTypeDTO>, IDocumentSubTypeService
	{
		public DocumentSubTypeService()
		{
			var connection = Helper.Helper.GetConnectionString();
			_context = new DataContext(connection);
			_dbset = _context.Set<DocumentSubTypeDTO>();
		}
	}
}