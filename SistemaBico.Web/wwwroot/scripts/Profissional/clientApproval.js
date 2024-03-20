async function Aceitar(id) {

    document.getElementById('Aceitar_'+id).disabled = true
    document.getElementById('spinner_' + id).classList.add('spinner');
    document.getElementById('spinner_' + id).classList.add('spinner-primary');
    document.getElementById('spinner_' + id).classList.add('spinner-left');

    let dto = {
        ProfessionalClientId: id,
        Aceitar : true
    };

    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dto)
    }
    const response = await fetch("Professional/ClientApprovalOrRecused", config)
    document.location.reload(true);
}

async function Recusar(id) {

    document.getElementById('Recusar_'+id).disabled = true
    document.getElementById('spinnerRemover_' + id).classList.add('spinner');
    document.getElementById('spinnerRemover_' + id).classList.add('spinner-primary');
    document.getElementById('spinnerRemover_' + id).classList.add('spinner-left');

    let dto = {
        ProfessionalClientId: id,
        Aceitar: false
    };

    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dto)
    }
    const response = await fetch("Professional/ClientApprovalOrRecused", config)
    document.location.reload(true);
}
