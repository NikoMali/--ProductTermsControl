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
    public class CompaniesController : ControllerBase
    {
        private ICompanyService _CompanyService;
        private IMapper _mapper;

        public CompaniesController(
            ICompanyService CompanyService,
            IMapper mapper)
        {
            _CompanyService = CompanyService;
            _mapper = mapper;
        }
        /// <summary>
        /// Company Full List
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var Companys = _CompanyService.GetAll();
            var model = _mapper.Map<IList<CompanyModel>>(Companys);
            return Ok(model);
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