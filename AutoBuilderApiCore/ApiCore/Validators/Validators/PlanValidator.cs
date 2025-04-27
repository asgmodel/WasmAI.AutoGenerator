using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class PlanValidator : BaseValidator<PlanResponseFilterDso, PlanValidatorStates>, ITValidator
    {
        public PlanValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(PlanValidatorStates.IsActive, new LambdaCondition<PlanResponseFilterDso>(nameof(PlanValidatorStates.IsActive), context => IsActive(context), "Plan is not active"));
        }

        private bool IsActive(PlanResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  PlanValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}