var myChart;
var myChart2;
var myChart3;
var myChart4;
var myChart5;
var myChart6;
var myChart7;
var myChart8;
var myChart9;
var myChart10;
var myChart11;
var myChart12;
var myChart13;
var myChart14;
var myChart15;
var myChart16;
var myChart17;
var myChart18;
var myChart19;
var myChart20;
var myChart21;
var myChart22;
var myChart23;
var myChart24;
var myChart25;
var myChart26;
var myChart27;
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

function TotalClassificacoesDiario() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();
    $.ajax({
        url: '/Lojas/RetornarClsVenAllDiasTotal',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'loja': loja }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }


            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }

            if (myChart2) {
                myChart2.destroy();
            }

            $("#totalClassificacoesDiaria-legenda").empty();
            $("#totalClassificacoesDiaria-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");

            myChart2 = new Chart(document.getElementById("totalClassificacoesDiaria-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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

function TotalClassificacoesMensal() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarClsVenAllMesesTotal',
        data: JSON.stringify({ 'ano': ano, 'loja': loja }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart) {
                myChart.destroy();
            }

            $("#totalClassificacoesMensal-legenda").empty();
            $("#totalClassificacoesMensal-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart = new Chart(document.getElementById("totalClassificacoesMensal-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao1Mensal() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();
    $.ajax({
        url: '/Lojas/RetornarClsVenAllMeses',
        data: JSON.stringify({ 'ano': ano, 'cls': 1, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            $('#spanClassificacao1Mensal').replaceWith('Classificação: ' + dados[0].NomeClassificação1 + ' - Vendas Mensal');
            $('#spanClassificacao2Mensal').replaceWith('Classificação: ' + dados[0].NomeClassificação2 + ' - Vendas Mensal');
            $('#spanClassificacao3Mensal').replaceWith('Classificação: ' + dados[0].NomeClassificação3 + ' - Vendas Mensal');
            $('#spanClassificacao4Mensal').replaceWith('Classificação: ' + dados[0].NomeClassificação4 + ' - Vendas Mensal');
            $('#spanClassificacao5Mensal').replaceWith('Classificação: ' + dados[0].NomeClassificação5 + ' - Vendas Mensal');
            $('#spanClassificacao6Mensal').replaceWith('Classificação: ' + dados[0].NomeClassificação6 + ' - Vendas Mensal');
            $('#spanClassificacao7Mensal').replaceWith('Classificação: ' + dados[0].NomeClassificação7 + ' - Vendas Mensal');
            $('#spanClassificacao8Mensal').replaceWith('Classificação: ' + dados[0].NomeClassificação8 + ' - Vendas Mensal');

            $('#spanClassificacao1').replaceWith('Classificação: ' + dados[0].NomeClassificação1 + ' - Vendas Diária');
            $('#spanClassificacao2').replaceWith('Classificação: ' + dados[0].NomeClassificação2 + ' - Vendas Diária');
            $('#spanClassificacao3').replaceWith('Classificação: ' + dados[0].NomeClassificação3 + ' - Vendas Diária');
            $('#spanClassificacao4').replaceWith('Classificação: ' + dados[0].NomeClassificação4 + ' - Vendas Diária');
            $('#spanClassificacao5').replaceWith('Classificação: ' + dados[0].NomeClassificação5 + ' - Vendas Diária');
            $('#spanClassificacao6').replaceWith('Classificação: ' + dados[0].NomeClassificação6 + ' - Vendas Diária');
            $('#spanClassificacao7').replaceWith('Classificação: ' + dados[0].NomeClassificação7 + ' - Vendas Diária');
            $('#spanClassificacao8').replaceWith('Classificação: ' + dados[0].NomeClassificação8 + ' - Vendas Diária');

            if (myChart7) {
                myChart7.destroy();
            }

            $("#Classificacao1-legenda").empty();
            $("#Classificacao1-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart7 = new Chart(document.getElementById("Classificacao1-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao2Mensal() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarClsVenAllMeses',
        data: JSON.stringify({ 'ano': ano, 'cls': 2, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart8) {
                myChart8.destroy();
            }

            $("#Classificacao2-legenda").empty();
            $("#Classificacao2-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart8 = new Chart(document.getElementById("Classificacao2-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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
        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao3Mensal() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarClsVenAllMeses',
        data: JSON.stringify({ 'ano': ano, 'cls': 3, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart9) {
                myChart9.destroy();
            }

            $("#Classificacao3-legenda").empty();
            $("#Classificacao3-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart9 = new Chart(document.getElementById("Classificacao3-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao4Mensal() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarClsVenAllMeses',
        data: JSON.stringify({ 'ano': ano, 'cls': 4, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart10) {
                myChart10.destroy();
            }

            $("#Classificacao4-legenda").empty();
            $("#Classificacao4-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart10 = new Chart(document.getElementById("Classificacao4-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao5Mensal() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarClsVenAllMeses',
        data: JSON.stringify({ 'ano': ano, 'cls': 5, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart11) {
                myChart11.destroy();
            }

            $("#Classificacao5-legenda").empty();
            $("#Classificacao5-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart11 = new Chart(document.getElementById("Classificacao5-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao6Mensal() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarClsVenAllMeses',
        data: JSON.stringify({ 'ano': ano, 'cls': 6, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            if (myChart12) {
                myChart12.destroy();
            }

            $("#Classificacao6-legenda").empty();
            $("#Classificacao6-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart12 = new Chart(document.getElementById("Classificacao6-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao7Mensal() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarClsVenAllMeses',
        data: JSON.stringify({ 'ano': ano, 'cls': 7, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart13) {
                myChart13.destroy();
            }


            $("#Classificacao7-legenda").empty();
            $("#Classificacao7-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart13 = new Chart(document.getElementById("Classificacao7-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao8Mensal() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();

    $.ajax({
        url: '/Lojas/RetornarClsVenAllMeses',
        data: JSON.stringify({ 'ano': ano, 'cls': 8, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {

            if (myChart14) {
                myChart14.destroy();
            }

            $("#Classificacao8-legenda").empty();
            $("#Classificacao8-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas - " + ano + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Vendas - " + ($("#ano").val() - 1));

            myChart14 = new Chart(document.getElementById("Classificacao8-chart"), {
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
                                    return year + ' - R$ ' + (tooltipItem.yLabel).toLocaleString('pt-BR', {
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

        },
        error: function (xhr, textStatus, errorThrown) {
            //TratarErro(xhr, textStatus, errorThrown, "Erro ao gravar o produto.");
        }
    });
}

function Classificacao1Diario() {
    var mes = $('#mes').val();

    var ano = $('#ano').val();

    var loja = $('#loja').val();


    $.ajax({
        url: '/Lojas/RetornarClsVenAllDias',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'cls': 1, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {




            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }

            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }

            if (myChart15) {
                myChart15.destroy();
            }

            $("#Classificacao1Diario-legenda").empty();
            $("#Classificacao1Diario-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");

            myChart15 = new Chart(document.getElementById("Classificacao1Diario-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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

function Classificacao2Diario() {
    var mes = $('#mes').val();

    var ano = $('#ano').val();

    var loja = $('#loja').val();


    $.ajax({
        url: '/Lojas/RetornarClsVenAllDias',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'cls': 2, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {



            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }

            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }

            if (myChart16) {
                myChart16.destroy();
            }

            $("#Classificacao2Diario-legenda").empty();
            $("#Classificacao2Diario-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");

            myChart16 = new Chart(document.getElementById("Classificacao2Diario-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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

function Classificacao3Diario() {
    var mes = $('#mes').val();

    var ano = $('#ano').val();

    var loja = $('#loja').val();


    $.ajax({
        url: '/Lojas/RetornarClsVenAllDias',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'cls': 3, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {



            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }

            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }

            if (myChart17) {
                myChart17.destroy();
            }

            $("#Classificacao3Diario-legenda").empty();
            $("#Classificacao3Diario-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");

            myChart17 = new Chart(document.getElementById("Classificacao3Diario-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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

function Classificacao4Diario() {
    var mes = $('#mes').val();

    var ano = $('#ano').val();

    var loja = $('#loja').val();


    $.ajax({
        url: '/Lojas/RetornarClsVenAllDias',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'cls': 4, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {



            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }


            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }


            if (myChart18) {
                myChart18.destroy();
            }

            $("#Classificacao4Diario-legenda").empty();
            $("#Classificacao4Diario-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");

            myChart18 = new Chart(document.getElementById("Classificacao4Diario-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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

function Classificacao5Diario() {
    var mes = $('#mes').val();

    var ano = $('#ano').val();

    var loja = $('#loja').val();


    $.ajax({
        url: '/Lojas/RetornarClsVenAllDias',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'cls': 5, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {



            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }


            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }


            if (myChart19) {
                myChart19.destroy();
            }

            $("#Classificacao5Diario-legenda").empty();
            $("#Classificacao5Diario-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");


            myChart19 = new Chart(document.getElementById("Classificacao5Diario-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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

function Classificacao6Diario() {
    var mes = $('#mes').val();

    var ano = $('#ano').val();

    var loja = $('#loja').val();


    $.ajax({
        url: '/Lojas/RetornarClsVenAllDias',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'cls': 6, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {


            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }



            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }


            if (myChart20) {
                myChart20.destroy();
            }

            $("#Classificacao6Diario-legenda").empty();
            $("#Classificacao6Diario-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");

            myChart20 = new Chart(document.getElementById("Classificacao6Diario-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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

function Classificacao7Diario() {
    var mes = $('#mes').val();

    var ano = $('#ano').val();

    var loja = $('#loja').val();


    $.ajax({
        url: '/Lojas/RetornarClsVenAllDias',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'cls': 7, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {


            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }


            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }

            if (myChart21) {
                myChart21.destroy();
            }

            $("#Classificacao7Diario-legenda").empty();
            $("#Classificacao7Diario-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");

            myChart21 = new Chart(document.getElementById("Classificacao7Diario-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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

function Classificacao8Diario() {
    var mes = $('#mes').val();
    var ano = $('#ano').val();
    var loja = $('#loja').val();
    $.ajax({
        url: '/Lojas/RetornarClsVenAllDias',
        data: JSON.stringify({ 'mes': mes, 'ano': ano, 'cls': 8, 'loja': loja }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {


            var chartDataDia = [];
            for (var i = 0; i < dados.length; i++) {
                var dataDia = dados[i].Dia;
                chartDataDia.push(dataDia);
            }


            var chartDataClassificacao1 = [];
            for (var i = 0; i < dados.length; i++) {
                var dataClassificacao1 = dados[i].Classificação1;
                chartDataClassificacao1.push(dataClassificacao1);
            }

            var chartDataMeta = [];
            for (var i = 0; i < dados.length; i++) {
                var dataMeta = dados[i].Meta;
                chartDataMeta.push(dataMeta);
            }

            var chartDataPercentual = [];
            for (var i = 0; i < dados.length; i++) {
                var dataPercentual = dados[i].Percentual;
                chartDataPercentual.push(dataPercentual);
            }

            if (myChart22) {
                myChart22.destroy();
            }

            $("#Classificacao8Diario-legenda").empty();
            $("#Classificacao8Diario-legenda").append("<i class='fa fa-bar-chart' style='color: #722F37'></i>&nbsp;Vendas Dia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #2FFF00'></i>&nbsp;Média Dia – " + getMonthName($('#mes').val()) + "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class='fa fa-line-chart' style='color: #3e95cd'></i>&nbsp;Média Dia – " + getLastMonthName($('#mes').val()) + "");

            myChart22 = new Chart(document.getElementById("Classificacao8Diario-chart"), {
                type: 'bar',
                data: {
                    labels: chartDataDia,
                    datasets: [
                        {
                            label: "Média Dia – " + getMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#2FFF00",
                            backgroundColor: "#2FFF00",
                            data: chartDataPercentual,
                            fill: false
                        },
                        {
                            label: "Média Dia – " + getLastMonthName($('#mes').val()) + ': ',
                            type: "line",
                            borderColor: "#3e95cd",
                            backgroundColor: "#3e95cd",
                            data: chartDataMeta,
                            fill: false
                        }, {
                            label: "Vendas Dia - " + getMonthName($('#mes').val()) + ': ',
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: chartDataClassificacao1
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
            buttonStop(document.getElementById('vendas-mensal-btn'));
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
                label: "Vendas  " + ($("#ano").val() - 1) + "",
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
                label: "Vendas  " + $('#ano').val() + "",
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
                label: "Vendas  " + ($("#ano").val() - 1) + "",
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
                label: "Vendas  " + $('#ano').val() + "",
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
    TotalClassificacoesDiario();
    TotalClassificacoesMensal();
    Classificacao1Mensal();
    Classificacao2Mensal();
    Classificacao3Mensal();
    Classificacao4Mensal();
    Classificacao5Mensal();
    Classificacao6Mensal();
    Classificacao7Mensal();
    Classificacao8Mensal();
    Classificacao1Diario();
    Classificacao2Diario();
    Classificacao3Diario();
    Classificacao4Diario();
    Classificacao5Diario();
    Classificacao6Diario();
    Classificacao7Diario();
    Classificacao8Diario();
}