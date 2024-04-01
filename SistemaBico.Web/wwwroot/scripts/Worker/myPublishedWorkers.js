$(window).on('load', function () {
    abrirLoaderCarregamento();
})
async function Interesse(id) {

    $('#spinnerModal2').modal('show');
    setTimeout(function () {
        $('#spinnerModal2').modal('hide');
    }, 600);


    await BuscaInteressados(id);

    jQuery('#modalInteressados').modal({
        backdrop: 'static',
        keyboard: false
    });
}

async function BuscaInteressados(id) {

    let dto = {
        Guid: id
    }



    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dto)
    }
    const response = await fetch("Professional/GetInterested", config)
    const data = await response.json();
    const professionalProfile = data.professionalProfile;


    let tr = '';
    professionalProfile.map(dado => {
        tr+= `
            <div class="card card-custom gutter-b col-lg-12">
									            <div class="card-body">
										            <!--begin::Top-->
										            <div class="d-flex">
											            <!--end::Pic-->
											            <!--begin: Info-->
											            <div class="flex-grow-1">
												            <!--begin::Title-->
												            <div class="d-flex align-items-center justify-content-between flex-wrap mt-2">
													            <!--begin::User-->
													            <div class="mr-3">
														            <!--begin::Name-->
														            <label class="d-flex align-items-center text-dark text-hover-primary font-size-h5 font-weight-bold mr-3">${dado.name + " " + dado.lastName}</label>
														            <!--end::Name-->
														            <!--begin::Contacts-->
														            <div class="d-flex flex-wrap my-2">
															            <a href="#" class="text-dark-75 font-weight-bolder font-size-sm">
															            <span class="svg-icon svg-icon-md svg-icon-gray-500 mr-1">
																            <!--begin::Svg Icon | path:assets/media/svg/icons/Map/Marker2.svg-->
																            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
																	            <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
																		            <rect x="0" y="0" width="24" height="24" />
																		            <path d="M9.82829464,16.6565893 C7.02541569,15.7427556 5,13.1079084 5,10 C5,6.13400675 8.13400675,3 12,3 C15.8659932,3 19,6.13400675 19,10 C19,13.1079084 16.9745843,15.7427556 14.1717054,16.6565893 L12,21 L9.82829464,16.6565893 Z M12,12 C13.1045695,12 14,11.1045695 14,10 C14,8.8954305 13.1045695,8 12,8 C10.8954305,8 10,8.8954305 10,10 C10,11.1045695 10.8954305,12 12,12 Z" fill="#042155" />
																	            </g>
																            </svg>
																            <!--end::Svg Icon-->
                                                                         </span>`;
                                                                        var city = "Remoto";
                                                                        if (dado.city != "")
                                                                        city = dado.city + " - " + dado.state;
                                                                      tr += `
                                                                        ${city}</a>
                                                                    </div>
                                                                     <div class="d-flex flex-wrap my-2 mt-10">
															             `;

                                                                        if(dado.avaliation != "" && parseFloat(dado.avaliation).toFixed(2) > 0) {
                                                                             var img = "star";
                                                                            var avaliation = Math.ceil(parseFloat(dado.avaliation).toFixed(2));

                                                                            if (avaliation < 0)
                                                                                avaliation = 1;
                                                                            else
                                                                                if (avaliation > 5)
                                                                                    avaliation = 5;

                                                                            img += avaliation + ".png";

                                                                            tr += ` <label class="text-dark-75 font-weight-bolder font-size-sm">Reputação</label>
                                                                                    <div class="input-group">
                                                                  
                                                                                      <img src="assets/media/logos/${img}" alt="image" style="width: 6.5rem" />
                                                                                     </div>
                                                                                   `;
                                                                        }
                                                                        else {
                                                                            tr += `<label class="text-dark-75 font-weight-bolder font-size-sm">Reputação</label>
                                                                                    <div class="input-group">
                                                                                        <span class="flex-grow-1 font-weight text-dark-50 py-1 py-lg-1 mr-2" style="font-style: italic">Esse profissional ainda não foi contratado. Vamos dar sua primeira oportunidade ?</span>
                                                                                    </div>
                                                                                  `;
                                                                        }
                                                                        tr +=
                                                                            `
                                                                    </div>

														            <!--end::Contacts-->
													            </div>

													            <div class="my-lg-0 my-1 mt-2">
														            <button class="btn btn-success btn-shadow font-weight-bold mr-2" type="button" id="BtnProfissionaisInteressado" onclick="location.href='profile/id/${dado.perfil}'"><i class="flaticon-eye"></i> Perfil</button>
													            </div>

												            </div>
											            </div>
											            <!--end::Info-->
										            </div>
									            </div>
								            </div> `;

		
    });
 
    jQuery('#ProfissionaisInteressados').html(tr);
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
