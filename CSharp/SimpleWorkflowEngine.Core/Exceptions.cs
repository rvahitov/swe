using System;
using System.Runtime.Serialization;

namespace SimpleWorkflowEngine.Core
{
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

        protected WorkflowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class FlagAllReadyExistsException : WorkflowException
    {
        public FlagAllReadyExistsException(string flag) : base($"Flag '{flag}' all ready exists")
        {
            Flag = flag;
        }

        public FlagAllReadyExistsException(string message, string flag) : base(message)
        {
            Flag = flag;
        }

        public FlagAllReadyExistsException(string message, Exception innerException, string flag)
            : base(message, innerException)
        {
            Flag = flag;
        }

        protected FlagAllReadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Flag = info.GetString(nameof(Flag));
        }

        public string Flag { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Flag), Flag);
        }
    }

    public class FlagNotExistsException : WorkflowException
    {
        public FlagNotExistsException(string flag) : base($"Flags ${flag} not exists.")
        {
            Flag = flag;
        }

        public FlagNotExistsException(string message, string flag) : base(message)
        {
            Flag = flag;
        }

        public FlagNotExistsException(string message, Exception innerException, string flag)
            : base(message, innerException)
        {
            Flag = flag;
        }

        protected FlagNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Flag = info.GetString(nameof(Flag));
        }

        public string Flag { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Flag), Flag);
        }
    }

    public class FlagsGroupAllReadyExistsException : WorkflowException
    {
        public FlagsGroupAllReadyExistsException(string flagGroupId)
            : base($"Flags group '{flagGroupId}' all ready exists")
        {
            FlagGroupId = flagGroupId;
        }

        public FlagsGroupAllReadyExistsException(string message, string flagGroupId) : base(message)
        {
            FlagGroupId = flagGroupId;
        }

        public FlagsGroupAllReadyExistsException(string message, Exception innerException, string flagGroupId)
            : base(message, innerException)
        {
            FlagGroupId = flagGroupId;
        }

        protected FlagsGroupAllReadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            FlagGroupId = info.GetString(nameof(FlagGroupId));
        }

        public string FlagGroupId { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FlagGroupId), FlagGroupId);
        }
    }

    public class FlagsGroupNotExistsException : WorkflowException
    {
        public FlagsGroupNotExistsException(string flagsGroupId) : base($"Flags group '{flagsGroupId}' not exists")
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

        protected FlagsGroupNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
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

    public class StepNotExistsException : WorkflowException
    {
        public StepNotExistsException(string stepId):base($"Step '{stepId}' not exists.")
        {
            StepId = stepId;
        }

        public StepNotExistsException(string message, string stepId) : base(message)
        {
            StepId = stepId;
        }

        public StepNotExistsException(string message, Exception innerException, string stepId) : base(message, innerException)
        {
            StepId = stepId;
        }

        public string StepId { get; }
    }

    public class IllegalTransitionException : WorkflowException
    {
        public IllegalTransitionException()
        {
        }

        public IllegalTransitionException(string message) : base(message)
        {
        }

        public IllegalTransitionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IllegalTransitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

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