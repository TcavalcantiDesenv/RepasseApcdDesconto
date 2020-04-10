var chartFaturamentoClassificacao;
var footerGlobalValues = [];
var months = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
function getMonthName(monthNumber) {
    return months[monthNumber - 1];
}

function getLastMonthName(monthNumber) {
    if (monthNumber === '01') {
        return months[11];
    }
    return months[monthNumber - 2];
}

function generateGraficoFaturamento(canvasId, chartVar) {
    showLoader(canvasId);
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();
    $("body :input").attr("disabled", true);
    $.ajax({
        url: '/Dashboard/GraficoFaturamentoClassificacao',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'loja': loja}),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            if (window[chartVar] != undefined || window[chartVar] != null) {
                window[chartVar].destroy()
            }
            else {
                removeLoader(canvasId);
            }
            changeChartPeriod(dados);
            var ctx = document.getElementById(canvasId).getContext('2d');           
            window[chartVar] = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: generateLabels(dados),
                    datasets: generateChartDataSet(dados)
                },
                options: {
                    legend: {
                        display: true
                    },
                    title: {
                        display: false,
                    },
                    responsive: true,
                    scales: {
                        xAxes: [{
                            stacked: true,
                        }],
                        yAxes: [{
                            stacked: true,
                            ticks: {
                                callback: function (label, index, labels) {
                                    return 'R$ ' + (label).toLocaleString('pt-BR');
                                }
                            }
                        }]
                    },
                    tooltips: {
                        callbacks: {
                            title: function (tooltipItems, data) {
                                var currentDataSet = data.datasets[tooltipItems[0].datasetIndex];
                                var valorTotal = 0;
                                $(currentDataSet.data).each(function (index, value) {
                                    valorTotal += value;
                                });
                                return tooltipItems[0].xLabel[0] + ' ' +  ((tooltipItems[0].yLabel / valorTotal) * 100).toFixed(2) + '%';
                            },
                            label: function (tooltipItem, data) {                                
                                var monthName = getMonthName($('#mes').val()) + ': ';
                                if (tooltipItem.datasetIndex == 0) {
                                    monthName = 'Vendas ' + getLastMonthName($('#mes').val()) + ': '
                                }
                                else if (tooltipItem.datasetIndex == 1) {
                                    monthName = 'Vendas ' + getMonthName($('#mes').val()) + ': '
                                }
                                else if (tooltipItem.datasetIndex == 2) {
                                    monthName = 'Projeção ' + getMonthName($('#mes').val()) + ': '
                                    var sellValue = data.datasets[1].data[tooltipItem.index];
                                    console.log(sellValue)
                                    return monthName + 'R$ ' + (tooltipItem.yLabel + sellValue).toLocaleString('pt-BR');
                                }
                                return monthName + 'R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                            }
                        }
                    },
                }
            });
            //parando botão
            buttonStop(document.getElementById('faturamento-classificacao-btn'));
            $("body :input").attr("disabled", false);
        },        
        error: function (xhr, textStatus, errorThrown) {
            
        }
    });
}

function carregarDashboard(element) {
    buttonLoading(element);
    generateGraficoFaturamento('faturamentoClassificacao', 'chartFaturamentoClassificacao');
    generateGridVendas();
    setTimeout(function () {
        generateGridVendasPorLoja();
        generateGridVendasMensalPorLoja();
    }, 2000)    
}

