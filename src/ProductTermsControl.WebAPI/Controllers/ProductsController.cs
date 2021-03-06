﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Application.Paging.Helpers;
using ProductTermsControl.Application.Paging.Services;
using ProductTermsControl.Application.Services;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Insfrastructure.Enums;
using ProductTermsControl.Insfrastructure.Helpers;
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
        private readonly IUriService _uriService;

        public ProductsController(
            IProductService ProductService,
            IMapper mapper,
            IUriService uriService
            )
        {
            _ProductService = ProductService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            
            var route = Request.Path.Value;
            var pageData = await _ProductService.GetAllForPaging(filter.PageNumber, filter.PageSize);
            var model = _mapper.Map<List<ProductResponseModel>>(pageData.entities);
            var pagedReponse = PaginationHelper.CreatePagedReponse<ProductResponseModel>(model, pageData.PaginationFilter, pageData.totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Product =await _ProductService.GetById(Id);
            var model = _mapper.Map<ProductResponseModel>(Product);
            return Ok(model);
        }

        [DescriptionUserActivity(UserActivityType.Update)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductModel ProductModel)
        {
            var model = _mapper.Map<Product>(ProductModel);
            var product =await _ProductService.Update(model);
            var modelResponse = _mapper.Map<ProductResponseModel>(product);
            return Ok(modelResponse);
        }

        [DescriptionUserActivity(UserActivityType.Delete)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var ProductResult =await _ProductService.Delete(Id);
            return Ok(new { status = ProductResult });
        }

        [DescriptionUserActivity(UserActivityType.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductModel ProductModel)
        {
            var model = _mapper.Map<Product>(ProductModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var Product =await _ProductService.Create(model);
            var modelResponse = _mapper.Map<ProductResponseModel>(Product);
            return Ok(modelResponse);
        }
    }
}