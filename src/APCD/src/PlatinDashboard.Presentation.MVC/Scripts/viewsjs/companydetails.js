function initializeUserTable() {
    $('#usersTable').DataTable();
    $('#usersTable').attr('style', 'border-collapse: collapse !important');
}

function editCompanyCallBack(data) {
    $('#form-content').replaceWith(data.view);
    if (data.updated) {
        showMessage('As informações do cliente foram atualizadas com sucesso.', 'success');
    }
}

function editCompanyFailed() {
    showMessage('Dados do cliente atualizado com sucesso!', 'success');
}

function showDeleteModal(element, userId) {
    buttonLoading(element);
    //Buscando modal de remoção de usuário
    $.when(
        $.get("/UsuariosEmpresa/Remover", { id: userId })
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

function changeAccessChartCallBack(data) {
    $('#access-charts-content').replaceWith(data.view);
    if (data.updated) {
        showNotification('As configurações de acesso aos gráficos da empresa foram atualizadas com sucesso.', 'success', 'access-charts-btn', 'access-charts-alert');
    }
}
function changeAccessChartFailed() {
    showNotification('Cadastro de cliente atualizado com sucesso!', 'success', 'access-charts-btn', 'access-charts-alert');
}
