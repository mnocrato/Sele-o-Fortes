using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudFortes.Entidades;
using FluentNHibernate.Mapping;

namespace CrudFortes.Map
{
    public class ProdutoMap: ClassMap<Produto>
    {
        public ProdutoMap()
        {
            Table("Produto");
            Id(u => u.IdProduto).Column("IdProduto");
            Map(u => u.Descricao).Column("Descricao").Not.Nullable();
            Map(u => u.DtCadastro).Column("DtCadastro").Not.Nullable();           
            Map(u => u.ValorProduto).Column("ValorProduto");
            HasMany(u => u.Pedidos)
                .KeyColumn("IdProduto").Not.LazyLoad();
                            
        }
    }
}