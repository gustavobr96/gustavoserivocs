/**
 * Script responsável por métodos padrões e validações
*/
document.addEventListener("DOMContentLoaded", function () {
    //$('.maskData').mask('00/00/0000');
    //$('.maskTelefone').mask('(99)999999999');
    //$('.maskTel').mask('0000-00009');
    //$('.maskDdd').mask('99');
    //$('.maskCnpj').mask('00.000.000/0000-00');
    //$('.maskCpf').mask('000.000.000-00');
    //$('.maskCep').mask('00000-000');
    //$('.maskMesAno').mask('00/0000');
});

function Alerta(divId, msg) {
    $("#" + divId).html(msg);
    $("#" + divId).show(300);
    $("#" + divId).delay(3000);
    $("#" + divId).hide(300);
};

function ValidarCPF(strCPF) {
    let Soma;
    let Resto;
    Soma = 0;
    if (strCPF == "00000000000") return false;
    if (strCPF == "11111111111") return false;
    if (strCPF == "22222222222") return false;
    if (strCPF == "33333333333") return false;
    if (strCPF == "44444444444") return false;
    if (strCPF == "55555555555") return false;
    if (strCPF == "66666666666") return false;
    if (strCPF == "77777777777") return false;
    if (strCPF == "88888888888") return false;
    if (strCPF == "99999999999") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
}

function ValidarCNPJ(cnpj) {

    cnpj = cnpj.replace(/[^\d]+/g, '');

    if (cnpj == '') return false;

    if (cnpj.length != 14)
        return false;

    // Elimina CNPJs invalidos conhecidos
    if (cnpj == "00000000000000" ||
        cnpj == "11111111111111" ||
        cnpj == "22222222222222" ||
        cnpj == "33333333333333" ||
        cnpj == "44444444444444" ||
        cnpj == "55555555555555" ||
        cnpj == "66666666666666" ||
        cnpj == "77777777777777" ||
        cnpj == "88888888888888" ||
        cnpj == "99999999999999")
        return false;

    // Valida DVs
    tamanho = cnpj.length - 2
    numeros = cnpj.substring(0, tamanho);
    digitos = cnpj.substring(tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(0))
        return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2)
            pos = 9;
    }
    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
    if (resultado != digitos.charAt(1))
        return false;

    return true;
}

function DataAtual() {

    let today = new Date();
    let dd = today.getDate();

    let mm = today.getMonth() + 1;
    const yyyy = today.getFullYear();
    if (dd < 10) {
        dd = `0${dd}`;
    }

    if (mm < 10) {
        mm = `0${mm}`;
    }
    today = `${dd}/${mm}/${yyyy}`;

    return today;
}


function validaData(data) {
    reg = /[^\d\/\.]/gi;                  // Mascara = dd/mm/aaaa | dd.mm.aaaa
    let valida = data.replace(reg, '');    // aplica mascara e valida só numeros
    if (valida && valida.length == 10) {  // é válida, então ;)
        let ano = data.substr(6),
            mes = data.substr(3, 2),
            dia = data.substr(0, 2),
            M30 = ['04', '06', '09', '11'],
            v_mes = /(0[1-9])|(1[0-2])/.test(mes),
            v_ano = /(19[1-9]\d)|(20\d\d)|2100/.test(ano),
            rexpr = new RegExp(mes),
            fev29 = ano % 4 ? 28 : 29;

        if (v_mes && v_ano) {
            if (mes == '02') return (dia >= 1 && dia <= fev29);
            else if (rexpr.test(M30)) return /((0[1-9])|([1-2]\d)|30)/.test(dia);
            else return /((0[1-9])|([1-2]\d)|3[0-1])/.test(dia);
        }
    }
    return false
}

// diferença entre datas
function calculaDias(date1, date2) {
    //formato do brasil 'pt-br'
    moment.locale('pt-br');
    //setando data1
    var data1 = moment(date1, 'DD/MM/YYYY');
    //setando data2
    var data2 = moment(date2, 'DD/MM/YYYY');
    //tirando a diferenca da data2 - data1 em dias
    var diff = data2.diff(data1, 'days');

    return diff;
}

