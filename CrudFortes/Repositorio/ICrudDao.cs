using CrudFortes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudFortes.Repositorio
{
    public interface ICrudDao <T>
    {
        void Insert(T entidade);
        void Update(T entidade);
        void Delet(T entidade);
        T FindId(int id);
        IList<T> Find();
        IList<Pedidos> FindByFornecedor(int idFonecedor);
    }
}
