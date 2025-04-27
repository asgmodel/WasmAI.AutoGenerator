using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class SubscriptionValidator : BaseValidator<SubscriptionResponseFilterDso, SubscriptionValidatorStates>, ITValidator
    {
        public SubscriptionValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(SubscriptionValidatorStates.IsActive, new LambdaCondition<SubscriptionResponseFilterDso>(nameof(SubscriptionValidatorStates.IsActive), context => IsActive(context), "Subscription is not active"));
        }

        private bool IsActive(SubscriptionResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  SubscriptionValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}