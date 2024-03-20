document.getElementById("salvar_registro").addEventListener("click", function () {
    AtualizarRegistro();
});


async function AtualizarRegistro() {

    let password = document.getElementById('txtSenha').value;
    let password2 = document.getElementById('txtSenha2').value;

    if (password != password2) { toastr['error']("As senhas não conferem!", TITULO_TOASTR_ERRO); return; }
    if (password.length <= 5) { toastr['error']("Preencha ao menos 6 caracteres!", TITULO_TOASTR_ERRO); return; }


    document.getElementById('salvar_registro').disabled = true;
    const dto = {
        Id: "",
        Password: password
    }
    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dto)
    }
    const response = await fetch("Password/Alterar", config)
    const data = await response.json();

    if (response.status == 200) {
        toastr['success'](MSG_ATUALIZADO, TITULO_TOASTR_SUCESSO);
        document.getElementById('txtSenha2').value = "";
        document.getElementById('txtSenha').value = "";
    } else {
        toastr['error'](MSG_ERRO_ATUALIZAR, TITULO_TOASTR_ERRO);
    }


    document.getElementById('salvar_registro').disabled = false;

}
