using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class AdvertisementValidator : BaseValidator<AdvertisementResponseFilterDso, AdvertisementValidatorStates>, ITValidator
    {
        public AdvertisementValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(AdvertisementValidatorStates.IsActive, new LambdaCondition<AdvertisementResponseFilterDso>(nameof(AdvertisementValidatorStates.IsActive), context => IsActive(context), "Advertisement is not active"));
        }

        private bool IsActive(AdvertisementResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  AdvertisementValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}