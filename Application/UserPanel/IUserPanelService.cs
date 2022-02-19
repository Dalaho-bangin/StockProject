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

        #region Find User With Id

        Task<User> FindUserWithId(long userId);

        #endregion

        #region Find User For Edit Panel

        Task<EditUserPanelDto> FindUserForEditPanel(long userId);

        #endregion

        #region Edit

        Task<EditUserPanelResult> Edit(EditUserPanelDto dto);


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

            if (dto.Avatar != null)
            {
                if(!dto.AvatarFile.IsImage())
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

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
}
