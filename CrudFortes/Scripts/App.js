$(document).ready(function () {
    $("#tabs").tabs();
    $("#accordion1").accordion();
    $("#accordion2").accordion();
    $('#txtCnpj').mask("99.999.999/9999-99");
    $('#txtQtndProduto').mask("999");
    $('#txtFiltro').mask("999");
    $('#txtUf').mask("SS");
    $('#txtVlProduto').maskMoney({
        prefix: "R$:",
        decimal: ",",
        thousands: "."
    });
    $('#resize').css("height", "100%");
    $('#resizeProduto').css("height", "100%");
    $('#resizePedido').css("height", "100%");
    $('#resizePedido').css("width", "100%");

    $('#hFornecedor').css("background-color", "#007fff")
    $('#hProduto').css("background-color", "#007fff")
    limparFornecedor();
    limparProduto();
    limparPedido();
    preencherGridFornecedor();
    preencherGridProduto();
    preencherGridPedidos();
    preencherGridProdutoPedido();
    preencherGridFornecedorPedido();
    
})

var valorTotalAtual;

function validarTotal() {
    var id = $("#tabelaProdutosPedidos").jqGrid('getGridParam', 'selrow');
    if (id) {
        var grid = $("#tabelaProdutosPedidos").jqGrid('getRowData', id);
        var valorTotal = grid.ValorProduto * $('#txtQtndProduto').val();
        $('#txtVlTotal').val("R$ " + valorTotal.toString().replace(",", ";").replace(".", ",").replace(";", "."));

    } else {
        if ($('#txtVlTotal').val() != "" && $('#txtQtndProduto').val() != "")
        {
            //auxVl = $('#txtVlTotal').val().toString().replace(",", ";").replace(".", ",").replace(";", ".").replace("R$ ", "");
            
            var valorTotal = valorTotalAtual * $('#txtQtndProduto').val();
             
            $('#txtVlTotal').val("R$ " + valorTotal);

        }
    }
    }

function limparProduto()
{
    $('#txtIdProduto').val('');
    $('#txtDescricao').val('');
    $('#txtDtCadastro').val('');
    $('#txtVlProduto').val('');
    $('#btnSalvarProduto').prop("disabled", false);
    $('#btnAtualizarProduto').prop("disabled", true);
    $('#btnDeletarProduto').prop("disabled", true);
}

function limparPedido()
{
    $('#txtIdPedido').val('');
    $('#txtIdFornecedorPedido').val(''); 
    $('#txtDescricaoFornecedorPedido').val('');
    $('#txtIdProdutoPedido').val(''); 
    $('#txtDescricaoProdutoPedido').val('');
    $('#txtQtndProduto').val('');
    $('#txtVlTotal').val('');
    $('#btnSalvarPedido').prop("disabled", false);
    $('#btnAtualizarPedido').prop("disabled", true);
    $('#btnDeletarPedido').prop("disabled", true);
    valorTotalAtual = "0";
   
}

function limparFornecedor()
{
    $('#txtIdFornecedor').val('');
    $('#txtRazaoSocial').val('');
    $('#txtCnpj').val('');
    $('#txtUf').val('');
    $('#txtEmail').val('');
    $('#txtNomeContato').val('');
    $('#btnSalvarFornecedor').prop("disabled", false);
    $('#btnAtualizarFornecedor').prop("disabled", true);
    $('#btnDeletarFornecedor').prop("disabled", true);
}

