function editUserCallBack(data) {
    $('#form-content').replaceWith(data.view);    
    changeFormStructure();
    $('.select2-element').select2({ placeholder: "Lojas", allowClear: true });
    if (data.updated) {
        showMessage('As informações do usuário foram atualizadas com sucesso.', 'success');
    }
    bindUserTypeChange();
}

function editUserFailed() {
    showMessage('Houve uma falha ao tentar atualizar os dados do usuário, por favor tente novamente.', 'danger');
}

function changePasswordCallBack(data) {
    $('#change-password-content').replaceWith(data.view);
    if (data.updated) {
        showNotification('A senha foi alterada com sucesso.', 'success', 'change-password-btn', 'change-password-alert');
    }
}

function changePasswordFailed() {
    showNotification('Houve uma falha ao tentar alterar a senha, por favor tente novamente.', 'danger', 'change-password-btn', 'change-password-alert');
}

function changeAccessChartCallBack(data) {
    $('#access-charts-content').replaceWith(data.view);
    if (data.updated) {
        showNotification('As configurações de acesso aos gráficos do usuário foram atualizadas com sucesso.', 'success', 'access-charts-btn', 'access-charts-alert');
    }
}

function changeAccessChartFailed() {
    showNotification('Houve uma falha ao tentar atualizar os acessos da usuário, por favor tente novamente.', 'danger', 'access-charts-btn', 'access-charts-alert');
}