﻿using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Application.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppException = ProductTermsControl.Application.Helpers.AppException;
using ProductTermsControl.Domain.HelperModel;
using System.Globalization;
using ProductTermsControl.Application.Localize;
using Microsoft.Extensions.Localization;

namespace ProductTermsControl.Application.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(User user, string password);
        Task<User> Update(User user, string password = null);
        Task<string> Delete(int id);

        Task<GetAllWithPaging<UserWithReference>> GetAllForPaging(int PageNumber, int PageSize);

        Task<UserReference> UserReferenceCreate(UserReference userReference);
        Task<UserReference> UserReferenceUpdate(UserReference userReference);
        Task<string> UserReferenceRemove(int userId);
        Task<UserReference> UserReferenceGetById(int userId);
        Task<IEnumerable<UserReference>> UserReferences();

        Task<Position> PositionCreate(Position position);
        Task<Position> PositionUpdate(Position position);
        Task<string> PositionRemove(int Id);
        Task<Position> PositionGetById(int Id);
        Task<IEnumerable<Position>> Positions();
        Task<List<UserActivityByDate>> UserActivityReport(int userId);
    }

    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IStringLocalizer<Resource> _localizer;

        public UserService(IApplicationDbContext context, IStringLocalizer<Resource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> Create(User user, string password)
        {
            var k = _localizer["String1"];
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException(_localizer["String1"]);
            


            if (await _context.Users.AnyAsync(u=>u.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
          


            return user;
        }

        public async Task<User> Update(User userParam,string password = null)
        {
            var user =await _context.Users.FindAsync(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (await _context.Users.AnyAsync(u=>u.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");

                user.Username = userParam.Username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            user.UpdateOtherProperies(userParam);
            _context.Users.Update(user);
            
            await _context.SaveChangesAsync();
            return user;


        }

        public async Task<string> Delete(int id)
        {
            var user =await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            return ResultStatus.FAILED;
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<UserReference> UserReferenceCreate(UserReference userReference)
        {
            await _context.UserReferences.AddAsync(userReference);
            await _context.SaveChangesAsync();
            return userReference;
        }
        public async Task<UserReference> UserReferenceUpdate(UserReference userReference)
        {
            _context.UserReferences.Update(userReference);
            await _context.SaveChangesAsync();
            return userReference;
        }
        public async Task<string> UserReferenceRemove(int userId)
        {
            _context.UserReferences.Remove(await _context.UserReferences.FindAsync(userId));
            await _context.SaveChangesAsync();
            return ResultStatus.SUCCESS;
        }

        public async Task<UserReference> UserReferenceGetById(int userId)
        {
            var result = from UR in _context.UserReferences
                         join U in _context.Users on UR.UserId equals U.Id
                         join MB in _context.MagazineBranches on UR.MagazineBranchId equals MB.Id into UR_MB
                         from MB in UR_MB.DefaultIfEmpty()
                         join P in _context.Positions on UR.PositionId equals P.Id into UR_P
                         from P in UR_P.DefaultIfEmpty()
                         where UR.UserId == userId
                         select new UserReference(UR, U, MB, P);

            return await result.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<UserReference>> UserReferences()
        {
            return await _context.UserReferences.ToListAsync();
        }

        public async Task<GetAllWithPaging<UserWithReference>> GetAllForPaging(int PageNumber, int PageSize)
        {

            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var totalRecords = _context.Users.CountAsync();
            if (PageSize == 0)
            {
                validFilter.PageSize = await totalRecords;
            }
            var pagedData = await
                (
                from U in _context.Users
                join UR in _context.UserReferences on U.Id equals UR.UserId into U_UR
                from UR in U_UR.DefaultIfEmpty()
                join MB in _context.MagazineBranches on UR.MagazineBranchId equals MB.Id into UR_MB
                from MB in UR_MB.DefaultIfEmpty()
                join P in _context.Positions on UR.PositionId equals P.Id into UR_P
                from P in UR_P.DefaultIfEmpty()
                orderby U.Id ascending
                select new UserWithReference { user = U, userReference = new UserReference(UR, U, MB, P) }
                )
                /*var pagedData = await _context.Users
                .GroupJoin(
                _context.UserReferences,
                U => U.Id,
                UR => UR.UserId,
                (U, UR) => new { user = U, userReference = UR }
                )
                .SelectMany(
                    xy => xy.userReference.DefaultIfEmpty(),
                    (x, y) => new UserWithReference { user = x.user, userReference = y }
                 )
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();*/
                //////////////////////
               
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();


            var result = new GetAllWithPaging<UserWithReference>(validFilter, pagedData, await totalRecords);
            return result;
        }


        
        public async Task<Position> PositionCreate(Position position)
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return position;
        }
        public async Task<Position> PositionUpdate(Position position)
        {
            _context.Positions.Update(position);
            await _context.SaveChangesAsync();
            return position;
        }
        public async Task<string> PositionRemove(int Id)
        {
            _context.Positions.Remove(await _context.Positions.FindAsync(Id));
            await _context.SaveChangesAsync();
            return ResultStatus.SUCCESS;
        }

        public async Task<Position> PositionGetById(int Id)
        {
            return await _context.Positions.FindAsync(Id);
        }
        public async Task<IEnumerable<Position>> Positions()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<List<UserActivityByDate>> UserActivityReport(int userId)
        {
            var result = from UA in await _context.UserActivities.ToListAsync()
                         where UA.UserId == userId && UA.ActivityType != "GET"
                         group UA by UA.CreateDate.Date into gUA
                         //join U in _context.Users on gUA.FirstOrDefault().UserId equals U.Id
                         select new UserActivityByDate(gUA.ToList());
            return result.OrderByDescending(x=>x.Date).ToList();
        }
    }
}
