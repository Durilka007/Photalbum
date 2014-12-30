using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Interface.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        DbContext Context { get; }
        void Commit();
    }
}
