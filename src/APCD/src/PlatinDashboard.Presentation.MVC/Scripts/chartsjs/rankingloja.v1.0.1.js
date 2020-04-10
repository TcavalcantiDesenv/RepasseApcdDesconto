var ranking;

function Ranking() {
    var ano = $('#ano').val();
    var mes = $('#mes').val();
    $.ajax({
        url: '/Ranking/Lojas',
        data: JSON.stringify({ 'ano': ano, 'mes': mes }),
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
                labels.push([element.Nome, 'R$ ' + element.ValorLiquido.toLocaleString('pt-BR', {
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