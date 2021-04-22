using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using CrudFortes.Entidades;
using CrudFortes.Repositorio;

namespace CrudFortes.Handler
{
    /// <summary>
    /// Descrição resumida de CrudHandlerGenerico
    /// </summary>
    public class CrudHandlerGenerico : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        

        private const string SESSION_ENTIDADE_FORNECEDOR = "Fornecedor";
        private const string SESSION_ENTIDADE_PRODUTO = "Produto";
        private const string SESSION_ENTIDADE_PEDIDO = "Pedido";

        private const string SESSION_ENTIDADE_CONSULTAR = "Consultar";
        private const string SESSION_SALVAR = "Salvar";
        private const string SESSION_ATUALIZAR = "Atualizar";
        private const string SESSION_DELETAR = "Deletar";

        public void ProcessRequest(HttpContext context)
        {
            NameValueCollection formulario = context.Request.Params;

            string entidade = formulario.Get("entidade");
            string tipo = formulario.Get("tipo");
            string linhas = formulario.Get("rows");
            string pagina = formulario.Get("page");

            if (entidade.Equals(SESSION_ENTIDADE_FORNECEDOR))
            {
                
                if (tipo.Equals(SESSION_SALVAR))
                {
                    string razaoSocial = formulario.Get("razaoSocial");
                    string cnpj = formulario.Get("cnpj");
                    string uf = formulario.Get("uf").ToUpper();
                    string email = formulario.Get("email");
                    string nomeContato = formulario.Get("nomeContato");

                    Fornecedor fornecedor = new Fornecedor();
                    FornecedorRepositorio fornecedorRepositorio = new FornecedorRepositorio();

                    fornecedor.RazaoSocial = razaoSocial;
                    fornecedor.Cnpj = cnpj;
                    fornecedor.Uf = uf;
                    fornecedor.Email = email;
                    fornecedor.NomeContato = nomeContato;
                    //fornecedor.Pedidos = null;

                    fornecedorRepositorio.Insert(fornecedor);
                    List<Fornecedor> fornecedores = new List<Fornecedor>();
                    fornecedores = ConvertIntoListFornecedor(fornecedorRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize(fornecedores);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_ATUALIZAR))
                {
                    int idFornecedor = Convert.ToInt32(formulario.Get("idFornecedor").ToString());
                    string razaoSocial = formulario.Get("razaoSocial");
                    string cnpj = formulario.Get("cnpj"); ;
                    string uf = formulario.Get("uf").ToUpper();
                    string email = formulario.Get("email");
                    string nomeContato = formulario.Get("nomeContato");

                    Fornecedor fornecedor = new Fornecedor();
                    FornecedorRepositorio fornecedorRepositorio = new FornecedorRepositorio();

                    fornecedor = fornecedorRepositorio.FindId(idFornecedor);
                    fornecedor.RazaoSocial = razaoSocial;
                    fornecedor.Cnpj = cnpj;
                    fornecedor.Uf = uf;
                    fornecedor.Email = email;
                    fornecedor.NomeContato = nomeContato;

                    fornecedorRepositorio.Update(fornecedor);
                    List<Fornecedor> fornecedores = new List<Fornecedor>();
                    fornecedores = ConvertIntoListFornecedor(fornecedorRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize(fornecedores);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_DELETAR))
                {
                    int idFornecedor = Convert.ToInt32(formulario.Get("idFornecedor").ToString());

                    Fornecedor fornecedor = new Fornecedor();
                    FornecedorRepositorio fornecedorRepositorio = new FornecedorRepositorio();

                    fornecedor = fornecedorRepositorio.FindId(idFornecedor);

                    fornecedorRepositorio.Delet(fornecedor);
                    List<Fornecedor> fornecedores = new List<Fornecedor>();
                    fornecedores = ConvertIntoListFornecedor(fornecedorRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize(fornecedores);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_ENTIDADE_CONSULTAR))
                {
                    FornecedorRepositorio fornecedorRepositorio = new FornecedorRepositorio();
                    List<Fornecedor> fornecedores = new List<Fornecedor>();
                    fornecedores = ConvertIntoListFornecedor(fornecedorRepositorio.Find());

                    int itensPorPagina = Convert.ToInt32(linhas);
                    int posicaoInicial = (Convert.ToInt32(pagina) - 1) * itensPorPagina;

                    if (posicaoInicial > fornecedores.Count)
                    {
                        posicaoInicial = fornecedores.Count - 1;
                    }

                    int posicaoFinal = posicaoInicial + itensPorPagina;

                    if (posicaoFinal > fornecedores.Count)
                    {
                        posicaoFinal = fornecedores.Count;
                    }

                    int totalPaginas = fornecedores.Count / itensPorPagina;

                    if ((fornecedores.Count % itensPorPagina) != 0)
                    {
                        totalPaginas += 1;
                    }

                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    

                    string fornecedoresJSON = jsonSerializer.Serialize(fornecedores);
                    string retornoJsonSerializado = "{\"total\":\"" + totalPaginas + "\",\"page\":\"" + pagina + "\",\"records\":\"" + fornecedores.Count + "\",\"rows\":" + fornecedoresJSON + "}";

                    context.Response.ContentType = "application/json";
                    context.Response.Write(retornoJsonSerializado);
               
                }
            }
            else if (entidade.Equals(SESSION_ENTIDADE_PRODUTO))
            {
                if (tipo.Equals(SESSION_SALVAR))
                {
                    string descricao = formulario.Get("descricao");
                    DateTime dtcadastro = DateTime.Now;
                    decimal valorProduto = Convert.ToDecimal(formulario.Get("valorProduto").ToString().Replace("R$:", ""));

                    Produto produto = new Produto();
                    ProdutoRepositorio produtoRepositorio = new ProdutoRepositorio();

                    produto.Descricao = descricao;
                    produto.ValorProduto = valorProduto;
                    produto.DtCadastro = DateTime.Now;
                    produto.Pedidos = null;

                    produtoRepositorio.Insert(produto);
                    List<Produto> produtos = new List<Produto>();
                    produtos = ConvertIntoListProduto(produtoRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize(produtos);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_ATUALIZAR))
                {
                    int idProduto = Convert.ToInt32(formulario.Get("idProduto").ToString());
                    string descricao = formulario.Get("descricao");
                    decimal valorProduto = Convert.ToDecimal(formulario.Get("valorProduto").ToString().Replace("R$:", ""));

                    Produto produto = new Produto();
                    ProdutoRepositorio produtoRepositorio = new ProdutoRepositorio();

                    produto = produtoRepositorio.FindId(idProduto);
                    produto.Descricao = descricao;
                    produto.ValorProduto = valorProduto;
                    produto.DtCadastro = DateTime.Now;

                    produtoRepositorio.Update(produto);
                    List<Produto> produtos = new List<Produto>();
                    produtos = ConvertIntoListProduto(produtoRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize(produtos);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_DELETAR))
                {
                    int idProduto = Convert.ToInt32(formulario.Get("idProduto").ToString());

                    Produto produto = new Produto();
                    ProdutoRepositorio produtoRepositorio = new ProdutoRepositorio();
                    produto = produtoRepositorio.FindId(idProduto);

                    produtoRepositorio.Delet(produto);
                    List<Produto> produtos = new List<Produto>();
                    produtos = ConvertIntoListProduto(produtoRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize(produtos);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_ENTIDADE_CONSULTAR))
                {
                    ProdutoRepositorio produtoRepositorio = new ProdutoRepositorio();
                    List<Produto> produtos = new List<Produto>();
                    produtos = ConvertIntoListProduto(produtoRepositorio.Find());

                    int itensPorPagina = Convert.ToInt32(linhas);
                    int posicaoInicial = (Convert.ToInt32(pagina) - 1) * itensPorPagina;

                    if (posicaoInicial > produtos.Count)
                    {
                        posicaoInicial = produtos.Count - 1;
                    }

                    int posicaoFinal = posicaoInicial + itensPorPagina;

                    if (posicaoFinal > produtos.Count)
                    {
                        posicaoFinal = produtos.Count;
                    }

                    int totalPaginas = produtos.Count / itensPorPagina;

                    if ((produtos.Count % itensPorPagina) != 0)
                    {
                        totalPaginas += 1;
                    }

                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();


                    string produtosJSON = jsonSerializer.Serialize(produtos);
                    string retornoJsonSerializado = "{\"total\":\"" + totalPaginas + "\",\"page\":\"" + pagina + "\",\"records\":\"" + produtos.Count + "\",\"rows\":" + produtosJSON + "}";

                    context.Response.ContentType = "application/json";
                    context.Response.Write(retornoJsonSerializado);
                }
            }
            else if (entidade.Equals(SESSION_ENTIDADE_PEDIDO))
            {

                if (tipo.Equals(SESSION_SALVAR))
                {

                    int idProduto = Convert.ToInt32(formulario.Get("idProduto").ToString());
                    int qtndProduto = Convert.ToInt32(formulario.Get("qtndProduto").ToString());
                    int idFornecedor = Convert.ToInt32(formulario.Get("idFornecedor").ToString());
                    decimal valorTotal = Convert.ToDecimal(formulario.Get("valorTotal").ToString().Replace("R$ ", ""));

                    Pedidos pedido = new Pedidos();
                    PedidoRepositorio pedidoRepositorio = new PedidoRepositorio();
                    ProdutoRepositorio produtoRepositorio = new ProdutoRepositorio();
                    FornecedorRepositorio fornecedorRepositorio = new FornecedorRepositorio();

                    pedido.DtPedido = DateTime.Now;
                    pedido.IdProduto = idProduto;
                    pedido.QntdProdutos = qtndProduto;
                    pedido.IdFornecedor = idFornecedor;
                    pedido.ValorTotal = valorTotal;


                    pedidoRepositorio.Insert(pedido);
                    List<PedidosJsonDTO> pedidos = new List<PedidosJsonDTO>();
                    pedidos = ConvertIntoPedidosDTO(pedidoRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize("ok");
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_ATUALIZAR))
                {
                    int idProduto = 0;
                    int idFornecedor = 0;
                    int idPedido = Convert.ToInt32(formulario.Get("idPedido").ToString());
                    if (formulario.Get("idProduto") != null && formulario.Get("idProduto") != "") idProduto = Convert.ToInt32(formulario.Get("idProduto").ToString());
                    if (formulario.Get("idFornecedor") != null && formulario.Get("idFornecedor") != "") idFornecedor = Convert.ToInt32(formulario.Get("idFornecedor").ToString());

                    int qtndProduto = Convert.ToInt32(formulario.Get("qtndProduto").ToString());
                    decimal valorTotal = Convert.ToDecimal(formulario.Get("valorTotal").ToString().Replace("R$ ", "").Replace(".",";").Replace(",",".").Replace(";",","));

                    Pedidos pedido = new Pedidos();
                    PedidoRepositorio pedidoRepositorio = new PedidoRepositorio();
                    ProdutoRepositorio produtoRepositorio = new ProdutoRepositorio();
                    FornecedorRepositorio fornecedorRepositorio = new FornecedorRepositorio();

                    pedido = pedidoRepositorio.FindId(idPedido);
                    pedido.DtPedido = DateTime.Now;

                    if(idProduto != 0) pedido.IdProduto = idProduto;
                    if(idFornecedor != 0) pedido.IdFornecedor = idFornecedor;
                    pedido.QntdProdutos = qtndProduto;
                    
                    pedido.ValorTotal = valorTotal;
                    pedido.Fornecedor = fornecedorRepositorio.FindId(idFornecedor);
                    pedido.Produto = produtoRepositorio.FindId(idProduto);

                    pedidoRepositorio.Update(pedido);
                    List<PedidosJsonDTO> pedidos = new List<PedidosJsonDTO>();
                    pedidos = ConvertIntoPedidosDTO(pedidoRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize(pedidos);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_DELETAR))
                {
                    int idPedido = Convert.ToInt32(formulario.Get("idPedido").ToString());

                    Pedidos pedido = new Pedidos();
                    PedidoRepositorio pedidoRepositorio = new PedidoRepositorio();
                    pedido = pedidoRepositorio.FindId(idPedido);
                    pedidoRepositorio.Delet(pedido);
                    List<PedidosJsonDTO> pedidos = new List<PedidosJsonDTO>();
                    pedidos = ConvertIntoPedidosDTO(pedidoRepositorio.Find());
                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string fornecedoresJSON = jsonSerializer.Serialize(pedidos);
                    context.Response.ContentType = "application/json";
                    context.Response.Write(fornecedoresJSON);
                }
                else if (tipo.Equals(SESSION_ENTIDADE_CONSULTAR))
                {
                    int filtro = 0;
                    if (formulario.Get("filtro") != "" && formulario.Get("filtro") != null) filtro = Convert.ToInt32(formulario.Get("filtro"));

                    PedidoRepositorio pedidoRepositorio = new PedidoRepositorio();
                    List<PedidosJsonDTO> pedidos = new List<PedidosJsonDTO>();
                    if (filtro == 0)
                    {
                        pedidos = ConvertIntoPedidosDTO(pedidoRepositorio.Find());
                    }
                    else
                    {
                        pedidos = ConvertIntoPedidosDTO(pedidoRepositorio.FindByFornecedor(filtro));
                    }

                    int itensPorPagina = Convert.ToInt32(linhas);
                    int posicaoInicial = (Convert.ToInt32(pagina) - 1) * itensPorPagina;

                    if (posicaoInicial > pedidos.Count)
                    {
                        posicaoInicial = pedidos.Count - 1;
                    }

                    int posicaoFinal = posicaoInicial + itensPorPagina;

                    if (posicaoFinal > pedidos.Count)
                    {
                        posicaoFinal = pedidos.Count;
                    }

                    int totalPaginas = pedidos.Count / itensPorPagina;

                    if ((pedidos.Count % itensPorPagina) != 0)
                    {
                        totalPaginas += 1;
                    }

                    System.Web.Script.Serialization.JavaScriptSerializer jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();


                    string pedidosJSON = jsonSerializer.Serialize(pedidos);
                    string retornoJsonSerializado = "{\"total\":\"" + totalPaginas + "\",\"page\":\"" + pagina + "\",\"records\":\"" + pedidos.Count + "\",\"rows\":" + pedidosJSON + "}";

                    context.Response.ContentType = "application/json";
                    context.Response.Write(retornoJsonSerializado);
                }
            }
        
        }

