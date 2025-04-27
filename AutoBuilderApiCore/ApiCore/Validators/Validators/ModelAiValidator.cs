using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class ModelAiValidator : BaseValidator<ModelAiResponseFilterDso, ModelAiValidatorStates>, ITValidator
    {
        public ModelAiValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(ModelAiValidatorStates.IsActive, new LambdaCondition<ModelAiResponseFilterDso>(nameof(ModelAiValidatorStates.IsActive), context => IsActive(context), "ModelAi is not active"));
        }

        private bool IsActive(ModelAiResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  ModelAiValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}