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
using ProductTermsControl.Insfrastructure.Filter;
using ProductTermsControl.Insfrastructure.Paging.Helpers;
using ProductTermsControl.Insfrastructure.Paging.Services;
using ProductTermsControl.Insfrastructure.Wrappers;
using ProductTermsControl.WebAPI.Models;

namespace ProductTermsControl.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private ICompanyService _CompanyService;
        private IMapper _mapper;
        private readonly IUriService _uriService;

        public CompaniesController(
            ICompanyService CompanyService,
            IMapper mapper,
            IUriService uriService)
        {
            _CompanyService = CompanyService;
            _mapper = mapper;
            _uriService = uriService;
        }
        /// <summary>
        /// Company Full List
        /// </summary>
        [HttpGet]
        public IActionResult GetAll([FromQuery] PaginationFilter filter)
        {
            
            var route = Request.Path.Value;
            var pageData = _CompanyService.GetAllForPaging(filter.PageNumber, filter.PageSize);
            //var pageData = PaginationData.GetAllForPaging<Company>(filter.PageNumber, filter.PageSize, _CompanyService.GetAll().ToList());
            var model = _mapper.Map<List<CompanyModel>>(pageData.entities);
            var pagedReponse = PaginationHelper.CreatePagedReponse<CompanyModel>(model, pageData.PaginationFilter, pageData.totalRecords, _uriService, route);
            return Ok(pagedReponse);

        }
        /// <summary>
        /// Get Company By Id
        /// </summary>
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var Company = _CompanyService.GetById(Id);
            var model = _mapper.Map<CompanyModel>(Company);
            return Ok(model);
        }
        /// <summary>
        /// Company update By Body
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromBody] CompanyModel CompanyModel)
        {
            var model = _mapper.Map<Company>(CompanyModel);
            var Company = _CompanyService.Update(model);
            return Ok(new { status = Company });
        }
        /// <summary>
        /// Delete Company By Id
        /// </summary>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var CompanyResult = _CompanyService.Delete(Id);
            return Ok(new { status = CompanyResult });
        }
        /// <summary>
        /// Company Create
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] CompanyModel CompanyModel)
        {
            var model = _mapper.Map<Company>(CompanyModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var Company = _CompanyService.Create(model);
            return Ok(new { status = Company });
        }
    }
}