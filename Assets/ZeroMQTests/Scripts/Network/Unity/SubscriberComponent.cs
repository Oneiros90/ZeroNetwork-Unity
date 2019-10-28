using System;
using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork.Unity
{
    public class SubscriberComponent : SocketComponent
    {
        [Header("Subscriber")]
        [SerializeField]
        public string Topic = "";

        public event Action<PubSubMessage> OnNewMessage;

        private Subscriber subscriber;

        void Update()
        {
            if (subscriber != null && subscriber.TryReceive(out PubSubMessage msg))
                OnNewMessage.Invoke(msg);
        }

        ///////////////////////////////////////////////////////////////////////////////
        public override Socket InitSocket()
        {
            subscriber = new Subscriber();
            return subscriber;
        }

        ///////////////////////////////////////////////////////////////////////////////
        public override void OnSocketStart()
        {
            subscriber.Subscribe(Topic);
        }

        ///////////////////////////////////////////////////////////////////////////////
        public override void OnSocketStop()
        {
            subscriber.Unsubscribe(Topic);
        }
    }
}