using System.Collections.Generic;

namespace BusinessRules
{
    public class BaseValidator<T>
    {
        private List<ErrorField> errors = new List<ErrorField>();

        protected void AddError(ErrorField error)
        {
            errors.Add(error);
        }

        public virtual void Validate(T item)
        {
            CheckErrors();
        }

        private void CheckErrors()
        {
            if(errors.Count != 0)
            {
                throw new BusinessException(errors);
            }
        }

    }
}
