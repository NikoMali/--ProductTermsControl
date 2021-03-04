using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.WebAPI.Models
{
    public class ProductToBranchModel
    {
        public int Id { get; set; }
        public string RegisterDate { get; set; }
        public string TermDate { get; set; }
        public int DaysBeforeNotifiWarning { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int MagazineBranchId { get; set; }
        public int ResponsiblePersonsGroupId { get; set; }



        
        public void ConvertMillisecondToDateTime(string millisecond, string keyWord)
        {
            if (nameof(RegisterDate) == keyWord)
            {
                RegisterDate = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(double.Parse(millisecond)).ToLocalTime().ToString();
            }
            if (nameof(TermDate) == keyWord)
            {
                TermDate = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(double.Parse(millisecond)).ToLocalTime().ToString();
            }
        }
    }
}
