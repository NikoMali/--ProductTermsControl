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
using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.Insfrastructure.Enums;

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
        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            /*var MagazineBranchs =await _MagazineBranchService.GetAll();
            var model = _mapper.Map<IList<MagazineBranchModel>>(MagazineBranchs);
            return Ok(model);*/
            var route = Request.Path.Value;
            var pageData = await _MagazineBranchService.GetAllForPaging(filter.PageNumber, filter.PageSize);
            var model = _mapper.Map<List<MagazineBranchResponseModel>>(pageData.entities);
            for (int i = 0; i < model.Count; i++)
            {
                model[i].NumberOfEmployeeUpdate(model[i], _MagazineBranchService);
            }
            var pagedReponse = PaginationHelper.CreatePagedReponse<MagazineBranchResponseModel>(model, pageData.PaginationFilter, pageData.totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var MagazineBranch =await _MagazineBranchService.GetById(Id);
            var model = _mapper.Map<MagazineBranchResponseModel>(MagazineBranch);
            model.NumberOfEmployeeUpdate(model, _MagazineBranchService);
            return Ok(model);
        }

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet("{BranchId}/users")]
        public async Task<IActionResult> GetUsersByBranchId(int BranchId)
        {
            var users = await _MagazineBranchService.GetUsersByBranchId(BranchId);
            /*var model = new List<BranchUserModel>();
            users.ToList().ForEach(x => model.Add(new BranchUserModel(x, _userService)));*/
            var model = _mapper.Map<IList<UserReferenceResponseModel>>(users);
            return Ok(model);
        }

        [DescriptionUserActivity(UserActivityType.Update)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MagazineBranchModel MagazineBranchModel)
        {
            var model = _mapper.Map<MagazineBranch>(MagazineBranchModel);
            var MagazineBranch =await _MagazineBranchService.Update(model);
            return Ok(_mapper.Map<MagazineBranchModel>(MagazineBranch));
        }

        [DescriptionUserActivity(UserActivityType.Delete)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var MagazineBranchResult =await _MagazineBranchService.Delete(Id);
            return Ok(new { status = MagazineBranchResult });
        }

        [DescriptionUserActivity(UserActivityType.Create)]
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


        

        
    }
}