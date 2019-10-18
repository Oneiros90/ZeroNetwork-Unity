using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ZeroNetwork
{
    public static class Utils
    {
        public static string PublicAddress => new WebClient().DownloadString("http://ipv4.icanhazip.com/").Trim();

        public static string PrivateAddress
        {
            get
            {
                foreach (var address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                        return address.ToString();
                return string.Empty;
            }
        }

        public static string BuildAddress(string ip, int port) => $"tcp://{ip}:{port}";

        public static long IPv4ToLong(string addr) => BitConverter.ToUInt32(IPAddress.Parse(addr).GetAddressBytes().Reverse().ToArray(), 0);

        public static string LongToIPv4(long address) => IPAddress.Parse(address.ToString()).ToString();
    }
}
