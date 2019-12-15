using System;
using AdessoRideShare.Domain.Core.Models;

namespace AdessoRideShare.Domain.Models
{
    public class Customer : Entity
    {
        public Customer(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        protected Customer() { }

        public string Name { get; private set; }

        public string Email { get; private set; } 
    }
}