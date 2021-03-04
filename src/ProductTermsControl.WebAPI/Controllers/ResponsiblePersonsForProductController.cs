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
    public class ResponsiblePersonsForProductsController : ControllerBase
    {
        private IResponsiblePersonsForProductService _ResponsiblePersonsForProductService;
        private IMapper _mapper;

        public ResponsiblePersonsForProductsController(
            IResponsiblePersonsForProductService ResponsiblePersonsForProductService,
            IMapper mapper)
        {
            _ResponsiblePersonsForProductService = ResponsiblePersonsForProductService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ResponsiblePersonsForProducts =await _ResponsiblePersonsForProductService.GetAll();
            var model = _mapper.Map<IList<ResponsiblePersonsForProductResponseModel>>(ResponsiblePersonsForProducts);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var ResponsiblePersonsForProduct = await _ResponsiblePersonsForProductService.GetById(Id);
            var model = _mapper.Map<ResponsiblePersonsForProductResponseModel>(ResponsiblePersonsForProduct);
            return Ok(model);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ResponsiblePersonsForProductModel ResponsiblePersonsForProductModel)
        {
            var model = _mapper.Map<ResponsiblePersonsForProduct>(ResponsiblePersonsForProductModel);
            var ResponsiblePersonsForProduct = await _ResponsiblePersonsForProductService.Update(model);
            return Ok(ResponsiblePersonsForProduct);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var ResponsiblePersonsForProductResult = await _ResponsiblePersonsForProductService.Delete(Id);
            return Ok(new {status = ResponsiblePersonsForProductResult });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IList<ResponsiblePersonsForProductModel> ResponsiblePersonsForProductModel)
        {
            var model = _mapper.Map<IList<ResponsiblePersonsForProduct>>(ResponsiblePersonsForProductModel);
            var ResponsiblePersonsForProduct =await _ResponsiblePersonsForProductService.Create(model);
            return Ok(new { status = ResponsiblePersonsForProduct });
        }

        //
    }
}