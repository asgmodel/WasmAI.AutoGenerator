using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class CategoryModelValidator : BaseValidator<CategoryModelResponseFilterDso, CategoryModelValidatorStates>, ITValidator
    {
        public CategoryModelValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(CategoryModelValidatorStates.IsActive, new LambdaCondition<CategoryModelResponseFilterDso>(nameof(CategoryModelValidatorStates.IsActive), context => IsActive(context), "CategoryModel is not active"));
        }

        private bool IsActive(CategoryModelResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  CategoryModelValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}