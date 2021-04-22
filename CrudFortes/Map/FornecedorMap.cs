using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudFortes.Entidades;
using FluentNHibernate.Mapping;

namespace CrudFortes.Map
{
    public class FornecedorMap: ClassMap<Fornecedor>
    {
        public FornecedorMap()
        {
            Table("Fornecedor");
            Id(u => u.IdFornecedor).Column("IdFornecedor");            
            Map(u => u.RazaoSocial).Column("RazaoSocial").Unique().Not.Nullable();
            Map(u => u.Cnpj).Column("CNPJ").Unique().Not.Nullable();
            Map(u => u.Uf).Column("UF").Not.Nullable();
            Map(u => u.Email).Column("Email").Not.Nullable();       
            Map(u => u.NomeContato).Column("Nomecontato");
            HasMany(u => u.Pedidos)
                .KeyColumn("IdFornecedor").Not.LazyLoad();
        }
    }
}