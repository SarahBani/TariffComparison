using NUnit.Framework;
using System;
using System.Collections.Generic;
using TariffComparison.Services;
using Test.Common;

namespace Test.IntegrationTests.Services
{
    public class ComparisonServiceTest
    {

        #region Properties

        private ComparisonService _sut;

        #endregion /Properties

        #region Methods

        [SetUp]
        public void Setup()
        {
            this._sut = new ComparisonService();
        }

        [Test]
        [TestCaseSource(typeof(TestCases), nameof(TestCases.ComparisionResult))]
        public void BuildProductsAndCompare(int consumption, IList<(string TariffName, string AnnualCosts)> comparisonResult)
        {
            try
            {
                var expectedResult = comparisonResult;
                // Arrange

                // Act
                foreach (var (key, value) in TestCases.ProductsData)
                {
                    this._sut.BuildProduct(key, value);
                }
                var actualResult = this._sut.Compare(consumption);

                //Assert
                Assert.AreEqual(actualResult, expectedResult);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCaseSource(typeof(TestCases), nameof(TestCases.EqualBasicComparisionResult))]
        public void BuildEqualBasicProductsAndCompare(int consumption, 
            IList<(string TariffName, string AnnualCosts)> comparisonResult)
        {
            try
            {
                var expectedResult = comparisonResult;
                // Arrange

                // Act
                var calculationModel = TestCases.ProductsData[TariffType.Basic.GetDescription()];
                this._sut.BuildProduct(TariffType.Basic.GetDescription(), calculationModel);
                this._sut.BuildProduct(TariffType.Basic.GetDescription(), calculationModel);

                var actualResult = this._sut.Compare(consumption);

                //Assert
                Assert.AreEqual(actualResult, expectedResult);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCaseSource(typeof(TestCases), nameof(TestCases.EqualPackagedComparisionResult))]
        public void BuildEqualPackagedProductsAndCompare(int consumption, 
            IList<(string TariffName, string AnnualCosts)> comparisonResult)
        {
            try
            {
                var expectedResult = comparisonResult;
                // Arrange

                // Act
                var calculationModel = TestCases.ProductsData[TariffType.Packaged.GetDescription()];
                this._sut.BuildProduct(TariffType.Packaged.GetDescription(), calculationModel);
                this._sut.BuildProduct(TariffType.Packaged.GetDescription(), calculationModel);

                var actualResult = this._sut.Compare(consumption);

                //Assert
                Assert.AreEqual(actualResult, expectedResult);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.Message);
            }
        }

        #endregion /Methods

    }
}