function generateGridVendas() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    $.ajax({
        url: '/Dashboard/GridTotalDeVendas',
        data: JSON.stringify({ 'mes': mes, 'ano': ano }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            var dataSet = [];
            var data = [];
            var footerValues = ['', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            sumGridVendasFooterValues(footerValues, dados);
            footerGlobalValues = footerValues;
            for (var i = 0; i < dados.length; i++) {                
                data = [
                    dados[i].Data,
                    dados[i].Bruto.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Desconto.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Devolucao.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }), 
                    dados[i].Liquida.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Custo.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Lucro.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].PercentualLucro,
                    dados[i].PercentualMargem,
                    dados[i].ClientesAtendidos,                                                                                                                                        
                    dados[i].QtMediaClientes.toLocaleString('pt-BR'),
                    dados[i].TicketMedio
                ];
                dataSet.push(data);
            }
            $(document).ready(function () {
                $('#gridVendas').append('<tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot>');
                $('#gridVendas').DataTable({
                    data: dataSet,
                    paging: false,
                    scrollY: "460px",
                    scrollCollapse: true,
                    order: [[0, 'asc']],
                    columns: [
                        { title: "Data" },
                        { title: "Valor Bruto", sType: "numeric-comma" },
                        { title: "Valor de Desconto", sType: "numeric-comma" },                        
                        { title: "Valor de Devolução", sType: "numeric-comma"  },
                        { title: "Valor Liquído", sType: "numeric-comma" },
                        { title: "Valor Custo", sType: "numeric-comma" },
                        { title: "Valor Lucro", sType: "numeric-comma" },  
                        { title: "Margem de Lucro" },
                        { title: "Margem de Contribuição" },                        
                        { title: "Clientes Atendidos" },
                        { title: "Quantidade Média por Cliente" },
                        { title: "Ticket Médio" },
                    ],
                    "bDestroy": true,
                    deferRender: true,
                    scroller: true,
                    "scrollX": true,
                    "footerCallback": function (tfoot, data, start, end, display) {
                        var api = this.api();
                        for (var i = 0; i < 12; i++) {
                            $(api.column(i).footer()).html(
                                footerValues[i].toLocaleString('pt-BR')
                            );
                        }                        
                    }
                });
            });
            $('#gridVendas').attr('style', 'border-collapse: collapse !important');
            $('#gridVendas').addClass("table table-responsive-sm table-hover table-outline mb-0 dataTable no-footer");
        },
        error: function (xhr, textStatus, errorThrown) {
            
        }
    });
}

function generateGridVendasPorLoja() {
    var mes = $('#mes').val(); 
    var ano = $('#ano').val();
    $.ajax({
        url: '/Dashboard/GridVendaPorLojas',
        data: JSON.stringify({ 'mes': mes, 'ano': ano }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            var chart;
            var dataSet = [];
            var data = [];
            //var footerValues = ['', '', '', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            //sumGridVendasPorLojasFooterValues(footerValues, dados);
            for (var i = 0; i < dados.length; i++) {
                data = [
                    dados[i].Data,
                    dados[i].LojaId,
                    dados[i].Loja,
                    dados[i].Bruto.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Desconto.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),                  
                    dados[i].Devolucao.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),                    
                    dados[i].Liquida.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Custo.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Lucro.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].PercentualLucro,
                    dados[i].PercentualMargem,                    
                    dados[i].ClientesAtendidos,
                    dados[i].QtMediaClientes,
                    dados[i].TicketMedio
                ];
                dataSet.push(data);
            }
            $(document).ready(function () {
                $('#gridVendasPorLoja').append('<tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot>');
                $('#gridVendasPorLoja').DataTable({
                    data: dataSet,
                    paging: false,
                    scrollY: "460px",
                    order: [[0, 'asc'], [1, 'asc']],
                    scrollCollapse: true,
                    columns: [
                        { title: "Data" },
                        { title: "UAD" },
                        { title: "Loja" },
                        { title: "Valor Bruto", sType: "numeric-comma"  },
                        { title: "Valor de Desconto", sType: "numeric-comma"  },                        
                        { title: "Valor de Devolução", sType: "numeric-comma"  },
                        { title: "Valor Líquido", sType: "numeric-comma"  },
                        { title: "Valor Custo", sType: "numeric-comma"  },
                        { title: "Valor Lucro", sType: "numeric-comma"  },     
                        { title: "Margem de Lucro" },
                        { title: "Margem  de Contribuição" },                        
                        { title: "Clientes Atendidos" },
                        { title: "Quantidade Média por Cliente" },
                        { title: "Ticket Médio" },
                    ],
                    "bDestroy": true,
                    deferRender: true,
                    scroller: true,
                    "scrollX": true,
                    "footerCallback": function (tfoot, data, start, end, display) {
                        var api = this.api();
                        for (var i = 2; i < 12; i++) {
                            $(api.column(i + 1).footer()).html(
                                footerGlobalValues[i - 1].toLocaleString('pt-BR')
                            );
                        }
                        $(api.column(0).footer()).html(
                            'Total:'
                        );
                        $(api.column(13).footer()).html(
                            footerGlobalValues[11].toLocaleString('pt-BR')
                        );
                    }
                });
            });
            $('#gridVendasPorLoja').attr('style', 'border-collapse: collapse !important');
            $('#gridVendasPorLoja').addClass("table table-responsive-sm table-hover table-outline mb-0 dataTable no-footer");
        },
        error: function (xhr, textStatus, errorThrown) {
            
        }
    });
}

