using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork.Unity
{
    ///////////////////////////////////////////////////////////////////////////////
    public class NetworkManager : MonoBehaviour
    {

        private Network network = null;
        public Network Network
        {
            get
            {
                if (network == null)
                    network = new Network();
                return network;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        void OnApplicationQuit()
        {
            Network.Stop();
        }
    }
}