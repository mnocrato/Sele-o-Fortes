using CrudFortes.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using CrudFortes.Entidades;

namespace CrudFortes.Implementacao
{
    public class RepositorioBase<T> : ICrudDao<T> where T : class
    {

        public void Delet(T entidade)
        {
            using (ISession _session = NibernateConexao.AbrirSessao())
            {
                using (ITransaction _transaction = _session.BeginTransaction())
                {
                    try
                    {
                        _session.Delete(entidade);
                        _transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!_transaction.WasCommitted) _transaction.Rollback();
                        throw new Exception("Erro ao deletar:" + ex.Message);
                    }
                }
            }
        }

        public IList<T> Find()
        {
            using (ISession _session = NibernateConexao.AbrirSessao())
            {
                return (from rs in _session.Query<T>() select rs).ToList();
            }
        }

        public T FindId(int id)
        {
            using (ISession _session = NibernateConexao.AbrirSessao())
            {
                return _session.Get<T>(id);
            }
        }

        public IList<Pedidos> FindByFornecedor(int idFornecedor)
        {
            using (ISession _session = NibernateConexao.AbrirSessao())
            {
                return (from rs in _session.Query<Pedidos>() select rs ).Where(x => x.IdFornecedor == idFornecedor).ToList();
            }
        }

        public void Insert(T entidade)
        {
            using (ISession _session = NibernateConexao.AbrirSessao())
            {
                using (ITransaction _transaction = _session.BeginTransaction())
                {
                    try
                    {
                        _session.Save(entidade);
                        _transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!_transaction.WasCommitted) _transaction.Rollback();
                        throw new Exception("Erro ao salvar:" + ex.Message);
                    }
                }
            }
        }

        public void Update(T entidade)
        {
            using (ISession _session = NibernateConexao.AbrirSessao())
            {
                using (ITransaction _transaction = _session.BeginTransaction())
                {
                    try
                    {
                        _session.Update(entidade);
                        _transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (!_transaction.WasCommitted) _transaction.Rollback();
                        throw new Exception("Erro ao atualizar:" + ex.Message);
                    }
                }
            }
        }
    }
}