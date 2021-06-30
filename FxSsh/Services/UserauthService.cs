using FxSsh.Messages;
using FxSsh.Messages.Userauth;
using System;
using System.Diagnostics.Contracts;

namespace FxSsh.Services
{
    public class UserauthService : SshService, IDynamicInvoker
    {
        public UserauthService(Session session)
            : base(session)
        {
        }

        public event EventHandler<UserauthArgs> Userauth;

        public event EventHandler<string> Succeed;

        protected internal override void CloseService()
        {
        }

        internal void HandleMessageCore(UserauthServiceMessage message)
        {
            Contract.Requires(message != null);

            this.InvokeHandleMessage(message);
        }

        private void HandleMessage(RequestMessage message)
        {
            switch (message.MethodName)
            {
                case "none":
                    _session.RegisterService(message.ServiceName, new UserauthArgs(_session, message.Username, null));
                    Succeed?.Invoke(this, message.ServiceName);
                    _session.SendMessage(new SuccessMessage());
                    break;
                default:
                    _session.SendMessage(new FailureMessage());
                    break;
            }
        }
    }
}
