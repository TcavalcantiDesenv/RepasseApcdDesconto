var chartVendasLojaPorHora;
var chartData;
function carregarGraficosLojaPorHora(canvasId, chartVar, onlyToday) {
    //function para carregar dados no gráfico Acumulado de Vendas por Hora (Lojas)
    showLoader(canvasId);
    var mes = $('#mes-loja').val();
    var ano = $('#ano-loja').val();
    var top = $('#top-loja').val();
    $.ajax({
        url: '/Lojas/VendasPorHorario',
        data: JSON.stringify({ 'top': top, 'mes': mes, 'ano': ano, 'onlyToday': onlyToday }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            chartData = dados;
            if (window[chartVar] != undefined || window[chartVar] != null) {
                window[chartVar].destroy();
            }
            else {
                removeLoader(canvasId);
            }
            var ctx = document.getElementById(canvasId).getContext('2d');
            window[chartVar] = new Chart(ctx, {
                type: 'line',
                data: generateChartLojaPorHoraData(dados),
                options: { 
                    elements: {
                        line: {
                            tension: 0
                        }
                    },
                    title: {
                        display: false,
                    },
                    responsive: true,
                    scales: {
                        yAxes: [{
                            ticks: {
                                callback: function (label, index, labels) {
                                    return '+ ' + (label).toLocaleString('pt-BR');
                                }
                            }
                        }]
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                return ' Clientes Atendidos: ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                            },
                            footer: function (tooltipItem, data) {
                                var tiket =        chartData[tooltipItem[0].datasetIndex].HorasVendaViewModels[tooltipItem[0].index].TicketMedio;
                                var ValorLiquido = chartData[tooltipItem[0].datasetIndex].HorasVendaViewModels[tooltipItem[0].index].Valor;
                                var clientesAtendidos = chartData[tooltipItem[0].datasetIndex].HorasVendaViewModels[tooltipItem[0].index].ClientesAtendidos;
                                return ['     Valor Líquido R$: ' + ValorLiquido.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }), '     Ticket Médio R$: ' + tiket.toLocaleString('pt-BR', {minimumFractionDigits: 2, maximumFractionDigits: 2 } )];
                            }
                        }
                    }
                }
            });
            buttonStop(document.getElementById('vendas-loja-btn'));
            buttonStop(document.getElementById('vendashoje-loja-btn'));
        },        
        error: function (xhr, textStatus, errorThrown) {
            
        }
    });
}

function generateChartLojaPorHoraData(data) {
    var chartData = {
        labels: getHours(data),
        datasets: generateLojaPorHoraDataSet(data)
    };
    return chartData;
}

function generateLojaPorHoraDataSet(data) {
    var dataSets = [];    
    $(data).each(function (index, element) {
        var values = [];
        $(element.HorasVendaViewModels).each(function (chave, valor) {
            values.push(valor.ClientesAtendidos);
        });
        var dataSet = {
            label: element.Nome,
            backgroundColor: getColors(index),
            borderColor: getColors(index),
            fill: false,
            data: values
        }
        dataSets.push(dataSet);
    });
    return dataSets;
}

function carregarVendaLojaPorHora(element, onlyToday) {
    buttonLoading(element);
    carregarGraficosLojaPorHora('vendasLojaPorHora', 'chartVendasLojaPorHora', onlyToday);
}

function getHours(data) {
    var hours = [];
    if (data[0] != null) {
        $(data[0].HorasVendaViewModels).each(function (index, value) {
            hours.push(value.Hora);
        });
    }    
    return hours;
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
                                    <canvas id="'+ element + '" class= "chartjs-render-monitor" ></canvas >\
                                  </div >')
}

function getColors(index) {
    var colors = [
        '#F86C6B',
        '#FF9F40',
        '#FFCD56',
        '#4BC0C0',
        '#36A2EB',
        '#9966FF',
        '#C9CBCF',
        '#34495E',
        '#A93226',
        '#884EA0',
        '#1F618D',
        '#196F3D',
        '#F1C40F',
        '#D35400',
        '#34495E',
    ]
    var colorIndex = 0;
    for (var i = 0; i < index; i++) {
        colorIndex++;
        if (colorIndex > 14) {
            colorIndex = 0;
        }
    }
    return colors[colorIndex];
}