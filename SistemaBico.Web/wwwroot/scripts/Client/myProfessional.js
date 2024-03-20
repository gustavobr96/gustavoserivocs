document.addEventListener("DOMContentLoaded", function () {
    //document.location.reload(true);
    PopularMyProfessional();
});

$(window).on('load', function () {
    abrirLoaderCarregamento();
})

function Remover(id) {

    document.getElementById(`spinnerRemover_${id}`).classList.add('spinner');
    document.getElementById(`spinnerRemover_${id}`).classList.add('spinner-primary');
    document.getElementById(`spinnerRemover_${id}`).classList.add('spinner-left');

    jQuery('#modalConfirmarExclusao').modal({
        backdrop: 'static',
        keyboard: false
    }).one('click', '#delete_registro', async function (e) {

        const config = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(id)
        }
        document.getElementById(`Contratar_${id}`).disabled = true;
        document.getElementById(`Remover_${id}`).disabled = true;
        jQuery('#modalConfirmarExclusao').modal('hide');
        const response = await fetch("Client/CancelApplyProfessional", config)
        document.getElementById(`spinnerRemover_${id}`).classList.remove('spinner');
        document.getElementById(`spinnerRemover_${id}`).classList.remove('spinner-primary');
        document.getElementById(`spinnerRemover_${id}`).classList.remove('spinner-left');
    });
}

async function PopularMyProfessional() {


    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
         body: JSON.stringify()
       }

    const response = await fetch("Worker/GetMyWorkersClientIdBasic", config)
    const data = await response.json();

     let options = "<option value='0'>Outro</option>";
     data.map(dado => {
            options += `<option value=${dado.id}>${dado.titulo}</option>`;
     });

    document.getElementById('SelWorker').innerHTML = options;
}

$("#SelWorker").change(function () {

    var selWorker = document.getElementById('SelWorker').value;
    if (selWorker != 0) {
        $("#Description").prop('disabled', true);
    }
    else {
        $("#Description").prop('disabled', false);
    }
});

async function starPrazo(id) {
    document.getElementById('StarPrazo').value = id;
    await paintStar(id, 'starPrazo')
}

async function starQualidade(id) {
    document.getElementById('StarQualidade').value = id;
    await paintStar(id, 'starQualidade')
}

async function starComunicacao(id) {
    document.getElementById('StarComunicacao').value = id;
    await paintStar(id,'starComunicacao')
}

async function paintStar(id, name) {
    var all = document.querySelectorAll(`.${name}`);

    for (let elem of all) {
         elem.classList.remove('ativo');
    }

    all[id-1].classList.add('ativo');
}

async function LimparCampos() {
    document.getElementById('SelWorker').value = 0;
    document.getElementById('Sobre').value = "";
    document.getElementById('Description').value = "";
}

async function EnviarAvaliation() {

    document.getElementById('enviar_registro').disabled = true;

    let perfil = document.getElementById('PerfilProfissional').value;
    let starPrazo = document.getElementById('StarPrazo').value == "" ? 1 : document.getElementById('StarPrazo').value;
    let starQualidade = document.getElementById('StarQualidade').value == "" ? 1 : document.getElementById('StarQualidade').value;
    let starComunicacao = document.getElementById('StarComunicacao').value == "" ? 1 : document.getElementById('StarComunicacao').value;
    let selWorker = document.getElementById('SelWorker').value;
    let Description = document.getElementById('Description').value;
    let Sobre = document.getElementById('Sobre').value;

    var erro = "";
    if (perfil == "")
        erro = "Ocorreu um erro, atualiza a página.<br/>";


    if (erro == "") {

        document.getElementById(`spinnerEnviarAvaliacao`).classList.add('spinner');
        document.getElementById(`spinnerEnviarAvaliacao`).classList.add('spinner-primary');
        document.getElementById(`spinnerEnviarAvaliacao`).classList.add('spinner-left');

        const dto = {
            PrestadorPerfil: perfil,
            AvaliationDeadline: starPrazo,
            AvaliationQuality: starQualidade,
            AvaliationCommunication: starComunicacao,
            WorkerIdString: selWorker,
            Description: Description,
            Comment: Sobre
        };

        const config = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(dto)
        }

        const response = await fetch("Worker/WorkerDone", config)
        const data = await response.json();
        jQuery('#modalConfirmeWorker').modal('hide');
        LimparCampos();
        toastr.success("Avaliação feita com sucesso!");
        document.getElementById(`spinnerEnviarAvaliacao`).classList.remove('spinner');
        document.getElementById(`spinnerEnviarAvaliacao`).classList.remove('spinner-primary');
        document.getElementById(`spinnerEnviarAvaliacao`).classList.remove('spinner-left');

   

        document.getElementById(`Finalizado_${perfil}`).disabled = true;
    }
    else {
        toastr.warning(erro, "Preencha os campos obrigatórios!");
        document.getElementById('enviar_registro').disabled = false;
      
    }
}

document.getElementById("enviar_registro").addEventListener("click", function () {
    EnviarAvaliation();
});

async function Finalizar(id) {

    document.getElementById('PerfilProfissional').value = id;

    jQuery('#modalConfirmeWorker').modal({
        backdrop: 'static',
        keyboard: false
    });
}

async function Contratar(id) {

    document.getElementById(`Contratar_${id}`).disabled = true;
    document.getElementById(`spinner_${id}`).classList.add('spinner');
    document.getElementById(`spinner_${id}`).classList.add('spinner-primary');
    document.getElementById(`spinner_${id}`).classList.add('spinner-left');

    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(id)
    }

    const response = await fetch("Client/UpdateContratadoProfessional ", config)
    const data = await response.json();

    if (data == '200') {
        document.location.reload(true);
    }
    else {
        toastr.warning("Ocorreu um erro ao contratar profissional, atualize a página e tente novamente.");
    }
}