// foi criado este método para conseguir atualizar o datatable
// primeiro destroi o datatable e depois cria outro novo.
function destruirDataTable(nomeTabela) {

    if ($.fn.dataTable.isDataTable(`#${nomeTabela}`)) {
        $(`#${nomeTabela}`).DataTable().destroy();
    }
}

function dataTable(nomeTabela, valida = 0, ordenacao = 'desc') {

    $(`#${nomeTabela}`).on('prepreInit.dt').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "zeroRecords": "Não foram encontrados registros",
            "searchPlaceholder": "Buscar registros",
            "info": "Mostrando registros de _START_ ao _END_ de um total de  _TOTAL_ registros",
            "infoEmpty": "Não existem registros",
            "infoFiltered": "(filtrado de um total de _MAX_ registros)",
            "search": "Buscar:",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            },

        },
        "iDisplayLength": 8,
        "bDestroy": true,
        "order": [[valida, ordenacao], [0, 'asc']],
        "initComplete": fecharLoader()

    });

}

function dataTableTeste(nomeTabela, valida = 0, ordenacao = 'desc') {

    $(`#${nomeTabela}`).on('prepreInit.dt').DataTable({
        columnDefs: [{
            orderable: false,
            className: 'select-checkbox',
            targets: 1
        }],
        select: {
            style: 'multi',
            selector: 'td:first-child',

        },
        dom: 'Bfrtip',
        buttons: [
            'selectAll',
            'selectNone'
        ],
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "zeroRecords": "Não foram encontrados registros",
            "searchPlaceholder": "Buscar registros",
            "info": "Mostrando registros de _START_ ao _END_ de um total de  _TOTAL_ registros",
            "infoEmpty": "Não existem registros",
            "infoFiltered": "(filtrado de um total de _MAX_ registros)",
            "buttons": {
                "selectAll": "Todos",
                "selectNone": "Nenhum",
            },
            "search": "Buscar:",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Próximo",
                "previous": "Anterior"
            }

        },
        "iDisplayLength": 8,
        "bDestroy": true,
        "order": [[valida, ordenacao], [0, 'asc']],
    });

}




function validarEmail(field) {

    let emailInvalido = false;
    let usuario = "";
    let dominio = "";

    if (field != null || field != '') {

        usuario = field.value.substring(0, field.value.indexOf("@"));
        dominio = field.value.substring(field.value.indexOf("@") + 1, field.value.length);


        if ((usuario.length >= 1) &&
            (dominio.length >= 3) &&
            (usuario.search("@") == -1) &&
            (dominio.search("@") == -1) &&
            (usuario.search(" ") == -1) &&
            (dominio.search(" ") == -1) &&
            (dominio.search(".") != -1) &&
            (dominio.indexOf(".") >= 1) &&
            (dominio.lastIndexOf(".") < dominio.length - 1)) {

            emailInvalido = false;
        }
        else {
            emailInvalido = true;
        }
    }

    return emailInvalido;
}


function converterPadraoDataBr(dt) {
    let data = "";
    if (dt != null && dt != "") {
        dt = dt.split('-');
        let day = dt[2].split('T');
        let hours = day[1].split('.');

        data = day[0] + '/' + dt[1] + '/' + dt[0] + " " + hours[0];
    }
    return data;
}

function abrirLoaderCarregamento() {
    $('#spinnerModal').modal('show');
    setTimeout(function () {
        $('#spinnerModal').modal('hide');
    }, 700);
}

function abrirLoader() {
    $('#spinnerModal').modal('show');
}

function fecharLoaderBusca() {
    setTimeout(function () {
        $('#spinnerModal').modal('hide');
    }, 50);
}

function fecharLoader() {
    setTimeout(function () {
        $('#spinnerModal').modal('hide');
    }, 500);
}


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}