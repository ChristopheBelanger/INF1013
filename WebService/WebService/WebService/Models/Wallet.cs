using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Serializable]
    public class Wallet
    {
        public string Hash { get; set; }
        public double Value { get; set; }

        public Wallet(string h, double v) {
            Hash = h;
            Value = v;
        }
    }
}
