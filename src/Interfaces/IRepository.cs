using System.Collections.Generic;

namespace ServerING.Interfaces {
    public interface IRepository<T> {

        void Add(T model);
        void Update(T model);
        T Delete(int id);

        IEnumerable<T> GetAll();
        T GetByID(int id);
    }
}
