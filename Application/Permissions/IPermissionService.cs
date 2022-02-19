using Application.Interfaces.Context;
using Application.Users;
using AutoMapper;
using Domain.Roles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Application.Permissions
{
    public interface IPermissionService
    {
        #region Filetr Roles

        IEnumerable<RoleDto> FilterRole();
        #endregion

        #region Is Exist Role

        bool IsExistRole(string roleTitle);
        #endregion

        #region Create Role

        CreateRoleResult Create(CreateRoleDto dto, List<long> Permission);

        #endregion

        #region Get All Permission

        IEnumerable<Permission> GetAllPermission();

        #endregion

        #region Add Permission To Role

        void AddPermissionToRoe(long roleId, List<long> PermissionsId);

        #endregion

        #region Get Role With RoleId

        Role GetRoleWithRoleId(long roleId);

        #endregion

        #region Get Role For Edit

        EditRoleDto GetRoleForEdit(long roleId);

        #endregion

        #region Get Permissions Role

        IEnumerable<long> GetPermissionsRole(long roleId);

        #endregion

        #region Edit Role

        EditRoleResult EditRole(EditRoleDto dto, List<long> selectedPermissions);


        #endregion

        #region Get Role For Delete

        DeleteRoleDto GetRoleForDelete(long roleId);


        #endregion

        #region Delete Role

        DeleteRoleResult DeleteRole(DeleteRoleDto dto);
      
        #endregion

        #region Add Role Permissions

        void AddRolePermissions(long roleId, List<long> permissionsId);
        

        #endregion

        #region Edit PermissionsRole

        void EditPermissionsRole(long roleId, List<long> permissionsId);
        
        #endregion
    }

    public class PermissionService : IPermissionService
    {

        #region Constructor

        private readonly IDataBaseContext _context;


        private readonly IMapper _mapper;

        public PermissionService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        #endregion


        #region Filetr Roles

        public IEnumerable<RoleDto> FilterRole()
        {
            var data = _context.Roles.AsQueryable();

            var result = _mapper.ProjectTo<RoleDto>(data).ToList();

            return result;
        }


        #endregion

        #region Create Role

        public CreateRoleResult Create(CreateRoleDto dto, List<long> Permission)
        {
            if (IsExistRole(dto.RoleTitle))
                return CreateRoleResult.IsExistRoleTitle;

            var data = _mapper.Map<Role>(dto);
            _context.Roles.Add(data);
            _context.SaveChanges();

            if (data.RoleId != 0)
            {
                AddPermissionToRoe(data.RoleId, Permission);
            }


            return CreateRoleResult.SuccessCreate;
        }


        #endregion

        #region Is Exist Role

        public bool IsExistRole(string roleTitle)
        {
            var role = _context.Roles.FirstOrDefault(x => x.RoleTitle == roleTitle);
            if (role == null)
                return false;

            return true;
        }


        #endregion

        #region Get All Permission

        public IEnumerable<Permission> GetAllPermission()
        {
            return _context.Permissions.AsQueryable().ToList();
        }

        #endregion

        #region Add Permission To Role

        public void AddPermissionToRoe(long roleId, List<long> PermissionsId)
        {
            foreach (var perId in PermissionsId)
            {
                _context.PermissionsRoles.Add(new PermissionsRole
                {
                    RoleId = roleId,
                    PermissionId = perId
                });
            }
            _context.SaveChanges();
        }


        #endregion

        #region Get Role With RoleId

        public Role GetRoleWithRoleId(long roleId)
        {
            return _context.Roles.FirstOrDefault(x => x.RoleId == roleId);
        }

        #endregion

        #region Get Role For Edit

        public EditRoleDto GetRoleForEdit(long roleId)
        {
            var role = GetRoleWithRoleId(roleId);
            if (role == null)
                return null;

            var result = _mapper.Map<EditRoleDto>(role);

            return result;
        }

        #endregion

        #region Get Permissions Role

        public IEnumerable<long> GetPermissionsRole(long roleId)
        {
            return _context.PermissionsRoles.Where(x => x.RoleId == roleId).Select(x => x.PermissionId).AsQueryable().ToList();
        }

        #endregion

        #region Edit Role

        public EditRoleResult EditRole(EditRoleDto dto, List<long> selectedPermissions)
        {
            var role = GetRoleWithRoleId(dto.RoleId);

            if (role == null)
                return EditRoleResult.NotFound;

            if (IsExistRole(dto.RoleTitle) && role.RoleId !=dto.RoleId)
                return EditRoleResult.IsExistRoleTitle;

            role.RoleTitle = dto.RoleTitle;
            _context.Roles.Update(role);
            _context.SaveChanges();

            if(selectedPermissions.Count()> 0)
            {
                List<long> rolePermission = _context.PermissionsRoles.Where(x => x.RoleId == role.RoleId).
                Select(x => x.PermissionsRoleId).ToList();

                if (rolePermission.Count() > 0)
                {
                    EditPermissionsRole(role.RoleId, rolePermission);
                }


                AddPermissionToRoe(role.RoleId, selectedPermissions);
            }

            return EditRoleResult.SuccessEdit;

        }


        #endregion

        #region Get Role For Delete

        public DeleteRoleDto GetRoleForDelete(long roleId)
        {
            var data = GetRoleWithRoleId(roleId);
            if (data == null)
                return null;
            var result = _mapper.Map<DeleteRoleDto>(data);

            return result;
        }

        #endregion

        #region Delete Role

        public DeleteRoleResult DeleteRole(DeleteRoleDto dto)
        {
            var role = GetRoleWithRoleId(dto.RoleId);
            if (role == null)
                return DeleteRoleResult.NotFound;



            var PermissionsRole = _context.PermissionsRoles.Where(x => x.RoleId == dto.RoleId).
                Select(x=> x.PermissionsRoleId).ToList();

            if(PermissionsRole.Count()>0)
            {
                EditPermissionsRole(dto.RoleId, PermissionsRole);
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return DeleteRoleResult.DeleteSuccess;
        }

        #endregion

        #region Add Role Permissions

        public void AddRolePermissions(long roleId, List<long> permissionsId)
        {
            if (roleId != 0 && permissionsId.Count() > 0)
            {
                foreach (var permissionId in permissionsId)
                {
                    _context.PermissionsRoles.Add(new PermissionsRole
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    });
                }
                _context.SaveChanges();
            }
        }

        #endregion

        #region Edit PermissionsRole

        public void EditPermissionsRole(long roleId, List<long> permissionsId)
        {
            _context.PermissionsRoles.
               Where(x => x.RoleId == roleId).ToList().ForEach(f =>
                   _context.PermissionsRoles.Remove(f));


        }
        #endregion


       
    }

    public class RoleDto
    {

        public long RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        public string RoleTitle { get; set; }

        [Display(Name = "تعداد دسترسی های نقش")]
        public int PermissionCount { get; set; }
    }

    public enum CreateRoleResult
    {
        SuccessCreate,
        IsExistRoleTitle,
    }

    public class CreateRoleDto
    {
        public string RoleTitle { get; set; }

        public ICollection<PermissionsRole> PermissionsRoles { get; set; }
    }

    public enum EditRoleResult
    {
        SuccessEdit,
        IsExistRoleTitle,
        NotFound
    }

    public class EditRoleDto
    {
        public long RoleId { get; set; }

        public string RoleTitle { get; set; }

    }

    public class DeleteRoleDto
    {
        public long RoleId { get; set; }

        public string RoleTitle { get; set; }
    }

    public enum DeleteRoleResult
    {
        NotFound,
        DeleteSuccess
    }




}
