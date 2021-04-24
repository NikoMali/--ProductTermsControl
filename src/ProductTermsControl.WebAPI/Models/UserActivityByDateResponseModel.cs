using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.WebAPI.Models
{
    public class UserActivityByDateResponseModel
    {
        private DateTime ConvertDate;
        public string Date
        {
            get => ConvertDate.ToString("dd/MM/yyyy");
            set => ConvertDate = DateTime.Parse(value);
        }
        //public string Date { get; set; }
        public List<UserActivityModel> Activities { get; set; }
    }
    
}
