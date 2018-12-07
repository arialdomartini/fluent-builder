using FluentAssertions;
using Xunit;
using static FluentBuilder.OrderBuilder;
namespace FluentBuilder
{
    public class OrderBuilder
    {
        private int _number;

        public static OrderBuilder Order() => new OrderBuilder();

        public OrderBuilder WithNumber(int i)
        {
            _number = i;
            return this;
        }


        public Order Build() =>
            new Order
            {
                Number = _number
            };
    }

    public class Order
    {
        public int Number { get; set; }
    }

    
    public class BuilderClient
    {
        [Fact]
        public void should_build_an_order()
        {
            var result = Order()
                .WithNumber(10)
                .Build();

            result.Number.Should().Be(10);
        }
    }
}