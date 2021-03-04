using System;
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
using ProductTermsControl.WebAPI.Models.Magaziness;

namespace ProductTermsControl.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MagazinesController : ControllerBase
    {
        private IMagazineService _magazineService;
        private IMapper _mapper;
        private readonly IUriService _uriService;


        public MagazinesController(
            IMagazineService magazineService,
            IMapper mapper,
            IUriService uriService
            )
        {
            _magazineService = magazineService;
            _mapper = mapper;
            _uriService = uriService;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            
            var route = Request.Path.Value;
            var pageData = await _magazineService.GetAllForPaging(filter.PageNumber, filter.PageSize);
            var model = _mapper.Map<List<MagazineModel>>(pageData.entities);
            var pagedReponse = PaginationHelper.CreatePagedReponse<MagazineModel>(model, pageData.PaginationFilter, pageData.totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var magazine =await _magazineService.GetById(Id);
            var model = _mapper.Map<MagazineModel>(magazine);
            return Ok(model);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MagazineModel magazineModel)
        {
            var model = _mapper.Map<Magazine>(magazineModel);
            var magazine =await _magazineService.Update(model);
            return Ok(magazine);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var magazineResult =await _magazineService.Delete(Id);
            return Ok(new {status = magazineResult});
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MagazineModel magazineModel)
        {
            var model = _mapper.Map<Magazine>(magazineModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var magazine =await _magazineService.Create(model);
            return Ok(magazine);
        }

        //
    }
}