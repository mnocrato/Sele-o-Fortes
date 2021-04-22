using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudFortes.Entidades
{
    public class Pedidos
    {
        public virtual int IdPedido { get; set; }
        public virtual DateTime DtPedido { get; set; }
        public virtual int IdProduto { get; set; }
        public virtual int QntdProdutos { get; set; }
        public virtual int IdFornecedor { get; set; }
        public virtual decimal ValorTotal { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Produto Produto { get; set; }

        public Pedidos(){}

        public Pedidos(int idPedido, DateTime dtPedido, int idProduto, int qntdProdutos, int idFornecedor, decimal valorTotal, Fornecedor fornecedor, Produto produto)
        {
            this.IdPedido = idPedido;
            this.DtPedido = dtPedido;
            this.IdProduto = idProduto;
            this.QntdProdutos = qntdProdutos;
            this.IdFornecedor = idFornecedor;
            this.ValorTotal = valorTotal;
            this.Fornecedor = fornecedor;
            this.Produto = produto;
        }
        
    }
}