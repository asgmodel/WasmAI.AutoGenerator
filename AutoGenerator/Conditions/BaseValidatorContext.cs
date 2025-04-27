namespace AutoGenerator.Conditions
{


    public enum GeneralValidatorStates
    {
        HasId,
        HasName,
        IsEnabled,
        IsDisabled,
        HasOwner,
        IsLinkedToEntity,
        IsActive,
        IsArchived,
        HasCreatedDate,
        HasUpdatedDate,
        HasValidUri,
        HasDescription,
        HasCategory,
        IsVerified,
        HasTags,
        HasPermissions,
        IsInUserClaims,
        HasParent,
        HasChildren,
        HasValidEmail,
        HasPhone,
        IsPublic,
        IsPrivate,
        IsDefault,
        IsRequired,
        IsEditable,
        IsDeletable,
        IsValidState,
        HasValidStatus,
        IsSystemDefined
    }

    public abstract class BaseValidatorContext<TContext, EValidator>: TBaseValidatorContext<TContext, EValidator>
    where TContext : class
    where EValidator : Enum

    {
        public BaseValidatorContext(IBaseConditionChecker checker) : base(checker) { }

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasId)]
        private Task<ConditionResult> ValidateHasId(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "Id", "Id is missing");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasName)]
        private Task<ConditionResult> ValidateHasName(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "Name", "Name is missing");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsEnabled)]
        private Task<ConditionResult> ValidateIsEnabled(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsEnabled", true, "Object is not enabled");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsDisabled)]
        private Task<ConditionResult> ValidateIsDisabled(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsEnabled", false, "Object is not disabled");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasOwner)]
        private Task<ConditionResult> ValidateHasOwner(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "OwnerId", "Owner is missing");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsLinkedToEntity)]
        private Task<ConditionResult> ValidateIsLinkedToEntity(DataFilter<string,TContext> f) =>
            ValidateCollectionNotEmpty(f, "LinkedEntities", "Not linked to any entity");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsActive)]
        private Task<ConditionResult> ValidateIsActive(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsActive", true, "Object is not active");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsArchived)]
        private Task<ConditionResult> ValidateIsArchived(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsArchived", true, "Object is not archived");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasCreatedDate)]
        private Task<ConditionResult> ValidateHasCreatedDate(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "CreatedAt", "Missing created date");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasUpdatedDate)]
        private Task<ConditionResult> ValidateHasUpdatedDate(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "UpdatedAt", "Missing updated date");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasValidUri)]
        private Task<ConditionResult> ValidateHasValidUri(DataFilter<string,TContext> f) =>
            ValidateUri(f, "Uri", "Invalid URI");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasDescription)]
        private Task<ConditionResult> ValidateHasDescription(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "Description", "Description is missing");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasCategory)]
        private Task<ConditionResult> ValidateHasCategory(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "Category", "Category is missing");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsVerified)]
        private Task<ConditionResult> ValidateIsVerified(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsVerified", true, "Object is not verified");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasTags)]
        private Task<ConditionResult> ValidateHasTags(DataFilter<string,TContext> f) =>
            ValidateCollectionNotEmpty(f, "Tags", "Tags are missing");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasPermissions)]
        private Task<ConditionResult> ValidateHasPermissions(DataFilter<string,TContext> f) =>
            ValidateCollectionNotEmpty(f, "Permissions", "Permissions are missing");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsInUserClaims)]
        private Task<ConditionResult> ValidateIsInUserClaims(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "UserClaims", "Not in user claims");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasParent)]
        private Task<ConditionResult> ValidateHasParent(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "ParentId", "Missing parent");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasChildren)]
        private Task<ConditionResult> ValidateHasChildren(DataFilter<string,TContext> f) =>
            ValidateCollectionNotEmpty(f, "Children", "No children found");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasValidEmail)]
        private Task<ConditionResult> ValidateHasValidEmail(DataFilter<string,TContext> f) =>
            ValidateEmail(f, "Email", "Invalid email");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasPhone)]
        private Task<ConditionResult> ValidateHasPhone(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "Phone", "Phone is missing");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsPublic)]
        private Task<ConditionResult> ValidateIsPublic(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsPublic", true, "Not public");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsPrivate)]
        private Task<ConditionResult> ValidateIsPrivate(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsPrivate", true, "Not private");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsDefault)]
        private Task<ConditionResult> ValidateIsDefault(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsDefault", true, "Not default");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsRequired)]
        private Task<ConditionResult> ValidateIsRequired(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsRequired", true, "Not required");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsEditable)]
        private Task<ConditionResult> ValidateIsEditable(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsEditable", true, "Not editable");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsDeletable)]
        private Task<ConditionResult> ValidateIsDeletable(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsDeletable", true, "Not deletable");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsValidState)]
        private Task<ConditionResult> ValidateIsValidState(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "State", "Invalid state");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.HasValidStatus)]
        private Task<ConditionResult> ValidateHasValidStatus(DataFilter<string,TContext> f) =>
            ValidatePropertyExists(f, "Status", "Invalid status");

        [RegisterConditionValidator(typeof(GeneralValidatorStates), GeneralValidatorStates.IsSystemDefined)]
        private Task<ConditionResult> ValidateIsSystemDefined(DataFilter<string,TContext> f) =>
            ValidateBoolProperty(f, "IsSystem", true, "Not system defined");

        // Helpers
        private Task<ConditionResult> ValidatePropertyExists(DataFilter<string,TContext> f, string propertyName, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share);
            return value != null ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(null, errorMsg);
        }

        private Task<ConditionResult> ValidateBoolProperty(DataFilter<string,TContext> f, string propertyName, bool expected, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share) as bool?;
            return value == expected ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(value, errorMsg);
        }

        private Task<ConditionResult> ValidateCollectionNotEmpty(DataFilter<string,TContext> f, string propertyName, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share) as System.Collections.IEnumerable;
            var hasItems = value?.Cast<object>().Any() ?? false;
            return hasItems ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(null, errorMsg);
        }

        private Task<ConditionResult> ValidateUri(DataFilter<string,TContext> f, string propertyName, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share)?.ToString();
            bool valid = Uri.IsWellFormedUriString(value, UriKind.Absolute);
            return valid ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(value, errorMsg);
        }

        private Task<ConditionResult> ValidateEmail(DataFilter<string,TContext> f, string propertyName, string errorMsg)
        {
            var value = f.Share?.GetType().GetProperty(propertyName)?.GetValue(f.Share)?.ToString();
            bool valid = !string.IsNullOrEmpty(value) && System.Text.RegularExpressions.Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return valid ? ConditionResult.ToSuccessAsync(value) : ConditionResult.ToFailureAsync(value, errorMsg);
        }


        protected override void InitializeConditions()
        {
        }
    }



}