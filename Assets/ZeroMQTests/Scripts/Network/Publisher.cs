using System.Collections.Generic;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork
{
    ///////////////////////////////////////////////////////////////////////////////
    public class Publisher : Socket
    {
        private readonly Queue<PubSubMessage> OutgoingMessages = new Queue<PubSubMessage>();

        ///////////////////////////////////////////////////////////////////////////////
        public override NetMQSocket Init()
        {
            ZeroMQSocket = new PublisherSocket();
            ZeroMQSocket.Options.SendHighWatermark = 1000;
            return ZeroMQSocket;
        }

        ///////////////////////////////////////////////////////////////////////////////
        public override void Update()
        {
            if (OutgoingMessages.Count > 0)
            {
                PubSubMessage message = OutgoingMessages.Dequeue();
                Debug.Log($"Sending message 'Topic: {message.Topic}, Data: {message.Data}'");
                ZeroMQSocket.SendMoreFrame(message.Topic).SendFrame(message.Data);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public void Publish(string topic, string data)
        {
            OutgoingMessages.Enqueue(new PubSubMessage
            {
                Topic = topic,
                Data = data
            });
        }
    }
}
