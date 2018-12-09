using System;
using FluentAssertions;
using Xunit;

namespace FluentBuilder
{
    public class SampleTest
    {
        [Fact]
        public void should_build_an_order()
        {
            var order = OrderBuilder.AnEmptyOrder()
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
            order.Customer.BirthDay.Year.Should().Be(DateTime.Now.Year - 42);
        }
    }
}