$(window).on('load', function () {
    abrirLoaderCarregamento();
})

async function Interesse(perfil) {
    document.getElementById(perfil).disabled = true;
    document.getElementById('spinner').classList.add('spinner');
    document.getElementById('spinner').classList.add('spinner-primary');
    document.getElementById('spinner').classList.add('spinner-left');

    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(perfil)
    }
    const response = await fetch("Professional/ApplyProfessional", config);
  
    document.getElementById('spinner').classList.remove('spinner');
    document.getElementById('spinner').classList.remove('spinner-primary');
    document.getElementById('spinner').classList.remove('spinner-left');
    location.href = "/client/GetMyProfessional";
}
