using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudFortes.Entidades
{
    public class Produto
    {
        public virtual int IdProduto { get; set; }
        public virtual string Descricao { get; set; }
        public virtual DateTime DtCadastro { get; set; }
        public virtual decimal? ValorProduto { get; set; }
        public virtual IList<Pedidos> Pedidos { get; set; }

        public Produto(int idProduto, string descricao, DateTime dtCadastro, decimal? valorProduto, IList<Pedidos> pedidos)
        {
            this.IdProduto = idProduto;
            this.Descricao = descricao;
            this.DtCadastro = DtCadastro;
            this.ValorProduto = valorProduto;
            this.Pedidos = pedidos;
        }

        public Produto(){}
    }
}