using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class ServiceMethodValidator : BaseValidator<ServiceMethodResponseFilterDso, ServiceMethodValidatorStates>, ITValidator
    {
        public ServiceMethodValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(ServiceMethodValidatorStates.IsActive, new LambdaCondition<ServiceMethodResponseFilterDso>(nameof(ServiceMethodValidatorStates.IsActive), context => IsActive(context), "ServiceMethod is not active"));
        }

        private bool IsActive(ServiceMethodResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  ServiceMethodValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}