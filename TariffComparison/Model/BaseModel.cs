using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TariffComparison.Model
{
    public abstract class BaseModel
    {

        #region Methods     

        protected void Validate()
        {
            var context = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(this, context, validationResults, true);

            if (!isValid)
            {
                throw new ValidationException();
            }
        }

        #endregion /Methods

    }
}
