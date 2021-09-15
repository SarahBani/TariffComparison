using System.ComponentModel.DataAnnotations;

namespace TariffComparison.Model
{
    public class Product: BaseModel
    {

        #region Properties

        public string Name { get; init; }

        [Required]
        public CalculationModel CalculationModel { get; init; }

        #endregion /Properties

        #region Constructors

        public Product(string name, CalculationModel calculationModel)
        {
            this.Name = name;
            this.CalculationModel = calculationModel;
            base.Validate();
        }

        #endregion /Constructors  

    }
}
