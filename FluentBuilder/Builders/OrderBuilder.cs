using System;

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
}