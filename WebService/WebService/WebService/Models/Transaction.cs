using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public double Content { get; set; }
    }
}
