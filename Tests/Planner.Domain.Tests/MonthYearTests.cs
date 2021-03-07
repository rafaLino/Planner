using Planner.Domain.ValueObjects;
using System;
using Xunit;

namespace Planner.Domain.Tests
{
    public class MonthYearTests
    {

        [Fact]
        public void Should_MonthYear_Be_Created()
        {
            MonthYear monthYearOne = new MonthYear(2021, 3);
            MonthYear monthYearSecond = DateTime.Now;


            Assert.NotNull(monthYearOne);
            Assert.NotNull(monthYearSecond);
        }

        [Fact]
        public void Should_MonthYear_Now_Created()
        {
            MonthYear monthYear = MonthYear.Now;

            Assert.NotNull(monthYear);
        }

        [Fact]
        public void Should_MonthYear_Show_As_DateTime()
        {
            DateTime dateTime = MonthYear.Now;
            MonthYear monthYear = DateTime.Now;

            Assert.Equal(monthYear.ToString(), dateTime.ToString());
        }

        [Fact]
        public void Should_MonthYear_Comparable_To_DateTime_Equals()
        {
            DateTime dateTime = DateTime.Now;
            MonthYear monthYear = new MonthYear(dateTime.Year, dateTime.Month);

            bool assertion = monthYear.CompareTo(dateTime) == 0;

            Assert.True(assertion);
        }

        [Fact]
        public void Should_MonthYear_Comparable_To_DateTime_Greater()
        {
            MonthYear monthYear = new MonthYear(2021, 3);
            DateTime dateTime = new DateTime(2021, 2, 1);

            bool assertion = monthYear.CompareTo(dateTime) == 1;

            Assert.True(assertion);
        }

        [Fact]
        public void Should_MonthYear_Comparable_To_DateTime_Lesser()
        {
            MonthYear monthYear = new MonthYear(2021, 2);
            DateTime dateTime = new DateTime(2021, 3, 1);

            bool assertion = monthYear.CompareTo(dateTime) == -1;

            Assert.True(assertion);
        }

        [Fact]
        public void Should_MonthYear_Comparable_Equals()
        {
            MonthYear monthYear = new MonthYear(2021, 3);
            MonthYear monthYearTwo = new MonthYear(2021, 3);

            bool assertion = monthYear.CompareTo(monthYearTwo) == 0;

            Assert.True(assertion);
        }

        [Fact]
        public void Should_MonthYear_Comparable_Greater()
        {
            MonthYear monthYear = new MonthYear(2021, 3);
            MonthYear monthYearTwo = new MonthYear(2021, 2);

            bool assertion = monthYear.CompareTo(monthYearTwo) == 1;

            Assert.True(assertion);
        }

        [Fact]
        public void Should_MonthYear_Comparable_Lesser()
        {
            MonthYear monthYear = new MonthYear(2021, 2);
            MonthYear monthYearTwo = new MonthYear(2021, 3);
            bool assertion = monthYear.CompareTo(monthYearTwo) == -1;

            Assert.True(assertion);
        }

        [Fact]
        public void Should_Throw_Exception_Give_Unknown_Type_To_Compare()
        {
            MonthYear monthYear = MonthYear.Now;
            string datetime = MonthYear.Now.ToString();

            Assert.Throws<ArgumentException>(() => monthYear.CompareTo(datetime));

        }

        [Fact]
        public void Should_MonthYear_Be_Equals()
        {
            MonthYear monthYearOne = new MonthYear(2021, 3);
            MonthYear monthYearSecond = new MonthYear(2021, 3);
            MonthYear monthYearThree = monthYearOne;

            Assert.True(monthYearOne.Equals(monthYearSecond));
            Assert.True(monthYearOne.Equals(monthYearThree));
            Assert.True(monthYearOne.GetHashCode() == monthYearThree.GetHashCode());
        }

        [Fact]
        public void Should_MonthYear_Not_Be_Equals()
        {
            MonthYear monthYearOne = new MonthYear(2021, 3);
            MonthYear monthYearSecond = new MonthYear(2021, 5);
            MonthYear monthYearThree = null;

            Assert.False(monthYearOne.Equals(monthYearSecond));
            Assert.False(monthYearOne.Equals(monthYearThree));
        }
    }
}
