var myChart23;
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

function StackedBarBalconistaAnual() {
    var ano = $('#ano').val();
    var loja = $('#loja').val();
    var balconista = $('#balconista').val();

    $.ajax({
        url: '/Vendas/AnualBalconistas',
        data: JSON.stringify({ 'idloja': loja, 'ano': ano, 'idBalconista': balconista }),
        headers: {
            'RequestVerificationToken': $('#antiForgeryToken').val(),
            'X-Requested-With': 'XMLHttpRequest'
        },
        type: "POST",
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        success: function (dados) {
            if (myChart23) {
                myChart23.destroy();
            }
            var ctx = document.getElementById("balconistaAnual-chart");
            myChart23 = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ["Janeiro", "Fevereiro", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
                    datasets: [
                        {
                            label: dados[0].NomeClassificação1,
                            data: [dados[0].MediaJaneiroClassificacao1, dados[0].MediaFevereiroClassificacao1, dados[0].MediaMarçoClassificacao1, dados[0].MediaAbrilClassificacao1, dados[0].MediaMaioClassificacao1, dados[0].MediaJunhoClassificacao1, dados[0].MediaJulhoClassificacao1, dados[0].MediaAgostoClassificacao1, dados[0].MediaSetembroClassificacao1, dados[0].MediaOutubroClassificacao1, dados[0].MediaNovembroClassificacao1, dados[0].MediaDezembroClassificacao1],
                            backgroundColor: '#b3b3b3',
                            borderWidth: 2
                        },
                        {
                            label: dados[0].NomeClassificação2,
                            data: [dados[0].MediaJaneiroClassificacao2, dados[0].MediaFevereiroClassificacao2, dados[0].MediaMarçoClassificacao2, dados[0].MediaAbrilClassificacao2, dados[0].MediaMaioClassificacao2, dados[0].MediaJunhoClassificacao2, dados[0].MediaJulhoClassificacao2, dados[0].MediaAgostoClassificacao2, dados[0].MediaSetembroClassificacao2, dados[0].MediaOutubroClassificacao2, dados[0].MediaNovembroClassificacao2, dados[0].MediaDezembroClassificacao2],
                            backgroundColor: '#ff6666',
                            borderWidth: 2
                        },
                        {
                            label: dados[0].NomeClassificação3,
                            data: [dados[0].MediaJaneiroClassificacao3, dados[0].MediaFevereiroClassificacao3, dados[0].MediaMarçoClassificacao3, dados[0].MediaAbrilClassificacao3, dados[0].MediaMaioClassificacao3, dados[0].MediaJunhoClassificacao3, dados[0].MediaJulhoClassificacao3, dados[0].MediaAgostoClassificacao3, dados[0].MediaSetembroClassificacao3, dados[0].MediaOutubroClassificacao3, dados[0].MediaNovembroClassificacao3, dados[0].MediaDezembroClassificacao3],
                            backgroundColor: '#00ff00',
                            borderWidth: 2
                        },
                        {
                            label: dados[0].NomeClassificação4,
                            data: [dados[0].MediaJaneiroClassificacao4, dados[0].MediaFevereiroClassificacao4, dados[0].MediaMarçoClassificacao4, dados[0].MediaAbrilClassificacao4, dados[0].MediaMaioClassificacao4, dados[0].MediaJunhoClassificacao4, dados[0].MediaJulhoClassificacao4, dados[0].MediaAgostoClassificacao4, dados[0].MediaSetembroClassificacao4, dados[0].MediaOutubroClassificacao4, dados[0].MediaNovembroClassificacao4, dados[0].MediaDezembroClassificacao4],
                            backgroundColor: '#ff9933',
                            borderWidth: 2
                        },
                        {
                            label: dados[0].NomeClassificação5,
                            data: [dados[0].MediaJaneiroClassificacao5, dados[0].MediaFevereiroClassificacao5, dados[0].MediaMarçoClassificacao5, dados[0].MediaAbrilClassificacao5, dados[0].MediaMaioClassificacao5, dados[0].MediaJunhoClassificacao5, dados[0].MediaJulhoClassificacao5, dados[0].MediaAgostoClassificacao5, dados[0].MediaSetembroClassificacao5, dados[0].MediaOutubroClassificacao5, dados[0].MediaNovembroClassificacao5, dados[0].MediaDezembroClassificacao5],
                            backgroundColor: '#66ffcc',
                            borderWidth: 2
                        },
                        {
                            label: dados[0].NomeClassificação6,
                            data: [dados[0].MediaJaneiroClassificacao6, dados[0].MediaFevereiroClassificacao6, dados[0].MediaMarçoClassificacao6, dados[0].MediaAbrilClassificacao6, dados[0].MediaMaioClassificacao6, dados[0].MediaJunhoClassificacao6, dados[0].MediaJulhoClassificacao6, dados[0].MediaAgostoClassificacao6, dados[0].MediaSetembroClassificacao6, dados[0].MediaOutubroClassificacao6, dados[0].MediaNovembroClassificacao6, dados[0].MediaDezembroClassificacao6],
                            backgroundColor: '#ff0066',
                            borderWidth: 2
                        },
                        {
                            label: dados[0].NomeClassificação7,
                            data: [dados[0].MediaJaneiroClassificacao7, dados[0].MediaFevereiroClassificacao7, dados[0].MediaMarçoClassificacao7, dados[0].MediaAbrilClassificacao7, dados[0].MediaMaioClassificacao7, dados[0].MediaJunhoClassificacao7, dados[0].MediaJulhoClassificacao7, dados[0].MediaAgostoClassificacao7, dados[0].MediaSetembroClassificacao7, dados[0].MediaOutubroClassificacao7, dados[0].MediaNovembroClassificacao7, dados[0].MediaDezembroClassificacao7],
                            backgroundColor: '#33cc33',
                            borderWidth: 2
                        },
                        {
                            label: dados[0].NomeClassificação8,
                            data: [dados[0].MediaJaneiroClassificacao8, dados[0].MediaFevereiroClassificacao8, dados[0].MediaMarçoClassificacao8, dados[0].MediaAbrilClassificacao8, dados[0].MediaMaioClassificacao8, dados[0].MediaJunhoClassificacao8, dados[0].MediaJulhoClassificacao8, dados[0].MediaAgostoClassificacao8, dados[0].MediaSetembroClassificacao8, dados[0].MediaOutubroClassificacao8, dados[0].MediaNovembroClassificacao8, dados[0].MediaDezembroClassificacao8],
                            backgroundColor: '#ffff00',
                            borderWidth: 2
                        }


                    ]
                },
                options: {
                    scales: {
                        yAxes: [{
                            stacked: true,
                            ticks: {
                                beginAtZero: true
                            }
                        }],
                        xAxes: [{
                            stacked: true,
                            ticks: {
                                beginAtZero: true
                            }
                        }]

                    }
                }
            });
            buttonStop(document.getElementById('vendas-anual-btn'));
        },
        error: function (xhr, textStatus, errorThrown) {
            
        }
    });
}

function carregarGraficoVendaAnual(element) {
    buttonLoading(element);
    StackedBarBalconistaAnual();
}