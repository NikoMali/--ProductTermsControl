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
        public IActionResult GetAll()
        {
            var ResponsiblePersonsForProducts = _ResponsiblePersonsForProductService.GetAll();
            var model = _mapper.Map<IList<ResponsiblePersonsForProductModel>>(ResponsiblePersonsForProducts);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var ResponsiblePersonsForProduct = _ResponsiblePersonsForProductService.GetById(Id);
            var model = _mapper.Map<ResponsiblePersonsForProductModel>(ResponsiblePersonsForProduct);
            return Ok(model);
        }
        [HttpPut]
        public IActionResult Update([FromBody] ResponsiblePersonsForProductModel ResponsiblePersonsForProductModel)
        {
            var model = _mapper.Map<ResponsiblePersonsForProduct>(ResponsiblePersonsForProductModel);
            var ResponsiblePersonsForProduct = _ResponsiblePersonsForProductService.Update(model);
            return Ok(ResponsiblePersonsForProduct);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var ResponsiblePersonsForProductResult = _ResponsiblePersonsForProductService.Delete(Id);
            return Ok(ResponsiblePersonsForProductResult);
        }
        [HttpPost]
        public IActionResult Create([FromBody] IList<ResponsiblePersonsForProductModel> ResponsiblePersonsForProductModel)
        {
            var model = _mapper.Map<IList<ResponsiblePersonsForProduct>>(ResponsiblePersonsForProductModel);
            var ResponsiblePersonsForProduct = _ResponsiblePersonsForProductService.Create(model);
            return Ok(ResponsiblePersonsForProduct);
        }

        //
    }
}