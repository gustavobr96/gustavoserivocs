$(window).on('load', function () {
    abrirLoaderCarregamento();
})

function getLocation() {

    var spanLoc = document.getElementById("spanLoc");

    if (!('geolocation' in navigator)) {
        alert("Navegador não tem suporte API Geolocation");
    }

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    }
    else { spanLoc.innerHTML = "Geolocalização não é suportada nesse browser."; }
}

function showPosition(position) {
    var spanLoc = document.getElementById("spanLoc");
    Address(position.coords.latitude, position.coords.longitude);

    //spanLoc.innerHTML = "Latitude: " + position.coords.latitude +
    //    "<br>Longitude: " + position.coords.longitude;
}

async function Address(lat, long) {
    const response = await fetch('https://nominatim.openstreetmap.org/reverse?format=json&lat=' + lat + '&lon=' + long);
    const data = await response.json();

    document.getElementById("CepCity").value = data.address.city_district;
}

function showError(error) {
    var spanLoc = document.getElementById("spanLoc");

    switch (error.code) {
        case error.PERMISSION_DENIED:
            spanLoc.innerHTML = "Usuário rejeitou a solicitação de Geolocalização."
            break;
        case error.POSITION_UNAVAILABLE:
            spanLoc.innerHTML = "Localização indisponível."
            break;
        case error.TIMEOUT:
            spanLoc.innerHTML = "O tempo da requisição expirou."
            break;
        case error.UNKNOWN_ERROR:
            spanLoc.innerHTML = "Algum erro desconhecido aconteceu."
            break;
    }
}

function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('CepCity').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('CepCity').value = (conteudo.localidade);
    } //end if.
}

function pesquisacep(valor) {

    //Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, '');

    //Expressão regular para validar o CEP.
    var validacep = /^[0-9]{8}$/;

    //Valida o formato do CEP.
    if (validacep.test(cep)) {
        //Cria um elemento javascript.
        var script = document.createElement('script');

        //Sincroniza com o callback.
        script.src = 'https://viacep.com.br/ws/' + cep + '/json/?callback=meu_callback';

        //Insere script no documento e carrega o conteúdo.
        document.body.appendChild(script);

    } //end if.

};

async function Interesse(id) {

    document.getElementById(id).disabled = true
    document.getElementById('spinner_' + id).classList.add('spinner');
    document.getElementById('spinner_' + id).classList.add('spinner-primary');
    document.getElementById('spinner_' + id).classList.add('spinner-left');

    let dto = {
        Guid : id
    }

    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dto)
    }
    const response = await fetch("Worker/ApplyWorker", config)
    const data = await response.json();


    document.getElementById('spinner_' + id).classList.remove('spinner');
    document.getElementById('spinner_' + id).classList.remove('spinner-primary');

    document.getElementById('spinner_' + id).classList.remove('spinner-left');
    document.getElementById('whatsApp').innerHTML = data.phone;
    document.getElementById(id).disabled = true;
    document.getElementById(id).innerHTML = "Enviado";

    jQuery('#modalContato').modal({
        backdrop: 'static',
        keyboard: false
    });
}
