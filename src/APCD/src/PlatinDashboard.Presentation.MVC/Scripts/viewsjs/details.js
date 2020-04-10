function buttonLoading(element) {
    var l = Ladda.create(element);
    l.start();
}

function buttonStop(element) {
    var l = Ladda.create(element);
    l.stop();
}

function submitForm(element, formId) {
    if ($('#' + formId).valid()) {
        buttonLoading(element);
        $('#' + formId).submit();
    }
}

function showMessage(message, messageType) {
    $('<div class="alert alert-' + messageType +'" id="edit-alert" style="display:none;">\
          <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>\
          <i class="fa fa-check-circle fa-fw fa-lg"></i>\
          '+ message +'\
	  </div>').insertBefore("#edit-btn");
    $("#edit-alert").show(1000);
    setTimeout(function () {
        buttonStop(document.getElementById('edit-btn'));
        $("#edit-alert").hide(1000, function () {
            $("#edit-alert").remove();            
        });
    }, 2500);
}

function showNotification(message, messageType, btnId, alertId) {
    $('<div class="alert alert-' + messageType + '" id="' + alertId + '" style="display:none;">\
          <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>\
          <i class="fa fa-check-circle fa-fw fa-lg"></i>\
          '+ message + '\
	  </div>').insertBefore("#" + btnId);
    $("#" + alertId).show(1000);
    setTimeout(function () {
        buttonStop(document.getElementById(btnId));
        $("#" + alertId).hide(1000, function () {
            $("#" + alertId).remove();
        });
    }, 2500);
}

function showErrorModal() {
    if ($("#error-modal").length) {
        $("#error-modal").remove();
    }
    $("#modal-section").append(
        '<div class="modal fade" id="error-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">\
            <div class="modal-dialog modal-danger" role="document">\
                <div class="modal-content">\
                    <div class="modal-header">\
                        <h4 class="modal-title">Falha de execução</h4>\
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">\
                            <span aria-hidden="true">&times;</span>\
                        </button>\
                    </div>\
                    <div class="modal-body">\
                        <p id="delete-company-modal-message">Houve uma falha no processamento, por favor tente novamente</p>\
                    </div>\
                    <div class="modal-footer">\
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>\
                    </div>\
                </div>\
            </div>\
        </div>');
    $("#error-modal").modal('show');
}