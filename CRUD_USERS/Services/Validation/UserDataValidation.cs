using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CRUD_USERS.Services.Validation
{
    public class UserDataValidation
    {
        public void Validate(object model)
        {
            string erroMessage = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, results, true);
            if(isValid==false)
            {
                foreach (var item in results)
                    erroMessage += "- " + item.ErrorMessage + "\n";
                throw new Exception(erroMessage);
            }
        }
    }
}
