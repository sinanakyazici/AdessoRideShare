using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdessoRideShare.Application.ViewModels
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
