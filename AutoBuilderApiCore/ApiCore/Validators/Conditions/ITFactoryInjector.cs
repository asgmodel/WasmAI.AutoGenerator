using AutoGenerator;
using AutoGenerator.Conditions;
using AutoMapper;
using LAHJAAPI.Data;
using System;

namespace ApiCore.Validators.Conditions
{
    public interface ITFactoryInjector : ITBaseFactoryInjector
    {
        public DataContext Context { get; }
    }
}