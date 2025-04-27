using AutoGenerator.Conditions;
using ApiCore.Validators.Conditions;
using System;

namespace ApiCore.Validators
{
    public class ConditionChecker : BaseConditionChecker, IConditionChecker
    {
        private readonly ITFactoryInjector _injector;
        public ITFactoryInjector Injector => _injector;

        public ConditionChecker(ITFactoryInjector injector) : base()
        {
        }
    // الدوال السابقة تبقى كما هي
    }
}