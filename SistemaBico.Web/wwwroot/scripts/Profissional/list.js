$(window).on('load', function () {
   
    abrirLoaderCarregamento();
})

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

async function Interesse(perfil) {

    document.getElementById(perfil).disabled = true
    document.getElementById('spinner_' + perfil).classList.add('spinner');
    document.getElementById('spinner_' + perfil).classList.add('spinner-primary');
    document.getElementById('spinner_' + perfil).classList.add('spinner-left');

    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(perfil)
    }
    const response = await fetch("Professional/ApplyProfessional", config)

    document.getElementById(perfil).disabled = true;
    document.getElementById('spinner_' + perfil).classList.remove('spinner');
    document.getElementById('spinner_' + perfil).classList.remove('spinner-primary');
    document.getElementById('spinner_' + perfil).classList.remove('spinner-left');


    location.href = "/client/GetMyProfessional";
}
