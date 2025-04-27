using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class DialectValidator : BaseValidator<DialectResponseFilterDso, DialectValidatorStates>, ITValidator
    {
        public DialectValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(DialectValidatorStates.IsActive, new LambdaCondition<DialectResponseFilterDso>(nameof(DialectValidatorStates.IsActive), context => IsActive(context), "Dialect is not active"));
        }

        private bool IsActive(DialectResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  DialectValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}