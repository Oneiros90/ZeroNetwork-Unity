using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork.Unity
{
    public class PublisherComponent : SocketComponent
    {
        [Header("Publisher")]
        [SerializeField]
        public string Topic = "";


        private Publisher publisher;

        ///////////////////////////////////////////////////////////////////////////////
        public void Publish(string data)
        {
            publisher.Publish(Topic, data);
        }

        ///////////////////////////////////////////////////////////////////////////////
        public override Socket InitSocket()
        {
            publisher = new Publisher();
            return publisher;
        }

        ///////////////////////////////////////////////////////////////////////////////
        public override void OnSocketStart()
        {
        }

        ///////////////////////////////////////////////////////////////////////////////
        public override void OnSocketStop()
        {
        }
    }
}