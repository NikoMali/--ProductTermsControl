using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTermsControl.Application.Services;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Application.Paging.Helpers;
using ProductTermsControl.Application.Paging.Services;
using ProductTermsControl.WebAPI.Models;
using ProductTermsControl.WebAPI.Models.Users;

namespace ProductTermsControl.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MagazineBranchesController : ControllerBase
    {
        private IMagazineBranchService _MagazineBranchService;
        private IProductToBranchService _productToBranchService;
        private IResponsiblePersonsForProductService _responsiblePersonsByProductService;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly IUriService _uriService;

        public MagazineBranchesController(
            IMagazineBranchService  magazineBranchService,
            IProductToBranchService productToBranchService,
            IResponsiblePersonsForProductService responsiblePersonsByProductService,
            IMapper mapper,
            IUriService uriService,
            IUserService userService
            )
        {
            _MagazineBranchService = magazineBranchService;
            _productToBranchService = productToBranchService;
            _responsiblePersonsByProductService = responsiblePersonsByProductService;
            _mapper = mapper;
            _uriService = uriService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            /*var MagazineBranchs =await _MagazineBranchService.GetAll();
            var model = _mapper.Map<IList<MagazineBranchModel>>(MagazineBranchs);
            return Ok(model);*/
            var route = Request.Path.Value;
            var pageData = await _MagazineBranchService.GetAllForPaging(filter.PageNumber, filter.PageSize);
            var model = _mapper.Map<List<MagazineBranchModel>>(pageData.entities);
            var pagedReponse = PaginationHelper.CreatePagedReponse<MagazineBranchModel>(model, pageData.PaginationFilter, pageData.totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var MagazineBranch =await _MagazineBranchService.GetById(Id);
            var model = _mapper.Map<MagazineBranchModel>(MagazineBranch);
            return Ok(model);
        }
        [HttpGet("{BranchId}/users")]
        public async Task<IActionResult> GetUsersByBranchId(int BranchId)
        {
            var users = await _MagazineBranchService.GetUsersByBranchId(BranchId);
            var model = new List<BranchUserModel>();
            users.ToList().ForEach(x => model.Add(new BranchUserModel(x, _userService)));
            return Ok(model);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MagazineBranchModel MagazineBranchModel)
        {
            var model = _mapper.Map<MagazineBranch>(MagazineBranchModel);
            var MagazineBranch =await _MagazineBranchService.Update(model);
            return Ok(_mapper.Map<MagazineBranchModel>(MagazineBranch));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var MagazineBranchResult =await _MagazineBranchService.Delete(Id);
            return Ok(new { status = MagazineBranchResult });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MagazineBranchModel MagazineBranchModel)
        {
            var model = _mapper.Map<MagazineBranch>(MagazineBranchModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var MagazineBranch =await _MagazineBranchService.Create(model);
            return Ok(_mapper.Map<MagazineBranchModel>(MagazineBranch));
        }
        
        /*[HttpPost("ProductsWithTerm")]
        public IActionResult RegisterProduct([FromBody] IList<ProductToBranchModel> productToBranch)
        {
            var model = _mapper.Map<IList<ProductToBranch>>(productToBranch);
            var ProductToBranchResult = _productToBranchService.Create(model);
            return Ok(ProductToBranchResult);
        }*/


        [HttpGet("{BranchId}/ProductsWithTerm")]
        public IActionResult GetAllProductByBranchId(int BranchId)
        {
            var ProductByBranchId = _productToBranchService.GetAllProductByBranchId(BranchId);
            var model = _mapper.Map<IList<ProductWithTermModel>>(ProductByBranchId);
            return Ok(model);
        }


        [HttpGet("{BranchId}/ProductsWithTerm/{BranchProductId}")]
        public IActionResult GetProductViewTermByBranchId(int BranchId,int BranchProductId)
        {
            var ProductByBranchId = _productToBranchService.GetProductViewTermByBranchId(BranchId, BranchProductId);
            var model = _mapper.Map<ProductToBranchModel>(ProductByBranchId);
            return Ok(model);
        }
        [HttpGet("{BranchId}/ProductsByResponsible/{UserId}")]
        public IActionResult ProductsByResponsible(int BranchId, int UserId)
        {
            var ProductByBranchId = _productToBranchService.GetAllProductByBranchIdAndResponsibleId(BranchId,UserId);
            var model = _mapper.Map<IList<ProductWithTermModel>>(ProductByBranchId);
            return Ok(model);
        }

        /* [HttpPut("ProductsWithTerm")]
         public IActionResult GetProductUpdateTermByBranchId([FromBody] ProductToBranchModel productTerm)
         {

             var ProductTerm = _mapper.Map<ProductToBranch>(productTerm);
             var result = _productToBranchService.Update(ProductTerm);

             return Ok(result);
         }


         [HttpDelete("ProductsWithTerm/{Id}")]
         public IActionResult BranchProductDelete(int Id)
         {
             var MagazineBranchResult = _productToBranchService.Delete(Id);
             return Ok(MagazineBranchResult);
         }


         [HttpPost("ProductsResponsible")]
         public IActionResult ProductsResponsible([FromBody] IList<ResponsiblePersonsForProductModel> responsiblePersonsByProductModel)
         {
             var model = _mapper.Map<IList<ResponsiblePersonsForProduct>>(responsiblePersonsByProductModel);
             var ProductToBranchResult = _responsiblePersonsByProductService.Create(model);
             return Ok(ProductToBranchResult);
         }


         [HttpPut("ProductsResponsible")]
         public IActionResult ProductsResponsiblePut([FromBody] ResponsiblePersonsForProductModel responsiblePersonsForProductModel)
         {

             var responsiblePersonsForProduct = _mapper.Map<ResponsiblePersonsForProduct>(responsiblePersonsForProductModel);
             var result = _responsiblePersonsByProductService.Update(responsiblePersonsForProduct);

             return Ok(result);
         }


         [HttpDelete("ProductsResponsible/{Id}")]
         public IActionResult ProductsResponsibleDelete(int Id)
         {
             var MagazineBranchResult = _responsiblePersonsByProductService.Delete(Id);
             return Ok(MagazineBranchResult);
         }*/
    }
}