        private List<PedidosJsonDTO> ConvertIntoPedidosDTO(IList<Pedidos> iPedidos)
        {
            List<PedidosJsonDTO> pedidosJson = new List<PedidosJsonDTO>();
            foreach (Pedidos item in iPedidos)
            {
                PedidosJsonDTO pedido = new PedidosJsonDTO();
                pedido.IdPedido = item.IdPedido;
                pedido.Produto = item.Produto.Descricao;
                pedido.DtPedido = item.DtPedido;
                pedido.Fornecedor = item.Fornecedor.RazaoSocial;
                pedido.QtndProduto = item.QntdProdutos;
                pedido.ValorTotal = item.ValorTotal;

                pedidosJson.Add(pedido);
            }
            return pedidosJson;
        }

        public static List<Fornecedor> ConvertIntoListFornecedor(IList<Fornecedor> ifornecedor)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            foreach (Fornecedor item in ifornecedor)
            {
                Fornecedor fornecedor = new Fornecedor();

                fornecedor.IdFornecedor = item.IdFornecedor;
                fornecedor.RazaoSocial = item.RazaoSocial;
                fornecedor.Cnpj = item.Cnpj;
                fornecedor.Uf = item.Uf;
                fornecedor.Email = item.Email;
                fornecedor.NomeContato = item.NomeContato;
                fornecedor.Pedidos = null;

                fornecedores.Add(fornecedor);
            }

