using AutoGenerator.Conditions;
using ApiCore.Validators.Conditions;
using System;

namespace ApiCore.Validators
{
    public interface IConditionChecker : IBaseConditionChecker
    {
        public ITFactoryInjector Injector { get; }
    }
}