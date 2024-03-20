$(window).on('load', function () {
    abrirLoaderCarregamento();
})

document.addEventListener('DOMContentLoaded', function () {
    FormValidation.formValidation(
        document.getElementById('kt_form'),
        {
            fields: {
                Area: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha a área'
                        }
                    }
                },
                Phone: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o telefone para contato.',
                        },
                        stringLength: {
                            min: 14,
                            max: 14,
                            message: 'Insira seu número de telefone no formato correto.'
                        }
                    }
                },
                Especiality: {
                    validators: {
                        notEmpty: {
                            message: 'Selecione pelo menos uma especialidade.'
                        }
                    }
                },
                Titulo: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o título.',
                        },
                        stringLength: {
                            min: 9,
                            max: 200,
                            message: 'Insira o título [9 até 200 letras].'
                        }
                    }
                },
                Sobre: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, descreva o projeto.',
                        },
                        stringLength: {
                            min: 9,
                            max: 2000,
                            message: 'Descreva o projeto [9 até 2000 letras].'
                        }
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                submitButton: new FormValidation.plugins.SubmitButton(),
                defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
                bootstrap: new FormValidation.plugins.Bootstrap({
                    eleInvalidClass: '',
                    eleValidClass: '',
                })
            }
        }
    );
});

const handlePhone = (event) => {
    let input = event.target
    input.value = phoneMask(input.value)
}

const phoneMask = (value) => {
    if (!value) return ""
    value = value.replace(/\D/g, '')
    value = value.replace(/(\d{2})(\d)/, "($1)$2")
    value = value.replace(/(\d)(\d{4})$/, "$1-$2")
    return value
}


/*Correios API*/

function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('City').value = ("");
    document.getElementById('State').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('City').value = (conteudo.localidade);
        document.getElementById('State').value = (conteudo.uf);
    } //end if.
}

function pesquisacep(valor) {

    //Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, '');

    //Verifica se campo cep possui valor informado.
    if (cep != "") {

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

    } //end if.
    else {
        //cep sem valor, limpa formulário.
        limpa_formulário_cep();
    }
};


String.prototype.reverse = function () {
    return this.split('').reverse().join('');
};

//function mascaraMoeda(campo) {
//    var elemento = campo;
//    var valor = elemento.value;

//    valor = valor + '';
//    valor = parseInt(valor.replace(/[\D]+/g, ''));
//    valor = valor + '';
//    valor = valor.replace(/([0-9]{2})$/g, ",$1");

//    if (valor.length > 6) {
//        valor = valor.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");
//    }

//    elemento.value = valor;
//    if (valor == 'NaN') elemento.value = '';
//}

function mascaraMoeda(campo, evento) {
    var tecla = (!evento) ? window.event.keyCode : evento.which;
    var valor = campo.value.replace(/[^\d]+/gi, '').reverse();
    var resultado = "";
    var mascara = "##.###.###,##".reverse();
    for (var x = 0, y = 0; x < mascara.length && y < valor.length;) {
        if (mascara.charAt(x) != '#') {
            resultado += mascara.charAt(x);
            x++;
        } else {
            resultado += valor.charAt(y);
            y++;
            x++;
        }
    }
    campo.value = resultado.reverse();
}