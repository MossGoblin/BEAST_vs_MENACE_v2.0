using System;
using System.Collections.Generic;
using System.Text;

namespace BvM
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class StatusAttribute : Attribute
    {
        public StatusAttribute(ElementStatus status)
        {
            this.Status = status;
        }

        public ElementStatus Status { get; private set; }
    }

    public enum ElementStatus
    {
        ToBeImplemented,
        UnderConstruction,
        Functional,
        Finished
    }
}
