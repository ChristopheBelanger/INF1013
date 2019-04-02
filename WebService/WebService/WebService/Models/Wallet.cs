using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Wallet
    {
        private string Hash;
        private double Value;

        public Wallet(string h, double v) {
            Hash = h;
            Value = v;
        }
    }
}
