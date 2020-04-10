var ranking;

function Ranking() {
    var ano = $('#ano').val();
    var mes = $('#mes').val();
    var loja = $('#loja').val();
    $.ajax({
        url: '/Ranking/Balconistas',
        data: JSON.stringify({ 'ano': ano, 'mes': mes, 'loja': loja }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            if (ranking) {
                ranking.destroy();
            }
            var labels = [];
            $(dados).each(function (index, element) {
                labels.push([formatName(element.Nome), 'R$ ' + element.ValorLiquido.toLocaleString('pt-BR', {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                })]);
            });
            var values = [];
            $(dados).each(function (index, element) {
                values.push(element.ValorLiquido);
            });
            ranking = new Chart(document.getElementById("ranking-chart"), {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [
                        {
                            label: "Valor Líquido",
                            type: "bar",
                            backgroundColor: "#722F37",
                            data: values,
                            fill: false
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
                            ticks: {
                                callback: function (label, index, labels) {
                                    console.log(label);
                                    return label;
                                }
                            }
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
                            title: function (tooltipItems, data) {
                                console.log(tooltipItems);
                                return tooltipItems[0].xLabel[0];
                            },
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

function carregarGraficos(element) {
    buttonLoading(element);
    Ranking();
}

function formatName(fullName) {
    console.log(fullName);
    var exceptions = ['de', 'da', 'do', 'dos', 'das', 'de'];
    var names = fullName.split(' ');
    console.log(names);
    var name = names[0];
    if (typeof names[1] != 'undefined') {
        name += ' ' + names[1]
    }
    if (typeof names[2] != 'undefined' &&!(exceptions.indexOf(names[2]) > -1)) {
        console.log('name 2 ' + names[2]);
        name += ' ' + names[2]
    }
    return name;
}