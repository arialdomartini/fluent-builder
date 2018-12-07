using System;
using FluentAssertions;
using Xunit;
using static FluentBuilder.OrderBuilder;

namespace FluentBuilder
{
    public class OrderBuilder
    {
        private int _number;
        private DateTime _date;

        public static OrderBuilder AnOrder() => new OrderBuilder();

        public OrderBuilder WithNumber(int i)
        {
            _number = i;
            return this;
        }

        public OrderBuilder WithDate(DateTime dateTime)
        {
            _date = dateTime;
            return this;
        }

        public Order Build() =>
            new Order
            {
                Number = _number,
                Date = _date
            };
    }

    public class Order
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
    }

    public class BuilderClient
    {
        [Fact]
        public void should_build_an_order()
        {
            var order = AnOrder()
                .WithNumber(10)
                .WithDate(new DateTime(2018, 12, 24))
                .Build();

            order.Number.Should().Be(10);
        }
    }
}