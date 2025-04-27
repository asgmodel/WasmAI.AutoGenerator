using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class FAQItemValidator : BaseValidator<FAQItemResponseFilterDso, FAQItemValidatorStates>, ITValidator
    {
        public FAQItemValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(FAQItemValidatorStates.IsActive, new LambdaCondition<FAQItemResponseFilterDso>(nameof(FAQItemValidatorStates.IsActive), context => IsActive(context), "FAQItem is not active"));
        }

        private bool IsActive(FAQItemResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  FAQItemValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}