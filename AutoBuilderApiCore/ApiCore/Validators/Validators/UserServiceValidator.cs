using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class UserServiceValidator : BaseValidator<UserServiceResponseFilterDso, UserServiceValidatorStates>, ITValidator
    {
        public UserServiceValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(UserServiceValidatorStates.IsActive, new LambdaCondition<UserServiceResponseFilterDso>(nameof(UserServiceValidatorStates.IsActive), context => IsActive(context), "UserService is not active"));
        }

        private bool IsActive(UserServiceResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  UserServiceValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}