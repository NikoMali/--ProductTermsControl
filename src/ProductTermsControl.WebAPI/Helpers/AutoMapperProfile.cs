using AutoMapper;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.HelperModel;
using ProductTermsControl.WebAPI.Models;
using ProductTermsControl.WebAPI.Models.Magazine;
using ProductTermsControl.WebAPI.Models.Users;

namespace ProductTermsControl.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<User, RegisterModel>();
            CreateMap<UpdateModel, User>();
            CreateMap<User, UpdateModel>();

            CreateMap<Magazine, MagazineModel>();
            CreateMap<MagazineModel, Magazine>();

            CreateMap<MagazineBranch, MagazineBranchModel>();
            CreateMap<MagazineBranchModel, MagazineBranch>();

            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();

            CreateMap<ResponsiblePersonsGroup, ResponsiblePersonsGroupModel>();
            CreateMap<ResponsiblePersonsGroupModel, ResponsiblePersonsGroup>();

            CreateMap<Company, CompanyModel>();
            CreateMap<CompanyModel, Company>();

            CreateMap<ProductWithTerm, ProductWithTermModel>();
            CreateMap<ProductWithTermModel, ProductWithTerm>();

            CreateMap<UserReference, UserReferenceModel>();
            CreateMap<UserReferenceModel, UserReference>();

            CreateMap<UserModel, UserReference>();
            CreateMap<UserReference, UserModel>();
            CreateMap<RegisterModel, UserReference>();
            CreateMap<UserReference, RegisterModel>();
            CreateMap<UpdateModel, UserReference>();
            CreateMap<UserReference, UpdateModel>();

        }
    }
}