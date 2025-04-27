using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class InvoiceValidator : BaseValidator<InvoiceResponseFilterDso, InvoiceValidatorStates>, ITValidator
    {
        public InvoiceValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(InvoiceValidatorStates.IsActive, new LambdaCondition<InvoiceResponseFilterDso>(nameof(InvoiceValidatorStates.IsActive), context => IsActive(context), "Invoice is not active"));
        }

        private bool IsActive(InvoiceResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  InvoiceValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}