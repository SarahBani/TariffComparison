using System;
using System.ComponentModel.DataAnnotations;

namespace TariffComparison.Model
{
    public class CalculationModel : BaseModel
    {

        #region Properties

        private const double _maximum = 1000000;

        /// <summary>
        /// base costs per month in €
        /// </summary>
        [Range(minimum: 0, maximum: _maximum)]
        public decimal MonthlyBaseCosts { get; init; }

        /// <summary>
        /// consumption costs for each kWh in €
        /// </summary>
        [Range(minimum: 0, maximum: _maximum)]
        public decimal KWhCosts { get; init; }

        /// <summary>
        /// consumption amount in kWh/year above which has additional costs
        /// </summary>
        [Range(minimum: 0, maximum: _maximum)]
        public decimal? ExceededKWhConsumption { get; init; }

        /// <summary>
        /// consumption costs for not exceeded Consumption in €
        /// </summary>
        [Range(minimum: 0, maximum: _maximum)]
        public decimal NotExceededCosts { get; init; }

        /// <summary>
        /// consumption costs for each kWh more than ExceededConsumption in €
        /// </summary>
        [Range(minimum: 0, maximum: _maximum)]
        public decimal AdditionalExceededKWhCosts { get; init; }

        private decimal _annualBaseCosts => this.MonthlyBaseCosts * 12;

        #endregion /Properties

        #region Constructors

        /// <summary>
        /// a calculation model based on a base costs per month + consumption costs for each kWh
        /// </summary>
        /// <param name="monthlyBaseCosts">base costs per month in €</param>
        /// <param name="kWhCosts">consumption costs for each kWh in €</param>
        public CalculationModel(decimal monthlyBaseCosts, decimal kWhCosts)
        {
            this.MonthlyBaseCosts = monthlyBaseCosts;
            this.KWhCosts = kWhCosts;
            Validate();
        }

        /// <summary>
        /// a calculation model based on costs for not exceeded and exceeded Consumption
        /// </summary>
        /// <param name="exceededKWhConsumption">consumption amount in kWh/year above which has additional costs</param>
        /// <param name="notExceededCosts">consumption costs for not exceeded Consumption in €</param>
        /// <param name="additionalExceededKWhCosts">consumption costs for each kWh more than ExceededConsumption in €</param>
        public CalculationModel(decimal exceededKWhConsumption, decimal notExceededCosts, decimal additionalExceededKWhCosts)
        {
            this.ExceededKWhConsumption = exceededKWhConsumption;
            this.NotExceededCosts = notExceededCosts;
            this.AdditionalExceededKWhCosts = additionalExceededKWhCosts;
            base.Validate();
        }

        #endregion /Constructors

        #region Methods

        /// <summary>
        /// Annual costs in €
        /// </summary>
        /// <param name="kWhConsumption">Annual Consumption (kWh/year)</param>
        /// <returns></returns>
        public decimal GetAnnualCosts(int kWhConsumption)
        {
            if (kWhConsumption < 0)
            {
                throw new ArgumentException();
            }
            if (!this.ExceededKWhConsumption.HasValue)
            {
                return this._annualBaseCosts + (kWhConsumption * this.KWhCosts);
            }
            else
            {
                decimal exceededCosts = 0;
                if (kWhConsumption > this.ExceededKWhConsumption.Value)
                {
                    exceededCosts = (kWhConsumption - this.ExceededKWhConsumption.Value) * this.AdditionalExceededKWhCosts;
                }
                return this.NotExceededCosts + exceededCosts;
            }
        }

        #endregion /Methods

    }
}
