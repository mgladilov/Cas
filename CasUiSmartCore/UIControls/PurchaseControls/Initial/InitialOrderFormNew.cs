﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CAS.UI.UIControls.Auxiliary;
using CASTerms;
using EFCore.DTO.Dictionaries;
using EFCore.DTO.General;
using EFCore.Filter;
using MetroFramework.Forms;
using SmartCore.Auxiliary;
using SmartCore.Calculations;
using SmartCore.Entities.Collections;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Filters;
using SmartCore.Purchase;

namespace CAS.UI.UIControls.PurchaseControls.Initial
{
    public partial class InitialOrderFormNew : MetroForm
    {
		#region Fields

		private List<InitialOrderRecord> _addedInitialOrderRecords = new List<InitialOrderRecord>();
		private List<InitialOrderRecord> _deleteExistInitialOrderRecords = new List<InitialOrderRecord>();

		private readonly InitialOrder _order;
		private List<BaseEntityObject> destinations = new List<BaseEntityObject>();
		private DefferedCategoriesCollection _defferedCategories = new DefferedCategoriesCollection();

		private readonly ProductPartNumberFilter _partNumberFilter = new ProductPartNumberFilter();
		private readonly ProductCollectionFilter _collectionFilter = new ProductCollectionFilter();
		private readonly ProductStandartFilter _standartFilter = new ProductStandartFilter();
		private List<Product> _currentAircraftKits = new List<Product>();

		#endregion

		#region Properties

		public InitialOrder AddedInitial
		{
			get { return _order; }
		}

		#endregion

		#region Constructor

		public InitialOrderFormNew()
	    {
		    InitializeComponent();
	    }

	    public InitialOrderFormNew(InitialOrder order, IEnumerable<Product> selectedProducts = null) : this()
	    {
		    if (selectedProducts != null)
		    {
			    foreach (var product in selectedProducts)
			    {
					var newRequest = new InitialOrderRecord(-1, product, 1);
					newRequest.Product = product;
					_addedInitialOrderRecords.Add(newRequest);
				}
		    }

			_order = order;

		    _collectionFilter.Filters.Add(_partNumberFilter);
		    _collectionFilter.Filters.Add(_standartFilter);

			Task.Run(() => DoWork())
			    .ContinueWith(task => Completed(), TaskScheduler.FromCurrentSynchronizationContext());
		}

		#endregion

		#region private void Completed()

		private void Completed()
		{
			   var filteredCollection = _collectionFilter.GatherDirectives();
			listViewKits.SetItemsArray(filteredCollection.ToArray());
			listViewInitialItems.SetItemsArray(_addedInitialOrderRecords.ToArray());

			UpdateControls();
			UpdateInitialControls();
		}

		#endregion

		#region private void DoWork()

		private void DoWork()
		{
			destinations.Clear();

			if(_currentAircraftKits.Count == 0)
				_currentAircraftKits = GlobalObjects.CasEnvironment.NewLoader.GetObjectList<AccessoryDescriptionDTO, Product>(loadChild: true).ToList();
			_collectionFilter.InitialCollection = _currentAircraftKits;

			if (_order != null && _order.ItemId > 0)
			{
				_addedInitialOrderRecords.AddRange(GlobalObjects.CasEnvironment.NewLoader.GetObjectList<InitialOrderRecordDTO, InitialOrderRecord>(new Filter("ParentPackageId", _order.ItemId)));
				var ids = _addedInitialOrderRecords.Select(i => i.ProductId);
				if (ids.Count() > 0)
				{
					foreach (var addedInitialOrderRecord in _addedInitialOrderRecords)
						addedInitialOrderRecord.Product = _currentAircraftKits.FirstOrDefault(i => i.ItemId == addedInitialOrderRecord.ProductId);
				}
			}

			_defferedCategories.Clear();
			_defferedCategories.AddRange(GlobalObjects.CasEnvironment.NewLoader.GetObjectListAll<DefferedCategorieDTO, DeferredCategory>(loadChild: true));

			destinations.AddRange(GlobalObjects.AircraftsCore.GetAllAircrafts().ToArray());
			destinations.AddRange(GlobalObjects.CasEnvironment.Stores.GetValidEntries());
			destinations.AddRange(GlobalObjects.CasEnvironment.Hangars.GetValidEntries());

		}

