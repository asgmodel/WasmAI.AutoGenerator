using AutoGenerator.Data;
using AutoGenerator.Helper.Translation;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Threading.Tasks; 

namespace AutoGenerator.Conditions
{
    public class ConditionResult
    {
        public bool? Success { get; set; }
        public object? Result { get; set; }
        public string? Message { get; set; }

        // Constructor to initialize the ConditionResult
        public ConditionResult(bool success, object? result, string message = "")
        {
            Success = success;
            Result = result;
            Message = message;
        }

        // Static method to create a success result (non-async)
        public static ConditionResult ToSuccess(object? result, string message = "")
        {
            return new ConditionResult(true, result, message);
        }

        // Static method to create a failure result (non-async)
        public static ConditionResult ToFailure(object? result, string message = "")
        {
            return new ConditionResult(false, result, message);
        }

        // Static method to create an error result (non-async)
        public static ConditionResult ToError(string message)
        {
            return new ConditionResult(false, null, message);
        }

        // Static async method to create a success result (Task-based)
        public static Task<ConditionResult> ToSuccessAsync(object? result, string message = "")
        {
            return Task.FromResult(ToSuccess(result, message));
        }

        // Static async method to create a failure result (Task-based)
        public static Task<ConditionResult> ToFailureAsync(object? result, string message = "")
        {
            return Task.FromResult(ToFailure(result, message));
        }

        // Static async method to create an error result (Task-based)
        public static Task<ConditionResult> ToErrorAsync(string message)
        {
            return Task.FromResult(ToError(message));
        }

        // Optionally override ToString() for better debug output
        public override string ToString()
        {
            return $"Success: {Success}, Message: {Message}, Result: {Result}";
        }
    }



    public interface ICondition
    {
        string Name { get; }
        string? ErrorMessage { get; }
    

        Task<ConditionResult> Evaluate(object context);
    }

    // Implementations
    public abstract class BaseCondition : ICondition
    {
        public string Name { get; protected set; }
        public string? ErrorMessage { get; protected set; }

        public BaseCondition(string name, string? errorMessage = null)
        {
            Name = name;
            ErrorMessage = errorMessage;
        }

        // œ«·… €Ì—  “«„‰Ì… ›ﬁÿ
        public abstract Task<ConditionResult> Evaluate(object context);

        
    }

    // Implementation of async evaluation with ConditionResult
    public class LambdaCondition<T> : BaseCondition
    {
        private readonly Func<T, Task<ConditionResult>> _predicate;

        public LambdaCondition(string name, Func<T, object> predicate, string? errorMessage = null)
            : base(name, errorMessage)
        {

            _predicate = ConvertToConditionResult(predicate,errorMessage);
        }

        public LambdaCondition(string name, Func<T, ConditionResult> predicate, string? errorMessage = null)
            : base(name, errorMessage)
        {
            _predicate = ConvertToConditionResult(predicate);
        }

        public LambdaCondition(string name, Func<T, Task<ConditionResult>> predicate, string? errorMessage = null)
            : base(name, errorMessage)
        {
            _predicate =predicate;
        }

        private static Func<T, Task<ConditionResult>> ConvertToConditionResult(Func<T, object> predicate,string errorMessage)
        {

            if (predicate == null)
            {

                throw new ArgumentNullException(nameof(predicate));

            }
            return (T context) =>
            {
                var result = predicate(context);
                if (result is bool flag)

                    return Task.FromResult(new ConditionResult(flag, result, errorMessage));
                
                return Task.FromResult(new ConditionResult(false, result, errorMessage));
            };
        }

        private static Func<T, Task<ConditionResult>> ConvertToConditionResult(Func<T, ConditionResult> predicate)
        {

            if (predicate == null)
            {

                throw new ArgumentNullException(nameof(predicate));

            }
            return (T context) =>
            {
             
                return Task.FromResult(predicate(context));
            };
        }



        public override async Task<ConditionResult> Evaluate(object context) 
        {
            try
            {
                if (context is T typedContext)
                {
                    ConditionResult result = await _predicate(typedContext);
                    return result;
                }
                else if (context is string str)
                {
                    var df = new DataFilter(str);
                  
                    var result = await _predicate((T)(object)df);
                    return result;
                }
                else if(context==null&& typeof(T) ==typeof(DataFilter))

                {

                    var dfn = new DataFilter();
                    var result = await _predicate((T)(object)dfn);
                    return result;

                }

                return new ConditionResult(false, null, $"Invalid context type: {context.GetType().Name}, expected {typeof(T).Name}");
            }
            catch (Exception ex)
            {
                return new ConditionResult(false, null, $"An error occurred: {ex.Message}");
            }
        }
    }

  
}
