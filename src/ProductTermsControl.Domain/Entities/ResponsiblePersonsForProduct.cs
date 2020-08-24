using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class ResponsiblePersonsForProduct
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }

        //referencee
        public int ResponsiblePersonsGroupId { get; set; }
        public ResponsiblePersonsGroup ResponsiblePersonsGroup { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


    }
}
