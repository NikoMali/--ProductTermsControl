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
using ProductTermsControl.Insfrastructure.Enums;
using ProductTermsControl.Insfrastructure.Helpers;
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

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ResponsiblePersonsGroupss =await _ResponsiblePersonsGroupsService.GetAll();
            var model = _mapper.Map<IList<ResponsiblePersonsGroupModel>>(ResponsiblePersonsGroupss);
            return Ok( model);
        }

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var ResponsiblePersonsGroups = await _ResponsiblePersonsGroupsService.GetById(Id);
            var model = _mapper.Map<ResponsiblePersonsGroupModel>(ResponsiblePersonsGroups);
            return Ok(model);
        }

        [DescriptionUserActivity(UserActivityType.Update)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ResponsiblePersonsGroupModel ResponsiblePersonsGroupsModel)
        {
            var model = _mapper.Map<ResponsiblePersonsGroup>(ResponsiblePersonsGroupsModel);
            var responsiblePersonsGroups = _mapper.Map<ResponsiblePersonsGroupModel>(await _ResponsiblePersonsGroupsService.Update(model));
            return Ok(responsiblePersonsGroups);
        }

        [DescriptionUserActivity(UserActivityType.Delete)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var ResponsiblePersonsGroupsResult = await _ResponsiblePersonsGroupsService.Delete(Id);
            return Ok(new { status = ResponsiblePersonsGroupsResult });
        }

        [DescriptionUserActivity(UserActivityType.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResponsiblePersonsGroupModel ResponsiblePersonsGroupsModel)
        {
            var model = _mapper.Map<ResponsiblePersonsGroup>(ResponsiblePersonsGroupsModel);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            var ResponsiblePersonsGroups = await _ResponsiblePersonsGroupsService.Create(model);
            var responsModel = _mapper.Map<ResponsiblePersonsGroupModel>(ResponsiblePersonsGroups);
            return Ok(responsModel);
        }

        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet("CurrentUseOfSection")]
        public async Task<IActionResult> CurrentUseOfSection()
        {
            var sectionFullInfo = await _ResponsiblePersonsGroupsService.SectionWithUsersAndProducts();
            var model = _mapper.Map<List<SectionWithUsersAndProductsModel>>(sectionFullInfo);
            return Ok(model);
        }
    }
}