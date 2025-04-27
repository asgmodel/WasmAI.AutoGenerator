
using System;


namespace AutoGenerator.Conditions
{


    
    public class DataFilter
    {
        public string? Id { get; set; }
        public string? Name { get; set; } = null;
        public object? Share { get; set; }
        public object? Value { get; set; }


        public  IDictionary<string, object>? Items { get; set; }

        public DataFilter() { }
        public DataFilter(string? id) { 
        
         Id = id;
        }
        public DataFilter(DataFilter other)
        {
            Id = other.Id;
            Name = other.Name;
            Share = other.Share;
            Value = other.Value;
            Items = other.Items;
        }

        public  static  implicit operator DataFilter(string? id)
        {
            return new DataFilter(id);
        }

        
    }

    public class DataFilter<T> : DataFilter
    {
        public new T? Value { get; set; }

        public DataFilter() { }

        public DataFilter(DataFilter other) : base(other)
        {
            if (other.Value is T typedValue)
            {
                Value = typedValue;
            }
        }


    }

    public class DataFilter<T, E> : DataFilter<T>
    {
        public new E? Share { get; set; }

        public DataFilter() { }

        public DataFilter(DataFilter other) : base(other)
        {
            if (other.Share is E typedShare)
            {
                Share = typedShare;
            }
        }

        public DataFilter(DataFilter<T> other) : base(other)
        {
            if (other.Share is E typedShare)
            {
                Share = typedShare;
            }
        }


    }

    /// <summary>
    /// Invoice  propegdf
    /// 

    public interface IValidator<TContext>
    {
        Task<bool> Validate(TContext entity);


    }

    public interface ITValidator
    {
        void Register(IBaseConditionChecker checker);
    }

    public abstract class BaseValidator<TContext, EValidator> : IValidator<TContext>, ITValidator
        where TContext : class
        where EValidator : Enum
    {


        protected readonly ConditionProvider<EValidator> _provider;


        protected readonly IBaseConditionChecker _checker;







        public BaseValidator(IBaseConditionChecker checker)
        {
            _provider = new ConditionProvider<EValidator>();
            _checker = checker;



             Initializer();
            _checker.RegisterProvider(_provider);


        }

        public virtual void Register(IBaseConditionChecker checker)
        {
            checker.RegisterProvider(_provider);
        }

        public Task<bool> Validate(TContext entity)
        {
            throw new NotImplementedException();
        }

        abstract protected void InitializeConditions();

        protected virtual void Initializer()
        {
            InitializeConditions();
        }


    }


}