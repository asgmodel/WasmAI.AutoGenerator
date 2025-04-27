using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class AuthorizationSessionValidator : BaseValidator<AuthorizationSessionResponseFilterDso, AuthorizationSessionValidatorStates>, ITValidator
    {
        public AuthorizationSessionValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(AuthorizationSessionValidatorStates.IsActive, new LambdaCondition<AuthorizationSessionResponseFilterDso>(nameof(AuthorizationSessionValidatorStates.IsActive), context => IsActive(context), "AuthorizationSession is not active"));
        }

        private bool IsActive(AuthorizationSessionResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  AuthorizationSessionValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}