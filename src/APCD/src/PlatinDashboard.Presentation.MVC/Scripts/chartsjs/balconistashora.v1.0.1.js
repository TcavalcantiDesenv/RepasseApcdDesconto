var chartBalconistaLojaPorHora;
var chartData;

function generateChartData(data) {
    var chartData = {
        labels: getHours(),
        datasets: generateDataSets(data)
    };
    return chartData;
}

function generateDataSets(data) {
    var dataSets = [];    
    $(data).each(function (index, element) {
        var values = [];
        $(element.HorasVendaViewModels).each(function (chave, valor) {
            values.push(valor.Valor);
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

function carregarGraficoVendasBalconistaPorHora(canvasId, chartVar) {
    //function para carregar dados no gráfico Acumulado de Vendas por Hora (Balconistas)
    showLoader(canvasId);
    var mes = $('#mes-balconista').val();
    var ano = $('#ano-balconista').val();
    var top = $('#top-balconista').val();
    var loja = $('#loja-balconista').val();
    $.ajax({
        url: '/Balconistas/VendasPorHorario',
        data: JSON.stringify({ 'lojaId': loja, 'top': top, 'mes': mes, 'ano': ano }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            chartData = dados
            console.log(dados);
            var vendas = [];
            if (window[chartVar] != undefined || window[chartVar] != null) {
                window[chartVar].destroy()
            }
            else {
                removeLoader(canvasId);
            }
            var ctx = document.getElementById(canvasId).getContext('2d');
            window[chartVar] = new Chart(ctx, {
                type: 'line',
                data: generateChartData(dados),
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
                                    return 'R$ ' + (label).toLocaleString('pt-BR');
                                }
                            }
                        }]
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                return ' Valor Bruto R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                            },
                            footer: function (tooltipItem, data) {
                                var tiket = chartData[tooltipItem[0].datasetIndex].HorasVendaViewModels[tooltipItem[0].index].TicketMedio;
                                var clientesAtendidos = chartData[tooltipItem[0].datasetIndex].HorasVendaViewModels[tooltipItem[0].index].ClientesAtendidos;
                                return ['     Clientes Atendidos ' + clientesAtendidos, '     Ticket Médio R$ ' + tiket.toLocaleString('pt-BR')];
                            }
                        }
                    }
                }
            });
            buttonStop(document.getElementById('vendas-hora-btn'));
        },
        error: function (xhr, textStatus, errorThrown) {
           
        }
    });
}

function carregarVendaBalconistaPorHora(element) {
    buttonLoading(element);
    carregarGraficoVendasBalconistaPorHora('vendasBalconistaPorHora', 'chartVendasBalconistaPorHora');
}

function getHours() {
    var hours = [];
    for (var i = 8; i <= 22; i++) {
        hours.push(i + ':00');
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