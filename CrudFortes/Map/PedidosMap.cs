using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudFortes.Entidades;
using FluentNHibernate.Mapping;

namespace CrudFortes.Map
{
    public class PedidosMap: ClassMap<Pedidos>
    {
        public PedidosMap()
        {
            Table("Pedido");

            Id(u => u.IdPedido).Column("IdPedido");
            Map(u => u.DtPedido).Column("DtPedido").Not.Nullable();
            Map(u => u.IdProduto).Column("IdProduto").Not.Nullable();
            Map(u => u.QntdProdutos).Column("QntdProdutos").Not.Nullable();
            Map(u => u.IdFornecedor).Column("IdFornecedor").Not.Nullable();
            Map(u => u.ValorTotal).Column("ValorTotal");

            References(u => u.Produto, "IdProduto").Not.LazyLoad().ReadOnly();
            References(u => u.Fornecedor, "IdFornecedor").Not.LazyLoad().ReadOnly();

        }
    }
}