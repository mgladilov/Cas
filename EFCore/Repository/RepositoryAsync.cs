﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFCore.Attributte;
using EFCore.DTO;
using EFCore.DTO.General;
using EFCore.Interfaces;
using Z.EntityFramework.Plus;

namespace EFCore.Repository
{
	public class RepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
	{
		protected DbContext _context;
		protected DbSet<T> _dbset;

		public RepositoryAsync(DataContext context)
		{
			_context = context;
			_dbset = context.Set<T>();
		}

		public RepositoryAsync()
		{
			
		}

		public async Task<IList<TType>> GetSelectColumnOnlyAsync<TType>(Expression<Func<T, bool>> @where, Expression<Func<T, TType>> @select, bool readOnly = true) where TType : new()
		{
			using (_context)
			{
				IQueryable<T> query = _dbset;

				var conditionAttributes = typeof(T).GetCustomAttributes(typeof(ConditionAttribute), false).Cast<ConditionAttribute>().ToList();
				foreach (var attribute in conditionAttributes)
				{
					var func = LambdaConstructor<T>(attribute.ColumnName, attribute.Condition, FilterType.Equal);
					query = query.Where(func);
				}
				return await query.Where(where).AsNoTracking().Select(select).ToListAsync();
			}
		}

		public async IList<int> GetSelectColumnOnlyAsync(IEnumerable<Filter.Filter> filters, string selectProperty)
		{
			using (_context)
			{
				IQueryable<T> query = _dbset;

				var conditionAttributes = typeof(T).GetCustomAttributes(typeof(ConditionAttribute), false).Cast<ConditionAttribute>().ToList();
				foreach (var attribute in conditionAttributes)
				{
					var func = LambdaConstructor<T>(attribute.ColumnName, attribute.Condition, FilterType.Equal);
					query = query.Where(func);
				}

				if (filters != null)
				{
					foreach (var filter in filters)
					{
						if (filter.Values != null && filter.Values.Any())
							query = query.Where(filter.FilterProperty, filter.Values);
						else if (filter.Value != null)
							query = query.Where(LambdaConstructor<T>(filter.FilterProperty, filter.Value, filter.FilterType));
					}
				}

				return query.AsNoTracking().ToListAsync().Select(selectProperty);
			}
		}

		public List<T> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public T GetObjectByIdAsync(int id, bool loadChild = false)
		{
			throw new NotImplementedException();
		}

		public T GetObjectAsync(IEnumerable<Filter.Filter> filters = null, bool loadChild = false, bool getDeleted = false, bool getAll = false)
		{
			throw new NotImplementedException();
		}

		public IList<T> GetObjectListAsync(IEnumerable<Filter.Filter> filters = null, bool readOnly = true, bool loadChild = false,
			bool getDeleted = false)
		{
			throw new NotImplementedException();
		}

		public IList<T> GetObjectListAllAsync(IEnumerable<Filter.Filter> filters = null, bool readOnly = true, bool loadChild = false,
			bool getDeleted = false)
		{
			throw new NotImplementedException();
		}

		public IQueryable<T> GetObjectQueryableAllAsync(IEnumerable<Filter.Filter> filters = null, bool readOnly = true, bool loadChild = false,
			bool getDeleted = false)
		{
			throw new NotImplementedException();
		}

		public int SaveAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public void DeleteAsync(T entity)
		{
			throw new NotImplementedException();
		}


		private Expression<Func<T, bool>> LambdaConstructor<T>(string propertyName, object inputText, FilterType condition) where T : BaseEntity
		{
			var item = Expression.Parameter(typeof(T), "item");
			var prop = Expression.Property(item, propertyName);
			var propertyInfo = typeof(T).GetProperty(propertyName);

			var type = propertyInfo.PropertyType;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				type = Nullable.GetUnderlyingType(type);
			}

