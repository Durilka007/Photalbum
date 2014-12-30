using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IService<TEntity>
    {
         IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(int key);
    }
}
