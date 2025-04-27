using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class CategoryTabValidator : BaseValidator<CategoryTabResponseFilterDso, CategoryTabValidatorStates>, ITValidator
    {
        public CategoryTabValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(CategoryTabValidatorStates.IsActive, new LambdaCondition<CategoryTabResponseFilterDso>(nameof(CategoryTabValidatorStates.IsActive), context => IsActive(context), "CategoryTab is not active"));
        }

        private bool IsActive(CategoryTabResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  CategoryTabValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}