using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ace_of_Spades_Pizza.Data.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Create(T model);
        void Update(T model);
        void Delete(int id);
    }
}
