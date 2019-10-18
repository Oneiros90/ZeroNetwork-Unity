using System;
using NetMQ;
using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork
{
    ///////////////////////////////////////////////////////////////////////////////
    public abstract class Socket
    {
        public Network Network { private set; get; }
        public NetMQSocket ZeroMQSocket { protected set; get; }
        public NetworkJob Job { private set; get; }


        ///////////////////////////////////////////////////////////////////////////////
        public void Start(Network network)
        {
            Job = new NetworkJob(this);
            Network = network;
            Network.Start(Job);
        }

        ///////////////////////////////////////////////////////////////////////////////
        public abstract NetMQSocket Init();

        ///////////////////////////////////////////////////////////////////////////////
        public abstract void Update();

        ///////////////////////////////////////////////////////////////////////////////
        public void Connect(string address)
        {
            if (!CheckNetwork())
                return;

            if (ZeroMQSocket != null)
            {
                Debug.Log($"Socket connecting to {address}");
                ZeroMQSocket.Connect(address);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public void Connect(string host, int port) => Connect(Utils.BuildAddress(host, port));

        ///////////////////////////////////////////////////////////////////////////////
        public void Bind(int port)
        {
            if (!CheckNetwork())
                return;

            if (ZeroMQSocket != null)
            {
                string address = Utils.BuildAddress("*", port);
                Debug.Log($"Socket binding to {address}");
                ZeroMQSocket.Bind(address);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public void Stop()
        {
            if (!CheckNetwork())
                return;

            Network.Stop(Job);
        }

        ///////////////////////////////////////////////////////////////////////////////
        private bool CheckNetwork()
        {
            if (Network == null)
            {
                Debug.LogWarning($"Socket is not connected! Have you called the {nameof(Start)}() method?");
                return false;
            }
            return true;
        }

    }
}
