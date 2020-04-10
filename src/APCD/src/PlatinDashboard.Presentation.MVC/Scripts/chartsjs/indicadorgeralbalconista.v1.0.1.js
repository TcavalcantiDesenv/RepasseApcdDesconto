var chartLiquido;
var myChart4;
var myChart5;
var myChart6;
var myChart27;
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

function TickeMedioMes() {
    var ano = $('#ano').val();
    var balconista = $('#balconista').val();

    $.ajax({
        url: '/Balconistas/RetornarTicketMedioMeses',
        data: JSON.stringify({ 'ano': ano, 'balconistaId': balconista }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart4) {
                myChart4.destroy();
            }

            $("#ticketMedioMes-legenda").empty();
            $("#ticketMedioMes-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart4 = new Chart(document.getElementById("ticketMedioMes"), {
                type: 'bar',
                data: {
                    labels: ["Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
                    datasets: [
                        {
                            label: ($("#ano").val() - 1),
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: [dados[0].MediaJaneiroAnterior,
                            dados[0].MediaFevereiroAnterior,
                            dados[0].MediaMarçoAnterior,
                            dados[0].MediaAbrilAnterior,
                            dados[0].MediaMaioAnterior,
                            dados[0].MediaJunhoAnterior,
                            dados[0].MediaJulhoAnterior,
                            dados[0].MediaAgostoAnterior,
                            dados[0].MediaSetembroAnterior,
                            dados[0].MediaOutubroAnterior,
                            dados[0].MediaNovembroAnterior,
                            dados[0].MediaDezembroAnterior],
                            fill: false
                        }, {
                            label: ano,
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: [dados[0].MediaJaneiro,
                            dados[0].MediaFevereiro,
                            dados[0].MediaMarço,
                            dados[0].MediaAbril,
                            dados[0].MediaMaio,
                            dados[0].MediaJunho,
                            dados[0].MediaJulho,
                            dados[0].MediaAgosto,
                            dados[0].MediaSetembro,
                            dados[0].MediaOutubro,
                            dados[0].MediaNovembro,
                            dados[0].MediaDezembro
                            ]
                        }
                    ]
                },
                options: {
                    title: {
                        display: true
                    },
                    legend: { display: false },
                    scales: {
                        xAxes: [{

                        }],
                        yAxes: [{
                            ticks: {
                                callback: function (label, index, labels) {
                                    return 'R$ ' + (label).toLocaleString('pt-BR', {
                                        minimumFractionDigits: 2,
                                        maximumFractionDigits: 2
                                    });
                                }
                            }
                        }]
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                var year = data.datasets[tooltipItem.datasetIndex].label;
                                return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
                                    minimumFractionDigits: 2,
                                    maximumFractionDigits: 2
                                });
                            }
                        },
                    }
                }
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function TotalClientesMes() {
    var ano = $('#ano').val();
    var balconista = $('#balconista').val();

    $.ajax({
        url: '/Balconistas/RetornarTicketMedioClientesMeses',
        data: JSON.stringify({ 'ano': ano, 'balconistaId': balconista }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart5) {
                myChart5.destroy();
            }

            $("#totalClientesMes-legenda").empty();
            $("#totalClientesMes-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart5 = new Chart(document.getElementById("totalClientesMes"), {
                type: 'bar',
                data: {
                    labels: ["Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
                    datasets: [
                        {
                            label: ($("#ano").val() - 1),
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: [dados[0].MediaJaneiroAnterior,
                            dados[0].MediaFevereiroAnterior,
                            dados[0].MediaMarçoAnterior,
                            dados[0].MediaAbrilAnterior,
                            dados[0].MediaMaioAnterior,
                            dados[0].MediaJunhoAnterior,
                            dados[0].MediaJulhoAnterior,
                            dados[0].MediaAgostoAnterior,
                            dados[0].MediaSetembroAnterior,
                            dados[0].MediaOutubroAnterior,
                            dados[0].MediaNovembroAnterior,
                            dados[0].MediaDezembroAnterior],
                            fill: false
                        }, {
                            label: ano,
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: [dados[0].MediaJaneiro,
                            dados[0].MediaFevereiro,
                            dados[0].MediaMarço,
                            dados[0].MediaAbril,
                            dados[0].MediaMaio,
                            dados[0].MediaJunho,
                            dados[0].MediaJulho,
                            dados[0].MediaAgosto,
                            dados[0].MediaSetembro,
                            dados[0].MediaOutubro,
                            dados[0].MediaNovembro,
                            dados[0].MediaDezembro
                            ]
                        }
                    ]
                },
                options: {
                    title: {
                        display: true
                    },
                    legend: { display: false },
                    scales: {
                        xAxes: [{

                        }],
                        yAxes: [{
                            ticks: {
                                callback: function (label, index, labels) {
                                    return (label).toLocaleString('pt-BR');
                                }
                            }
                        }]
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                var year = data.datasets[tooltipItem.datasetIndex].label;
                                return year + ' - ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                            }
                        },
                    }
                }
            });

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function TotaltensVendidosMes() {
    var ano = $('#ano').val();
    var balconista = $('#balconista').val();

    $.ajax({
        url: '/Balconistas/RetornarTicketMedioItensMeses',
        data: JSON.stringify({ 'ano': ano, 'balconistaId': balconista }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            if (myChart6) {
                myChart6.destroy();
            }

            $("#totalItensVendidosMes-legenda").empty();
            $("#totalItensVendidosMes-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart6 = new Chart(document.getElementById("totalItensVendidosMes"), {
                type: 'bar',
                data: {
                    labels: ["Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
                    datasets: [
                        {
                            label: ($("#ano").val() - 1),
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: [dados[0].MediaJaneiroAnterior,
                            dados[0].MediaFevereiroAnterior,
                            dados[0].MediaMarçoAnterior,
                            dados[0].MediaAbrilAnterior,
                            dados[0].MediaMaioAnterior,
                            dados[0].MediaJunhoAnterior,
                            dados[0].MediaJulhoAnterior,
                            dados[0].MediaAgostoAnterior,
                            dados[0].MediaSetembroAnterior,
                            dados[0].MediaOutubroAnterior,
                            dados[0].MediaNovembroAnterior,
                            dados[0].MediaDezembroAnterior],
                            fill: false
                        }, {
                            label: ano,
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: [dados[0].MediaJaneiro,
                            dados[0].MediaFevereiro,
                            dados[0].MediaMarço,
                            dados[0].MediaAbril,
                            dados[0].MediaMaio,
                            dados[0].MediaJunho,
                            dados[0].MediaJulho,
                            dados[0].MediaAgosto,
                            dados[0].MediaSetembro,
                            dados[0].MediaOutubro,
                            dados[0].MediaNovembro,
                            dados[0].MediaDezembro
                            ]
                        }
                    ]
                },
                options: {
                    title: {
                        display: true
                    },
                    legend: { display: false },
                    scales: {
                        xAxes: [{

                        }],
                        yAxes: [{
                            ticks: {
                                callback: function (label, index, labels) {
                                    return (label).toLocaleString('pt-BR');
                                }
                            }
                        }]
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                var year = data.datasets[tooltipItem.datasetIndex].label;
                                return year + ' - ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                            }
                        },
                    }
                }
            });

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function LucroLiquidoMes() {
    var ano = $('#ano').val();
    var balconista = $('#balconista').val();
    $.ajax({
        url: '/Balconistas/RetornarLucroLiquidoMeses',
        data: JSON.stringify({ 'ano': ano, 'balconistaId': balconista }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (chartLiquido) {
                chartLiquido.destroy();
            }

            $("#valorLiquidoMes-legenda").empty();
            $("#valorLiquidoMes-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            chartLiquido = new Chart(document.getElementById("valorLiquidoMes"), {
                type: 'bar',
                data: {
                    labels: ["Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
                    datasets: [
                        {
                            label: ($("#ano").val() - 1),
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: [dados[0].MediaJaneiroAnterior,
                            dados[0].MediaFevereiroAnterior,
                            dados[0].MediaMarçoAnterior,
                            dados[0].MediaAbrilAnterior,
                            dados[0].MediaMaioAnterior,
                            dados[0].MediaJunhoAnterior,
                            dados[0].MediaJulhoAnterior,
                            dados[0].MediaAgostoAnterior,
                            dados[0].MediaSetembroAnterior,
                            dados[0].MediaOutubroAnterior,
                            dados[0].MediaNovembroAnterior,
                            dados[0].MediaDezembroAnterior],
                            fill: false
                        }, {
                            label: ano,
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: [dados[0].MediaJaneiro,
                            dados[0].MediaFevereiro,
                            dados[0].MediaMarço,
                            dados[0].MediaAbril,
                            dados[0].MediaMaio,
                            dados[0].MediaJunho,
                            dados[0].MediaJulho,
                            dados[0].MediaAgosto,
                            dados[0].MediaSetembro,
                            dados[0].MediaOutubro,
                            dados[0].MediaNovembro,
                            dados[0].MediaDezembro
                            ]
                        }
                    ]
                },
                options: {
                    title: {
                        display: true
                    },
                    legend: { display: false },
                    scales: {
                        xAxes: [{

                        }],
                        yAxes: [{
                            ticks: {
                                callback: function (label, index, labels) {
                                    return 'R$ ' + (label).toLocaleString('pt-BR', {
                                        minimumFractionDigits: 2,
                                        maximumFractionDigits: 2
                                    });
                                }
                            }
                        }]
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                var year = data.datasets[tooltipItem.datasetIndex].label;
                                return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
                                    minimumFractionDigits: 2,
                                    maximumFractionDigits: 2
                                });
                            }
                        },
                    }
                }
            });
            buttonStop(document.getElementById('indice-gerenciais-btn'));
        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function ItensPorCliente() {
    var ano = $('#ano').val();
    var balconista = $('#balconista').val();

    $.ajax({
        url: '/Balconistas/RetornarItensPorCliente',
        data: JSON.stringify({ 'ano': ano, 'balconistaId': balconista }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart27) {
                myChart27.destroy();
            }

            $("#itensPorCliente-legenda").empty();
            $("#itensPorCliente-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart27 = new Chart(document.getElementById("itensPorCliente"), {
                type: 'bar',
                data: {
                    labels: ["Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
                    datasets: [
                        {
                            label: ($("#ano").val() - 1),
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: [dados[0].MediaJaneiroAnterior,
                            dados[0].MediaFevereiroAnterior,
                            dados[0].MediaMarçoAnterior,
                            dados[0].MediaAbrilAnterior,
                            dados[0].MediaMaioAnterior,
                            dados[0].MediaJunhoAnterior,
                            dados[0].MediaJulhoAnterior,
                            dados[0].MediaAgostoAnterior,
                            dados[0].MediaSetembroAnterior,
                            dados[0].MediaOutubroAnterior,
                            dados[0].MediaNovembroAnterior,
                            dados[0].MediaDezembroAnterior],
                            fill: false
                        }, {
                            label: ano,
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: [dados[0].MediaJaneiro,
                            dados[0].MediaFevereiro,
                            dados[0].MediaMarço,
                            dados[0].MediaAbril,
                            dados[0].MediaMaio,
                            dados[0].MediaJunho,
                            dados[0].MediaJulho,
                            dados[0].MediaAgosto,
                            dados[0].MediaSetembro,
                            dados[0].MediaOutubro,
                            dados[0].MediaNovembro,
                            dados[0].MediaDezembro
                            ]
                        }
                    ]
                },
                options: {
                    title: {
                        display: true
                    },
                    legend: { display: false },
                    scales: {
                        xAxes: [{

                        }],
                        yAxes: [{
                            ticks: {
                                callback: function (label, index, labels) {
                                    return (label).toLocaleString('pt-BR');
                                }
                            }
                        }]
                    },
                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem, data) {
                                var year = data.datasets[tooltipItem.datasetIndex].label;
                                return year + ' - ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                            }
                        },
                    }
                }
            });

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function carregarGraficoVendasBalconistaPorHoraIndicador(canvasId, chartVar) {
    //function para carregar dados no gráfico Acumulado de Vendas por Hora (Balconistas)
    showLoader(canvasId);
    var ano = $('#ano').val();
    var balconista = $('#balconista').val();
    $.ajax({
        url: '/Balconistas/VendasPorHorarioIndicador',
        data: JSON.stringify({ 'ano': ano, 'balconistaId': balconista }),
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

function carregarGraficos(element) {
    buttonLoading(element);
    LucroLiquidoMes();
    TickeMedioMes();
    TotalClientesMes();
    TotaltensVendidosMes();
    ItensPorCliente();
    carregarGraficoVendasBalconistaPorHoraIndicador('vendasBalconistaPorHora', 'chartVendasBalconistaPorHora');
}