			var value = Expression.Constant(Convert.ChangeType(inputText, type));
			var res = Expression.Convert(value, propertyInfo.PropertyType);
			BinaryExpression equal;
			switch (condition)
			{
				case FilterType.Equal:
					equal = Expression.Equal(prop, res);
					break;
				case FilterType.Grather:
					equal = Expression.GreaterThan(prop, res);
					break;
				case FilterType.GratherOrEqual:
					equal = Expression.GreaterThanOrEqual(prop, res);
					break;
				case FilterType.Less:
					equal = Expression.LessThan(prop, res);
					break;
				case FilterType.LessOrEqual:
					equal = Expression.LessThanOrEqual(prop, res);
					break;
				case FilterType.NotEqual:
					equal = Expression.NotEqual(prop, res);
					break;
				default:
					equal = Expression.Equal(prop, res);
					break;
			}
			var lambda = Expression.Lambda<Func<T, bool>>(equal, item);
			return lambda;
		}

		private IQueryable<T> GetQuery(IQueryable<T> query, string typeCollection, string property, string parentProperty,
			object inputText, FilterType condition, bool deletedCondition)
		{

			Type baseType = typeof(T);
			Type parentType = Type.GetType(typeCollection);
			// Create object of desired type
			ParameterExpression aircraftFlightObject = Expression.Parameter(baseType, "parameter");

			// Locate files property
			MemberExpression filesPropertyExpression = Expression.Property(aircraftFlightObject, property);

			// Prepare prototype where method
			var whereMethod =
				new Func<IEnumerable<IBaseEntity>, Func<IBaseEntity, bool>, IEnumerable<IBaseEntity>>(Enumerable.Where);

			// Prepare specific where method. itemFileLinkType can be replaced by any other type
			var whereMethodGeneric = whereMethod.Method.GetGenericMethodDefinition().MakeGenericMethod(parentType);

			// Getting item types and parentId property
			ParameterExpression itemFileLinkExpression = Expression.Parameter(parentType, "i");
			Expression parentTypeIdExpression = Expression.Property(itemFileLinkExpression, parentProperty);

			var type = parentType.GetProperty(parentProperty);

			BinaryExpression equal;
			// Create the lambda for where method (your condition for where clause)
			switch (condition)
			{
				case FilterType.Equal:
					equal = Expression.Equal(parentTypeIdExpression,
						Expression.Convert(Expression.Constant(inputText), type.PropertyType));
					break;
				case FilterType.Grather:
					equal = Expression.GreaterThan(parentTypeIdExpression,
						Expression.Convert(Expression.Constant(inputText), type.PropertyType));
					break;
				case FilterType.GratherOrEqual:
					equal = Expression.GreaterThanOrEqual(parentTypeIdExpression,
						Expression.Convert(Expression.Constant(inputText), type.PropertyType));
					break;
				case FilterType.Less:
					equal = Expression.LessThan(parentTypeIdExpression,
						Expression.Convert(Expression.Constant(inputText), type.PropertyType));
					break;
				case FilterType.LessOrEqual:
					equal = Expression.LessThanOrEqual(parentTypeIdExpression,
						Expression.Convert(Expression.Constant(inputText), type.PropertyType));
					break;
				case FilterType.NotEqual:
					equal = Expression.NotEqual(parentTypeIdExpression,
						Expression.Convert(Expression.Constant(inputText), type.PropertyType));
					break;
				default:
					equal = Expression.Equal(parentTypeIdExpression,
						Expression.Convert(Expression.Constant(inputText), type.PropertyType));
					break;
			}


			Expression isDelExpression = Expression.Property(itemFileLinkExpression, "IsDeleted");
			var isDeltype = parentType.GetProperty("IsDeleted");

			var isDel = Expression.Equal(isDelExpression, Expression.Convert(Expression.Constant(deletedCondition), isDeltype.PropertyType));

			var lambdaForWhere = Expression.Lambda(Expression.AndAlso(equal, isDel), itemFileLinkExpression);
			var whereCall = Expression.Call(null, whereMethodGeneric, filesPropertyExpression, lambdaForWhere);
			var lambda = Expression.Lambda(whereCall, aircraftFlightObject);

