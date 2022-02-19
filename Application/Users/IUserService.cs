using Application.Interfaces.Context;
using AutoMapper;
using Common;
using Common.Paging;
using Common.UploadImage;
using Common.Utils;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users
{
    public interface IUserService
    {
        #region Register

        RegisterResult Register(RegisterUserDTO dto);


        #endregion

        #region Login

        LoginResult Login(LoginDTO dto);

        #endregion

        #region Get User By Mobile

        User GetUserByMobile(string mobile);

        #endregion

        #region Validation Mobile Number

        bool ValidationMobileNumber(string mobile);

        #endregion

        #region Get User With userId

        User GetUserWithUserId(long userId);

        #endregion

        #region Admin

        #region Users

        #region Filter Users

        List<UsersDTO> FilterUsers();

        #endregion

        #region Add User

        AddUserResult AddUser(AddUserDTo dto);

        #endregion

        #region Get User For Edit

        EditUserDTo GetUserForEdit(long userId);

        #endregion

        #region Edit User

        EditUserResult Edit(EditUserDTo dto, IFormFile AvatarFile);

        #endregion

        #region Get User For Delete

        DeleteUserDTo GetUserForDelete(long userId);

        #endregion

        #region Delete User 

        DeleteUserResult Delete(DeleteUserDTo dto);

        #endregion

        #region Get User For Change Password

        ChangePasswordUserDto GetUserForChangePassword(long userId);

        #endregion

        #region Changed Password

        ChangePasswordUserResult ChangedPassword(ChangePasswordUserDto dto);

        #endregion

        #region Add User Role

        AddUserRoleResult AddUserRole(AddUserRolesDTo dto);

        #endregion

        #region Selected Roles

        IEnumerable<long> SelectedRoles(long userId);

        #endregion

        #region Delete User Roles

        void DeleteUserRoles(long userId, IEnumerable<long> rolesId);


        #endregion

        #region Edit User Roles

        EditUserRoleResult EditUserRoles(AddUserRolesDTo dto);


        #endregion

        #endregion

        #endregion
    }

    public class UserServcie : IUserService
    {

        #region Constructor

        private readonly IDataBaseContext _context;

        private readonly IPasswordHasher _passwordhasher;

        private readonly IMapper _mapper;

        public UserServcie(IDataBaseContext context, IPasswordHasher passwordhasher, IMapper mapper)
        {
            _context = context;
            _passwordhasher = passwordhasher;
            _mapper = mapper;
        }



        #endregion

        #region Register

        public RegisterResult Register(RegisterUserDTO dto)
        {
            if (ValidationMobileNumber(dto.Mobile))
                return RegisterResult.IsExistMobileNumber;

            var password = _passwordhasher.Hash(dto.Password);
            var user = new User(dto.FirstName, dto.LastName, dto.Mobile, password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return RegisterResult.SuccessRegister;

        }

        #endregion

        #region Login

        public LoginResult Login(LoginDTO dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Mobile == dto.Mobile);

            if (user == null)
                return LoginResult.NotFound;

            if (user.IsMobileActive == false)
                return LoginResult.NotActivated;

            (bool Verified, bool NeedsUpgrade) result = _passwordhasher.Check(user.Password, dto.Password);
            if (!result.Verified)
                return LoginResult.NotFound;

            return LoginResult.Success;
        }

        #endregion

        #region Get User By Mobile

        public User GetUserByMobile(string mobile)
        {
            var user = _context.Users.FirstOrDefault(x => x.Mobile == mobile && x.IsMobileActive);

            if (user != null)
                return user;

            return null;

        }


        #endregion

        #region Validation Mobile Number

        public bool ValidationMobileNumber(string mobile)
        {
            var Mobile = _context.Users.AsQueryable().FirstOrDefault(x => x.Mobile == mobile);

            if (Mobile != null)
                return true;

            return false;
        }

        #endregion

        #region Get User With userId

        public User GetUserWithUserId(long userId)
        {
            return _context.Users.FirstOrDefault(x => x.UserId == userId);
        }

        #endregion

        #region Admin

        #region Users


        #region Filter Users

        public List<UsersDTO> FilterUsers()
        {
            var model = _context.Users.AsQueryable();

            var result = _mapper.ProjectTo<UsersDTO>(model).ToList();

            return result;


        }

        #endregion

        #region Add User

        public AddUserResult AddUser(AddUserDTo dto)
        {
            if (ValidationMobileNumber(dto.Mobile))
                return AddUserResult.IsExistMobileNumber;
            dto.MobileActiveCode = new Random().Next(10000, 999999).ToString();
            dto.EmailActiveCode = Guid.NewGuid().ToString("N");
            dto.Password = _passwordhasher.Hash(dto.Password);
            if (dto.AvatarFile != null && dto.AvatarFile.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.AvatarFile.FileName);
                dto.AvatarFile.AddImageToServer(imageName, PathExtension.UserAvatarOriginServer, 100, 100,
                    PathExtension.UserAvatarThumbServer);
                dto.Avatar = imageName;
            }

            var data = _mapper.Map<User>(dto);

            _context.Users.Add(data);
            _context.SaveChanges();

            return AddUserResult.SuccessRegister;

        }


        #endregion

        #region Get User For Edit

        public EditUserDTo GetUserForEdit(long userId)
        {
            var data = _context.Users.FirstOrDefault(x => x.UserId == userId);
            if (data == null)
                return null;

            var user = _mapper.Map<EditUserDTo>(data);
            return user;
        }


        #endregion

        #region Edit User

        public EditUserResult Edit(EditUserDTo dto, IFormFile AvatarFile)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == dto.UserId);
            if (user == null)
                return EditUserResult.NotFoundUser;

            if (ValidationMobileNumber(dto.Mobile) && user.UserId != dto.UserId)
                return EditUserResult.IsExistMobileNumber;

            if (AvatarFile != null && AvatarFile.IsImage())
            {

                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(AvatarFile.FileName);
                AvatarFile.AddImageToServer(imageName, PathExtension.UserAvatarOriginServer, 100, 100,
                    PathExtension.UserAvatarThumbServer, dto.Avatar);
                dto.Avatar = imageName;
            }

            user.Edit(dto.FirstName, dto.LastName, dto.Email, dto.Mobile, dto.Avatar, dto.IsEmailActive, dto.IsMobileActive);

            _context.Users.Update(user);
            _context.SaveChanges();

            return EditUserResult.SuccessEdit;
        }

        #endregion

        #region Get User For Delete

        public DeleteUserDTo GetUserForDelete(long userId)
        {
            var data = _context.Users.FirstOrDefault(x => x.UserId == userId);
            if (data == null)
                return null;

            var user = _mapper.Map<DeleteUserDTo>(data);
            return user;
        }


        #endregion

        #region Delete User 

        public DeleteUserResult Delete(DeleteUserDTo dto)
        {
            var user = GetUserWithUserId(dto.UserId);
            if (user == null)
                return DeleteUserResult.NotFoundUser;

            _context.Users.Remove(user);
            _context.SaveChanges();

            return DeleteUserResult.DeleteSuccess;
        }

        #endregion

        #region Get User For Change Password

        public ChangePasswordUserDto GetUserForChangePassword(long userId)
        {
            var user = GetUserWithUserId(userId);
            if (user == null)
                return null;
            var model = _mapper.Map<ChangePasswordUserDto>(user);

            model.Password = "";

            return model;
        }

        #endregion

        #region Changed Password

        public ChangePasswordUserResult ChangedPassword(ChangePasswordUserDto dto)
        {
            var user = GetUserWithUserId(dto.UserId);

            if (user == null)
                return ChangePasswordUserResult.NotFoundUser;

            dto.Password = _passwordhasher.Hash(dto.Password);
            user.ChangePassword(dto.Password);
            _context.Users.Update(user);
            _context.SaveChanges();

            return ChangePasswordUserResult.SuccessChangedPassword;
        }
        #endregion

        #region Add User Role

        public AddUserRoleResult AddUserRole(AddUserRolesDTo dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == dto.UserId);

            if (user == null)
                return AddUserRoleResult.NotFound;

            foreach (var roleId in dto.RolesId)
            {
                _context.RolesUsers.Add(new Domain.Roles.RolesUser()
                {
                    RoleId = roleId,
                    UserId = dto.UserId
                });
            }
            _context.SaveChanges();

            return AddUserRoleResult.Success;
        }


        #endregion

        #region Selected Roles

        public IEnumerable<long> SelectedRoles(long userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
                return null;
            return _context.RolesUsers.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
        }
        #endregion

        #region Delete User Roles

        public void DeleteUserRoles(long userId, IEnumerable<long> rolesId)
        {
            _context.RolesUsers.Where(x => x.UserId == userId).ToList()
                .ForEach(x =>
                _context.RolesUsers.Remove(x));
        }

        #endregion

        #region Edit User Roles

        public EditUserRoleResult EditUserRoles(AddUserRolesDTo dto)
        {
            var user = GetUserWithUserId(dto.UserId);
            if (user == null)
                return EditUserRoleResult.NotFound;

            IEnumerable<long> userRoles = _context.RolesUsers.Where(x => x.UserId == dto.UserId).
                Select(x => x.RoleId).ToList();

            if (userRoles.Count()>0)
            {
                DeleteUserRoles(dto.UserId, dto.RolesId);
                _context.SaveChanges();
            }

           if(dto.RolesId !=null)
                AddUserRole(dto);

            return EditUserRoleResult.EditSuccess;

        }


        #endregion

        #endregion

        #endregion
    }

}

