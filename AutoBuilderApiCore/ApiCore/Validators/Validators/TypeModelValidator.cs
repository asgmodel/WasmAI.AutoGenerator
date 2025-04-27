using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class TypeModelValidator : BaseValidator<TypeModelResponseFilterDso, TypeModelValidatorStates>, ITValidator
    {
        public TypeModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(TypeModelValidatorStates.IsActive, new LambdaCondition<TypeModelResponseFilterDso>(nameof(TypeModelValidatorStates.IsActive), context => IsActive(context), "TypeModel is not active"));
        }

        private bool IsActive(TypeModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  TypeModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}