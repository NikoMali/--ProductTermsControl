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
using ProductTermsControl.WebAPI.Models.Magazine;

namespace ProductTermsControl.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MagazinesController : ControllerBase
    {
        private IMagazineService _magazineService;
        private IMapper _mapper;

        public MagazinesController(
            IMagazineService magazineService,
            IMapper mapper)
        {
            _magazineService = magazineService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var magazines = _magazineService.GetAll();
            var model = _mapper.Map<IList<MagazineModel>>(magazines);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var magazine = _magazineService.GetById(Id);
            var model = _mapper.Map<MagazineModel>(magazine);
            return Ok(model);
        }
        [HttpPut]
        public IActionResult Update([FromBody] MagazineModel magazineModel)
        {
            var model = _mapper.Map<Magazine>(magazineModel);
            var magazine = _magazineService.Update(model);
            return Ok(new { status = magazine });
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var magazineResult = _magazineService.Delete(Id);
            return Ok(new {status = magazineResult});
        }
        [HttpPost]
        public IActionResult Create([FromBody] MagazineModel magazineModel)
        {
            var model = _mapper.Map<Magazine>(magazineModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var magazine = _magazineService.Create(model);
            return Ok(new { status = magazine });
        }

        //
    }
}