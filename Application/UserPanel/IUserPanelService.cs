using Application.Interfaces.Context;
using AutoMapper;
using Common;
using Common.UploadImage;
using Common.Utils;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserPanel
{
    public interface IUserPanelService
    {
        #region Panel

        Task<UserPanelDto> Panel(long userId);


        #endregion

        #region Validation Mobile Number

        bool ValidationMobileNumber(string mobile);

        #endregion

        #region Find User With Id

        Task<User> FindUserWithId(long userId);

        #endregion

        #region Find User For Edit Panel

        Task<EditUserPanelDto> FindUserForEditPanel(long userId);

        #endregion

        #region Edit

        Task<EditUserPanelResult> Edit(EditUserPanelDto dto);


        #endregion

        #region Find User For Change Password

        Task<ChangePasswordDto> FindUserForChangePassword(long userId);



        #endregion

        #region Change Password

        Task<ChangePassworResult> ChangePassword(ChangePasswordDto dto);
       

        #endregion

    }

    public class UserPanelService : IUserPanelService
    {
        #region Constructor

        private readonly IDataBaseContext _context;

        private readonly IPasswordHasher _passwordhasher;

        private readonly IMapper _mapper;

        public UserPanelService(IDataBaseContext context, IPasswordHasher passwordhasher, IMapper mapper)
        {
            _context = context;
            _passwordhasher = passwordhasher;
            _mapper = mapper;
        }



        #endregion

        #region Panel

        public async Task<UserPanelDto> Panel(long userId)
        {
            var data = await FindUserWithId(userId);
            if (data == null)
                return null;
            var result = _mapper.Map<UserPanelDto>(data);

            return result;
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

        #region Find User With Id

        public async Task<User> FindUserWithId(long userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        #endregion

        #region Find User For Edit Panel

        public async Task<EditUserPanelDto> FindUserForEditPanel(long userId)
        {
            var data = await FindUserWithId(userId);
            if (data == null)
                return null;
            var result = _mapper.Map<EditUserPanelDto>(data);

            return result;
        }

        #endregion

        #region Edit

        public async Task<EditUserPanelResult> Edit(EditUserPanelDto dto)
        {
            var date = await FindUserWithId(dto.UserId);

            if (date == null)
                return EditUserPanelResult.NotFound;

            if (ValidationMobileNumber(dto.Mobile) && date.UserId != dto.UserId)
                return EditUserPanelResult.NotFound;

            if (dto.AvatarFile != null)
            {
                if (!dto.AvatarFile.IsImage())
                    return EditUserPanelResult.AvatarHasNotImage;

                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.AvatarFile.FileName);
                dto.AvatarFile.AddImageToServer(imageName, PathExtension.UserAvatarOriginServer, 100, 100,
                     PathExtension.UserAvatarThumbServer, dto.Avatar);
                dto.Avatar = imageName;

            }

            date.Edit(dto.FirstName, dto.LastName, dto.Email, dto.Mobile, dto.Avatar,
                date.IsEmailActive, date.IsMobileActive);

            _context.Users.Update(date);
            _context.SaveChanges();

            return EditUserPanelResult.EditSuccess;
        }


        #endregion

        #region Find User For Change Password

        public async Task<ChangePasswordDto>FindUserForChangePassword(long userId)
        {
            var data = await FindUserWithId(userId);

            if (data == null)
                return null;
            ChangePasswordDto result = new ChangePasswordDto();
            result.UserId = data.UserId;

            return result;

        }


        #endregion

        #region Change Password

        public async Task<ChangePassworResult>ChangePassword(ChangePasswordDto dto)
        {
            var data = await FindUserWithId(dto.UserId);

            if (data == null)
                return ChangePassworResult.NotFound;
          
            var validationPassowrd = _passwordhasher.Check(data.Password, dto.OldPassword);

            if (validationPassowrd.Verified != true)
                return ChangePassworResult.InvalidPassword;

            dto.NewPassword = _passwordhasher.Hash(dto.NewPassword);
            data.ChangePassword(dto.NewPassword);
            _context.Users.Update(data);
            _context.SaveChanges();
            return ChangePassworResult.ChangedSuccess;

        }

        #endregion
    }

    public class UserPanelDto
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Avatar { get; set; }

    }

    public class EditUserPanelDto
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }

        public string Avatar { get; set; }

        public IFormFile AvatarFile { get; set; }

    }

    public enum EditUserPanelResult
    {
        EditSuccess,
        NotFound,
        AvatarHasNotImage
    }

    public class ChangePasswordDto
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور باهم متابقت ندارد")]
        public string ReNewPassword { get; set; }


    }

    public enum ChangePassworResult
    {
        ChangedSuccess,
        NotFound,
        InvalidPassword
    }
}
