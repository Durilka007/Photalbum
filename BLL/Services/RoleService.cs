using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
            Debug.WriteLine("Role service create!");
        }
        public IEnumerable<RoleEntity> GetAll()
        {
            return roleRepository.GetAll().Select(role => role.ToBllRole());
        }

        public void Create(RoleEntity role)
        {
            roleRepository.Create(role.ToDalRole());
            uow.Commit();
        }

        public void Delete(RoleEntity role)
        {
            roleRepository.Delete(role.ToDalRole());
            uow.Commit();
        }

        public RoleEntity GetById(int id)
        {
            return roleRepository.GetById(id).ToBllRole();
        }
    }
}
