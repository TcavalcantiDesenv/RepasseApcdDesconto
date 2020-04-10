var chartMesAtual;
var chartMesPassado;
var chartMesRetrasado;
var months = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];

function getMonthName(monthNumber) {
    return months[monthNumber - 1];
}

function carregarGraficoVendasMensal(element) {
    buttonLoading(element);
    var periodos = getPeriods();
    generateStackeChart('mesAtual', 'chartMesAtual', periodos.mesAtual);
    generateStackeChart('mesPassado', 'chartMesPassado', periodos.mesPassado);
    generateStackeChart('mesRetrasado', 'chartMesRetrasado', periodos.mesRetrasado);
}

function generateStackeChart(canvasId, chartVar, mesPeriodo) {
    showLoader(canvasId);
    var loja = $('#loja').val();
    var top = $('#top').val();    
    $.ajax({
        url: '/Balconistas/VendasMensal',
        data: JSON.stringify({ 'lojaId': loja, 'mes': mesPeriodo, 'top': top }),
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
            var ctx = document.getElementById(canvasId).getContext('2d');
            var legendElement = $(document.getElementById(canvasId)).parent().parent()[0].getElementsByClassName("legend-Month")[0];
            console.log(legendElement);
            legendElement.innerHTML = 'Dados do mês de ' + getMonthName(mesPeriodo.slice(-2));
            window[chartVar] = new Chart(ctx, {
                type: 'bar',
                data: generateChartData(dados),
                options: {
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
                            label: function (tooltipItem, data) {
                                return 'R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                            }
                        }
                    }
                }
            });                       
            buttonStop(document.getElementById('vendas-mensal-btn'));
        },
        error: function () {
            
        }
    });
}

function generateChartData(data) {
    //Criando array com os nomes dos balconistas
    var labels = [];
    for (var i = 0; i < data.balconistas.length; i++) {
        var label = data.balconistas[i].Nome;
        labels.push(label);
    }
    var barChartData = {
        labels: labels,
        datasets: [
            generateDataSet(data, 0, '#F86C6B'),
            generateDataSet(data, 1, '#FF9F40'),
            generateDataSet(data, 2, '#FFCD56'),
            generateDataSet(data, 3, '#4BC0C0'),
            generateDataSet(data, 4, '#36A2EB'),
            generateDataSet(data, 5, '#9966FF'),
            generateDataSet(data, 6, '#C9CBCF'),
            generateDataSet(data, 7, '#34495E')
        ]
    };
    return barChartData;
}

function generateDataSet(data, classificacaoIndex, color) {
    var valores = [];
    for (var i = 0; i < data.vendasBalconista[classificacaoIndex].length; i++) {
        var valorTotal = data.vendasBalconista[classificacaoIndex][i].ValorTotal;
        valores.push(valorTotal);
    }
    var dataSet = {
        label: data.categorias[classificacaoIndex],
        backgroundColor: color,
        data: valores
    }
    return dataSet;
}

function getPeriods() {
    var periods = {
        mesAtual: moment().add('month', +1).year().toString() + formatMonth(moment().add('month', +1).month().toString()),
        mesPassado: moment().year().toString() + formatMonth(moment().month().toString()),
        mesRetrasado: moment().add('month', -1).year().toString() + formatMonth(moment().add('month', -1).month().toString())
    }
    return periods;
}

function formatMonth(month) {
    return month.toString().length < 2 ? '0' + month : month;
}

function showLoader(element) {
    $('#loading-' + element).replaceWith('<div class="sk-folding-cube" id="'+ element +'">\
                                    <div class= "sk-cube1 sk-cube" ></div >\
                                    <div class="sk-cube2 sk-cube"></div>\
                                    <div class="sk-cube4 sk-cube"></div>\
                                    <div class="sk-cube3 sk-cube"></div>\
                                </div >');
}

function removeLoader(element) {
    //Substituindo loader pelo canvas do gráfico
    $('#' + element).replaceWith('<div class="chart-wrapper" style="margin-top:40px;">\
                                    <canvas id="'+ element +'" class= "chartjs-render-monitor" ></canvas >\
                                  </div >')
}