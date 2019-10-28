using System.Collections.Generic;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork
{
    ///////////////////////////////////////////////////////////////////////////////
    public class Subscriber : Socket
    {
        private readonly Queue<PubSubMessage> IncomingMessages = new Queue<PubSubMessage>();

        ///////////////////////////////////////////////////////////////////////////////
        public override NetMQSocket Init()
        {
            ZeroMQSocket = new SubscriberSocket();
            ZeroMQSocket.Options.SendHighWatermark = 1000;
            return ZeroMQSocket;
        }

        ///////////////////////////////////////////////////////////////////////////////
        public override void Update()
        {
            if (ZeroMQSocket.TryReceiveFrameString(out string topic))
            {
                if (ZeroMQSocket.TryReceiveFrameString(out string data))
                {
                    PubSubMessage msg = new PubSubMessage
                    {
                        Topic = topic,
                        Data = data
                    };
                    Debug.Log($"Received message 'Topic: {msg.Topic}, Data: {msg.Data}'");
                    IncomingMessages.Enqueue(msg);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public void Subscribe(string topic = "")
        {
            if (ZeroMQSocket != null)
            {
                Debug.Log($"Subscribing to topic {topic}");
                if (ZeroMQSocket is SubscriberSocket subscriber && !subscriber.IsDisposed)
                    subscriber.Subscribe(topic);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public void Unsubscribe(string topic = "")
        {
            if (ZeroMQSocket != null)
            {
                Debug.Log($"Unsubscribing to topic {topic}");
                if (ZeroMQSocket is SubscriberSocket subscriber && !subscriber.IsDisposed)
                    subscriber.Unsubscribe(topic);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public bool TryReceive(out PubSubMessage msg)
        {
            bool isThereOne = IncomingMessages.Count > 0;
            msg = isThereOne ? IncomingMessages.Dequeue() : default;
            return isThereOne;
        }

    }
}
