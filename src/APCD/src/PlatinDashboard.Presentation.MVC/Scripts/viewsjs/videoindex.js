function initializeVideoTable() {
    $('#videosTable').DataTable();
    $('#videosTable').attr('style', 'border-collapse: collapse !important');
}

function showDeleteModal(element, videoId) {
    buttonLoading(element);
    //Buscando modal de remoção de usuário
    $.when(
        $.get("/Midias/ModalRemover", { id: videoId })
            .done(function (data) {
                //Verificando se já há um modal criado
                if ($("#delete-video-modal").length) {
                    $("#delete-video-modal").remove();
                }
                //Criando Modal de exclusão
                $("#modal-section").append(data);
                $("#delete-video-modal").modal('show');
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

function deleteVideoCallBack(data) {
    if (data.deleted) {
        //Removendo elemento do array datatabless
        $('#videosTable').DataTable().row("#video-row-" + data.videoId).remove().draw();
        //Removendo modal de remoção
        $("#delete-video-modal").modal("hide").on("hidden.bs.modal", function () {
            $("#delete-video-modal").remove();
        });
    }
}

function deleteVideoFailed() {
    //caso houver falha esconder modal de exclução e mostrar o de erro
    $("#delete-video-modal").modal("hide").on("hidden.bs.modal", function () {
        $("#delete-video-modal").remove();
    });
    showErrorModal();
}

function editVideoCallBack(data) {
    $('#form-content').replaceWith(data.view);
    if (data.updated) {
        showMessage('As informações do vídeo foram atualizadas com sucesso.', 'success');
    }
}

function editVideoFailed() {
    showMessage('Houve uma falha ao tentar atualizar os dados do vídeo, por favor tente novamente.', 'danger');
}