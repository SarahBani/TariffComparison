using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TariffComparison.Services;
using TariffComparison.Model;
using Test.Common;

namespace Test.UnitTests.Services
{
    public class ComparisonServiceTest
    {

        #region Properties

        private ComparisonService _sut;

        #endregion /Properties

        #region Methods

        [OneTimeSetUp]
        public void Setup()
        {
            this._sut = new ComparisonService();
        }

        [Test, Order(1)]
        public void Compare_BeforeBuildProduct_ShouldReturnEmptyList()
        {
            // Arrange
            var expectedResult = new List<(string TariffName, decimal AnnualCosts)>();

            // Act
            var actualResult = this._sut.Compare(3500);

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test, Order(2)]
        [TestCaseSource(typeof(TestCases), nameof(TestCases.Products))]
        public void BuildProduct(string name, CalculationModel calculationModel)
        {
            try
            {
                // Arrange

                // Act
                this._sut.BuildProduct(name, calculationModel);

                //Assert
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.Message);
            }
        }

        [Test, Order(3)] // caution: do not run alone, because Products must be built
        [TestCaseSource(typeof(TestCases), nameof(TestCases.ComparisionResult))]
        public void Compare(int consumption, IList<(string TariffName, string AnnualCosts)> list)
        {
            // Arrange
            var expectedResult = list;

            // Act
            var actualResult = this._sut.Compare(consumption);

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void BuildProduct_NullCalculation()
        {
            Assert.Throws<ValidationException>(() => new Product("", null));
        }

        #endregion /Methods

    }
}