function salvarAtualizarDeletarFornecedor(crud)
{
    var tipo = crud
   

    if (tipo == "Salvar") {
        if ($('#txtRazaoSocial').val() == "" || $('#txtCnpj').val() == "" || $('#txtUf').val() == "" || $('#txtEmail').val() == "" || $('#txtCnpj').val().length < 18)
        {
            alert("Preencha os campos corretamente.");
            return;
        }
    }
    else
    {
        if ($('#txtIdFornecedor').val() == "") {
            alert("Selecione um fornecedor.");
            return;
        } else {
            if ($('#txtRazaoSocial').val() == "" || $('#txtCnpj').val() == "" || $('#txtUf').val() == "" || $('#txtEmail').val() == "" || $('#txtCnpj').val().length < 18)
            {
                alert("Preencha os campos corretamente.");
                return;
            }
        }
    }    
        var corfirma = confirm("Deseja " + tipo +" esse Fornecedor ?");
        if (corfirma)
        {
            if (tipo == "Salvar") {

                var objParams =
                {
                    "entidade": "Fornecedor",
                    "tipo": tipo,
                    "razaoSocial": $('#txtRazaoSocial').val(),
                    "cnpj": $('#txtCnpj').val(),
                    "uf": $('#txtUf').val(),
                    "email": $('#txtEmail').val(),
                    "nomeContato": $('#txtNomeContato').val(),
                };

            }
            else
            {
                var objParams =
                {
                    "entidade": "Fornecedor",
                    "tipo": tipo,
                    "idFornecedor": $('#txtIdFornecedor').val(),
                    "razaoSocial": $('#txtRazaoSocial').val(),
                    "cnpj": $('#txtCnpj').val(),
                    "uf": $('#txtUf').val(),
                    "email": $('#txtEmail').val(),
                    "nomeContato": $('#txtNomeContato').val(),
                };
            }
                $.ajax({
                    type: "POST",
                    url: "Handler/CrudHandlerGenerico.ashx?" + $.param(objParams),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Content-type", "application/json; charset=utf-8");
                    },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        alert('Lista de fornecedor atualizada com sucesso!');
                        limparFornecedor();
                        preencherGridFornecedor();
                        preencherGridFornecedorPedido();
                    },
                    error: function (xhr, msg, e) {
                        alert("Falha ao salvar o Fornecedor!");
                    }

                });
            }
        
    
}

function salvarAtualizarDeletarProduto(crud) {
    var tipo = crud


    if (tipo == "Salvar") {
        if ($('#txtVlProduto').val() == "" || $('#txtDescricao').val() == "") {
            alert("Preencha os campos corretamente.");
            return;
        }
    }
    else {
        if ($('#txtIdProduto').val() == "") {
            alert("Selecione um Produto.");
            return;
        }
        else
        {
            if ($('#txtVlProduto').val() == "" || $('#txtDescricao').val() == "")
            {
                alert("Preencha os campos corretamente.");
                return;
            }
        }
    }
    var corfirma = confirm("Deseja " + tipo + " esse Produto ?");
    if (corfirma) {
        if (tipo == "Salvar") {

            var objParams =
            {
                "entidade": "Produto",
                "tipo": tipo,
                "descricao": $('#txtDescricao').val(),
                "valorProduto": $('#txtVlProduto').val()
            };

        }
        else {
            var objParams =
            {
                "entidade": "Produto",
                "tipo": tipo,
                'idProduto': $('#txtIdProduto').val(),
                "descricao": $('#txtDescricao').val(),
                "valorProduto": $('#txtVlProduto').val()
            };
        }
        $.ajax({
            type: "POST",
            url: "Handler/CrudHandlerGenerico.ashx?" + $.param(objParams),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Content-type", "application/json; charset=utf-8");
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                alert('Lista de produtos atualizada com sucesso!');
                limparProduto();
                preencherGridProduto();
                preencherGridProdutoPedido();

            },
            error: function (xhr, msg, e) {
                alert("Falha ao salvar o Produto!");
            }

        });
    }


}


