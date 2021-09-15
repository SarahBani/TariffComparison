using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using TariffComparison.Model;
using Test.Common;

namespace Test.UnitTests
{
    [TestFixtureSource(typeof(TestCases), nameof(TestCases.CalculationModels))]
    public class CalculationModelTest
    {

        #region Properties

        private CalculationModel _sut;
        private TariffType _type;

        #endregion /Properties

        #region Constructors

        // since decimal is not primitive, we should define arguments as double & then convert them to decimal
        public CalculationModelTest(double monthlyBaseCosts, double kWhCosts)
        {
            this._type = TariffType.Basic;
            this._sut = new CalculationModel((decimal)monthlyBaseCosts, (decimal)kWhCosts);
        }

        public CalculationModelTest(double exceededKWhConsumption, double notExceededCosts, double additionalExceededKWhCosts)
        {
            this._type = TariffType.Packaged;
            this._sut = new CalculationModel((decimal)exceededKWhConsumption, (decimal)notExceededCosts, (decimal)additionalExceededKWhCosts);
        }

        #endregion /Constructors

        #region Methods

        [Test]
        [TestCaseSource(typeof(TestCases), nameof(TestCases.ConsumptionAnnualCosts))]
        public void GetAnnualCosts_ValidConsumption(int kWhConsumption, (decimal Basic, decimal Packaged) annualCosts)
        {
            // Arrange
            decimal expectedResult = this._type switch
            {
                TariffType.Basic => annualCosts.Basic,
                TariffType.Packaged => annualCosts.Packaged,
                _ => 0
            };

            // Act
            decimal actualResult = this._sut.GetAnnualCosts(kWhConsumption);

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        [TestCaseSource(typeof(TestCases), nameof(TestCases.InvalidCalculationModels1))]
        public void Constructor_InvalidData1(decimal monthlyBaseCosts, decimal kWhCosts)
        {
            Assert.Throws<ValidationException>(() => new CalculationModel(monthlyBaseCosts, kWhCosts));
        }

        [Test]
        [TestCaseSource(typeof(TestCases), nameof(TestCases.InvalidCalculationModels2))]
        public void Constructor_InvalidData2(decimal exceededKWhConsumption, decimal notExceededCosts, decimal additionalExceededKWhCosts)
        {
            Assert.Throws<ValidationException>(() => new CalculationModel(exceededKWhConsumption, notExceededCosts, additionalExceededKWhCosts));
        }

        [Test]
        public void GetAnnualCosts_NegativeConsumption()
        {
            Assert.Throws<ArgumentException>(() => this._sut.GetAnnualCosts(-1));
        }       

        #endregion /Methods

    }
}
