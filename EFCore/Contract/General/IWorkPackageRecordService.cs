﻿using System.ServiceModel;
using EFCore.DTO.General;
using EFCore.Repository;

namespace EFCore.Contract.General
{
	[ServiceContract]
	public interface IWorkPackageRecordService : IRepository<WorkPackageRecordDTO>
	{
		
	}
}