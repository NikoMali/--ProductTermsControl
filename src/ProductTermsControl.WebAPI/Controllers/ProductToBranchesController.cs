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
            var model = _mapper.Map<IList<ProductToBranchResponseModel>>(ProductToBranchs);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var ProductToBranch =await _ProductToBranchService.GetById(Id);
            var model = _mapper.Map<ProductToBranchResponseModel>(ProductToBranch);
            return Ok(model);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductToBranchModel productToBranchModel)
        {
            
            productToBranchModel.ConvertMillisecondToDateTime(productToBranchModel.RegisterDate,nameof(productToBranchModel.RegisterDate));
            productToBranchModel.ConvertMillisecondToDateTime(productToBranchModel.TermDate, nameof(productToBranchModel.TermDate));
            
            var model = _mapper.Map<ProductToBranch>(productToBranchModel);
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
        public async Task<IActionResult> Create([FromBody] IList<ProductToBranchModel> productToBranchModel)
        {
            for (int i = 0; i < productToBranchModel.Count; i++)
            {
                
                productToBranchModel[i].ConvertMillisecondToDateTime(productToBranchModel[i].RegisterDate, "RegisterDate");
                productToBranchModel[i].ConvertMillisecondToDateTime(productToBranchModel[i].TermDate, "TermDate");
            }
            
            var model = _mapper.Map<IList<ProductToBranch>>(productToBranchModel);
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

        [HttpGet("OutOfStocks")]
        public async Task<IActionResult> OutOfStocks()
        {
            var outOfStocks = await _ProductToBranchService.OutOfStocks();
            var model = _mapper.Map<IList<BranchProductStockResponseModel>>(outOfStocks);
            return Ok(model);
        }
        [HttpGet("OutOfStocks/{stockId}")]
        public async Task<IActionResult> OutOfStock(int stockId)
        {
            var outOfStock = await _ProductToBranchService.OutOfStockById(stockId);
            var model = _mapper.Map<BranchProductStockResponseModel>(outOfStock);
            return Ok(model);
        }
        [HttpPost("OutOfStockCreate")]
        public async Task<IActionResult> OutOfStockCreate(BranchProductStockModel branchProductStockModel)
        {
            var convertModel = _mapper.Map<BranchProductStock>(branchProductStockModel);
            var outOfStock = await _ProductToBranchService.OutOfStockCreate(convertModel);
            var model = _mapper.Map<BranchProductStockResponseModel>(outOfStock);
            return Ok(model);
        }
        [HttpPut("OutOfStockUpdate")]
        public async Task<IActionResult> OutOfStockUpdate(BranchProductStockModel branchProductStockModel)
        {
            var convertModel = _mapper.Map<BranchProductStock>(branchProductStockModel);
            var outOfStock = await _ProductToBranchService.OutOfStockUpdate(convertModel);
            var model = _mapper.Map<BranchProductStockResponseModel>(outOfStock);
            return Ok(model);
        }
        [HttpDelete("OutOfStockRemove/{stockId}")]
        public async Task<IActionResult> OutOfStockRemove(int stockId)
        {
            var outOfStock = await _ProductToBranchService.OutOfStockRemove(stockId);
           
            return Ok(new { status = outOfStock });
        }

    }
}