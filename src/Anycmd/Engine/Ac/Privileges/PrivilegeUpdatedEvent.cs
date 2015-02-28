﻿
namespace Anycmd.Engine.Ac.Privileges
{
    using Events;

    public class PrivilegeUpdatedEvent : DomainEvent
    {
        public PrivilegeUpdatedEvent(IAcSession acSession, PrivilegeBase source, IPrivilegeUpdateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        public IPrivilegeUpdateIo Output { get; private set; }
    }
}