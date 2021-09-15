using System.Collections.Generic;
using System.Linq;
using TariffComparison.Model;

namespace TariffComparison.Services
{
    public class ComparisonService
    {

        #region Properties

        private IList<Product> _products;

        #endregion /Properties

        #region Constructors

        public ComparisonService()
        {
            this._products = new List<Product>();
        }

        #endregion /Constructors

        #region Methods

        public void BuildProduct(string name, CalculationModel calculationModel)
        {
            this._products.Add(new Product(name, calculationModel));
        }

        /// <summary>
        /// Calculare the built products annual costs based the specific consumption 
        /// & return back a list of their TariffName and AnnualCosts in ascending order
        /// </summary>
        /// <param name="consumption"></param>
        /// <returns></returns>
        public IList<(string TariffName, string AnnualCosts)> Compare(int consumption)
        {
            return this._products
                    .Select(q =>
                    (
                        q.Name,
                        AnnualCosts: q.CalculationModel.GetAnnualCosts(consumption)
                    ))
                    .OrderBy(q => q.AnnualCosts)
                    .Select(q => 
                    (
                        TariffName: q.Name,
                        AnnualCosts: $"{q.AnnualCosts.ToString("0.##")} (€/year)"
                    ))
                    .ToList();
        }

        #endregion /Methods

    }
}
