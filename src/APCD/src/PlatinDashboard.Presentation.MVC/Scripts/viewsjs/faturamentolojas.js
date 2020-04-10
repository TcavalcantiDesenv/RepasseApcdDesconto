function HighchartsClassificacoes() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('select[id=ddlLojas]').val();

    $.ajax({
        url: '/Faturamento/GraficoFaturamentoClassificacao',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'loja': -1 }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            var somaClassificacoes = dados[0].Classificação1 + dados[0].Classificação2 + dados[0].Classificação3 + dados[0].Classificação4 + dados[0].Classificação5 +
                dados[0].Classificação6 + dados[0].Classificação7 + dados[0].Classificação8;

            var classificacaoPorcentagem1 = (dados[0].Classificação1 / somaClassificacoes) * 100;
            var classificacaoPorcentagem2 = (dados[0].Classificação2 / somaClassificacoes) * 100;
            var classificacaoPorcentagem3 = (dados[0].Classificação3 / somaClassificacoes) * 100;
            var classificacaoPorcentagem4 = (dados[0].Classificação4 / somaClassificacoes) * 100;
            var classificacaoPorcentagem5 = (dados[0].Classificação5 / somaClassificacoes) * 100;
            var classificacaoPorcentagem6 = (dados[0].Classificação6 / somaClassificacoes) * 100;
            var classificacaoPorcentagem7 = (dados[0].Classificação7 / somaClassificacoes) * 100;
            var classificacaoPorcentagem8 = (dados[0].Classificação8 / somaClassificacoes) * 100;


            Highcharts.setOptions({
                lang: {
                    thousandsSep: '.',
                    decimalPoint: ','
                }
            });

            var chart = Highcharts.chart('container', {

                title: {
                    text: ''
                },

                subtitle: {
                    text: ''
                },

                xAxis: {
                    categories: [dados[0].NomeClassificação1 + "    " + classificacaoPorcentagem1.toFixed(2) + '%', dados[0].NomeClassificação2 + "   " + classificacaoPorcentagem2.toFixed(2) + '%', dados[0].NomeClassificação3 + "   " + classificacaoPorcentagem3.toFixed(2) + '%', dados[0].NomeClassificação4 + "   " + classificacaoPorcentagem4.toFixed(2) + '%', dados[0].NomeClassificação5 + "    " + classificacaoPorcentagem5.toFixed(2) + '%', dados[0].NomeClassificação6 + "    " + classificacaoPorcentagem6.toFixed(2) + '%', dados[0].NomeClassificação7 + "   " + classificacaoPorcentagem7.toFixed(2) + '%', dados[0].NomeClassificação8 + "    " + classificacaoPorcentagem8.toFixed(2) + '%']
                },

                series: [{
                    type: 'column',
                    colorByPoint: true,
                    data: [dados[0].Classificação1, dados[0].Classificação2, dados[0].Classificação3, dados[0].Classificação4, dados[0].Classificação5, dados[0].Classificação6, dados[0].Classificação7, dados[0].Classificação8],
                    showInLegend: false
                }],
                plotOptions: {
                    column: {
                        dataLabels: {
                            enabled: true,
                            crop: true,
                            overflow: 'none'
                        }
                    },
                }
            });
            //parando botão
            buttonStop(document.getElementById('faturamento-classificacao-btn'));
        },        
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function carregarGraficoFaturamentoClassificacao(element) {
    buttonLoading(element);
    HighchartsClassificacoes();
    DataTableGridVenda();
    DataTableGridVenUad();
}

function DataTableGridVenda() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    $.ajax({
        url: '/Faturamento/GridTotalDeVendas',
        data: JSON.stringify({ 'mes': mes, 'ano': ano }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            var chart;

            var dataSet = [];
            var data = [];
            for (var i = 0; i < dados.length; i++) {

                data = [
                    dados[i].Data,
                    dados[i].TotalLojas,
                    dados[i].Bruto,
                    dados[i].Desconto,
                    dados[i].Lucro,
                    dados[i].Devolucao,
                    dados[i].Liquida,
                    dados[i].Custo,
                    dados[i].PercentualMargem,
                    dados[i].ClientesAtendidos,
                    dados[i].QtMediaClientes
                ];
                dataSet.push(data);
            }
            $(document).ready(function () {
                $('#example').DataTable({
                    data: dataSet,
                    columns: [
                        { title: "Data" },
                        { title: "Total de Lojas" },
                        { title: "Bruto" },
                        { title: "Valor de Desconto" },
                        { title: "Lucro" },
                        { title: "Devolução" },
                        { title: "Liquído" },
                        { title: "Custo" },
                        { title: "Margem" },
                        { title: "Clientes Atendidos" },
                        { title: "Quantidade Média por Cliente" }
                    ],
                    "bDestroy": true,
                    deferRender: true,
                    scroller: true,
                    "scrollX": true
                });
            });
            $('#example').attr('style', 'border-collapse: collapse !important');
            $('#example').addClass("table table-responsive-sm table-hover table-outline mb-0 dataTable no-footer");
        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function DataTableGridVenUad() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();

    $.ajax({
        url: '/Faturamento/GridVendaPorLojas',
        data: JSON.stringify({ 'mes': mes, 'ano': ano }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            var chart;

            var dataSet = [];
            var data = [];
            for (var i = 0; i < dados.length; i++) {

                data = [
                    dados[i].Data,
                    dados[i].Loja,
                    dados[i].Bruto,
                    dados[i].Desconto,
                    dados[i].Lucro,
                    dados[i].Devolucao,
                    dados[i].Liquida,
                    dados[i].PercentualMargem,
                    dados[i].ClientesAtendidos,
                    dados[i].QtMediaClientes,
                    dados[i].TicketMedio
                ];
                dataSet.push(data);
            }
            $(document).ready(function () {
                $('#dtVendas').DataTable({
                    data: dataSet,
                    columns: [
                        { title: "Data" },
                        { title: "Loja" },
                        { title: "Bruto" },
                        { title: "Desconto" },
                        { title: "Lucro" },
                        { title: "Devolução" },
                        { title: "Líquido" },
                        { title: "Percentual Margem" },
                        { title: "Clientes Atendidos" },
                        { title: "Quantidade Média por Cliente" },
                        { title: "Ticket Médio" },
                    ],
                    "bDestroy": true,
                    deferRender: true,
                    scroller: true,
                    "scrollX": true
                });
            });
            $('#dtVendas').attr('style', 'border-collapse: collapse !important');
            $('#dtVendas').addClass("table table-responsive-sm table-hover table-outline mb-0 dataTable no-footer");
        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}