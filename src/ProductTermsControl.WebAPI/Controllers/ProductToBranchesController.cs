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
        public IActionResult GetAll()
        {
            var ProductToBranchs = _ProductToBranchService.GetAll();
            var model = _mapper.Map<IList<ProductToBranchModel>>(ProductToBranchs);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var ProductToBranch = _ProductToBranchService.GetById(Id);
            var model = _mapper.Map<ProductToBranchModel>(ProductToBranch);
            return Ok(model);
        }
        [HttpPut]
        public IActionResult Update([FromBody] ProductToBranchModel ProductToBranchModel)
        {
            var model = _mapper.Map<ProductToBranch>(ProductToBranchModel);
            var ProductToBranch = _ProductToBranchService.Update(model);
            return Ok(ProductToBranch);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var ProductToBranchResult = _ProductToBranchService.Delete(Id);
            return Ok(ProductToBranchResult);
        }
        [HttpPost]
        public IActionResult Create([FromBody] IList<ProductToBranchModel> ProductToBranchModel)
        {
            var model = _mapper.Map<IList<ProductToBranch>>(ProductToBranchModel);
            var ProductToBranch = _ProductToBranchService.Create(model);
            return Ok(ProductToBranch);
        }

        //
    }
}