		#endregion

		#region private void UpdateControls()

		private void UpdateControls()
		{
			comboBoxMeasure.Items.Clear();
			comboBoxMeasure.Items.AddRange(Measure.GetByCategories(new[] { MeasureCategory.Mass, MeasureCategory.EconomicEntity }));

			comboBoxDestination.Items.Clear();
			comboBoxDestination.Items.AddRange(destinations.ToArray());

			comboBoxPriority.Items.Clear();
			comboBoxPriority.Items.AddRange(Priority.Items.ToArray());

			comboBoxReason.Items.Clear();
			comboBoxReason.Items.AddRange(InitialReason.Items.ToArray());
			
		}

		#endregion

		#region private void comboBoxDestination_SelectedIndexChanged(object sender, System.EventArgs e)

		private void comboBoxDestination_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBoxDefferedCategory.Items.Clear();
			comboBoxDIR.Items.Clear();

			var a = comboBoxDestination.SelectedItem as Aircraft;

			if (a != null)
			{
				comboBoxDefferedCategory.Enabled = true;

				if (a.Model != null)
				{
					var categories = _defferedCategories.Where(c => a.Model.Equals(c.AircraftModel)).ToList();
					categories.Add(DeferredCategory.Unknown);

					comboBoxDefferedCategory.Items.AddRange(categories.ToArray());
					comboBoxDefferedCategory.SelectedItem = categories.FirstOrDefault(c => c.Equals(listViewInitialItems.SelectedItem.DeferredCategory)) ?? DeferredCategory.Unknown;
				}
			}
			else comboBoxDefferedCategory.Enabled = false;
		}

		#endregion

		#region private void comboBoxMeasure_SelectedIndexChanged(object sender, System.EventArgs e)

