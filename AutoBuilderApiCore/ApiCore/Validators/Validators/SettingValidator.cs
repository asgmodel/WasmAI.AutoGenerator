using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class SettingValidator : BaseValidator<SettingResponseFilterDso, SettingValidatorStates>, ITValidator
    {
        public SettingValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(SettingValidatorStates.IsActive, new LambdaCondition<SettingResponseFilterDso>(nameof(SettingValidatorStates.IsActive), context => IsActive(context), "Setting is not active"));
        }

        private bool IsActive(SettingResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  SettingValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}