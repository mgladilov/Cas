﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;
using CASTerms;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General.Attributes;
using SmartCore.Entities.General.Interfaces;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace CAS.UI.UIControls.NewGrid
{
	public partial class BaseGridViewControl<T> : UserControl where T : class, IBaseCoreObject
	{
		#region Fields

		//заголовки списка
		protected readonly List<GridViewDataColumn> ColumnHeaderList = new List<GridViewDataColumn>();
		//коллекция выделенных элементов
		private readonly List<T> _selectedItemsList = new List<T>();
		private readonly List<T> _items = new List<T>();

		private List<CustomCell>  _cells = new List<CustomCell>();

		#endregion

		#region Properties

		#region public List<T> SelectedItems
		/// <summary>
		/// Возвращает выбранные элементы
		/// </summary>
		public List<T> SelectedItems
		{
			get
			{
				if (_selectedItemsList == null)
					return null;

				_selectedItemsList.Clear();
				_selectedItemsList.AddRange(radGridView1.SelectedRows
					.Cast<GridViewDataRowInfo>()
					.Where(i => i.Tag != null && i.Tag is T)
					.Select(i => i.Tag as T)
					.ToArray());
				return _selectedItemsList;
			}
		}
		#endregion

		#region public T SelectedItem
		/// <summary>
		/// Выбранный элемент
		/// </summary>
		public T SelectedItem
		{
			get
			{
				if (radGridView1.CurrentRow != null)
					return (radGridView1.CurrentRow.Tag as T);
				return null;
			}
		}
		#endregion

		#region public IDisplayer Displayer
		/// <summary>
		/// Displayer for displaying entity
		/// </summary>
		public IDisplayer Displayer { get; set; }

		#endregion

		#region public string DisplayerText { get; set;}
		/// <summary>
		/// Text of page's header that Reference lead to
		/// </summary>
		public string DisplayerText { get; set; }
		#endregion

		#region public IDisplayingEntity Entity

		/// <summary>
		/// Entity to display
		/// </summary>
		public IDisplayingEntity Entity { get; set; }

		#endregion

		#region public ReflectionTypes ReflectionType

		/// <summary>
		/// Type of reflection
		/// </summary>
		public ReflectionTypes ReflectionType { get; set; }

		#endregion

		#endregion

		#region Constructor

		public BaseGridViewControl()
		{
			InitializeComponent();
			radGridView1.SelectionMode = GridViewSelectionMode.FullRowSelect;
			radGridView1.MultiSelect = true;
			FirstLoad();
		}


		#endregion


		#region protected virtual void SetHeaders()
		/// <summary>
		/// Устанавливает заголовки
		/// </summary>
		protected virtual void SetHeaders()
		{
			var properties = GetTypeProperties();

			radGridView1.Columns.Clear();
			GridViewDataColumn columnHeader;
			foreach (var propertyInfo in properties)
			{
				var attr = (ListViewDataAttribute)propertyInfo.GetCustomAttributes(typeof(ListViewDataAttribute), false)[0];
				columnHeader = new GridViewBrowseColumn(attr.Title);
				columnHeader.Width = attr.HeaderWidth > 1 ? (int)attr.HeaderWidth : (int)(radGridView1.Width * attr.HeaderWidth);
				columnHeader.Tag = propertyInfo.PropertyType;

				ColumnHeaderList.Add(columnHeader);
			}
		}
		#endregion

		#region public void AddHeader(string title, int? size = null)

		public void AddHeader(string title, int? size = null)
		{
			var col = new GridViewBrowseColumn(title);

			if (size.HasValue)
				col.Width = size.Value;
			else col.AutoSizeMode = BestFitColumnMode.DisplayedCells;

			ColumnHeaderList.Add(col);
		}

		#endregion

		#region private List<PropertyInfo> GetTypeProperties()
		/// <summary>
		/// Получает свойства типа, на основе которых будут созданы колонки 
		/// </summary>
		/// <returns></returns>
		private List<PropertyInfo> GetTypeProperties()
		{
			//определение типа
			var type = typeof(T);
			//определение своиств, имеющих атрибут "отображаемое в списке"
			var properties =
				type.GetProperties().Where(p => p.GetCustomAttributes(typeof(ListViewDataAttribute), false).Length != 0).ToList();

			//поиск своиств у которых задан порядок отображения
			//своиства, имеющие порядок отображения
			var orderedProperties = new Dictionary<int, PropertyInfo>();
			//своиства, НЕ имеющие порядок отображения
			var unOrderedProperties = new List<PropertyInfo>();
			foreach (var propertyInfo in properties)
			{
				var lvda = (ListViewDataAttribute)
					propertyInfo.GetCustomAttributes(typeof(ListViewDataAttribute), false).First();
				if (lvda.Order > 0) orderedProperties.Add(lvda.Order, propertyInfo);
				else unOrderedProperties.Add(propertyInfo);
			}

			var ordered = orderedProperties.OrderBy(p => p.Key).ToList();

			properties.Clear();
			properties.AddRange(ordered.Select(keyValuePair => keyValuePair.Value));
			properties.AddRange(unOrderedProperties);

			return properties;

		}
		#endregion

		public virtual void SetItemsArray(T[] itemsArray)
		{
			if(itemsArray == null)
				throw new ArgumentNullException("itemsArray", "itemsArray can't be null");
			//очищение предварительной коллекции элементов
			_cells.Clear();
			_items.Clear();
			_items.AddRange(itemsArray);

			try
			{
				AddItems(itemsArray);
				SetItemsColor();
				SetTotalText();

			}
			catch (Exception ex)
			{
				Program.Provider.Logger.Log("Error while deleting data", ex);
				return;
			}
		}


		#region public virtual T[] GetItemsArray()
		/// <summary>
		/// Метод возвращает массив директив
		/// </summary>
		/// <returns>Массив директив</returns>
		public virtual T[] GetItemsArray()
		{
			return _items.ToArray();
		}
		#endregion

		#region public CustomCell CreateRow(string text, object tag)

		public CustomCell CreateRow(string text, object tag, Color? foreColor = null)
		{
			return new CustomCell()
			{
				Text = text,
				Tag = tag,
				ForeColor = foreColor
			};
		}

		#endregion

		#region private void AddItems(T[] itemsArray)
		/// <summary>
		/// Добавляет элементы в ListView
		/// </summary>
		/// <param name="itemsArray"></param>
		private void AddItems(T[] itemsArray)
		{
			try
			{
				var temp = new List<GridViewDataRowInfo>();
				foreach (var item in itemsArray)
				{
					var i = 0;
					var rowInfo = new GridViewDataRowInfo(this.radGridView1.MasterView){AllowResize = true};
					rowInfo.Tag = item;

					foreach (var cell in GetListViewSubItems(item))
					{
						rowInfo.Cells[i].Value = cell;

						if(cell.ForeColor.HasValue)
							rowInfo.Cells[i].Style.ForeColor = cell.ForeColor.Value;

						i++;
					}
					temp.Add(rowInfo);
				}

				radGridView1.BeginUpdate();
				radGridView1.Rows.AddRange(temp.ToArray());
				radGridView1.EndUpdate();
			}
			catch (Exception ex)
			{
				Program.Provider.Logger.Log(ex);
			}
		}

		#endregion

		#region protected List<CustomCell> GetListViewSubItems(T item)
		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected virtual List<CustomCell> GetListViewSubItems(T item)
		{
			var properties = GetTypeProperties();

			var subItems = new List<CustomCell>();
			foreach (var property in properties)
			{
				var value = property.GetValue(item, null);
				if (value != null)
				{
					if (property.Name == "CorrectorId")
						value = GlobalObjects.CasEnvironment.GetCorrector((int)value);

					string valueString;
					if (value is DateTime)
						valueString = SmartCore.Auxiliary.Convert.GetDateFormat((DateTime)value);
					else if (value is double)
						valueString = ((double)value).ToString("F");
					else valueString = value.ToString();

					subItems.Add(new CustomCell()
					{
						Text = valueString,
						Tag = value
					});
				}
				else
				{
					subItems.Add(new CustomCell()
					{
						Text = "",
						Tag = ""
					});
				}
			}
			return subItems;
		}

		#endregion

		#region private void SetItemsColor()
		private void SetItemsColor()
		{
			foreach (var item in radGridView1.Rows)
				SetItemColor(item, (T)item.Tag);
		}
		#endregion

		#region protected virtual void SetItemColor(GridViewRowInfo listViewItem, T item)
		protected virtual void SetItemColor(GridViewRowInfo listViewItem, T item)
		{
			var imd = item as IDirective;
			if (imd == null) return;

			foreach (GridViewCellInfo cell in listViewItem.Cells)
			{
				cell.Style.CustomizeFill = true;

				if (imd.NextPerformanceIsBlocked)
				{
					cell.Style.BackColor = Color.FromArgb(Highlight.Grey.Color);
					//cell.Style.ToolTipText = "This performance is involved on Work Package:" + imd.NextPerformances[0].BlockedByPackage.Title;
				}
				else
				{
					if (imd.Condition == ConditionState.Overdue)
						cell.Style.BackColor = Color.FromArgb(Highlight.Red.Color);
					if (imd.Condition == ConditionState.Notify)
						cell.Style.BackColor = Color.FromArgb(Highlight.Yellow.Color);
					if (imd.Condition == ConditionState.NotEstimated)
						cell.Style.BackColor = Color.FromArgb(Highlight.Blue.Color);
				}
			}
		}
		#endregion

		#region private void SetTotalText()
		/// <summary>
		/// Устанавивает информацию об общем количестве элементов в нижней панели
		/// </summary>
		private void SetTotalText()
		{
			label1.Text = "Total: " + radGridView1.Rows.Count;
		}

		#endregion

		#region private void Load()

		private void FirstLoad()
		{
			try
			{
				SetHeaders();
				radGridView1.Columns.AddRange(ColumnHeaderList.ToArray());
			}
			catch (Exception ex)
			{
				Program.Provider.Logger.Log("Error while building control", ex);
				return;
			}
		}

		#endregion
	}


	public class CustomCell
	{
		public string Text { get; set; }
		public object Tag { get; set; }
		public Color? ForeColor { get; set; }

		#region Overrides of Object

		public override string ToString()
		{
			return Text;
		}

		#endregion
	}
}