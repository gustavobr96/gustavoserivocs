"use strict";

// Class Definition
var KTAddUser = function () {
    // Private Variables
    var _wizardEl;
    var _formEl;
    var _wizard;
    var _avatar;
    var _validations = [];

    // Private Functions
    var _initWizard = function () {
        // Initialize form wizard
        _wizard = new KTWizard(_wizardEl, {
            startStep: 1, // initial active step number
            clickableSteps: true  // allow step clicking
        });

        // Validation before going to next page
        _wizard.on('beforeNext', function (wizard) {
            _validations[wizard.getStep() - 1].validate().then(function (status) {
                if (status == 'Valid') {
                    _wizard.goNext();
                    KTUtil.scrollTop();
                } else {
                    swal.fire({
                        text: "Desculpe, parece que alguns erros foram detectados, tente novamente..",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok",
                        confirmButtonClass: "btn font-weight-bold btn-light"
                    }).then(function () {
                        KTUtil.scrollTop();
                    });
                }
            });

            _wizard.stop();  // Don't go to the next step
        });

        // Change Event
        _wizard.on('change', function (wizard) {
            KTUtil.scrollTop();
        });
    }

    var _initValidations = function () {
        // Validation Rules For Step 1
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                fields: {
                    Name: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o primeiro nome'
                            }
                        }
                    },
                    LastName: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha segundo nome'
                            }
                        }
                    },
                    //Phone: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'Preencha o telefone'
                    //        },
                    //        phone: {
                    //            country: 'BR',
                    //            message: 'Formato incorreto ex: (XX)XXXXXXXXX'
                    //        }
                    //    }
                    //},

                    Area: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha a sua area'
                            }
                        }
                    },
                    Especiality: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha pelo menos uma especialidade'
                            }
                        }
                    }


                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        ));

        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                fields: {
                    Logradouro: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o logradouro'
                            }
                        }
                    },
                    Number: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o numero'
                            }
                        }
                    },
                    Bairro: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o bairro'
                            }
                        }
                    },
                    CEP: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o CEP'
                            }
                        }
                    },
                    City: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha a sua cidade'
                            }
                        }
                    },
                    State: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha seu estado'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        ));
    }

    var _initAvatar = function () {
        _avatar = new KTImageInput('kt_user_add_avatar');
    }

    return {
        // public functions
        init: function () {
            _wizardEl = KTUtil.getById('kt_wizard');
            _formEl = KTUtil.getById('kt_form');

            _initWizard();
            _initValidations();
            _initAvatar();
        }
    };
}();

jQuery(document).ready(function () {
    KTAddUser.init();
});