			if (typeCollection.Contains("ItemFileLinkDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ItemFileLinkDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ItemFileLinkDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ComponentLLPCategoryDataDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ComponentLLPCategoryDataDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ComponentLLPCategoryDataDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ComponentLLPCategoryChangeRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ComponentLLPCategoryChangeRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ComponentLLPCategoryChangeRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("TransferRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<TransferRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<TransferRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ComponentDirectiveDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ComponentDirectiveDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ComponentDirectiveDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ActualStateRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ActualStateRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ActualStateRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("MaintenanceProgramChangeRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<MaintenanceProgramChangeRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<MaintenanceProgramChangeRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("AuditRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<AuditRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<AuditRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("DirectiveRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<DirectiveRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<DirectiveRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("CategoryRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<CategoryRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<CategoryRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("AccessoryRequiredDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<AccessoryRequiredDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<AccessoryRequiredDTO>)), lambda.Parameters));
			if (typeCollection.Contains("KitSuppliersRelationDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<KitSuppliersRelationDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<KitSuppliersRelationDTO>)), lambda.Parameters));
			if (typeCollection.Contains("EventConditionDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<EventConditionDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<EventConditionDTO>)), lambda.Parameters));
			if (typeCollection.Contains("DocumentDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<DocumentDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<DocumentDTO>)), lambda.Parameters));
			if (typeCollection.Contains("DamageSectorDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<DamageSectorDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<DamageSectorDTO>)), lambda.Parameters));
			if (typeCollection.Contains("DamageDocumentDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<DamageDocumentDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<DamageDocumentDTO>)), lambda.Parameters));
			if (typeCollection.Contains("FlightTrackRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<FlightTrackRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<FlightTrackRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("InitialOrderRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<InitialOrderRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<InitialOrderRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("JobCardTaskDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<JobCardTaskDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<JobCardTaskDTO>)), lambda.Parameters));
			if (typeCollection.Contains("MaintenanceDirectiveDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<MaintenanceDirectiveDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<MaintenanceDirectiveDTO>)), lambda.Parameters));
			if (typeCollection.Contains("MTOPCheckRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<MTOPCheckRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<MTOPCheckRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ProcedureDocumentReferenceDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ProcedureDocumentReferenceDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ProcedureDocumentReferenceDTO>)), lambda.Parameters));
			if (typeCollection.Contains("RequestRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<RequestRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<RequestRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("RequestForQuotationRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<RequestForQuotationRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<RequestForQuotationRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistLicenseDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistLicenseDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistLicenseDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistTrainingDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistTrainingDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistTrainingDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistLicenseDetailDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistLicenseDetailDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistLicenseDetailDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistLicenseRemarkDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistLicenseRemarkDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistLicenseRemarkDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistCAADTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistCAADTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistCAADTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistLicenseRatingDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistLicenseRatingDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistLicenseRatingDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistInstrumentRatingDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistInstrumentRatingDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistInstrumentRatingDTO>)), lambda.Parameters));
			if (typeCollection.Contains("WorkOrderRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<WorkOrderRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<WorkOrderRecordDTO>)), lambda.Parameters));
			throw new Exception($"Type({typeCollection}) not Found!");
		}

		private IQueryable<T> GetQuery(IQueryable<T> query, string typeCollection, string property, bool deletedCondition)
		{

			Type baseType = typeof(T);
			Type parentType = Type.GetType(typeCollection);
			// Create object of desired type
			ParameterExpression aircraftFlightObject = Expression.Parameter(baseType, "parameter");

			// Locate files property
			MemberExpression filesPropertyExpression = Expression.Property(aircraftFlightObject, property);

			// Prepare prototype where method
			var whereMethod =
				new Func<IEnumerable<IBaseEntity>, Func<IBaseEntity, bool>, IEnumerable<IBaseEntity>>(Enumerable.Where);

			// Prepare specific where method. itemFileLinkType can be replaced by any other type
			var whereMethodGeneric = whereMethod.Method.GetGenericMethodDefinition().MakeGenericMethod(parentType);

			// Getting item types and parentId property
			ParameterExpression itemFileLinkExpression = Expression.Parameter(parentType, "i");


			Expression isDelExpression = Expression.Property(itemFileLinkExpression, "IsDeleted");
			var isDeltype = parentType.GetProperty("IsDeleted");

			var isDel = Expression.Equal(isDelExpression, Expression.Convert(Expression.Constant(deletedCondition), isDeltype.PropertyType));

			var lambdaForWhere = Expression.Lambda(isDel, itemFileLinkExpression);
			var whereCall = Expression.Call(null, whereMethodGeneric, filesPropertyExpression, lambdaForWhere);
			var lambda = Expression.Lambda(whereCall, aircraftFlightObject);

			if (typeCollection.Contains("ItemFileLinkDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ItemFileLinkDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ItemFileLinkDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ComponentLLPCategoryDataDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ComponentLLPCategoryDataDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ComponentLLPCategoryDataDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ComponentLLPCategoryChangeRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ComponentLLPCategoryChangeRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ComponentLLPCategoryChangeRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("TransferRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<TransferRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<TransferRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ComponentDirectiveDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ComponentDirectiveDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ComponentDirectiveDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ActualStateRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ActualStateRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ActualStateRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("MaintenanceProgramChangeRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<MaintenanceProgramChangeRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<MaintenanceProgramChangeRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("AuditRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<AuditRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<AuditRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("DirectiveRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<DirectiveRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<DirectiveRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("CategoryRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<CategoryRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<CategoryRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("AccessoryRequiredDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<AccessoryRequiredDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<AccessoryRequiredDTO>)), lambda.Parameters));
			if (typeCollection.Contains("KitSuppliersRelationDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<KitSuppliersRelationDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<KitSuppliersRelationDTO>)), lambda.Parameters));
			if (typeCollection.Contains("EventConditionDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<EventConditionDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<EventConditionDTO>)), lambda.Parameters));
			if (typeCollection.Contains("DocumentDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<DocumentDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<DocumentDTO>)), lambda.Parameters));
			if (typeCollection.Contains("DamageSectorDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<DamageSectorDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<DamageSectorDTO>)), lambda.Parameters));
			if (typeCollection.Contains("DamageDocumentDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<DamageDocumentDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<DamageDocumentDTO>)), lambda.Parameters));
			if (typeCollection.Contains("FlightTrackRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<FlightTrackRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<FlightTrackRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("InitialOrderRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<InitialOrderRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<InitialOrderRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("JobCardTaskDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<JobCardTaskDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<JobCardTaskDTO>)), lambda.Parameters));
			if (typeCollection.Contains("MaintenanceDirectiveDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<MaintenanceDirectiveDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<MaintenanceDirectiveDTO>)), lambda.Parameters));
			if (typeCollection.Contains("MTOPCheckRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<MTOPCheckRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<MTOPCheckRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("ProcedureDocumentReferenceDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<ProcedureDocumentReferenceDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<ProcedureDocumentReferenceDTO>)), lambda.Parameters));
			if (typeCollection.Contains("RequestRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<RequestRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<RequestRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("RequestForQuotationRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<RequestForQuotationRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<RequestForQuotationRecordDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistLicenseDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistLicenseDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistLicenseDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistTrainingDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistTrainingDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistTrainingDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistLicenseDetailDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistLicenseDetailDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistLicenseDetailDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistLicenseRemarkDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistLicenseRemarkDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistLicenseRemarkDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistCAADTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistCAADTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistCAADTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistLicenseRatingDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistLicenseRatingDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistLicenseRatingDTO>)), lambda.Parameters));
			if (typeCollection.Contains("SpecialistInstrumentRatingDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<SpecialistInstrumentRatingDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<SpecialistInstrumentRatingDTO>)), lambda.Parameters));
			if (typeCollection.Contains("WorkOrderRecordDTO"))
				return query.IncludeOptimized(Expression.Lambda<Func<T, IEnumerable<WorkOrderRecordDTO>>>(
					Expression.Convert(lambda.Body, typeof(IEnumerable<WorkOrderRecordDTO>)), lambda.Parameters));
			throw new Exception($"Type({typeCollection}) not Found!");
		}
	}
}