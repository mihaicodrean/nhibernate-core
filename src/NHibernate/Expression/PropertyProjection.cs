using System;
using System.Text;

using NHibernate.Type;
using NHibernate.SqlCommand;

namespace NHibernate.Expression
{
	/// <summary>
	/// A property value, or grouped property value
	/// </summary>
	public class PropertyProjection : SimpleProjection
	{
		string propertyName;
		bool grouped;

		protected internal PropertyProjection(string propertyName, bool grouped)
		{
			this.propertyName = propertyName;
			this.grouped = grouped;
		}

		protected internal PropertyProjection(string propertyName)
			: this(propertyName, false)
		{
		}

		public string PropertyName { get { return propertyName; } }

		public override string ToString()
		{
			return propertyName;
		}

		public override bool IsGrouped { get { return grouped; } }

		public override IType[] GetTypes(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			return new IType[] { criteriaQuery.GetType(criteria, propertyName) };
		}

		public override SqlString ToSqlString(ICriteria criteria, int loc, ICriteriaQuery criteriaQuery)
		{
            return new SqlString(new object[] 
            {
			    criteriaQuery.GetColumn(criteria, propertyName),
			    " as y",
			    loc,
			    "_"
            });
		}

		public override SqlString ToGroupSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			if (!grouped)
			{
				return base.ToGroupSqlString(criteria, criteriaQuery);
			}
			else
			{
                return new SqlString(criteriaQuery.GetColumn(criteria, propertyName));
			}
		}
	}
}
using System;
using System.Text;

using NHibernate.Type;
using NHibernate.SqlCommand;

namespace NHibernate.Expression
{
	/// <summary>
	/// A property value, or grouped property value
	/// </summary>
	public class PropertyProjection : SimpleProjection
	{
		string propertyName;
		bool grouped;

		protected internal PropertyProjection(string propertyName, bool grouped)
		{
			this.propertyName = propertyName;
			this.grouped = grouped;
		}

		protected internal PropertyProjection(string propertyName)
			: this(propertyName, false)
		{
		}

		public string PropertyName { get { return propertyName; } }

		public override string ToString()
		{
			return propertyName;
		}

		public override bool IsGrouped { get { return grouped; } }

		public override IType[] GetTypes(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			return new IType[] { criteriaQuery.GetType(criteria, propertyName) };
		}

		public override SqlString ToSqlString(ICriteria criteria, int loc, ICriteriaQuery criteriaQuery)
		{
            return new SqlString(new object[] 
            {
			    criteriaQuery.GetColumn(criteria, propertyName),
			    " as y",
			    loc,
			    "_"
            });
		}

		public override SqlString ToGroupSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			if (!grouped)
			{
				return base.ToGroupSqlString(criteria, criteriaQuery);
			}
			else
			{
                return new SqlString(criteriaQuery.GetColumn(criteria, propertyName));
			}
		}
	}
}
