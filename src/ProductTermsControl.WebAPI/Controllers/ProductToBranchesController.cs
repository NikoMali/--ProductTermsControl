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
using ProductTermsControl.WebAPI.Models;

namespace ProductTermsControl.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductToBranchesController : ControllerBase
    {
        private IProductToBranchService _ProductToBranchService;
        private IMapper _mapper;

        public ProductToBranchesController(
            IProductToBranchService ProductToBranchService,
            IMapper mapper)
        {
            _ProductToBranchService = ProductToBranchService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ProductToBranchs =await _ProductToBranchService.GetAll();
            var model = _mapper.Map<IList<ProductToBranchModel>>(ProductToBranchs);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var ProductToBranch =await _ProductToBranchService.GetById(Id);
            var model = _mapper.Map<ProductToBranchModel>(ProductToBranch);
            return Ok(model);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductToBranchModel ProductToBranchModel)
        {
            var model = _mapper.Map<ProductToBranch>(ProductToBranchModel);
            var ProductToBranch =await _ProductToBranchService.Update(model);
            return Ok(ProductToBranch);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var ProductToBranchResult =await _ProductToBranchService.Delete(Id);
            return Ok(new { status = ProductToBranchResult });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IList<ProductToBranchModel> ProductToBranchModel)
        {
            var model = _mapper.Map<IList<ProductToBranch>>(ProductToBranchModel);
            var ProductToBranch =await _ProductToBranchService.Create(model);
            return Ok(new { status = ProductToBranch });
        }

        [HttpGet("{BranchId}/ProductsWithTerm")]
        public async Task<IActionResult> GetAllProductByBranchId(int BranchId)
        {
            var ProductByBranchId =await _ProductToBranchService.GetAllProductByBranchId(BranchId);
            var model = _mapper.Map<IList<ProductWithTermModel>>(ProductByBranchId);
            return Ok(model);
        }


        [HttpGet("{BranchId}/ProductsWithTerm/{BranchProductId}")]
        public async Task<IActionResult> GetProductViewTermByBranchId(int BranchId, int BranchProductId)
        {
            var ProductByBranchId = await _ProductToBranchService.GetProductViewTermByBranchId(BranchId, BranchProductId);
            var model = _mapper.Map<ProductToBranchModel>(ProductByBranchId);
            return Ok(model);
        }
        [HttpGet("{BranchId}/ProductsByResponsible/{UserId}")]
        public async Task<IActionResult> ProductsByResponsible(int BranchId, int UserId)
        {
            var ProductByBranchId = await _ProductToBranchService.GetAllProductByBranchIdAndResponsibleId(BranchId, UserId);
            var model = _mapper.Map<IList<ProductWithTermModel>>(ProductByBranchId);
            return Ok(model);
        }

        //
    }
}