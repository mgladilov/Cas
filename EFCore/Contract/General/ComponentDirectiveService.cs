﻿using EFCore.DTO;
using EFCore.DTO.General;
using EFCore.Repository;

namespace EFCore.Contract.General
{
	public class ComponentDirectiveService : Repository<ComponentDirectiveDTO>, IComponentDirectiveService
	{
		public ComponentDirectiveService()
		{
			var connection = Helper.Helper.GetConnectionString();
			_context = new DataContext(connection);
			_dbset = _context.Set<ComponentDirectiveDTO>();
		}
	}
}