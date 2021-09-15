using System.Collections.Generic;
using TariffComparison.Model;

namespace Test.Common
{
    /// <summary>
    /// Sample data used for Test Cases & Fixtures
    /// </summary>
    public static class TestCases
    {

        public static decimal MaxAllowedValue = 1000000;

        private static IDictionary<TariffType, object[]> calculationModelData =
            new Dictionary<TariffType, object[]>()
            {
                [TariffType.Basic] = new object[] { 5, .22 }, // base costs per month 5 € + consumption costs 22 cent/kWh
                [TariffType.Packaged] = new object[] { 4000, 800, 0.30 },  // 800 € for up to 4000 kWh/year and above 4000 kWh/year additionally 30 cent/kWh
            };

        public static IDictionary<string, CalculationModel> ProductsData =
            new Dictionary<string, CalculationModel>()
            {
                [TariffType.Basic.GetDescription()] = new CalculationModel(5, .22M),
                [TariffType.Packaged.GetDescription()] = new CalculationModel(4000, 800, 0.30M)
            };

        private static IDictionary<int, (decimal Basic, decimal Packaged)> consumptionAnnualCostsData =
            new Dictionary<int, (decimal Basic, decimal Packaged)>()
            {
                [3500] = (830M, 800M),
                [4500] = (1050M, 950M),
                [6000] = (1380M, 1400M),
                [0] = (5 * 12, 800),
            };

        private static IDictionary<int, IList<(string TariffName, string AnnualCosts)>> comparisionResultData =
            new Dictionary<int, IList<(string TariffName, string AnnualCosts)>>
            {
                [3500] = new List<(string, string)>
                {
                    GetFormattedConsumptionAnnualCosts(3500, TariffType.Packaged),
                    GetFormattedConsumptionAnnualCosts(3500, TariffType.Basic),
                },
                [4500] = new List<(string, string)>
                {
                    GetFormattedConsumptionAnnualCosts(4500, TariffType.Packaged),
                    GetFormattedConsumptionAnnualCosts(4500, TariffType.Basic),
                },
                [6000] = new List<(string, string)>
                {
                    GetFormattedConsumptionAnnualCosts(6000, TariffType.Basic),
                    GetFormattedConsumptionAnnualCosts(6000, TariffType.Packaged),
                },
                [0] = new List<(string, string)>
                {
                    GetFormattedConsumptionAnnualCosts(0, TariffType.Basic),
                    GetFormattedConsumptionAnnualCosts(0, TariffType.Packaged),
                },
            };

        private static IDictionary<int, IList<(string TariffName, string AnnualCosts)>> equalBasicComparisionResultData =
           new Dictionary<int, IList<(string TariffName, string AnnualCosts)>>
           {
               [3500] = new List<(string, string)>
               {
                    GetFormattedConsumptionAnnualCosts(3500, TariffType.Basic),
                    GetFormattedConsumptionAnnualCosts(3500, TariffType.Basic),
               },
               [4500] = new List<(string, string)>
               {
                    GetFormattedConsumptionAnnualCosts(4500, TariffType.Basic),
                    GetFormattedConsumptionAnnualCosts(4500, TariffType.Basic),
               },
               [6000] = new List<(string, string)>
               {
                    GetFormattedConsumptionAnnualCosts(6000, TariffType.Basic),
                    GetFormattedConsumptionAnnualCosts(6000, TariffType.Basic),
               },
               [0] = new List<(string, string)>
               {
                    GetFormattedConsumptionAnnualCosts(0, TariffType.Basic),
                    GetFormattedConsumptionAnnualCosts(0, TariffType.Basic),
               },
           };

        private static IDictionary<int, IList<(string TariffName, string AnnualCosts)>> equalPackagedComparisionResultData =
            new Dictionary<int, IList<(string TariffName, string AnnualCosts)>>
            {
                [3500] = new List<(string, string)>
                {
                    GetFormattedConsumptionAnnualCosts(3500, TariffType.Packaged),
                    GetFormattedConsumptionAnnualCosts(3500, TariffType.Packaged),
                },
                [4500] = new List<(string, string)>
                {
                    GetFormattedConsumptionAnnualCosts(4500, TariffType.Packaged),
                    GetFormattedConsumptionAnnualCosts(4500, TariffType.Packaged),
                },
                [6000] = new List<(string, string)>
                {
                    GetFormattedConsumptionAnnualCosts(6000, TariffType.Packaged),
                    GetFormattedConsumptionAnnualCosts(6000, TariffType.Packaged),
                },
                [0] = new List<(string, string)>
                {
                    GetFormattedConsumptionAnnualCosts(0, TariffType.Packaged),
                    GetFormattedConsumptionAnnualCosts(0, TariffType.Packaged),
                },
            };

        private static (string TariffName, string AnnualCosts) GetFormattedConsumptionAnnualCosts(int consumption, TariffType tariffType)
        {
            return (tariffType.GetDescription(), consumptionAnnualCostsData[consumption].GetValueByIndex((int)tariffType).FormatAnnualCosts());
        }

        public static object[] CalculationModels =
        {
             calculationModelData[TariffType.Packaged],
             calculationModelData[TariffType.Basic],
        };

        public static object[] Products = ProductsData.ToObject();

        public static object[] ConsumptionAnnualCosts = consumptionAnnualCostsData.ToObject();

        public static object[] ComparisionResult = comparisionResultData.ToObject();

        public static object[] EqualBasicComparisionResult = equalBasicComparisionResultData.ToObject();

        public static object[] EqualPackagedComparisionResult = equalPackagedComparisionResultData.ToObject();

        public static object[] InvalidCalculationModels1 =
        {
             new decimal[] { -5, .22M },
             new decimal[] { 5, -.22M },
             new decimal[] {decimal.MaxValue, .22M },
             new decimal[] { 5, decimal.MaxValue },
        };

        public static object[] InvalidCalculationModels2 =
        {
             new decimal[] { -4000, 800, 0.30M },
             new decimal[] { 4000, -800, 0.30M },
             new decimal[] { 4000, 800, -0.30M },
             new decimal[] { decimal.MaxValue, 800, 0.30M },
             new decimal[] { 4000, decimal.MaxValue, 0.30M },
             new decimal[] { 4000, 800, decimal.MaxValue},
        };

    }
}
