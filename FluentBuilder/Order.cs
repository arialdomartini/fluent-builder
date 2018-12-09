using System;

namespace FluentBuilder
{
    public class Order
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public Article Article { get; set; }
        public Customer Customer { get; set; }
    }
}