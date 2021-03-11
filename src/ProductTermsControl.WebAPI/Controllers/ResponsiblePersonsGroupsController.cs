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
    public class ResponsiblePersonsGroupsController : ControllerBase
    {
        private IResponsiblePersonsGroupService _ResponsiblePersonsGroupsService;
        private IMapper _mapper;

        public ResponsiblePersonsGroupsController(
            IResponsiblePersonsGroupService ResponsiblePersonsGroupsService,
            IMapper mapper)
        {
            _ResponsiblePersonsGroupsService = ResponsiblePersonsGroupsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ResponsiblePersonsGroupss =await _ResponsiblePersonsGroupsService.GetAll();
            var model = _mapper.Map<IList<ResponsiblePersonsGroupModel>>(ResponsiblePersonsGroupss);
            return Ok( model);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var ResponsiblePersonsGroups = await _ResponsiblePersonsGroupsService.GetById(Id);
            var model = _mapper.Map<ResponsiblePersonsGroupModel>(ResponsiblePersonsGroups);
            return Ok(model);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ResponsiblePersonsGroupModel ResponsiblePersonsGroupsModel)
        {
            var model = _mapper.Map<ResponsiblePersonsGroup>(ResponsiblePersonsGroupsModel);
            var ResponsiblePersonsGroups = await _ResponsiblePersonsGroupsService.Update(model);
            return Ok(new { status = ResponsiblePersonsGroups });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var ResponsiblePersonsGroupsResult = await _ResponsiblePersonsGroupsService.Delete(Id);
            return Ok(new { status = ResponsiblePersonsGroupsResult });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResponsiblePersonsGroupModel ResponsiblePersonsGroupsModel)
        {
            var model = _mapper.Map<ResponsiblePersonsGroup>(ResponsiblePersonsGroupsModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var ResponsiblePersonsGroups = await _ResponsiblePersonsGroupsService.Create(model);
            return Ok(new { status = ResponsiblePersonsGroups });
        }
    }
}