using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class AdvertisementTabValidator : BaseValidator<AdvertisementTabResponseFilterDso, AdvertisementTabValidatorStates>, ITValidator
    {
        public AdvertisementTabValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(AdvertisementTabValidatorStates.IsActive, new LambdaCondition<AdvertisementTabResponseFilterDso>(nameof(AdvertisementTabValidatorStates.IsActive), context => IsActive(context), "AdvertisementTab is not active"));
        }

        private bool IsActive(AdvertisementTabResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  AdvertisementTabValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}