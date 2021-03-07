using System;

namespace Planner.Domain.ValueObjects
{
    public sealed class Amount
    {
        private decimal _value;

        public Amount(decimal value)
        {
            _value = value;
        }


        public static implicit operator decimal(Amount value)
        {
            return value?._value ?? 0;
        }

        public static Amount operator -(Amount value)
        {
            return new Amount(Math.Abs(value._value) * -1);
        }


        public static implicit operator Amount(decimal value)
        {
            return new Amount(value);
        }

        public static Amount operator +(Amount value, Amount value2)
        {
            return new Amount(value._value + value2._value);
        }

        public static Amount operator -(Amount value, Amount value2)
        {
            return new Amount(value._value - value2._value);
        }

        public static bool operator <(Amount value, Amount value2)
        {
            return value?._value < value2?._value;
        }

        public static bool operator >(Amount value, Amount value2)
        {
            return value?._value > value2?._value;
        }

        public static bool operator <=(Amount value, Amount value2)
        {
            return value?._value <= value2?._value;
        }

        public static bool operator >=(Amount value, Amount value2)
        {
            return value?._value >= value2?._value;
        }

        public static bool operator ==(Amount value, Amount value2)
        {
            return value?._value == value2?._value;
        }

        public static bool operator !=(Amount value, Amount value2)
        {
            return value?._value != value2?._value;
        }

        public override string ToString()
        {
            return _value.ToString();
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

            if (obj is decimal)
            {
                return (decimal)obj == _value;
            }

            return ((Amount)obj)._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }


    }
}
