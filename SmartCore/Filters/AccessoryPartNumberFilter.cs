using SmartCore.Entities.General.Accessory;
using TextProcessing;

namespace SmartCore.Filters
{
    public class ProductPartNumberFilter : ProductFilter
    {
        #region Fields
        /// <summary>
        /// ����� ��� ����������
        /// </summary>
        private string _mask;
        Pattern _pattern;
        #endregion

        #region Constructors

        #region public ProductPartNumberFilter():this("*")
        /// <summary>
        /// ��������� ������ �� ������-�� ���� �� �������� �����. ����� = "*"
        /// </summary>
        public ProductPartNumberFilter()
            : this("*")
        {
        }
        #endregion

        #region protected ProductPartNumberFilter(string mask)
        /// <summary>
        /// ��������� ������ �� ������-�� ���� �� �������� �����
        /// </summary>
        /// <param name="mask">����� ������</param>
        public ProductPartNumberFilter(string mask)
        {
            Mask = mask;
        }
        #endregion

        #endregion

        #region Properties

        #region public string Mask
        /// <summary>
        /// ����� ��� ����������
        /// </summary>
        public string Mask
        {
            get { return _mask; }
            set
            {
                _mask = value;
                _pattern = new Pattern(_mask);
            }
        }
        #endregion

        #endregion

        #region Methods

        #region public override bool Acceptable(Product item)
        ///<summary>
        /// �����������, �������� �� ������� ��� ������
        ///</summary>
        ///<param name="item">����������� �������</param>
        ///<returns>��������� - �������� �� �������</returns>
        public override bool Acceptable(Product item)
        {
            return _pattern.IsMatch(item.PartNumber);
        }
        #endregion

        #region public override bool Equals(object obj)
        ///<summary>
        ///Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
        ///</returns>
        ///
        ///<param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            return _mask == ((ProductPartNumberFilter)obj)._mask;
        }

        #endregion

        #region public override int GetHashCode()
        ///<summary>
        ///Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        ///</summary>
        ///
        ///<returns>
        ///A hash code for the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion

        #endregion
    }

	public class ProductStandartFilter : ProductFilter
	{
		#region Fields
		/// <summary>
		/// ����� ��� ����������
		/// </summary>
		private string _mask;
		Pattern _pattern;
		#endregion

		#region Constructors

		#region public ProductPartNumberFilter():this("*")
		/// <summary>
		/// ��������� ������ �� ������-�� ���� �� �������� �����. ����� = "*"
		/// </summary>
		public ProductStandartFilter()
			: this("*")
		{
		}
		#endregion

		#region protected ProductPartNumberFilter(string mask)
		/// <summary>
		/// ��������� ������ �� ������-�� ���� �� �������� �����
		/// </summary>
		/// <param name="mask">����� ������</param>
		public ProductStandartFilter(string mask)
		{
			Mask = mask;
		}
		#endregion

		#endregion

		#region Properties

		#region public string Mask
		/// <summary>
		/// ����� ��� ����������
		/// </summary>
		public string Mask
		{
			get { return _mask; }
			set
			{
				_mask = value;
				_pattern = new Pattern(_mask);
			}
		}
		#endregion

		#endregion

		#region Methods

		#region public override bool Acceptable(Product item)
		///<summary>
		/// �����������, �������� �� ������� ��� ������
		///</summary>
		///<param name="item">����������� �������</param>
		///<returns>��������� - �������� �� �������</returns>
		public override bool Acceptable(Product item)
		{
			return _pattern.IsMatch(item.Standart?.ToString() ?? "");
		}
		#endregion

		#region public override bool Equals(object obj)
		///<summary>
		///Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
		///</summary>
		///
		///<returns>
		///true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
		///</returns>
		///
		///<param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj)) return false;
			return _mask == ((ProductStandartFilter)obj)._mask;
		}

		#endregion

		#region public override int GetHashCode()
		///<summary>
		///Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
		///</summary>
		///
		///<returns>
		///A hash code for the current <see cref="T:System.Object"></see>.
		///</returns>
		///<filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		#endregion

		#endregion
	}
}