		private void comboBoxMeasure_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetForMeasure();
		}

		#endregion

		#region private void numericUpDownQuantity_ValueChanged(object sender, System.EventArgs e)

		private void numericUpDownQuantity_ValueChanged(object sender, EventArgs e)
		{
			SetForMeasure();
		}

		#endregion

		#region private void SetForMeasure()
		/// <summary>
		/// Изменяет контрол в соотствествии с выбранной единицей измерения
		/// </summary>
		private void SetForMeasure()
		{
			var measure = comboBoxMeasure.SelectedItem as Measure;
			if (measure == null || measure.MeasureCategory != MeasureCategory.Mass)
				numericUpDownQuantity.DecimalPlaces = 0;
			else numericUpDownQuantity.DecimalPlaces = 2;

			var quantity = numericUpDownQuantity.Value;

			textBoxTotal.Text = $"{quantity:0.##}" + (measure != null ? " " + measure + "(s)" : "");
		}
		#endregion

		#region private void UpdateListViewItems()
		private void UpdateListViewItems()
		{
			var filteredCollection = _collectionFilter.GatherDirectives();
			listViewKits.SetItemsArray(filteredCollection.ToArray());
		}
		#endregion

		#region private void UpdateInitialControls()

		private void UpdateInitialControls()
		{
			comboBoxStatus.Items.Clear();
			comboBoxStatus.DataSource = Enum.GetValues(typeof(WorkPackageStatus));
			comboBoxStatus.SelectedItem = _order.Status;

			if (_order.ItemId > 0)
			{
				textBoxTitle.Text = _order.Title;
				metroTextBoxNumber.Text = _order.Number;
				dateTimePickerOpeningDate.Value = _order.OpeningDate;
				dateTimePickerClosingDate.Value = _order.ClosingDate;
				dateTimePickerPublishDate.Value = _order.PublishingDate;
				textBoxClosingBy.Text = _order.CloseByUser;
				textBoxPublishedBy.Text = _order.PublishedByUser;
				textBoxAuthor.Text = _order.Author;
				textBoxRemarks.Text = _order.Remarks;
			}
			else
			{
				dateTimePickerClosingDate.Enabled = false;
				dateTimePickerPublishDate.Enabled = false;

				textBoxAuthor.Text = GlobalObjects.CasEnvironment.IdentityUser.ToString();
			}
		}

		#endregion

		#region private void textBoxSearchPartNumber_TextChanged(object sender, EventArgs e)

		private void textBoxSearchPartNumber_TextChanged(object sender, EventArgs e)
		{
			_partNumberFilter.Mask = textBoxSearchPartNumber.Text;
			UpdateListViewItems();
		}

		#endregion

		#region private void textBoxSearchStandart_TextChanged(object sender, EventArgs e)

		private void textBoxSearchStandart_TextChanged(object sender, EventArgs e)
		{
			_standartFilter.Mask = textBoxSearchStandart.Text;
			UpdateListViewItems();
		}

		#endregion

		#region private void buttonCancel_Click(object sender, EventArgs e)

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		#endregion

		#region private void buttonOk_Click(object sender, EventArgs e)

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (textBoxTitle.Text == "")
			{
				MessageBox.Show("Please, enter a Title", (string)new GlobalTermsProvider()["SystemName"],
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
				return;
			}

			if (listViewInitialItems.ListViewItemList.Count <= 0)
			{
				MessageBox.Show("Please select a kits for initional order", (string)new GlobalTermsProvider()["SystemName"],
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
				return;
			}
			//запись новой информации в запросный ордер
			ApplyInitialData();
			//сохранение запросного ордера
			GlobalObjects.CasEnvironment.NewKeeper.Save(_order);

			if (listViewKits.ListViewItemList.Count <= 0)
			{
				MessageBox.Show("Please select a kits for initional order", (string)new GlobalTermsProvider()["SystemName"],
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
				return;
			}

			foreach (var record in _addedInitialOrderRecords)
			{
				record.ParentPackageId = _order.ItemId;
				GlobalObjects.CasEnvironment.NewKeeper.Save(record);

				if (record.Product != null)
				{
					foreach (var ksr in record.Product.SupplierRelations)
					{
						if (ksr.SupplierId != 0)
						{
							ksr.KitId = record.Product.ItemId;
							ksr.ParentTypeId = record.Product.SmartCoreObjectType.ItemId;

							try
							{
								GlobalObjects.CasEnvironment.NewKeeper.Save(ksr);
							}
							catch (Exception ex)
							{
								Program.Provider.Logger.Log("Error while saving data", ex);
								return;
							}
						}
					}
				}
			}

			foreach (var record in _deleteExistInitialOrderRecords)
				GlobalObjects.CasEnvironment.NewKeeper.Delete(record);
		}

		#endregion

		#region private void ApplyOrderData()
		private void ApplyInitialData()
		{
			_order.Title = textBoxTitle.Text;
			_order.Number = metroTextBoxNumber.Text;
			_order.Status = (WorkPackageStatus)comboBoxStatus.SelectedItem;
			_order.Remarks = textBoxRemarks.Text;

			if (_order.ItemId <= 0)
				_order.Author = GlobalObjects.CasEnvironment.IdentityUser.ToString();

			if (_order.Status == WorkPackageStatus.All)
			{
				_order.OpeningDate = dateTimePickerOpeningDate.Value;
				_order.ClosingDate = dateTimePickerClosingDate.Value;
				_order.PublishingDate = dateTimePickerPublishDate.Value;
			}
			else if (_order.Status == WorkPackageStatus.Opened)
			{
				_order.OpeningDate = dateTimePickerOpeningDate.Value;
			}
			else if (_order.Status == WorkPackageStatus.Closed)
			{
				_order.ClosingDate = dateTimePickerClosingDate.Value;
			}
			else if (_order.Status == WorkPackageStatus.Published)
			{
				_order.PublishingDate = dateTimePickerPublishDate.Value;
			}
		}
		#endregion

		#region private void ButtonAdd_Click(object sender, EventArgs e)

		private void ButtonAdd_Click(object sender, EventArgs e)
		{
			foreach (var product in listViewKits.SelectedItems.ToArray())
			{
				var newRequest = new InitialOrderRecord(-1, product, 1);
				newRequest.Product = product;
				_addedInitialOrderRecords.Add(newRequest);
			}

			listViewInitialItems.SetItemsArray(_addedInitialOrderRecords.ToArray());
		}

		#endregion

		#region private void ButtonDelete_Click(object sender, EventArgs e)

		private void ButtonDelete_Click(object sender, EventArgs e)
		{
			if(listViewInitialItems.SelectedItems.Count == 0) return;

			foreach (var item in listViewInitialItems.SelectedItems.ToArray())
			{
				if (item.ItemId > 0)
					_deleteExistInitialOrderRecords.Add(item);

				_addedInitialOrderRecords.Remove(item);
			}

			listViewInitialItems.SetItemsArray(_addedInitialOrderRecords.ToArray());
		}


		#endregion

		#region private void listViewInitialItems_SelectedItemsChanged(object sender, Auxiliary.SelectedItemsChangeEventArgs e)

		private void listViewInitialItems_SelectedItemsChanged(object sender, SelectedItemsChangeEventArgs e)
		{
			button1.Enabled = listViewInitialItems.SelectedItem != null;
			if (listViewInitialItems.SelectedItem == null) return;

			var product = listViewInitialItems.SelectedItem.Product;

			comboBoxMeasure.SelectedItem = product.Measure;
			comboBoxReason.SelectedItem = listViewInitialItems.SelectedItem.InitialReason;
			numericUpDownQuantity.Value = (decimal)listViewInitialItems.SelectedItem.Quantity;
			checkBoxNew.Checked = (listViewInitialItems.SelectedItem.CostCondition & ComponentStatus.New) != 0;
			checkBoxOverhaul.Checked = (listViewInitialItems.SelectedItem.CostCondition & ComponentStatus.Overhaul) != 0;
			checkBoxRepair.Checked = (listViewInitialItems.SelectedItem.CostCondition & ComponentStatus.Repair) != 0;
			checkBoxServiceable.Checked = (listViewInitialItems.SelectedItem.CostCondition & ComponentStatus.Serviceable) != 0;

			var destination =
				destinations.FirstOrDefault(d => d.SmartCoreObjectType == listViewInitialItems.SelectedItem.DestinationObjectType
				                                 && d.ItemId == listViewInitialItems.SelectedItem.DestinationObjectId);

			comboBoxDestination.SelectedItem = destination;
			comboBoxPriority.SelectedItem = listViewInitialItems.SelectedItem.Priority;
			metroTextBox1.Text = listViewInitialItems.SelectedItem.Remarks;

			lifelengthViewerLifeLimit.Lifelength = new Lifelength(listViewInitialItems.SelectedItem.LifeLimit);
			lifelengthViewerNotify.Lifelength = new Lifelength(listViewInitialItems.SelectedItem.LifeLimitNotify);

		}

		#endregion

		#region private void button1_Click(object sender, EventArgs e)

		private void button1_Click(object sender, EventArgs e)
		{
			if (listViewInitialItems.SelectedItem == null) return;

			listViewInitialItems.SelectedItem.Priority = comboBoxPriority.SelectedItem as Priority ?? Priority.UNK;
			listViewInitialItems.SelectedItem.Measure = comboBoxMeasure.SelectedItem as Measure ?? Measure.Unknown;
			listViewInitialItems.SelectedItem.Quantity = (double)numericUpDownQuantity.Value;
			listViewInitialItems.SelectedItem.DeferredCategory = comboBoxDefferedCategory.SelectedItem as DeferredCategory ?? DeferredCategory.Unknown;
			listViewInitialItems.SelectedItem.Remarks = metroTextBox1.Text;

			ComponentStatus costCondition = ComponentStatus.Unknown;
			if (checkBoxNew.Checked)
				costCondition = costCondition | ComponentStatus.New;
			if (checkBoxServiceable.Checked)
				costCondition = costCondition | ComponentStatus.Serviceable;
			if (checkBoxOverhaul.Checked)
				costCondition = costCondition | ComponentStatus.Overhaul;
			if (checkBoxRepair.Checked)
				costCondition = costCondition | ComponentStatus.Repair;

			listViewInitialItems.SelectedItem.CostCondition = costCondition;

			var destination = comboBoxDestination.SelectedItem as BaseEntityObject;
			if (destination != null)
			{
				listViewInitialItems.SelectedItem.DestinationObjectType = destination.SmartCoreObjectType;
				listViewInitialItems.SelectedItem.DestinationObjectId = destination.ItemId;
			}
			else
			{
				listViewInitialItems.SelectedItem.DestinationObjectType = SmartCoreType.Unknown;
				listViewInitialItems.SelectedItem.DestinationObjectId = -1;
			}
			listViewInitialItems.SelectedItem.InitialReason = comboBoxReason.SelectedItem as InitialReason ?? InitialReason.Unknown;
			listViewInitialItems.SelectedItem.LifeLimit = lifelengthViewerLifeLimit.Lifelength;
			listViewInitialItems.SelectedItem.LifeLimitNotify = lifelengthViewerNotify.Lifelength;

			listViewInitialItems.SetItemsArray(_addedInitialOrderRecords.ToArray());

			Reset();
		}

		#endregion

		#region private void Reset()

		private void Reset()
		{
			button1.Enabled = false;
			comboBoxMeasure.SelectedItem = null;
			comboBoxReason.SelectedItem = null;
			metroTextBox1.Text = "";
			numericUpDownQuantity.Value = 0;
			checkBoxNew.Checked = false;
			checkBoxOverhaul.Checked = false;
			checkBoxRepair.Checked = false;
			checkBoxServiceable.Checked = false;
			comboBoxDestination.SelectedItem = null;
			lifelengthViewerLifeLimit.Lifelength = new Lifelength();
			lifelengthViewerNotify.Lifelength = new Lifelength();
		}

		#endregion

		#region private void dateTimePickerOpeningDate_ValueChanged(object sender, EventArgs e)

		private void dateTimePickerOpeningDate_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimePickerOpeningDate.Value < DateTimeExtend.GetCASMinDateTime())
				dateTimePickerOpeningDate.Value = DateTimeExtend.GetCASMinDateTime();
			if (dateTimePickerOpeningDate.Value > DateTime.Now)
				dateTimePickerOpeningDate.Value = DateTime.Now;
		}

		#endregion

		private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			var status = (WorkPackageStatus)comboBoxStatus.SelectedItem;
			if (status == WorkPackageStatus.Opened)
			{
				dateTimePickerOpeningDate.Enabled = true;
				dateTimePickerPublishDate.Enabled = false;
				dateTimePickerClosingDate.Enabled = false;
			}
			else if (status == WorkPackageStatus.Published)
			{
				dateTimePickerOpeningDate.Enabled = false;
				dateTimePickerPublishDate.Enabled = true;
				dateTimePickerClosingDate.Enabled = false;
			}
			else if (status == WorkPackageStatus.Closed)
			{
				dateTimePickerOpeningDate.Enabled = false;
				dateTimePickerPublishDate.Enabled = false;
				dateTimePickerClosingDate.Enabled = true;
			}
			else
			{
				dateTimePickerOpeningDate.Enabled = true;
				dateTimePickerPublishDate.Enabled = true;
				dateTimePickerClosingDate.Enabled = true;
			}
		}
	}
}