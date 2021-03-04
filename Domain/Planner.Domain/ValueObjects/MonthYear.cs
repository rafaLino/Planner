using System;

namespace Planner.Domain.ValueObjects
{
    public sealed class MonthYear : IComparable
    {
        private DateTime _date;
        public static MonthYear Now
        {
            get
            {
                DateTime now = DateTime.Now;
                return new MonthYear(now.Year, now.Month);
            }
        }

        public MonthYear(int year, int month)
        {
            _date = new DateTime(year, month, 1);
        }

        public static implicit operator MonthYear(DateTime date)
        {
            return new MonthYear(date.Year, date.Month);
        }

        public static implicit operator DateTime(MonthYear date)
        {
            return date._date;
        }

        public override string ToString()
        {
            return _date.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            return _date.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj is MonthYear monthYear)
            {
                return _date.CompareTo(monthYear._date);
            }
            else if (obj is DateTime dt)
            {
                DateTime date = new DateTime(dt.Year, dt.Month, 1);
                return _date.CompareTo(date);
            }

            throw new ArgumentException("obj is not the same type as this instance.");
        }
    }
}
