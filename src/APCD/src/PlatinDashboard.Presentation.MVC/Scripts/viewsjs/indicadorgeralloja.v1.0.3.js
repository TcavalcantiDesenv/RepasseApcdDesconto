var myChart4;
var myChart5;
var myChart6;
var myChart27;
var chartLiquido;
var chartVendasLojaPorHora;
var chartData;
function TickeMedioMes() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();
    $.ajax({
        url: '/Lojas/RetornarTicketMedioMeses',
        data: JSON.stringify({ 'ano': ano, 'loja': loja }),
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
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarTicketMedioClientesMeses',
        data: JSON.stringify({ 'ano': ano, 'loja': loja }),
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
                    datasets: generateChartDataSet(dados)
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
                                if (tooltipItem.datasetIndex != 2) {
                                    return year + ' - ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                                }
                                else {
                                    var sellValue = data.datasets[1].data[tooltipItem.index];
                                    return 'Projeção ' + year + ' - ' + (parseInt(tooltipItem.yLabel) + sellValue).toLocaleString('pt-BR');
                                } 
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
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarTicketMedioItensMeses',
        data: JSON.stringify({ 'ano': ano, 'loja': loja }),
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
                    datasets: generateChartDataSet(dados)
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
                                if (tooltipItem.datasetIndex != 2) {
                                    return 'Quantidade Vendida ' + year + ' - ' + (tooltipItem.yLabel).toLocaleString('pt-BR');
                                }
                                else {
                                    var sellValue = data.datasets[1].data[tooltipItem.index];
                                    return 'Projeção ' + year + ' - ' + (parseInt(tooltipItem.yLabel) + sellValue).toLocaleString('pt-BR');
                                }      
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

function ItensPorCliente() {

    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarItensPorCliente',
        data: JSON.stringify({ 'ano': ano, 'loja': loja }),
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

function LucroLiquidoMes()   {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarLucroLiquidoMeses',
        data: JSON.stringify({ 'ano': ano, 'loja': loja }),
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
                    datasets: generateChartDataSet(dados)
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
                                if (tooltipItem.datasetIndex != 2) {
                                    return 'Vendas ' + year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
                                        minimumFractionDigits: 2,
                                        maximumFractionDigits: 2
                                    });
                                }
                                else {
                                    var sellValue = data.datasets[1].data[tooltipItem.index];
                                    return 'Projeção ' + year + ' - R$ ' + (tooltipItem.yLabel + sellValue).toLocaleString('pt-BR', {
                                        minimumFractionDigits: 2,
                                        maximumFractionDigits: 2
                                    });
                                }                                
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

function generateChartDataSet(dados) {
    if ($('#ano').val() == moment().year()) {
        return [
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
                label: $('#ano').val(),
                type: "bar",
                backgroundColor: "#722F37",
                stack: 'Stack 0',
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
            }, {
                label: $('#ano').val(),
                type: "bar",
                backgroundColor: "#b92c26",
                stack: 'Stack 0',
                data: generateProjectionValues(dados[0])
            }
        ]
    }
    else {
        return [
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
                label: $('#ano').val(),
                type: "bar",
                backgroundColor: "#722F37",
                stack: 'Stack 0',
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
    }
}

function generateProjectionValues(arrayValues) {
    var currentMonth = moment().month();
    var projectionValues = [];
    if (currentMonth == 0) {
        projectionValues.push(calcProjection(arrayValues.MediaJaneiro));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 1) {
        projectionValues.push(calcProjection(arrayValues.MediaFevereiro));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 2) {
        projectionValues.push(calcProjection(arrayValues.MediaAbril));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 3) {
        projectionValues.push(calcProjection(arrayValues.MediaMarço));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 4) {
        projectionValues.push(calcProjection(arrayValues.MediaMaio));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 5) {
        projectionValues.push(calcProjection(arrayValues.MediaJunho));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 6) {
        projectionValues.push(calcProjection(arrayValues.MediaJulho));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 7) {
        projectionValues.push(calcProjection(arrayValues.MediaAgosto));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 8) {
        projectionValues.push(calcProjection(arrayValues.MediaSetembro));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 9) {
        projectionValues.push(calcProjection(arrayValues.MediaOutubro));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 10) {
        projectionValues.push(calcProjection(arrayValues.MediaNovembro));
    }
    else {
        projectionValues.push(0);
    }
    if (currentMonth == 11) {
        projectionValues.push(calcProjection(arrayValues.MediaDezembro));
    }
    else {
        projectionValues.push(0);
    }
    return projectionValues;
}

function calcProjection(value) {
    var year = $('#ano').val()
    var daysQuantity = moment(year + "-" + moment().month(), "YYYY-MM").daysInMonth();
    var media = value / moment().date();
    var total = media * daysQuantity;
    var projection = total - value;
    return projection;
}

function carregarGraficos(element) {
    buttonLoading(element);
    LucroLiquidoMes();
    TickeMedioMes();
    TotalClientesMes();
    TotaltensVendidosMes();
    ItensPorCliente();
}