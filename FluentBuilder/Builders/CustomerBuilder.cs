using System;

namespace FluentBuilder
{
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
}