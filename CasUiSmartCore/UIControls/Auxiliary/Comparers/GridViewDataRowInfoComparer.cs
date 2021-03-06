﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CAS.UI.UIControls.NewGrid;
using SmartCore.Entities.Dictionaries;
using Telerik.WinControls.UI;

namespace CAS.UI.UIControls.Auxiliary.Comparers
{
	public class DirectiveGridViewDataRowInfoComparer : GridViewDataRowInfoComparer
	{
		#region Fields
		private const string Pattern = "^\\s*(?<Year>\\d+)\\-(?<Number1>\\d+)\\-(?<Number2>\\d+)?";

		#endregion

		#region Constructor

		/// <summary>
		/// Создает сравнивалку <see cref="ListViewItem"/>
		/// </summary>
		/// <param name="columnIndex"></param>
		/// <param name="sortMultiplier"></param>
		public DirectiveGridViewDataRowInfoComparer(int columnIndex, int sortMultiplier) : base(columnIndex, sortMultiplier)
		{
		}

		#endregion

		#region Methods

		#region IComparer<ListViewItem> Members

		///<summary>
		///Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
		///</summary>
		///
		///<returns>
		///Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.
		///</returns>
		///
		///<param name="y">The second object to compare.</param>
		///<param name="x">The first object to compare.</param>
		public override int Compare(GridViewRowInfo x, GridViewRowInfo y)
		{
			var first = ((CustomCell)x.Cells[ColumnIndex].Tag);
			var second = ((CustomCell)y.Cells[ColumnIndex].Tag);

			if (ColumnIndex == 0)
			{
				Match xMatch = Regex.Match(first.Text, Pattern);
				Match yMatch = Regex.Match(second.Text, Pattern);
				if (xMatch.Success && yMatch.Success)
				{
					int xYear;
					int xNumber1;
					int xNumber2;
					int.TryParse(xMatch.Groups["Year"].Value, out xYear);
					int.TryParse(xMatch.Groups["Number1"].Value, out xNumber1);
					int.TryParse(xMatch.Groups["Number2"].Value, out xNumber2);
					string xParagraphLetter = "";
					if (xMatch.Groups["ParagraphLetter"].Success)
						xParagraphLetter = xMatch.Groups["ParagraphLetter"].Value;
					int xParagraphNumber = 0;
					if (xMatch.Groups["ParagraphNumber"].Success)
						xParagraphNumber = int.Parse(xMatch.Groups["ParagraphNumber"].Value);
					int yYear;
					int yNumber1;
					int yNumber2;
					int.TryParse(yMatch.Groups["Year"].Value, out yYear);
					int.TryParse(yMatch.Groups["Number1"].Value, out yNumber1);
					int.TryParse(yMatch.Groups["Number2"].Value, out yNumber2);
					string yParagraphLetter = "";
					if (yMatch.Groups["ParagraphLetter"].Success)
						yParagraphLetter = yMatch.Groups["ParagraphLetter"].Value;
					int yParagraphNumber = 0;
					if (yMatch.Groups["ParagraphNumber"].Success)
						yParagraphNumber = int.Parse(yMatch.Groups["ParagraphNumber"].Value);

					if (xYear < 100)
						xYear += 1900;
					if (yYear < 100)
						yYear += 1900;
					return (xYear == yYear) ? (xNumber1 == yNumber1) ? (xNumber2 == yNumber2) ? (xParagraphLetter == yParagraphLetter) ? (xParagraphNumber == yParagraphNumber) ? 0 : (xParagraphNumber - yParagraphNumber) : string.Compare(xParagraphLetter, yParagraphLetter) : SortMultiplier * (xNumber2 - yNumber2) : SortMultiplier * (xNumber1 - yNumber1) : SortMultiplier * (xYear - yYear);
				}
				if (xMatch.Success)
					return -1;
				if (yMatch.Success)
					return 1;
				return SortMultiplier * string.Compare(first.Text, second.Text);
			}




			if (first.Tag == null || second.Tag == null)
			{
				return SortMultiplier * string.Compare(first.Text, second.Text);
			}
			if (first.Tag is AtaChapter && second.Tag is AtaChapter)
			{
				return ComparerMethods.ATAComparer(first.Text, second.Text) *
					   SortMultiplier;
			}
			if (first.Tag is DirectiveStatus && second.Tag is DirectiveStatus)
			{
				return ComparerMethods.DirectiveStatusComparer((DirectiveStatus)first.Tag,
																(DirectiveStatus)second.Tag) *
					   SortMultiplier;
			}
			if (first.Tag is DateTime && second.Tag is DateTime)
			{
				DateTime xValue = (DateTime)first.Tag;
				DateTime yValue = (DateTime)second.Tag;
				return SortMultiplier * DateTime.Compare(yValue, xValue);
			}
			if (first.Tag is string && second.Tag is string)
			{
				int i, j;
				if (int.TryParse(first.Text, out i) && int.TryParse(second.Text, out j))
				{
					return SortMultiplier * (i - j);
				}

				return SortMultiplier * string.Compare(first.Text, second.Text);
			}
			if (first.Tag is int && second.Tag is int)
			{
				int xValue = (int)first.Tag;
				int yValue = (int)second.Tag;
				return SortMultiplier * (yValue - xValue);
			}
			if (first.Tag is double && second.Tag is double)
			{
				double xValue = (double)first.Tag;
				double yValue = (double)second.Tag;
				return SortMultiplier * (int)(yValue - xValue);
			}
			if (first.Tag as IComparable != null && (first.Tag.GetType().Equals(second.Tag.GetType())))
			{
				try
				{
					return SortMultiplier * ((IComparable)first.Tag).CompareTo(second.Tag);
				}
				catch
				{
				}
			}
			return SortMultiplier * string.Compare(first.Text, second.Text);



			return base.Compare(x, y);
		}

