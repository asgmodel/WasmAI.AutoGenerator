using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class EventRequestValidator : BaseValidator<EventRequestResponseFilterDso, EventRequestValidatorStates>, ITValidator
    {
        public EventRequestValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(EventRequestValidatorStates.IsActive, new LambdaCondition<EventRequestResponseFilterDso>(nameof(EventRequestValidatorStates.IsActive), context => IsActive(context), "EventRequest is not active"));
        }

        private bool IsActive(EventRequestResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  EventRequestValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}