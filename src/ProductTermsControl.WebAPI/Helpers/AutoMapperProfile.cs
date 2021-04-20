using AutoMapper;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.HelperModel;
using ProductTermsControl.WebAPI.Models;
using ProductTermsControl.WebAPI.Models.Magaziness;
using ProductTermsControl.WebAPI.Models.Users;

namespace ProductTermsControl.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<RegisterModel, User>();
            CreateMap<User, RegisterModel>();
            CreateMap<UpdateModel, User>();
            CreateMap<User, UpdateModel>();

            CreateMap<Magazine, MagazineModel>();
            CreateMap<MagazineModel, Magazine>();

            CreateMap<MagazineBranch, MagazineBranchModel>();
            CreateMap<MagazineBranchModel, MagazineBranch>();
            CreateMap<MagazineBranch, MagazineBranchResponseModel>();
            CreateMap<MagazineBranchResponseModel, MagazineBranch>();

            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductResponseModel>();
            CreateMap<ProductResponseModel, Product>();

            CreateMap<ProductToBranch, ProductToBranchModel>();
            CreateMap<ProductToBranchModel, ProductToBranch>();
            CreateMap<ProductToBranch, ProductToBranchResponseModel>();
            CreateMap<ProductToBranchResponseModel, ProductToBranch>();


            CreateMap<ResponsiblePersonsForProduct, ResponsiblePersonsForProductModel>();
            CreateMap<ResponsiblePersonsForProductModel, ResponsiblePersonsForProduct>();
            CreateMap<ResponsiblePersonsForProduct, ResponsiblePersonsForProductResponseModel>();
            CreateMap<ResponsiblePersonsForProductResponseModel, ResponsiblePersonsForProduct>();

            CreateMap<ResponsiblePersonsGroup, ResponsiblePersonsGroupModel>();
            CreateMap<ResponsiblePersonsGroupModel, ResponsiblePersonsGroup>();

            CreateMap<Company, CompanyModel>();
            CreateMap<CompanyModel, Company>();

            CreateMap<ProductWithTerm, ProductWithTermModel>();
            CreateMap<ProductWithTermModel, ProductWithTerm>();

            CreateMap<UserReference, UserReferenceModel>();
            CreateMap<UserReferenceModel, UserReference>();

            CreateMap<UserReference, UserReferenceResponseModel>();
            CreateMap<UserReferenceResponseModel, UserReference>();

            CreateMap<UserModel, UserReference>();
            CreateMap<UserReference, UserModel>();
            CreateMap<RegisterModel, UserReference>();
            CreateMap<UserReference, RegisterModel>();
            CreateMap<UpdateModel, UserReference>();
            CreateMap<UserReference, UpdateModel>();

            CreateMap<PositionModel, Position>();
            CreateMap<Position, PositionModel>();

            CreateMap<UserModel, UserWithReference>();
            CreateMap<UserWithReference, UserModel>();

            CreateMap<User, BranchUserModel>();
            CreateMap<BranchUserModel, User>();

            CreateMap<ReasonForOutOfStock, ReasonForOutOfStockModel>();
            CreateMap<ReasonForOutOfStockModel, ReasonForOutOfStock>();

            CreateMap<ResponsiblePersonsGroup, ResponsiblePersonsGroupModel>();
            CreateMap<ResponsiblePersonsGroupModel, ResponsiblePersonsGroup>();

            CreateMap<BranchProductStock, BranchProductStockModel>();
            CreateMap<BranchProductStockModel, BranchProductStock>();
            CreateMap<BranchProductStock, BranchProductStockResponseModel>();
            CreateMap<BranchProductStockResponseModel, BranchProductStock>();

            CreateMap<SectionWithUsersAndProducts, SectionWithUsersAndProductsModel>();
            CreateMap<SectionWithUsersAndProductsModel, SectionWithUsersAndProducts>();

        }
    }
}