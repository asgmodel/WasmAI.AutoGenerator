using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class SpaceValidator : BaseValidator<SpaceResponseFilterDso, SpaceValidatorStates>, ITValidator
    {
        public SpaceValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(SpaceValidatorStates.IsActive, new LambdaCondition<SpaceResponseFilterDso>(nameof(SpaceValidatorStates.IsActive), context => IsActive(context), "Space is not active"));
        }

        private bool IsActive(SpaceResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  SpaceValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}