function generateChartDataSet(dados) {
    if (moment().month() + 1 == parseInt($('#mes').val())) {
        return [{
                type: 'line',
                label: 'Vendas ' + getLastMonthName($('#mes').val()),
                borderColor: '#3e95cd',
                backgroundColor: '#3e95cd',
                borderWidth: 2,
                fill: false,
                data: generateMonthValues(dados.vendasMesAnterior)
            },{
                type: 'bar',
                label: 'Vendas ' + getMonthName($('#mes').val()),
                backgroundColor: '#791B26',
                stack: 'Stack 0',
                data: generateMonthValues(dados.vendas)
            }, {
                type: 'bar',
                label: 'Projeção ' + getMonthName($('#mes').val()),
                backgroundColor: '#b92c26',
                stack: 'Stack 0',
                data: generateProjectionValues(dados.vendas)
            }]
    }
    else {
        return [{
            type: 'line',
            label: 'Vendas ' + getLastMonthName($('#mes').val()),
            borderColor: '#3e95cd',
            backgroundColor: '#3e95cd',
            borderWidth: 2,
            fill: false,
            data: generateMonthValues(dados.vendasMesAnterior)
        }, {
            type: 'bar',
            label: 'Vendas ' + getMonthName($('#mes').val()),
            backgroundColor: '#791B26',
            stack: 'Stack 0',
            data: generateMonthValues(dados.vendas)
        }]
    }
}

function generateGridVendasMensalPorLoja() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    $.ajax({
        url: '/Dashboard/GridVendaMensalPorLojas',
        data: JSON.stringify({ 'mes': mes, 'ano': ano }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            var chart;
            var dataSet = [];
            var data = [];
            //var footerValues = ['', '', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            //sumGridVendasMensalPorLojasFooterValues(footerValues, dados);
            for (var i = 0; i < dados.length; i++) {
                data = [
                    dados[i].LojaId,
                    dados[i].Loja,
                    dados[i].Bruto.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Desconto.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Devolucao.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Liquida.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Custo.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),
                    dados[i].Lucro.toLocaleString('pt-BR', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }),     
                    dados[i].PercentualLucro,
                    dados[i].PercentualMargem,                    
                    dados[i].ClientesAtendidos,
                    dados[i].QtMediaClientes,
                    dados[i].TicketMedio
                ];
                dataSet.push(data);
            }
            $(document).ready(function () {
                $('#gridVendasMensalPorLoja').append('<tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot>');
                $('#gridVendasMensalPorLoja').DataTable({
                    data: dataSet,
                    paging: false,
                    scrollY: "460px",
                    order: [[0, 'asc']],
                    scrollCollapse: true,
                    columns: [
                        { title: "UAD" },
                        { title: "Loja" },
                        { title: "Valor Bruto", sType: "numeric-comma"   },
                        { title: "Valor de Desconto", sType: "numeric-comma"   },
                        { title: "Valor de Devolução", sType: "numeric-comma"   },
                        { title: "Valor Líquido", sType: "numeric-comma"   },
                        { title: "Valor Custo", sType: "numeric-comma"   },
                        { title: "Valor Lucro", sType: "numeric-comma"   },  
                        { title: "Margem de Lucro" },
                        { title: "Margem  de Contribuição" },                        
                        { title: "Clientes Atendidos" },
                        { title: "Quantidade Média por Cliente" },
                        { title: "Ticket Médio" },
                    ],
                    "bDestroy": true,
                    deferRender: true,
                    scroller: true,
                    "scrollX": true,
                    "footerCallback": function (tfoot, data, start, end, display) {
                        var api = this.api();
                        $(api.column(0).footer()).html(
                            'Total:'
                        );
                        for (var i = 1; i < 12; i++) {
                            $(api.column(i + 1).footer()).html(                                
                                footerGlobalValues[i].toLocaleString('pt-BR')
                            );                            
                        }
                    }
                });
            });
            $('#gridVendasMensalPorLoja').attr('style', 'border-collapse: collapse !important');
            $('#gridVendasMensalPorLoja').addClass("table table-responsive-sm table-hover table-outline mb-0 dataTable no-footer");
        },
        error: function (xhr, textStatus, errorThrown) {

        }
    });
}

