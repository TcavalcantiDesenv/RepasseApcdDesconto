function loadClientes() {
    $("#menucliente").attr("style", "display:block");
    $("#escolherCliente").text('Alterar Cliente');
}
function buttonLoading(element) {
    var l = Ladda.create(element);
    l.start();
}
function buttonStop(element) {
    var l = Ladda.create(element);
    l.stop();
}
function showChangeClientModal() {
    listCompany();
    if ($("#client-modal").length) {
        $("#client-modal").remove();
    }
    $("#modal-section").append(
        '<div class="modal fade" id="client-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">\
            <div class="modal-dialog modal-danger" role="document">\
                <div class="modal-content">\
                    <div class="modal-header">\
                        <h4 class="modal-title">Lista de Clientes</h4>\
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">\
                            <span aria-hidden="true">&times;</span>\
                        </button>\
                    </div>\
                    <div class="card col-md-12">\
                        <div class="modal-body">\
                           <div class="col-sm-12 d-md-block">\
                              <button type="button" class="btn btn-primary float-right d-md-block ladda-button" id="indice-gerenciais-btn" onclick="changeCompany(this)">\
                                    <i class="icon-magnifier"></i> Buscar\
                              </button>\
                              <div class="col-md-7" style ="display: inline-block">\
                                    <select class="form-control" id="selCompany">\
                                          <option value="0">-- Selecionar Cliente --</option>\
                                    </select>\
                              </div>\
                           </div>\
                        </div>\
                    </div>\
                    <div class="modal-footer">\
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="logoutClient()">Sair</button>\
                    </div >\
                </div>\
            </div>\
        </div>');
    $("#client-modal").modal('show');
}

function logoutClient() {

    var _url = '/AcessarClientes/logoutClient';
    $.ajax({
        type: "GET",
        url: _url,
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#menucliente").attr("style", "display:none");
            window.location.href = '/Home';
        },
        error: function () {
            alert("Falha no cancelamento da solicitação.");
        }
    });

}
function listCompany() {

    var _url = '/AcessarClientes/ListarCompanias';
    $.ajax({
        type: "GET",
        url: _url,
        async:true,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#selCompany').empty().append('<option selected="selected" value="0">--  Selecionar Cliente  --</option>');
            $.each(data, function (index, value) {
                $('#selCompany').append('<option value="' + data[index].CompanyId + '">' + data[index].Name + '</option>');
            });
        },
        error: function () {
            alert("Falha para carregar os Clientes.");
        }
    });
}
function changeCompany(element) {

    buttonLoading(element);

    var id = $('#selCompany option:selected').val()
    var nome = $('#selCompany option:selected').text();
    var _url = '/AcessarClientes/AlterarCompania';

    $.ajax({
        type: "GET",
        url: _url,
        async: true,
        data: { CompanyId: id, CompanyName: nome },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data) {
                buttonStop(element);
                if ($("#client-modal").length) {
                    $("#client-modal").modal("hide");
                    $("#menucliente").attr("style", "display:block");
                }
                window.location.href = '/Dashboard';
            }
        },
        error: function () {
            alert("Falha para alterar os Clientes.");
        }
    });
}
