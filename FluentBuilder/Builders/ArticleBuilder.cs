namespace FluentBuilder
{
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
}