using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class RequestValidator : BaseValidator<RequestResponseFilterDso, RequestValidatorStates>, ITValidator
    {
        public RequestValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(RequestValidatorStates.IsActive, new LambdaCondition<RequestResponseFilterDso>(nameof(RequestValidatorStates.IsActive), context => IsActive(context), "Request is not active"));
        }

        private bool IsActive(RequestResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  RequestValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}