		#endregion

		#endregion
	}


	public class GridViewDataRowInfoComparer : IComparer<GridViewRowInfo>
	{
		#region Fields

		protected readonly int ColumnIndex;
		protected readonly int SortMultiplier;
		#endregion

		#region Constructor

		/// <summary>
		/// Создает сравнивалку <see cref="ListViewItem"/>
		/// </summary>
		/// <param name="columnIndex"></param>
		/// <param name="sortMultiplier"></param>
		public GridViewDataRowInfoComparer(int columnIndex, int sortMultiplier)
		{
			ColumnIndex = columnIndex;
			SortMultiplier = sortMultiplier;
		}

		#endregion

		#region Methods

		#region IComparer<ListViewItem> Members

		///<summary>
		///Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
		///</summary>
		///
		///<returns>
		///Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.
		///</returns>
		///
		///<param name="y">The second object to compare.</param>
		///<param name="x">The first object to compare.</param>
		public virtual int Compare(GridViewRowInfo x, GridViewRowInfo y)
		{
			var first = ((CustomCell)x.Cells[ColumnIndex].Tag);
			var second = ((CustomCell)y.Cells[ColumnIndex].Tag);

			if (first.Tag == null || second.Tag == null)
			{
				return SortMultiplier * string.Compare(first.Text, second.Text);
			}
			if (first.Tag is AtaChapter && second.Tag is AtaChapter)
			{
				return ComparerMethods.ATAComparer(first.Text, second.Text) *
					   SortMultiplier;
			}
			if (first.Tag is DirectiveStatus && second.Tag is DirectiveStatus)
			{
				return ComparerMethods.DirectiveStatusComparer((DirectiveStatus)first.Tag,
																(DirectiveStatus)second.Tag) *
					   SortMultiplier;
			}
			if (first.Tag is DateTime && second.Tag is DateTime)
			{
				DateTime xValue = (DateTime)first.Tag;
				DateTime yValue = (DateTime)second.Tag;
				return SortMultiplier * DateTime.Compare(yValue, xValue);
			}
			if (first.Tag is string && second.Tag is string)
			{
				int i, j;
				if (int.TryParse(first.Text, out i) && int.TryParse(second.Text, out j))
				{
					return SortMultiplier * (i - j);
				}

				return SortMultiplier * string.Compare(first.Text, second.Text);
			}
			if (first.Tag is int && second.Tag is int)
			{
				int xValue = (int)first.Tag;
				int yValue = (int)second.Tag;
				return SortMultiplier * (yValue - xValue);
			}
			if (first.Tag is double && second.Tag is double)
			{
				double xValue = (double)first.Tag;
				double yValue = (double)second.Tag;
				return SortMultiplier * (int)(yValue - xValue);
			}
			if (first.Tag as IComparable != null && (first.Tag.GetType().Equals(second.Tag.GetType())))
			{
				try
				{
					return SortMultiplier * ((IComparable)first.Tag).CompareTo(second.Tag);
				}
				catch
				{
				}
			}
			return SortMultiplier * string.Compare(first.Text, second.Text);
		}

		#endregion

		#endregion
	}
}