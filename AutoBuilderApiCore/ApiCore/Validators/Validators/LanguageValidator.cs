using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class LanguageValidator : BaseValidator<LanguageResponseFilterDso, LanguageValidatorStates>, ITValidator
    {
        public LanguageValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(LanguageValidatorStates.IsActive, new LambdaCondition<LanguageResponseFilterDso>(nameof(LanguageValidatorStates.IsActive), context => IsActive(context), "Language is not active"));
        }

        private bool IsActive(LanguageResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  LanguageValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}