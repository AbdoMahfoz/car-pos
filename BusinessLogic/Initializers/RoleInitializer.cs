using Repository.ExtendedRepositories;
using System.Linq;
using Models.DataModels;

namespace BusinessLogic.Initializers
{
    public class RoleInitializer : BaseInitializer
    {
        private readonly IRolesRepository RoleRepository;
        private readonly IPermissionsRepository PermissionsRepository;
        public RoleInitializer(IRolesRepository RoleRepository, IPermissionsRepository PermissionsRepository)
        {
            this.RoleRepository = RoleRepository;
            this.PermissionsRepository = PermissionsRepository;
        }
        protected override void Initialize()
        {
            if (RoleRepository.GetAll().Any()) return;
            RoleRepository.Insert(new Role { Name = "User" });
            Role admin = new Role { Name = "Admin" };
            RoleRepository.Insert(admin).Wait();
            if (!PermissionsRepository.GetAll().Where(u => u.Name == "CanManageRoles").Any())
            {
                PermissionsRepository.Insert(new Permission { Name = "CanManageRoles" }).Wait();
            }
            foreach (int PermissionId in PermissionsRepository.GetAll().Select(u => u.Id).ToList())
            {
                PermissionsRepository.AssignPermissionToRole(PermissionId, admin.Id);
            }
        }
    }
}