function preencherGridFornecedor()
{
    var objParams =
    {
        "entidade": "Fornecedor",
        "tipo": "Consultar"
    };

    $("#tabelaFornecedores").GridUnload();
    $("#tabelaFornecedores").jqGrid({
        url: 'Handler/CrudHandlerGenerico.ashx?' + $.param(objParams),
        datatype: 'json',
        colNames: [
            'Código Fornecedor', 'Razão Social', 'CNPJ', 'UF', 'Email', 'Contato'],
        colModel: [
            { name: 'IdFornecedor', index: 'IdFornecedor', align: 'center',width: "135px"},
            { name: 'RazaoSocial', index: 'RazaoSocial', width: "250px", align: 'left' },
            { name: 'Cnpj', index: 'Cnpj', width: "250px", align: 'left', align: 'center' },
            { name: 'Uf', index: 'Uf', align: 'center', width: '60px', fixed: true },
            { name: 'Email', index: 'Email', width: "250px", align: 'left' },
            { name: 'NomeContato', index: 'NomeContato', width: '100%', align: 'left' }
        ],
        loadonce:true,
        rowNum: 10,
        type: 'GET',
        rowList: [10, 20, 30],
        pager: '#paginacaoFornecedores',
        sortname: 'IdFornecedor',
        emptyrecords: 'Não existe Fornecedores.',
        sortorder: 'asc',
        caption: 'Fornecedores',
        height: 250,
        hidegrid: false,
        width: "100%",
        jsonReader: {
            repeatitems: false
        },
        onSelectRow: function () {
            var id = $("#tabelaFornecedores").jqGrid('getGridParam', 'selrow');
            if (id) {
                var grid = $("#tabelaFornecedores").jqGrid('getRowData', id);
                $('#txtIdFornecedor').val(grid.IdFornecedor);
                $('#txtRazaoSocial').val(grid.RazaoSocial);
                $('#txtCnpj').val(grid.Cnpj);
                $('#txtUf').val(grid.Uf);
                $('#txtEmail').val(grid.Email);
                $('#txtNomeContato').val(grid.NomeContato);
                $('#btnAtualizarFornecedor').prop("disabled", false);
                $('#btnDeletarFornecedor').prop("disabled", false);
                $('#btnSalvarFornecedor').prop("disabled", true);
            }
        }
    });
}

function preencherGridProduto() {
    var objParams =
    {
        "entidade": "Produto",
        "tipo": "Consultar"
    };

    $("#tabelaProdutos").GridUnload();
    $("#tabelaProdutos").jqGrid({
        url: 'Handler/CrudHandlerGenerico.ashx?' + $.param(objParams),
        datatype: 'json',
        colNames: [
            'Código Produto', 'Descrição', 'Data do cadastro', 'Valor do Produto'],
        colModel: [
            { name: 'IdProduto', index: 'IdProduto', align: 'center', width: "135px" },
            { name: 'Descricao', index: 'Descricao', width: "250px", align: 'left' },
            { name: 'DtCadastro', index: 'DtCadastro', width: "250px", align: 'left', align: 'center', formatter: 'date', formatoptions: { srcformat: 'U', newformat: 'd/m/Y' }},
            {
                name: 'ValorProduto', index: 'ValorProduto', align: 'center', width: '100%', fixed: true, formatter: 'currency',
                formatoptions: { prefix: 'R$', thousandsSeparator: '.', decimalSeparator: ',' ,decimalPlaces: 2 }
            }
        ],
        loadonce: true,
        rowNum: 10,
        type: 'GET',
        rowList: [10, 20, 30],
        pager: '#paginacaoProdutos',
        sortname: 'IdProduto',
        emptyrecords: 'Não existe Produtos.',
        sortorder: 'asc',
        caption: 'Produtos',
        height: 250,
        hidegrid: false,
        width: "100%",
        jsonReader: {
            repeatitems: false
        },
        onSelectRow: function () {
            var id = $("#tabelaProdutos").jqGrid('getGridParam', 'selrow');
            if (id) {
                var grid = $("#tabelaProdutos").jqGrid('getRowData', id);
                $('#txtIdProduto').val(grid.IdProduto);
                $('#txtDescricao').val(grid.Descricao);
                $('#txtVlProduto').val("R$ " + grid.ValorProduto.replace(",", ";").replace(".", ",").replace(";", "."));
                valorTotalAtual = grid.ValorProduto;
                $('#btnAtualizarProduto').prop("disabled", false);
                $('#btnDeletarProduto').prop("disabled", false);
                $('#btnSalvarProduto').prop("disabled", true);
            }
        }
    });
}

