namespace AutoGenerator.Conditions
{
    public enum UserStatus
    {
        Active,
        Suspended,
        Pending,
        Deactivated,
        Locked
    }

    public enum PermissionStatus
    {
        CanEdit,
        CanAdd,
        CanDelete,
        CanView,
        NoPermission
    }

    public enum OwnershipStatus
    {
        Owner,
        NonOwner
    }

    public enum EntityRelationStatus
    {
        HasParent,
        HasChildren,
        IsLinkedToEntity,
        IsNotLinkedToEntity
    }

    public enum EntityRecordStatus
    {
        Active,
        Inactive,
        Archived,
        Suspended,
        Deleted
    }

    public enum ValidationStatus
    {
        IsVerified,
        NotVerified,
        IsValid,
        Invalid
    }

    public enum CommunicationStatus
    {
        CanCommunicate,
        CannotCommunicate
    }

    public enum RecordActionStatus
    {
        CanBeEdited,
        CanBeAdded,
        CanBeDeleted,
        CannotBeEdited,
        CannotBeAdded,
        CannotBeDeleted
    }

    public enum EntityState
    {
        Initialized,
        InProgress,
        Completed,
        Failed
    }

    public enum UserActivityStatus
    {
        IsActive,
        IsInactive,
        IsOnline,
        IsOffline
    }
}
