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
using ProductTermsControl.Application.Wrappers;
using ProductTermsControl.WebAPI.Models;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.Insfrastructure.Enums;
using Microsoft.AspNetCore.Localization;

namespace ProductTermsControl.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StaticEntitiesController : ControllerBase
    {
        private IRepository<ReasonForOutOfStock> _reasonForOutOfStock;
        private IMapper _mapper;
        

        public StaticEntitiesController(
            IRepository<ReasonForOutOfStock> reasonForOutOfStock,
            IMapper mapper
            )
        {
            _reasonForOutOfStock = reasonForOutOfStock;
            _mapper = mapper;
            
        }

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet("ReasonForOutOfStocks")]
        public async Task<IActionResult> GetAll()
        {
            
            
            return Ok(_mapper.Map<IList<ReasonForOutOfStockModel>>(await _reasonForOutOfStock.GetAll()));

        }

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet("ReasonForOutOfStocks/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var model = await _reasonForOutOfStock.Get(Id);
            
            return Ok(_mapper.Map<ReasonForOutOfStockModel>(model));
        }

        [DescriptionUserActivity(UserActivityType.Update)]
        [HttpPut("ReasonForOutOfStockUpdate")]
        public async Task<IActionResult> Update([FromBody] ReasonForOutOfStockModel reasonForOutOfStockModel)
        {
            var model = _mapper.Map<ReasonForOutOfStock>(reasonForOutOfStockModel);
            var ReasonForOutOfStock = await _reasonForOutOfStock.Update(model);
            
            return Ok(_mapper.Map<ReasonForOutOfStockModel>(ReasonForOutOfStock));
        }

        [DescriptionUserActivity(UserActivityType.Delete)]
        [HttpDelete("ReasonForOutOfStock/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var reason =await _reasonForOutOfStock.Delete(Id);
            return Ok(_mapper.Map<ReasonForOutOfStockModel>(reason));
        }

        [DescriptionUserActivity(UserActivityType.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReasonForOutOfStockModel reasonForOutOfStockModel)
        {
            var model = _mapper.Map<ReasonForOutOfStock>(reasonForOutOfStockModel);
            var ReasonForOutOfStock = await _reasonForOutOfStock.Add(model);
            return Ok(_mapper.Map<ReasonForOutOfStockModel>(ReasonForOutOfStock));
        }

        [HttpPost("SetLanguage")]
        public IActionResult SetLanguage([FromQuery]string culture)
        {
                HttpContext.Response.Cookies.Delete("Set-Cookie");
            

            HttpContext.Response.Cookies.Append(".AspNetCore.Culture",
                $"c={culture}|uic={culture}", new CookieOptions { Expires = DateTime.UtcNow.AddYears(1) });

            return Ok(new { message = Response.Cookies });
        }
    }
}