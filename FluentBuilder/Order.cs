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

        private readonly ArticleBuilder _articleBuilder;
        private readonly CustomerBuilder _customerBuilder;

        public static OrderBuilder AnOrder() =>
            new OrderBuilder();

        private OrderBuilder()
        {
            _articleBuilder = new ArticleBuilder();
            _customerBuilder = new CustomerBuilder();
        }

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

        public OrderBuilder HavingAnArticle(Func<ArticleBuilder, ArticleBuilder> func)
        {
            func(_articleBuilder);
            return this;
        }
        
        public OrderBuilder HavingCustomer(Func<CustomerBuilder, CustomerBuilder> func)
        {
            func(_customerBuilder);
            return this;
        }

        public Order Build() =>
            new Order
            {
                Number = _number,
                Date = _date,
                Article = _articleBuilder.Build(),
                Customer = _customerBuilder.Build()
            };
    }

    public class CustomerBuilder
    {
        private string _name;

        public CustomerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Customer Build() => 
            new Customer
            {
                Name = _name
            };
    }

    public class ArticleBuilder
    {
        public ArticleBuilder WithPrice(decimal price)
        {
            _price = price;
            return this;
        }

        private decimal _price;
        private string _category;

        public Article Build()
        {
            return new Article
            {
                Price = _price,
                Category = _category
            };
        }

        public ArticleBuilder WithCategory(string category)
        {
            _category = category;
            return this;
        }
    }

    public class Order
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public Article Article { get; set; }
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }
    }

    public class Article
    {
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    public class BuilderClient
    {
        [Fact]
        public void should_build_an_order()
        {
            var order = AnOrder()
                .WithNumber(10)
                .WithDate(new DateTime(2018, 12, 24))
                .HavingAnArticle(a => a
                    .WithPrice(32.50m)
                    .WithCategory("books"))
                .HavingCustomer(c => c
                    .WithName("Amelia"))
                .Build();

            order.Number.Should().Be(10);
            order.Date.Should().Be(new DateTime(2018, 12, 24));
            
            order.Article.Price.Should().Be(32.50m);
            order.Article.Category.Should().Be("books");

            order.Customer.Name.Should().Be("Amelia");
        }
    }
}