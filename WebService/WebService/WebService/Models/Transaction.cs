﻿using System;
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

        public Transaction(long i, string from, string to, double content) {
            Id = i;
            FromWallet = from;
            ToWallet = to;
            Content = content;
        }
    }
}
