﻿using System.Collections.Generic;
using CAS.UI.UIControls.NewGrid;
using CASTerms;
using SmartCore.Entities.Dictionaries;
using SmartCore.Purchase;

namespace CAS.UI.UIControls.PurchaseControls.Purchase
{
	public partial class PurchaseRecordListView : BaseGridViewControl<PurchaseRequestRecord>
	{
		private readonly bool _orderBySupplies;

		#region Constructor

		public PurchaseRecordListView(bool orderBySupplies = false)
		{
			_orderBySupplies = orderBySupplies;
			InitializeComponent();
			DisableContectMenu();
			OldColumnIndex = 2;
			SortMultiplier = 1;
		}

		#endregion

		#region protected override void SetHeaders()

		protected override void SetHeaders()
		{
			AddColumn("Supplier", (int)(radGridView1.Width * 0.2f));
			AddColumn("Q-ty", (int)(radGridView1.Width * 0.2f));
			AddColumn("Unit Cost", (int)(radGridView1.Width * 0.2f));
			AddColumn("Item Cost", (int)(radGridView1.Width * 0.2f));
			AddColumn("Total Cost", (int)(radGridView1.Width * 0.2f));
			AddColumn("Condition", (int)(radGridView1.Width * 0.2f));
			AddColumn("Type", (int)(radGridView1.Width * 0.2f));
			AddColumn("Measure", (int)(radGridView1.Width * 0.2f));
			AddColumn("Product", (int)(radGridView1.Width * 0.2f));
			AddColumn("Signer", (int)(radGridView1.Width * 0.1f));
		}

		#endregion

		#region protected override ListViewItem.ListViewSubItem[] GetListViewSubItems(Product item)

		protected override List<CustomCell> GetListViewSubItems(PurchaseRequestRecord item)
		{
			var author = GlobalObjects.CasEnvironment.GetCorrector(item);
			var destiantion = "";
			if (item?.ParentInitialRecord?.DestinationObjectType == SmartCoreType.Aircraft)
				destiantion = GlobalObjects.AircraftsCore.GetAircraftById(item?.ParentInitialRecord?.DestinationObjectId ?? -1)?.ToString();
			else destiantion = GlobalObjects.StoreCore.GetStoreById(item?.ParentInitialRecord?.DestinationObjectId ?? -1)?.ToString();
			var temp = $"P/N: {item?.Product?.PartNumber}";
			if (item?.ParentInitialRecord != null)
				temp += $"| {item.Product?.Standart} | Name: {item?.Product?.Name} | {destiantion} | {item?.ParentInitialRecord?.Priority} | Requsted By: {((InitialOrder)item?.ParentInitialRecord?.ParentPackage)?.Author}";

			return new List<CustomCell>()
			{
				CreateRow(item.Supplier.ToString(),item.Supplier),
				CreateRow(item.Quantity.ToString(),item.Quantity),
				CreateRow($"{item.Cost} {item.Currency}",item.Cost),
				CreateRow($"{item.ItemCost:0.##} {item.Currency}", item.ItemCost),
				CreateRow($"{item.TotalCost:0.##} {item.Currency}", item.TotalCost),
				CreateRow(item.CostCondition.ToString(),item.CostCondition),
				CreateRow(item.CostType.ToString(),item.CostType),
				CreateRow(item.Measure.ToString(),item.Measure),
				CreateRow(temp,temp),
				CreateRow(author,author),
			};
		}

		#endregion

		#region Overrides of BaseGridViewControl<InitialOrderRecord>

		protected override void GroupingItems()
		{
			Grouping("Product");
		}

		#endregion
	}
}
