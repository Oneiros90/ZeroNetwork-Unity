using System;
using System.Threading.Tasks;
using NetMQ;
using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork
{
    public class NetworkJob
    {
        public event Action Started;
        public bool IsRunning => Task.Status == TaskStatus.Running;

        private readonly Task Task;
        private bool StopRequested = false;


        ///////////////////////////////////////////////////////////////////////////////
        public NetworkJob(Socket socket)
        {
            Task = new Task(() =>
            {
                NetMQSocket lowLevelSocket = null;
                try
                {
                    AsyncIO.ForceDotNet.Force();
                    using (lowLevelSocket = socket.Init())
                    {
                        Started?.Invoke();
                        while (!StopRequested)
                            socket.Update();
                    }
                }
                catch (TerminatingException e)
                {
                    // Ignore this kind of exception
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                finally
                {
                    lowLevelSocket?.Close();
                    lowLevelSocket?.Dispose();
                }
            });
        }

        ///////////////////////////////////////////////////////////////////////////////
        internal void Start()
        {
            Task.Start();
        }

        ///////////////////////////////////////////////////////////////////////////////
        internal void RequestStop()
        {
            StopRequested = true;
            Task.Wait();
        }
    }
}