function salvarAtualizarDeletarPedido(crud) {
    var tipo = crud


    if (tipo == "Salvar") {
        if ($('#txtIdFornecedorPedido').val() == "" || $('#txtIdProdutoPedido').val() == "" || $('#txtQtndProduto').val() == "" || $('#txtVlTotal').val() == "") {
            alert("Preencha os campos corretamente.");
            return;
        }
    }
    else {
        if ($('#txtIdPedido').val() == "") {
            alert("Selecione um pedido.");
            return;
        } else {
            if ( $('#txtQtndProduto').val() == "" || $('#txtVlTotal').val() == "") {
                alert("Preencha os campos corretamente.");
                return;
            }
        }
    }
    var corfirma = confirm("Deseja " + tipo + " esse Pedido ?");
    if (corfirma) {
        if (tipo == "Salvar") {

            var objParams =
            {
                "entidade": "Pedido",
                "tipo": tipo,
                "idProduto": $('#txtIdProdutoPedido').val(),
                "idFornecedor": $('#txtIdFornecedorPedido').val(),
                "qtndProduto": $('#txtQtndProduto').val(),
                "valorTotal": $('#txtVlTotal').val()
            };

        }
        else {
            var objParams =
            {
                "entidade": "Pedido",
                "tipo": tipo,
                "idPedido": $('#txtIdPedido').val(),
                "idProduto": $('#txtIdProdutoPedido').val(),
                "idFornecedor": $('#txtIdFornecedorPedido').val(),
                "qtndProduto": $('#txtQtndProduto').val(),
                "valorTotal": $('#txtVlTotal').val()
            };
        }
        $.ajax({
            type: "POST",
            url: "Handler/CrudHandlerGenerico.ashx?" + $.param(objParams),
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Content-type", "application/json; charset=utf-8");
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                alert('Lista de pedidos atualizada com sucesso!');
                limparPedido();
                preencherGridPedidos();
            },
            error: function (xhr, msg, e) {
                alert("Falha ao salvar o pedido!");
            }

        });
    }
}

function preencherGridPedidos(filtro = "") {
    if (filtro != "" && filtro != "0") {
        var objParams =
        {
            "entidade": "Pedido",
            "tipo": "Consultar",
            "filtro": filtro
        };
    } else {
        var objParams =
        {
            "entidade": "Pedido",
            "tipo": "Consultar"
        };
    }
        $("#tabelaPedidos").GridUnload();
        $("#tabelaPedidos").jqGrid({
            url: 'Handler/CrudHandlerGenerico.ashx?' + $.param(objParams),
            datatype: 'json',
            colNames: [
                'Código Pedido', 'Data do Pedido', 'Produto', 'Fornecedor','Quantidade de produtos', 'Valor Total'],
            colModel: [
                { name: 'IdPedido', index: 'IdPedido', align: 'center', width: "135px" },
                { name: 'DtPedido', index: 'DtPedido', width: "250px", align: 'left', align: 'center', formatter: 'date', formatoptions: { srcformat: 'U', newformat: 'd/m/Y' } },
                { name: 'Produto', index: 'Produto', width: "250px", align: 'left', align: 'center' },
                { name: 'Fornecedor', index: 'Fornecedor', width: "250px", align: 'left', align: 'center' },
                { name: 'QtndProduto', index: 'QtndProduto', align: 'center', width: '60px', fixed: true },
                {
                    name: 'ValorTotal', index: 'ValorTotal', align: 'center', width: '100%', fixed: true, formatter: 'currency',
                    formatoptions: { prefix: 'R$', thousandsSeparator: '.', decimalSeparator: ',', decimalPlaces: 2 }
                }
            ],
            loadonce: true,
            rowNum: 10,
            type: 'GET',
            rowList: [10, 20, 30],
            pager: '#paginacaoPedidos',
            sortname: 'IdPedido',
            emptyrecords: 'Não existe pedidos.',
            sortorder: 'asc',
            caption: 'Pedidos',
            height: 250,
            hidegrid: false,
            width: "100%",
            jsonReader: {
                repeatitems: false
            },
            onSelectRow: function () {
                var id = $("#tabelaPedidos").jqGrid('getGridParam', 'selrow');
                if (id) {
                    var grid = $("#tabelaPedidos").jqGrid('getRowData', id);
                    $('#txtIdPedido').val(grid.IdPedido);
                    $('#txtDescricaoProdutoPedido').val(grid.Produto);
                    $('#txtDescricaoFornecedorPedido').val(grid.Fornecedor);
                    $('#txtQtndProduto').val(grid.QtndProduto);
                    $('#txtVlTotal').val("R$ " + grid.ValorTotal.replace(",", ";").replace(".", ",").replace(";", "."));
                    valorTotalAtual = grid.ValorTotal/grid.QtndProduto;
                    $('#btnAtualizarPedido').prop("disabled", false);
                    $('#btnDeletarPedido').prop("disabled", false);
                    $('#btnSalvarPedido').prop("disabled", true);
                   
                }
            }
        });
    }
    
