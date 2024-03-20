document.addEventListener('DOMContentLoaded', function () {
    FormValidation.formValidation(
        document.getElementById('kt_form_update'),
        {
            fields: {
                Name: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o primeiro nome.'
                        },
                        stringLength: {
                            min: 3,
                            max: 40,
                            message: 'Insira seu nome [3 até 40 letras].'
                        }
                    }
                },
                LastName: {
                    validators: {
                        notEmpty: {
                            message: 'Por favor, preencha o segundo nome.',
                        },
                        stringLength: {
                            min: 3,
                            max: 40,
                            message: 'Insira seu sobrenome [3 até 40 letras].'
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

