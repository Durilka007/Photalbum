using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class PhotoeRepository : IPhotoeRepository
    {
        private readonly DbContext context;

        public PhotoeRepository(IUnitOfWork uow)
        {
            this.context = uow.Context;
            Debug.WriteLine("PhotoeRepository create!");
        }

        public IEnumerable<DalPhotoe> GetAll()
        {
            return context.Set<Photoe>().Select(photoe => new DalPhotoe()
            {
                Id = photoe.Id,
                AlbumId = photoe.AlbumId,
                Like = photoe.Like,
                Description = photoe.Description,
                ImagePhotoe= photoe.ImagePhotoe,
                ImagePhotoMimeType = photoe.ImagePhotoMimeType,
                AddTime = photoe.AddTime
            });
        }

        public DalPhotoe GetById(int key)
        {
            var ormphotoe = context.Set<Photoe>().FirstOrDefault(photoe => photoe.Id == key);
            return new DalPhotoe()
            {
                Id = ormphotoe.Id,
                AlbumId = ormphotoe.AlbumId,
                Like = ormphotoe.Like,
                Description = ormphotoe.Description,
                ImagePhotoe = ormphotoe.ImagePhotoe,
                ImagePhotoMimeType = ormphotoe.ImagePhotoMimeType,
                AddTime = ormphotoe.AddTime

            };
        }

        public DalPhotoe GetByPredicate(System.Linq.Expressions.Expression<Func<DalPhotoe, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalPhotoe e)
        {
            var photoe = new Photoe()
            {
                Id = e.Id,
                AlbumId = e.AlbumId,
                Like = e.Like,
                Description = e.Description,
                ImagePhotoe = e.ImagePhotoe,
                ImagePhotoMimeType = e.ImagePhotoMimeType,
                AddTime = e.AddTime
            };
            context.Set<Photoe>().Add(photoe);
        }

        public void Delete(DalPhotoe e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalPhotoe entity)
        {
            throw new NotImplementedException();
        }
    }
}
