using AutoGenerator;
using AutoGenerator.Conditions;
using AutoGenerator.Notifications;
using AutoMapper;
using LAHJAAPI.Data;
using System;

namespace ApiCore.Validators.Conditions
{
    public class TFactoryInjector : TBaseFactoryInjector, ITFactoryInjector
    {
        private readonly DataContext _context;
        public TFactoryInjector(IMapper mapper, IAutoNotifier notifier, DataContext context) : base(mapper, notifier)
        {
            _context = context;
        }

        public DataContext Context => _context;
    // يمكنك حقن اي طبقة
    }
}