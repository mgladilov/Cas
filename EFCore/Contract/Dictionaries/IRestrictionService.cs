﻿using System.ServiceModel;
using EFCore.DTO.Dictionaries;
using EFCore.Repository;

namespace EFCore.Contract.Dictionaries
{
	[ServiceContract]
	public interface IRestrictionService : IRepository<RestrictionDTO>
	{
		
	}
}