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

        private ArticleBuilder _articleBuilder;
        private CustomerBuilder _customerBuilder;

        public static OrderBuilder AnEmptyOrder() =>
            new OrderBuilder();

        public static OrderBuilder AnOrderWithOneArticle() => 
            AnEmptyOrder()
                .WithNumber(100)
                .CreatedOn(new DateTime(2018, 10, 10))
                .ContainingAnArticle(a => a
                    .InCategory("some category")
                    .WithPrice(50.50m));


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

        public OrderBuilder CreatedOn(DateTime dateTime)
        {
            _date = dateTime;
            return this;
        }

        public OrderBuilder ContainingAnArticle(Func<ArticleBuilder, ArticleBuilder> func)
        {
            func(_articleBuilder);
            return this;
        }
        
        public OrderBuilder ByACustomer(Func<CustomerBuilder, CustomerBuilder> func)
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
        private DateTime _birthday;

        public CustomerBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public Customer Build() => 
            new Customer
            {
                Name = _name,
                BirthDay = _birthday
            };

        public CustomerBuilder BornOn(DateTime dateTime)
        {
            _birthday = dateTime;
            return this;
        }

        public CustomerBuilder Aged(int age)
        {
            _birthday = DateTime.Now.AddYears(-age);
            return this;
        }
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

        public ArticleBuilder InCategory(string category)
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
        public DateTime BirthDay { get; set; }
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
            var order = AnEmptyOrder()
                .WithNumber(10)
                .CreatedOn(new DateTime(2018, 12, 24))
                .ByACustomer(c => c
                    .Named("Amelia")
                    .Aged(42))
                .ContainingAnArticle(a => a
                    .WithPrice(32.50m)
                    .InCategory("books"))
                .Build();

            order.Number.Should().Be(10);
            order.Date.Should().Be(new DateTime(2018, 12, 24));
            
            order.Article.Price.Should().Be(32.50m);
            order.Article.Category.Should().Be("books");

            order.Customer.Name.Should().Be("Amelia");
        }
    }
}