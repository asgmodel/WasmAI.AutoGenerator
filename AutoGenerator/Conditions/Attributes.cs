
using System;
namespace AutoGenerator.Conditions;



   

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RegisterConditionValidatorAttribute : Attribute
    {
        public object? State { get; }
        public Type? EnumType { get; }
        public string? ErrorMessage { get; }
        public object? Value { get; set; } = null;
        public bool IsCachability { get; set; }

        public RegisterConditionValidatorAttribute(Type enumType, object state, string? errorMessage="", bool isCachability = true)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("EnumType must be an enumeration type");

            EnumType = enumType;
            State = state;
            ErrorMessage = errorMessage;
            IsCachability = isCachability;
        }
    }


[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class BuildConditionValidatorAttribute : Attribute
{
    public object State { get; }
    public Type EnumType { get; }
    public string ErrorMessage { get; }
    public object Value { get; set; } = null;
    public bool IsCachability { get; set; }

    public BuildConditionValidatorAttribute(Type enumType, object state, string errorMessage, bool isCachability = true)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException("EnumType must be an enumeration type");

        EnumType = enumType;
        State = state;
        ErrorMessage = errorMessage;
        IsCachability = isCachability;
    }
}