using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class PaymentValidator : BaseValidator<PaymentResponseFilterDso, PaymentValidatorStates>, ITValidator
    {
        public PaymentValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(PaymentValidatorStates.IsActive, new LambdaCondition<PaymentResponseFilterDso>(nameof(PaymentValidatorStates.IsActive), context => IsActive(context), "Payment is not active"));
        }

        private bool IsActive(PaymentResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  PaymentValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}