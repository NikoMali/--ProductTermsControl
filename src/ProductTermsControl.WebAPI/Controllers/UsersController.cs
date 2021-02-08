using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using ProductTermsControl.Insfrastructure.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using ProductTermsControl.Domain.Entities;
using ProductTermsControl.WebAPI.Models.Users;
using ProductTermsControl.Application.Services;
using System.Threading.Tasks;
using ProductTermsControl.Insfrastructure.Filter;
using ProductTermsControl.Insfrastructure.Paging.Helpers;
using ProductTermsControl.Insfrastructure.Paging.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IUriService _uriService;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IUriService uriService
            )
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _uriService = uriService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user =await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                var createUser = await _userService.Create(user, model.Password);
                return Ok(_mapper.Map<RegisterModel>(createUser));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            /*var users =await _userService.GetAll();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);*/
            var route = Request.Path.Value;
            var pageData = await _userService.GetAllForPaging(filter.PageNumber, filter.PageSize);
            var model = _mapper.Map<List<UserModel>>(pageData.entities);
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserModel>(model, pageData.PaginationFilter, pageData.totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user =await _userService.GetById(id);
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateModel model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = id;

            try
            {
                // update user 
                var updateUser = await _userService.Update(user, model.Password);
                return Ok(_mapper.Map<UpdateModel>(updateUser));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteStatus = await _userService.Delete(id);
            return Ok(new { status = deleteStatus });
        }
        [AllowAnonymous]
        [HttpPost("UserReferenceCreate")]
        public async Task<IActionResult> UserReferenceCreate([FromBody] UserReferenceModel model)
        {
            var user = _mapper.Map<UserReference>(model);
            var result =await _userService.UserReferenceCreate(user);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPut("UserReferenceUpdate")]
        public async Task<IActionResult> UserReferenceUpdate([FromBody] UserReferenceModel model)
        {
            var user = _mapper.Map<UserReference>(model);
            var result = await _userService.UserReferenceUpdate(user);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("UserReference/{userId}")]
        public async Task<IActionResult> UserReferenceGetById(int userId)
        {
            var result = await _userService.UserReferenceGetById(userId);
            var user = _mapper.Map<UserReferenceModel>(result);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpDelete("UserReferenceRemove/{userId}")]
        public async Task<IActionResult> UserReferenceRemove(int userId)
        {
            
            var result =await _userService.UserReferenceRemove(userId);
            return Ok(new { status = result });
        }
    }
}
