using System;
namespace Blockchain
{
    public class AddresseIP
    {
        public String ip { get; set; } = "";
        public int port { get; set; } = 0;

        public override String ToString()
        {
            return ip + ":" + port;
        }
    }
}
