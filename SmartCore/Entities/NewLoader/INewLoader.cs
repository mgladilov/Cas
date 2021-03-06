﻿using System;
using System.Collections;
using System.Collections.Generic;
using EntityCore.DTO;
using EntityCore.Filter;
using SmartCore.Entities.Collections;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Deprecated;

namespace SmartCore.Entities.NewLoader
{
	public interface INewLoader
	{

		IList<int> GetSelectColumnOnly<T>(IEnumerable<Filter> filters, string selectProperty)
			where T : BaseEntity;

		TOut GetObjectById<T, TOut>(int id, bool loadChild = false) where T : BaseEntity, new() 
			where TOut : BaseEntityObject, new();

		TOut GetObject<T, TOut>(IEnumerable<Filter> filters = null, bool loadChild = false, bool getDeleted = false, bool getAll = false)
			where T : BaseEntity, new() where TOut : BaseEntityObject, new();

		TOut GetObject<T, TOut>(Filter filter, bool loadChild = false, bool getDeleted = false, bool getAll = false)
			where T : BaseEntity, new() where TOut : BaseEntityObject, new();

		IList<TOut> GetObjectList<T, TOut>(IEnumerable<Filter> filters = null, bool loadChild = false,
			bool getDeleted = false) where T : BaseEntity, new() where TOut : BaseEntityObject, new();

		IList<TOut> GetObjectList<T, TOut>(Filter filter, bool loadChild = false,
			bool getDeleted = false) where T : BaseEntity, new() where TOut : BaseEntityObject, new();

		IList GetObjectList(Type dtoType, Type blType, bool loadChild = false, bool getDeleted = false);

		IList<TOut> GetObjectListAll<T, TOut>(IEnumerable<Filter> filters = null, bool loadChild = false,
			bool getDeleted = false) where T : BaseEntity, new() where TOut : BaseEntityObject, new();

		IList<TOut> GetObjectListAll<T, TOut>(Filter filter, bool loadChild = false,
			bool getDeleted = false) where T : BaseEntity, new() where TOut : BaseEntityObject, new();

		void GetDictionaries();

		void FirstLoad();

		void SetParentsToStores();

		void SetParentsToBaseComponents();

		void SetDestinations(BaseComponent baseComponent);

		void SetDestinations(General.Accessory.Component component);

		void LoadBaseComponents(Aircraft aircraft);

		IList<DamageChart> GetDamageChartsByAircraftModel(AircraftModel aircraftModel);

		AttachedFile GetAttachedFileById(int id);

		ICommonCollection<EmployeeSubject> GetEmployeeSubject(params object[] parametres);

		ICommonCollection<JobCard> GetJobCard(params object[] parametres);
	}
}