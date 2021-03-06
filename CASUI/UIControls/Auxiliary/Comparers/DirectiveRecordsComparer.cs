﻿using System;
using System.Collections.Generic;
using CAS.Core.Types.Directives;

namespace CAS.UI.UIControls.Auxiliary.Comparers
{
    /// <summary>
    /// Сравнивалка <see cref="DirectiveRecord"/>
    /// </summary>
    public class DirectiveRecordsComparer : IComparer<DirectiveRecord>
    {
        #region IComparer<BiWeekly> Members

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
        public int Compare(DirectiveRecord x, DirectiveRecord y)
        {
            return (-1)*DateTime.Compare(y.RecordDate, x.RecordDate);
        }

        #endregion
    }
}
