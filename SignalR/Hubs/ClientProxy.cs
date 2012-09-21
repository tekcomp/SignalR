using System.Dynamic;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    public class ClientProxy : DynamicObject, IClientProxy
    {
        private readonly IConnection _connection;
        private readonly string _hubName;
        private readonly bool _sendToAll;

        public ClientProxy(IConnection connection, string hubName)
            : this(connection, hubName, sendToAll: true)
        {
        }

        public ClientProxy(IConnection connection, string hubName, bool sendToAll)
        {
            _connection = connection;
            _hubName = hubName;
            _sendToAll = sendToAll;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Invoke(binder.Name, args);
            return true;
        }

        public Task Invoke(string method, params object[] args)
        {
            var invocation = new
            {
                Hub = _hubName,
                Method = method,
                Args = args
            };

            if (_sendToAll)
            {
                return _connection.Send(_hubName, invocation);
            }

            var message = new ConnectionMessage(_hubName, invocation, ignoreSender: true);
            return _connection.Send(message);
        }
    }
}
