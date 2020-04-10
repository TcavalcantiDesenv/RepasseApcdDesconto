function initializeUserTable() {
    $('#usersTable').DataTable();
    $('#usersTable').attr('style', 'border-collapse: collapse !important');
}

function showDeleteModal(element, userId) {
    buttonLoading(element);
    //Buscando modal de remoção de usuário
    $.when(
        $.get("/Usuarios/ModalRemover", { id: userId })
            .done(function (data) {
                //Verificando se já há um modal criado
                if ($("#delete-user-modal").length) {
                    $("#delete-user-modal").remove();
                }
                //Criando Modal de exclusão
                $("#modal-section").append(data);
                $("#delete-user-modal").modal('show');
            })
            .fail(function () {
                //Caso houver falha mostrar modal com erro
                showErrorModal();
                buttonStop(element);
            })
    )
    .then(function (data, textStatus, jqXHR) {
        //Para animação loading do botão
        buttonStop(element);
    });
}

function deleteUserCallBack(data) {
    if (data.deleted) {
        //Removendo elemento do array datatabless
        $('#usersTable').DataTable().row("#user-row-" + data.userId).remove().draw();
        //Removendo modal de remoção
        $("#delete-user-modal").modal("hide").on("hidden.bs.modal", function () {
            $("#delete-user-modal").remove();
        });
    }
}

function deleteUserFailed() {
    //caso houver falha esconder modal de exclução e mostrar o de erro
    $("#delete-user-modal").modal("hide").on("hidden.bs.modal", function () {
        $("#delete-user-modal").remove();
    });
    showErrorModal();
}