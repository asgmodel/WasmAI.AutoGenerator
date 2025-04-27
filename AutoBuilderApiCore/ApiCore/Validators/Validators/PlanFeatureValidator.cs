using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class PlanFeatureValidator : BaseValidator<PlanFeatureResponseFilterDso, PlanFeatureValidatorStates>, ITValidator
    {
        public PlanFeatureValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(PlanFeatureValidatorStates.IsActive, new LambdaCondition<PlanFeatureResponseFilterDso>(nameof(PlanFeatureValidatorStates.IsActive), context => IsActive(context), "PlanFeature is not active"));
        }

        private bool IsActive(PlanFeatureResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  PlanFeatureValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}