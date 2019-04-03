using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Containers
{
    static public class ConnectionStore
    {
        private static object ConnectionLock = new object();
        private static List<string> ConnectedNodes = new List<string>();

        public static string[] getConnectedNodes()
        {
            lock (ConnectionLock)
            {
                return ConnectedNodes.ToArray();
            }
        }

        public static void Connect(string id)
        {
            lock (ConnectionLock)
            {
                ConnectedNodes.Add(id);
            }
        }

        public static void Disconnect(string id)
        {
            lock (ConnectionLock)
            {
                ConnectedNodes.Remove(id);
            }
        }

    }
}
