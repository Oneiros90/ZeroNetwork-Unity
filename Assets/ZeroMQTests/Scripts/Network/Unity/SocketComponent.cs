using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork.Unity
{
    ///////////////////////////////////////////////////////////////////////////////
    public abstract class SocketComponent : MonoBehaviour
    {
        [Header("Network")]
        [SerializeField]
        private NetworkManager NetworkManager;

        [Header("Connection")]
        [SerializeField]
        private string ConnectionHost = "";

        [SerializeField]
        private int ConnectionPort = 8123;

        [Header("Server")]
        [SerializeField]
        private int ServerPort = 8124;


        private Socket socket;

        public Socket Socket
        {
            get
            {
                if (socket == null)
                    socket = InitSocket();
                return socket;
            }
        }

        public abstract Socket InitSocket();

        public abstract void OnSocketStart();

        public abstract void OnSocketStop();

        ///////////////////////////////////////////////////////////////////////////////
        protected virtual void OnEnable()
        {
            Socket.Start(NetworkManager.Network);
            Socket.Job.Started += () =>
            {
                if (ConnectionHost != string.Empty)
                    Socket.Connect(ConnectionHost, ConnectionPort);
                if (ServerPort > 0)
                    Socket.Bind(ServerPort);
                OnSocketStart();
            };
        }

        ///////////////////////////////////////////////////////////////////////////////
        protected virtual void OnDisable()
        {
            Socket.Stop();
            OnSocketStop();
        }
    }
}