function generateLabels(data) {
    var labels = [];
    $(data.vendas).each(function (index, value) {
        labels.push([data.vendas[index].Nome, 'R$ ' + data.vendas[index].Valor.toLocaleString('pt-BR')]);
    });
    console.log(labels);
    return labels;
}

function generateMonthValues(data) {
    var values = [
        data[0].Valor,
        data[1].Valor,
        data[2].Valor,
        data[3].Valor,
        data[4].Valor,
        data[5].Valor,
        data[6].Valor,
        data[7].Valor
    ]
    return values;
}

function generateProjectionValues(data) {
    var values = [
        calcProjection(data[0].Valor),
        calcProjection(data[1].Valor),
        calcProjection(data[2].Valor),
        calcProjection(data[3].Valor),
        calcProjection(data[4].Valor),
        calcProjection(data[5].Valor),
        calcProjection(data[6].Valor),
        calcProjection(data[7].Valor)
    ]
    return values;    
}

function calcProjection(value) {
    var year = $('#ano').val()
    var month = $('#mes').val()
    var daysQuantity = moment(year + "-" + month, "YYYY-MM").daysInMonth();
    var media = value / moment().date();
    var total = media * daysQuantity;
    var projection = total - value;
    return projection;
}

function showLoader(element) {
    $('#loading-' + element).replaceWith('<div class="sk-folding-cube" id="' + element + '">\
                                    <div class= "sk-cube1 sk-cube" ></div >\
                                    <div class="sk-cube2 sk-cube"></div>\
                                    <div class="sk-cube4 sk-cube"></div>\
                                    <div class="sk-cube3 sk-cube"></div>\
                                </div >');
}

function removeLoader(element) {
    //Substituindo loader pelo canvas do gráfico
    $('#' + element).replaceWith('<div class="chart-wrapper" style="margin-top:40px;">\
                                    <canvas id="'+ element + '" class= "chartjs-render-monitor"></canvas>\
                                  </div>')
}

function changeChartPeriod(data) {
    $("#chartPeriod").text("Dados de " + data.month + " de " + data.year);
}

function sumGridVendasFooterValues(totalArrayValues, objectValues) {
    //console.log(objectValues.length);
    for (var i = 0; i < objectValues.length; i++) {
        totalArrayValues[0] = "Total: ";
        totalArrayValues[1] += objectValues[i].Bruto;
        totalArrayValues[2] += objectValues[i].Desconto;
        totalArrayValues[3] += objectValues[i].Devolucao;
        totalArrayValues[4] += objectValues[i].Liquida;
        totalArrayValues[5] += objectValues[i].Custo;
        totalArrayValues[6] += objectValues[i].Lucro;     
        totalArrayValues[7] += parseFloat(objectValues[i].PercentualLucro.replace('%', ''));
        totalArrayValues[8] += parseFloat(objectValues[i].PercentualMargem.replace('%', ''));        
        totalArrayValues[9] += objectValues[i].ClientesAtendidos;
        totalArrayValues[10] += parseFloat(objectValues[i].QtMediaClientes);
        totalArrayValues[11] += parseFloat(objectValues[i].TicketMedio);
    }
    //Fazendo a divisão para gerar as médias
    totalArrayValues[7] = (totalArrayValues[7] / objectValues.length).toFixed(2) + '%';
    totalArrayValues[8] = (totalArrayValues[8] / objectValues.length).toFixed(2) + '%';
    totalArrayValues[10] = (totalArrayValues[10] / objectValues.length).toFixed(2);
    totalArrayValues[11] = (totalArrayValues[11] / objectValues.length).toFixed(2);
}