function preencherGridProdutoPedido() {
    var objParams =
    {
        "entidade": "Produto",
        "tipo": "Consultar"
    };

    $("#tabelaProdutosPedidos").GridUnload();
    $("#tabelaProdutosPedidos").jqGrid({
        url: 'Handler/CrudHandlerGenerico.ashx?' + $.param(objParams),
        datatype: 'json',
        colNames: [
            'Código Produto', 'Descrição', 'Data do cadastro', 'Valor do Produto'],
        colModel: [
            { name: 'IdProduto', index: 'IdProduto', align: 'center', width: "135px" },
            { name: 'Descricao', index: 'Descricao', width: "250px", align: 'left' },
            { name: 'DtCadastro', index: 'DtCadastro', width: "250px", align: 'left', align: 'center', formatter: 'date', formatoptions: { srcformat: 'U', newformat: 'd/m/Y' } },
            {
                name: 'ValorProduto', index: 'ValorProduto', align: 'center', width: '100%', fixed: true, formatter: 'currency',
                formatoptions: { prefix: 'R$', thousandsSeparator: '.', decimalSeparator: ',', decimalPlaces: 2 }
            }
        ],
        loadonce: true,
        rowNum: 10,
        type: 'GET',
        rowList: [10, 20, 30],
        pager: '#paginacaoProdutosPedidos',
        sortname: 'IdProduto',
        emptyrecords: 'Não existe Produtos.',
        sortorder: 'asc',
        caption: 'Produtos',
        height: 250,
        hidegrid: false,
        width: "100%",
        jsonReader: {
            repeatitems: false
        }, onSelectRow: function () {
            var id = $("#tabelaProdutosPedidos").jqGrid('getGridParam', 'selrow');
                if (id) {
                    var grid = $("#tabelaProdutosPedidos").jqGrid('getRowData', id);
                    $('#txtIdProdutoPedido').val(grid.IdProduto);
                    $('#txtDescricaoProdutoPedido').val(grid.Descricao);
                    $('#txtVlTotal').val('0');
                    $('#txtQtndProduto').val('0');
                   
                }
            }
    });
}


function preencherGridFornecedorPedido() {
    var objParams =
    {
        "entidade": "Fornecedor",
        "tipo": "Consultar"
    };

    $("#tabelaFornecedoresPedidos").GridUnload();
    $("#tabelaFornecedoresPedidos").jqGrid({
        url: 'Handler/CrudHandlerGenerico.ashx?' + $.param(objParams),
        datatype: 'json',
        colNames: [
            'Código Fornecedor', 'Razão Social', 'CNPJ', 'UF', 'Email', 'Contato'],
        colModel: [
            { name: 'IdFornecedor', index: 'IdFornecedor', align: 'center', width: "135px" },
            { name: 'RazaoSocial', index: 'RazaoSocial', width: "250px", align: 'left' },
            { name: 'Cnpj', index: 'Cnpj', width: "250px", align: 'left', align: 'center' },
            { name: 'Uf', index: 'Uf', align: 'center', width: '60px', fixed: true },
            { name: 'Email', index: 'Email', width: "250px", align: 'left' },
            { name: 'NomeContato', index: 'NomeContato', width: '100%', align: 'left' }
        ],
        loadonce: true,
        rowNum: 10,
        type: 'GET',
        rowList: [10, 20, 30],
        pager: '#paginacaoFornecedoresPedidos',
        sortname: 'IdFornecedor',
        emptyrecords: 'Não existe Fornecedores.',
        sortorder: 'asc',
        caption: 'Fornecedores',
        height: 250,
        hidegrid: false,
        width: "100%",
        jsonReader: {
            repeatitems: false
        }, onSelectRow: function () {
            var id = $("#tabelaFornecedoresPedidos").jqGrid('getGridParam', 'selrow');
            if (id) {
                var grid = $("#tabelaFornecedoresPedidos").jqGrid('getRowData', id);
                $('#txtIdFornecedorPedido').val(grid.IdFornecedor);
                $('#txtDescricaoFornecedorPedido').val(grid.RazaoSocial);

            
            }
        }
    });
}

function filtrarPedidos()
{
    preencherGridPedidos($('#txtFiltro').val());
}


