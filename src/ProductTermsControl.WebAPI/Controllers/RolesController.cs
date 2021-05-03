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
using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.Insfrastructure.Enums;

namespace ProductTermsControl.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private IRoleService _roleService;
        private IMapper _mapper;
        

        public RolesController(
            IRoleService roleService,
            IMapper mapper
           )
        {
            _roleService = roleService;
            _mapper = mapper;
            
        }
       
    }
}