using System.Collections.Generic;
using System.Linq;
using NetMQ;
using UnityEngine;

///////////////////////////////////////////////////////////////////////////////
namespace ZeroNetwork
{
    ///////////////////////////////////////////////////////////////////////////////
    public class Network
    {
        private readonly HashSet<NetworkJob> ActiveTasks = new HashSet<NetworkJob>();

        ///////////////////////////////////////////////////////////////////////////////
        public void Start(NetworkJob task)
        {
            if (ActiveTasks.Contains(task))
                return;

            task.Start();
            ActiveTasks.Add(task);
            Log($"New task started (total: {ActiveTasks.Count})");
        }

        ///////////////////////////////////////////////////////////////////////////////
        public void Stop(NetworkJob task)
        {
            if (!ActiveTasks.Contains(task))
                return;

            task.RequestStop();
            ActiveTasks.Remove(task);
            Log($"Task stopped (total: {ActiveTasks.Count})");
        }

        ///////////////////////////////////////////////////////////////////////////////
        public void Stop()
        {
            if (ActiveTasks.Count == 0)
                return;

            while (ActiveTasks.Count > 0)
                Stop(ActiveTasks.First());

            ActiveTasks.Clear();
            NetMQConfig.Cleanup(true);
            Log("Network shutdown");
        }

        ///////////////////////////////////////////////////////////////////////////////
        private void Log(string log) => Debug.Log($"[{nameof(Network)}] {log}");
    }
}