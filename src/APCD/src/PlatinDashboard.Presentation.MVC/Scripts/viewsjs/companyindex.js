function initializeCompanyTable() {
    $('#companiesTable').DataTable();
    $('.datatable').attr('style', 'border-collapse: collapse !important');
}

function showDeleteModal(element, companyId) {
    buttonLoading(element);
    //Buscando modal de remoção de empresa
    $.when(
    $.get("/Empresas/ModalRemover", { id: companyId })
        .done(function (data) {
            //Verificando se já há um modal criado
            if ($("#delete-company-modal").length) {
                $("#delete-company-modal").remove();
            }
            $("#modal-section").append(data);
            $("#delete-company-modal").modal('show');
        })
        .fail(function () {
            showErrorModal();
            buttonStop(element);
        })
    )
    .then(function (data, textStatus, jqXHR) {
        buttonStop(element);
    });    
}

function deleteCompanyCallBack(data) {
    if (data.deleted) {
        $('#companiesTable').DataTable().row("#company-row-" + data.companyId).remove().draw();
        $("#delete-company-modal").modal("hide").on("hidden.bs.modal", function () {
            $("#delete-company-modal").remove();
        });
    }
}

function deleteCompanyFailed() {
    $("#delete-company-modal").modal("hide").on("hidden.bs.modal", function () {
        $("#delete-company-modal").remove();
    });
    showErrorModal();
}