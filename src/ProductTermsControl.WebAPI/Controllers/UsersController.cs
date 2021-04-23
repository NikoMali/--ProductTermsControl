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
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Application.Paging.Helpers;
using ProductTermsControl.Application.Paging.Services;
using ProductTermsControl.WebAPI.Models;
using Serilog;
using ProductTermsControl.Insfrastructure.Enums;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMagazineBranchService _magazineBranchService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IUriService _uriService;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IUriService uriService,
            IMagazineBranchService magazineBranchService
            )
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _uriService = uriService;
            _magazineBranchService = magazineBranchService;
        }
        [DescriptionUserActivity(UserActivityType.Get)]
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
            Log.Information("User "+user.Username+" authenticated success");
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
        [DescriptionUserActivity(UserActivityType.Create)]
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);
            var userReference = _mapper.Map<UserReference>(model);
            try
            {
                // create user
                var createUser = await _userService.Create(user, model.Password);
                var userWithReference = new RegisterModel(createUser);
                //userWithReference = _mapper.Map<RegisterModel>(await _userService.UserReferenceGetById(createUser.Id));
                return Ok(userWithReference);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        //[TypeFilter(typeof(UserActivityFilter))]
        [DescriptionUserActivity(UserActivityType.Get)]
        [AllowAnonymous]    
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            /*var users =await _userService.GetAll();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);*/
            var route = Request.Path.Value;
            var pageData = await _userService.GetAllForPaging(filter.PageNumber, filter.PageSize);
            var model = new List<UserModel>();
            pageData.entities.ForEach(x => model.Add(new UserModel(x.user,x.userReference)));
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserModel>(model, pageData.PaginationFilter, pageData.totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        [DescriptionUserActivity(UserActivityType.Get)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user =await _userService.GetById(id);
            var userReference = await _userService.UserReferenceGetById(id);
            var model =new UserModel(user, userReference);
            //model = _mapper.Map<UserModel>(userReference);
            return Ok(model);
        }
        [DescriptionUserActivity(UserActivityType.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateModel model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = id;
            var userReference = _mapper.Map<UserReference>(model);
            try
            {
                // update user 
                var updateUser = await _userService.Update(user, model.Password);
                var updateUserWithReference = new UpdateModel(updateUser);
                //updateUserWithReference = _mapper.Map<UpdateModel>(await _userService.UserReferenceGetById(updateUser.Id));
                return Ok(updateUserWithReference);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        [DescriptionUserActivity(UserActivityType.Delete)]
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
            var result = _mapper.Map <UserReferenceModel>(await _userService.UserReferenceCreate(user));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPut("UserReferenceUpdate")]
        public async Task<IActionResult> UserReferenceUpdate([FromBody] UserReferenceModel model)
        {
            var user = _mapper.Map<UserReference>(model);
            var result = _mapper.Map<UserReferenceModel>(await _userService.UserReferenceUpdate(user));
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
        [HttpGet("UserReferences")]
        public async Task<IActionResult> UserReferences()
        {
            var result = await _userService.UserReferences();
            var user = _mapper.Map<IList<UserReferenceModel>>(result);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpDelete("UserReferenceRemove/{userId}")]
        public async Task<IActionResult> UserReferenceRemove(int userId)
        {
            
            var result =await _userService.UserReferenceRemove(userId);
            return Ok(new { status = result });
        }
        ////////////////////////////////////////////////////////
        ///

        [AllowAnonymous]
        [HttpPost("PositionCreate")]
        public async Task<IActionResult> PositionCreate([FromBody] PositionModel model)
        {
            var position = _mapper.Map<Position>(model);
            var result = _mapper.Map<PositionModel>(await _userService.PositionCreate(position));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPut("PositionUpdate")]
        public async Task<IActionResult> PositionUpdate([FromBody] PositionModel model)
        {
            var position = _mapper.Map<Position>(model);
            var result = _mapper.Map<PositionModel>(await _userService.PositionUpdate(position));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("PositionGet/{Id}")]
        public async Task<IActionResult> PositionGet(int Id)
        {
            var result = await _userService.PositionGetById(Id);
            var position = _mapper.Map<PositionModel>(result);
            return Ok(position);
        }
        [AllowAnonymous]
        [HttpGet("Positions")]
        public async Task<IActionResult> Positions()
        {
            var result = await _userService.Positions();
            var position = _mapper.Map<IList<PositionModel>>(result);
            return Ok(position);
        }

        [AllowAnonymous]
        [HttpDelete("PositionRemove/{Id}")]
        public async Task<IActionResult> PositionRemove(int Id)
        {

            var result = await _userService.PositionRemove(Id);
            return Ok(new { status = result });
        }
        [AllowAnonymous]
        [HttpGet("Activity/{userId}")]
        public async Task<IActionResult> UserActivity(int userId)
        {

            var result = await _userService.UserActivityReport(userId);
            return Ok(result);
        }
    }
}
