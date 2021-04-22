using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudFortes.Entidades
{
    public class Fornecedor
    {
        public virtual int IdFornecedor { get; set; }
        public virtual string RazaoSocial { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual string Uf { get; set; }
        public virtual string Email { get; set; }
        public virtual string NomeContato { get; set; }
        public virtual IList<Pedidos> Pedidos { get; set; }

        public Fornecedor(){}

        public Fornecedor(int idFornecedor, string razaoSocial, string cnpj, string uf,string email,string nomecontato, IList<Pedidos> pedidos)
        {
            this.IdFornecedor = IdFornecedor;
            this.RazaoSocial = razaoSocial;
            this.Cnpj = cnpj;
            this.Uf = uf;
            this.Email = email;
            this.NomeContato = nomecontato;
            this.Pedidos = pedidos;
        }
    }
}