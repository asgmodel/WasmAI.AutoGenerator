using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class ServiceValidator : BaseValidator<ServiceResponseFilterDso, ServiceValidatorStates>, ITValidator
    {
        public ServiceValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(ServiceValidatorStates.IsActive, new LambdaCondition<ServiceResponseFilterDso>(nameof(ServiceValidatorStates.IsActive), context => IsActive(context), "Service is not active"));
        }

        private bool IsActive(ServiceResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  ServiceValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}