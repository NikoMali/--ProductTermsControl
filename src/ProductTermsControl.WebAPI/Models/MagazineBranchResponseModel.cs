using ProductTermsControl.Application.Services;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.WebAPI.Models.Magaziness;

namespace ProductTermsControl.WebAPI.Models
{
  public class MagazineBranchResponseModel
    {
        public int Id { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int NumberOfEmployees { get; set; }
        public int MagazineId { get; set; }
        public MagazineModel Magazine { get; set; }

        public MagazineBranchResponseModel()
        {

        }
        public async void NumberOfEmployeeUpdate(MagazineBranchResponseModel magazineBranch,IMagazineBranchService magazineBranchService)
        {
            magazineBranch.NumberOfEmployees = await magazineBranchService.EmployeOfNumbers(magazineBranch.Id);
        }
    }
    
}