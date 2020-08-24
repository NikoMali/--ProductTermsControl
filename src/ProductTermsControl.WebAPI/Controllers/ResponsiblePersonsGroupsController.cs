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
        public IActionResult GetAll()
        {
            var ResponsiblePersonsGroupss = _ResponsiblePersonsGroupsService.GetAll();
            var model = _mapper.Map<IList<ResponsiblePersonsGroupModel>>(ResponsiblePersonsGroupss);
            return Ok(model);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var ResponsiblePersonsGroups = _ResponsiblePersonsGroupsService.GetById(Id);
            var model = _mapper.Map<ResponsiblePersonsGroupModel>(ResponsiblePersonsGroups);
            return Ok(model);
        }
        [HttpPut]
        public IActionResult Update([FromBody] ResponsiblePersonsGroupModel ResponsiblePersonsGroupsModel)
        {
            var model = _mapper.Map<ResponsiblePersonsGroup>(ResponsiblePersonsGroupsModel);
            var ResponsiblePersonsGroups = _ResponsiblePersonsGroupsService.Update(model);
            return Ok(ResponsiblePersonsGroups);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var ResponsiblePersonsGroupsResult = _ResponsiblePersonsGroupsService.Delete(Id);
            return Ok(ResponsiblePersonsGroupsResult);
        }
        [HttpPost]
        public IActionResult Create([FromBody] ResponsiblePersonsGroupModel ResponsiblePersonsGroupsModel)
        {
            var model = _mapper.Map<ResponsiblePersonsGroup>(ResponsiblePersonsGroupsModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var ResponsiblePersonsGroups = _ResponsiblePersonsGroupsService.Create(model);
            return Ok(ResponsiblePersonsGroups);
        }
    }
}