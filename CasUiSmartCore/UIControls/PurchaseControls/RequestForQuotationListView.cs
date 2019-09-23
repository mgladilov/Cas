﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CAS.UI.UIControls.NewGrid;
using CAS.UI.UIControls.PurchaseControls.Initial;
using CASTerms;
using SmartCore.Purchase;

namespace CAS.UI.UIControls.PurchaseControls
{
	///<summary>
	/// список для отображения ордеров запроса
	///</summary>
	public partial class RequestForQuotationListView : BaseGridViewControl<RequestForQuotation>
	{
		#region public RequestForQuotationListView()
		///<summary>
		///</summary>
		public RequestForQuotationListView()
		{
			InitializeComponent();
			SortMultiplier = 0;
		}
		#endregion

		#region Methods

		#region protected override void SetGroupsToItems(int columnIndex)


		protected override void GroupingItems()
		{
			Grouping("Status");
		}


		#endregion

		#region protected override ListViewItem.ListViewSubItem[] GetItemsString(RequestForQuotation item)

		protected override List<CustomCell> GetListViewSubItems(RequestForQuotation item)
		{
			var author = GlobalObjects.CasEnvironment.GetCorrector(item);

			return new List<CustomCell>
			{
				CreateRow(item.Status.ToString(), item.Status),
				CreateRow(item.Number, item.Number),
				CreateRow(item.Title, item.Title),
				CreateRow(SmartCore.Auxiliary.Convert.GetDateFormat(item.OpeningDate), item.OpeningDate),
				CreateRow(SmartCore.Auxiliary.Convert.GetDateFormat(item.PublishingDate), item.PublishingDate),
				CreateRow(SmartCore.Auxiliary.Convert.GetDateFormat(item.ClosingDate), item.ClosingDate),
				CreateRow(item.Author, item.Author),
				CreateRow(item.PublishedByUser, item.PublishedByUser),
				CreateRow(item.CloseByUser, item.CloseByUser),
				CreateRow(item.Remarks, item.Remarks),
				CreateRow(author, author),
			};
		}

		#endregion

		#region protected override void ItemsListViewMouseDoubleClick(object sender, MouseEventArgs e)
		protected override void RadGridView1_DoubleClick(object sender, EventArgs e)
		{
			if (SelectedItem != null)
			{

				var editForm = new QuatationOrderFormNew(SelectedItem);
				if (editForm.ShowDialog() == DialogResult.OK)
				{
					var subs = GetListViewSubItems(SelectedItem);
					for (int i = 0; i < subs.Count; i++)
						radGridView1.SelectedRows[0].Cells[i].Value = subs[i].Text;
				}
			}
		}
		#endregion

		#region protected override void SetHeaders()

		protected override void SetHeaders()
		{
			AddColumn("Status", (int)(radGridView1.Width * 0.2f));
			AddColumn("Order No", (int)(radGridView1.Width * 0.2f));
			AddColumn("Title", (int)(radGridView1.Width * 0.3f));
			AddDateColumn("Opening date", (int)(radGridView1.Width * 0.2f));
			AddDateColumn("Publishing date", (int)(radGridView1.Width * 0.2f));
			AddDateColumn("Closing date", (int)(radGridView1.Width * 0.2f));
			AddColumn("Author", (int)(radGridView1.Width * 0.2f));
			AddColumn("Published By", (int)(radGridView1.Width * 0.2f));
			AddColumn("Closed By", (int)(radGridView1.Width * 0.2f));
			AddColumn("Remark", (int)(radGridView1.Width * 0.2f));
			AddColumn("Signer", (int)(radGridView1.Width * 0.3f));

		}

		#endregion
		#endregion
	}
}
