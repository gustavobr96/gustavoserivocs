document.addEventListener('DOMContentLoaded', function () {
    FormValidation.formValidation(
        document.getElementById('kt_form_address_update'),
        {
            fields: {
                CEP: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o endereço.'
                        },
                        stringLength: {
                            min: 8,
                            max: 8,
                            message: 'Preencha o CEP corretamente.'
                        }
                    }
                },
                Cidade: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha a cidade.',
                        },
                        stringLength: {
                            min: 3,
                            max: 40,
                            message: 'Insira sua cidade [3 até 40 letras].'
                        }
                    }
                },
                State: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o estado.',
                        },
                        stringLength: {
                            min: 2,
                            max: 3,
                            message: 'Insira seu estado.'
                        }
                    }
                },

                Logradouro: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o logradouro.',
                        },
                        stringLength: {
                            min: 5,
                            max: 70,
                            message: 'Insira sua rua [5 até 70 letras]'
                        }
                    }
                },
                Number: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o número da rua.',
                        }
                    }
                },
                Bairro: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o seu bairro.',
                        }
                    },
                    stringLength: {
                        min: 2,
                        max: 70,
                        message: 'Insira seu bairro [2 até 70 letras].'
                    }
                },
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


/*Correios API*/

function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('Logradouro').value = ("");
    document.getElementById('Bairro').value = ("");
    document.getElementById('City').value = ("");
    document.getElementById('State').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('Logradouro').value = (conteudo.logradouro);
        document.getElementById('Bairro').value = (conteudo.bairro);
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
