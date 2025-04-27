using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class UserModelAiValidator : BaseValidator<UserModelAiResponseFilterDso, UserModelAiValidatorStates>, ITValidator
    {
        public UserModelAiValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(UserModelAiValidatorStates.IsActive, new LambdaCondition<UserModelAiResponseFilterDso>(nameof(UserModelAiValidatorStates.IsActive), context => IsActive(context), "UserModelAi is not active"));
        }

        private bool IsActive(UserModelAiResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  UserModelAiValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}