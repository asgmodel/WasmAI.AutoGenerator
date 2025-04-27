using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Conditions;
using ApiCore.DyModels.Dso.ResponseFilters;
using System;

namespace ApiCore.Validators
{
    public class ModelGatewayValidator : BaseValidator<ModelGatewayResponseFilterDso, ModelGatewayValidatorStates>, ITValidator
    {
        public ModelGatewayValidator(IConditionChecker checker) : base(checker)
        {
        }

        protected override void InitializeConditions()
        {
            _provider.Register(ModelGatewayValidatorStates.IsActive, new LambdaCondition<ModelGatewayResponseFilterDso>(nameof(ModelGatewayValidatorStates.IsActive), context => IsActive(context), "ModelGateway is not active"));
        }

        private bool IsActive(ModelGatewayResponseFilterDso context)
        {
            if (context != null)
            {
                return true;
            }

            return false;
        }
    } //
    //  Base
    public  enum  ModelGatewayValidatorStates //
    { IsActive ,  IsFull ,  IsValid ,  //
    }

}