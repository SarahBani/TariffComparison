using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using TariffComparison.Model;
using Test.Common;

namespace Test.UnitTests
{
    public class ProductTest
    {

        #region Methods

        [Test]
        [TestCaseSource(typeof(TestCases), nameof(TestCases.Products))]
        public void Constructor_ValidData(string name, CalculationModel calculationModel)
        {
            try
            {
                // Arrange          

                // Act
                new Product(name, calculationModel);

                //Assert
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Constructor_InvalidCalculationModel()
        {
            Assert.Throws<ValidationException>(() => new Product("", null));
        }

        #endregion /Methods

    }
}