            return fornecedores;
        }

        public static List<Produto> ConvertIntoListProduto(IList<Produto> iProduto)
        {
            List<Produto> produtos = new List<Produto>();
            foreach (Produto item in iProduto)
            {
                Produto produto = new Produto();
                produto.IdProduto = item.IdProduto;
                produto.Descricao = item.Descricao;
                produto.DtCadastro= item.DtCadastro;
                produto.ValorProduto = item.ValorProduto;
                produto.Pedidos= null;
                
                produtos.Add(produto);                
            }

            return produtos;
        }

        public static List<Pedidos> ConvertIntoListPedido(IList<Pedidos> iPedido)
        {
            

            List<Pedidos> pedidos = new List<Pedidos>();
            foreach (Pedidos item in iPedido)
            {
                Pedidos pedido = new Pedidos();

                pedido.IdPedido = item.IdPedido;
                pedido.IdProduto = item.IdProduto;
                pedido.IdFornecedor = item.IdFornecedor;
                pedido.DtPedido = item.DtPedido;
                pedido.QntdProdutos = item.QntdProdutos;
                pedido.Produto = item.Produto;
                pedido.Fornecedor = item.Fornecedor;
                pedido.ValorTotal = item.ValorTotal;

                pedidos.Add(pedido);
            }

            return pedidos;
        }

        internal class PedidosJsonDTO
        {
            private int idPedido;
            private DateTime dtPedido;
            private string produto;
            private string fornecedor;
            private int qtndProduto;
            private decimal valorTotal;

            public int IdPedido
            {
                get { return this.idPedido; }
                set { this.idPedido = value; }
            }
            public DateTime DtPedido
            {
                get { return this.dtPedido; }
                set { this.dtPedido = value; }
            }

            public string Produto
            {
                get { return this.produto; }
                set { this.produto = value; }
            }

            public string Fornecedor
            {
                get { return this.fornecedor; }
                set { this.fornecedor = value; }
            }

            public int QtndProduto
            {
                get { return this.qtndProduto; }
                set { this.qtndProduto = value; }
            }

            public decimal ValorTotal
            {
                get { return this.valorTotal; }
                set { this.valorTotal = value; }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}