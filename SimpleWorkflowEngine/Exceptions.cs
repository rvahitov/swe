using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

namespace SimpleWorkflowEngine
{
    [Serializable]
    public class WorkflowException : Exception
    {
        public WorkflowException()
        {
        }

        public WorkflowException(string message) : base(message)
        {
        }

        public WorkflowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorkflowException([NotNull] SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class FlagAllReadyExistsException : WorkflowException
    {
        public FlagAllReadyExistsException(string flagsGroup, string flag)
            : base($"Flag '{flag}' all ready exists in group '{flagsGroup}'.")
        {
            Flag = flag;
            FlagsGroup = flagsGroup;
        }

        public FlagAllReadyExistsException(string message, string flagsGroup, string flag) : base(message)
        {
            Flag = flag;
            FlagsGroup = flagsGroup;
        }

        public FlagAllReadyExistsException(string message, Exception innerException, string flagsGroup, string flag)
            : base(message, innerException)
        {
            Flag = flag;
            FlagsGroup = flagsGroup;
        }

        protected FlagAllReadyExistsException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            FlagsGroup = info.GetString(nameof(FlagsGroup));
            Flag = info.GetString(nameof(Flag));
        }

        public string FlagsGroup { get; }
        public string Flag { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FlagsGroup), FlagsGroup);
            info.AddValue(nameof(Flag), Flag);
        }
    }

    [Serializable]
    public class FlagNotExistsException : WorkflowException
    {
        public FlagNotExistsException(string flagsGroup, string flag)
        {
            Flag = flag;
            FlagsGroup = flagsGroup;
        }

        public FlagNotExistsException(string message, string flagsGroup, string flag) : base(message)
        {
            Flag = flag;
            FlagsGroup = flagsGroup;
        }

        public FlagNotExistsException(string message, Exception innerException, string flagsGroup, string flag)
            : base(message, innerException)
        {
            Flag = flag;
            FlagsGroup = flagsGroup;
        }

        protected FlagNotExistsException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            FlagsGroup = info.GetString(nameof(FlagsGroup));
            Flag = info.GetString(nameof(Flag));
        }

        public string FlagsGroup { get; }
        public string Flag { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FlagsGroup), FlagsGroup);
            info.AddValue(nameof(Flag), Flag);
        }
    }

    [Serializable]
    public class IllegalTransitionException : WorkflowException
    {
        public IllegalTransitionException(string fromStepId, string toStepId)
            : base($"Transition from step '{fromStepId}' to step '{toStepId}' is illegal.")
        {
            FromStepId = fromStepId;
            ToStepId = toStepId;
        }

        public IllegalTransitionException(string message, string fromStepId, string toStepId) : base(message)
        {
            FromStepId = fromStepId;
            ToStepId = toStepId;
        }

        public IllegalTransitionException(string message, Exception innerException, string fromStepId, string toStepId)
            : base(message, innerException)
        {
            FromStepId = fromStepId;
            ToStepId = toStepId;
        }

        protected IllegalTransitionException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            FromStepId = info.GetString(nameof(FromStepId));
            ToStepId = info.GetString(nameof(ToStepId));
        }

        public string FromStepId { get; }
        public string ToStepId { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FromStepId), FromStepId);
            info.AddValue(nameof(ToStepId), ToStepId);
        }
    }

    [Serializable]
    public class FlagsGroupAllReadyExistsException : WorkflowException
    {
        public FlagsGroupAllReadyExistsException(string flagsGroupId)
            : base($"Flags group '{flagsGroupId}' all ready exists.")
        {
            FlagsGroupId = flagsGroupId;
        }

        public FlagsGroupAllReadyExistsException(string message, string flagsGroupId) : base(message)
        {
            FlagsGroupId = flagsGroupId;
        }

        public FlagsGroupAllReadyExistsException(string message, Exception innerException, string flagsGroupId)
            : base(message, innerException)
        {
            FlagsGroupId = flagsGroupId;
        }

        protected FlagsGroupAllReadyExistsException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            FlagsGroupId = info.GetString(nameof(FlagsGroupId));
        }

        public string FlagsGroupId { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FlagsGroupId), FlagsGroupId);
        }
    }

    [Serializable]
    public class FlagsGroupNotExistsException : WorkflowException
    {
        public FlagsGroupNotExistsException(string flagsGroupId) : base($"Flags group '{flagsGroupId}' is not exists.")
        {
            FlagsGroupId = flagsGroupId;
        }

        public FlagsGroupNotExistsException(string message, string flagsGroupId) : base(message)
        {
            FlagsGroupId = flagsGroupId;
        }

        public FlagsGroupNotExistsException(string message, Exception innerException, string flagsGroupId)
            : base(message, innerException)
        {
            FlagsGroupId = flagsGroupId;
        }

        protected FlagsGroupNotExistsException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            FlagsGroupId = info.GetString(nameof(FlagsGroupId));
        }

        public string FlagsGroupId { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FlagsGroupId), FlagsGroupId);
        }
    }

    [Serializable]
    public class StepAllReadyExistsException : WorkflowException
    {
        public StepAllReadyExistsException(string stepId) : base($"Step '{stepId}' all ready exists.")
        {
            StepId = stepId;
        }

        public StepAllReadyExistsException(string message, string stepId) : base(message)
        {
            StepId = stepId;
        }

        public StepAllReadyExistsException(string message, Exception innerException, string stepId)
            : base(message, innerException)
        {
            StepId = stepId;
        }

        protected StepAllReadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            StepId = info.GetString(nameof(StepId));
        }

        public string StepId { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(StepId), StepId);
        }
    }

    [Serializable]
    public class TransitionAllReadyExistsException : WorkflowException
    {
        public TransitionAllReadyExistsException(string fromStepId, string toStepId)
            : base($"Transition from step '{fromStepId}' to step '{toStepId}' all ready exists.")
        {
            FromStepId = fromStepId;
            ToStepId = toStepId;
        }

        public TransitionAllReadyExistsException(string message, string fromStepId, string toStepId) : base(message)
        {
            FromStepId = fromStepId;
            ToStepId = toStepId;
        }

        public TransitionAllReadyExistsException(string message, Exception innerException, string fromStepId,
            string toStepId) : base(message, innerException)
        {
            FromStepId = fromStepId;
            ToStepId = toStepId;
        }

        protected TransitionAllReadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            FromStepId = info.GetString(nameof(FromStepId));
            ToStepId = info.GetString(nameof(ToStepId));
        }

        public string FromStepId { get; }
        public string ToStepId { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FromStepId), FromStepId);
            info.AddValue(nameof(ToStepId), ToStepId);
        }
    }
}