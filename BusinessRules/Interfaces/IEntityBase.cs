using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.Interfaces
{
    public interface IEntityBase<T> where T: Entity
    {
        IList<T> GetAll();

        T GetById(int id);

        Task<T> Insert(T usuario);

        Task<T> Update(T usuario);

        Task Delete(int id);
    }
}
