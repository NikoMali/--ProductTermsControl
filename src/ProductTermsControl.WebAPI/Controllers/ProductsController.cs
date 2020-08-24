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
    public class ProductsController : ControllerBase
    {
        private IProductService _ProductService;
        private IMapper _mapper;

        public ProductsController(
            IProductService ProductService,
            IMapper mapper)
        {
            _ProductService = ProductService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var Products = _ProductService.GetAll();
            var model = _mapper.Map<IList<ProductModel>>(Products);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var Product = _ProductService.GetById(Id);
            var model = _mapper.Map<ProductModel>(Product);
            return Ok(model);
        }
        [HttpPut]
        public IActionResult Update([FromBody] ProductModel ProductModel)
        {
            var model = _mapper.Map<Product>(ProductModel);
            var Product = _ProductService.Update(model);
            return Ok(Product);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var ProductResult = _ProductService.Delete(Id);
            return Ok(ProductResult);
        }
        [HttpPost]
        public IActionResult Create([FromBody] ProductModel ProductModel)
        {
            var model = _mapper.Map<Product>(ProductModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var Product = _ProductService.Create(model);
            return Ok(Product);
        }
    }
}