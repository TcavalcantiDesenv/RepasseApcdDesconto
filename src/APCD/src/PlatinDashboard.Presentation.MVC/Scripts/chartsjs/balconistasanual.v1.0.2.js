var chartBalconistaAnual;

function CarregarDadosBalconistas() {
    var loja = $("#loja").val();
    $.ajax({
        url: '/Balconistas/BalconistaPorLoja',
        data: JSON.stringify({ 'uad': loja }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            //Combo Balconistas
            var comboBalconista = document.getElementById("balconista");
            for (var i = 0; i < dados.listBalconista.length; i++) {
                var opt = document.createElement("option");
                opt.value = dados.listBalconista[i].Cod;
                opt.text = dados.listBalconista[i].Nom;
                comboBalconista.add(opt, comboBalconista.options[0]);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            
        }
    });
}

function StackedBarBalconistaAnual(canvasId, chartVar) {
    showLoader(canvasId);
    var ano = $('#ano').val();
    var balconista = $('#balconista').val();
    $.ajax({
        url: '/Balconistas/VendasAnual',
        data: JSON.stringify({ 'ano': ano, 'balconistaId': balconista }),
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
            buttonStop(document.getElementById('vendas-anual-btn'));
        },
        error: function (xhr, textStatus, errorThrown) {
            
        }
    });
}

function generateChartData(data) {
    //Criando array com os meses    
    var barChartData = {
        labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        datasets: [
            generateDataSet(data, 1, '#F86C6B'),
            generateDataSet(data, 2, '#FF9F40'),
            generateDataSet(data, 3, '#FFCD56'),
            generateDataSet(data, 4, '#4BC0C0'),
            generateDataSet(data, 5, '#36A2EB'),
            generateDataSet(data, 6, '#9966FF'),
            generateDataSet(data, 7, '#C9CBCF'),
            generateDataSet(data, 8, '#34495E')
        ]
    };
    console.log(barChartData);
    return barChartData;
}

function generateDataSet(data, classificacaoId, color) {
    var valores = [];
    for (var i = 1; i <= 12; i++) {
        for (var j = 0; j < data.balconistaViewModel.BalconistaVendaViewModels.length; j++) {
            //Verificando se o item corresponde ao mês e categoria correta
            if (data.balconistaViewModel.BalconistaVendaViewModels[j].Mes == i
                && data.balconistaViewModel.BalconistaVendaViewModels[j].CategoriaId == classificacaoId) {
                var valorTotal = data.balconistaViewModel.BalconistaVendaViewModels[j].ValorTotal;
                valores.push(valorTotal);
            }
        }        
    }
    //removendo 1 do valor da categoria, pois está utilizando o index do array
    var dataSet = {
        label: data.categorias[classificacaoId - 1],
        backgroundColor: color,
        data: valores
    }
    console.log(dataSet);
    return dataSet;
}

function carregarGraficoVendaAnual(element) {
    buttonLoading(element);
    StackedBarBalconistaAnual('balconistaAnual', 'chartBalconistaAnual');
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