function sumGridVendasPorLojasFooterValues(totalArrayValues, objectValues) {
    for (var i = 0; i < objectValues.length; i++) {
        totalArrayValues[0] = "Total: ";
        totalArrayValues[1] = "";
        totalArrayValues[2] = "";
        totalArrayValues[3] += objectValues[i].Bruto;
        totalArrayValues[4] += objectValues[i].Desconto;
        totalArrayValues[5] += objectValues[i].Devolucao;
        totalArrayValues[6] += objectValues[i].Liquida;
        totalArrayValues[7] += objectValues[i].Lucro;
        totalArrayValues[8] += objectValues[i].Custo;
        totalArrayValues[9] += parseFloat(objectValues[i].PercentualMargem.replace('%', '').replace(',', '.'));
        totalArrayValues[10] += parseFloat(objectValues[i].PercentualLucro.replace('%', '').replace(',', '.'));
        totalArrayValues[11] += objectValues[i].ClientesAtendidos;
        totalArrayValues[12] += parseFloat(objectValues[i].QtMediaClientes.replace(',', '.'));
        totalArrayValues[13] += parseFloat(objectValues[i].TicketMedio.replace('.', '').replace(',', '.'));
    }
    //Fazendo a divisão para gerar as médias
    totalArrayValues[9] = (totalArrayValues[9] / objectValues.length).toFixed(2) + '%';
    totalArrayValues[10] = (totalArrayValues[10] / objectValues.length).toFixed(2) + '%';
    totalArrayValues[12] = (totalArrayValues[12] / objectValues.length).toFixed(2);
    totalArrayValues[13] = (totalArrayValues[13] / objectValues.length).toFixed(2);
}

function sumGridVendasMensalPorLojasFooterValues(totalArrayValues, objectValues) {
    for (var i = 0; i < objectValues.length; i++) {
        totalArrayValues[0] = "Total: ";
        totalArrayValues[1] = "";
        totalArrayValues[2] += objectValues[i].Bruto;
        totalArrayValues[3] += objectValues[i].Desconto;
        totalArrayValues[4] += objectValues[i].Devolucao;
        totalArrayValues[5] += objectValues[i].Liquida;
        totalArrayValues[6] += objectValues[i].Lucro;
        totalArrayValues[7] += objectValues[i].Custo;
        totalArrayValues[8] += parseFloat(objectValues[i].PercentualMargem.replace('%', '').replace(',', '.'));
        totalArrayValues[9] += parseFloat(objectValues[i].PercentualLucro.replace('%', '').replace(',', '.'));
        totalArrayValues[10] += objectValues[i].ClientesAtendidos;
        totalArrayValues[11] += parseFloat(objectValues[i].QtMediaClientes.replace(',', '.'));
        totalArrayValues[12] += parseFloat(objectValues[i].TicketMedio.replace(',', ''));
    }
    //Fazendo a divisão para gerar as médias
    totalArrayValues[8] = (totalArrayValues[8] / objectValues.length).toFixed(2) + '%';
    totalArrayValues[9] = (totalArrayValues[9] / objectValues.length).toFixed(2) + '%';
    totalArrayValues[11] = (totalArrayValues[11] / objectValues.length).toFixed(2);
    totalArrayValues[12] = (totalArrayValues[12] / objectValues.length).toFixed(2);
}

