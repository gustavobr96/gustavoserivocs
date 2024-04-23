$(window).on('load', function () {
    VerifyPayment();
})

async function VerifyPayment() {

    abrirLoader();
    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify()
    }
    const response = await fetch("Payment/GetPaymentId", config)
    const data = await response.json();
    if (data != "0") {
        await ScreenBrick(data);
    }

    fecharLoader();
}

const mp = new MercadoPago('APP_USR-7ce1020c-e802-4323-b5fd-ee81d556673e', {
    locale: 'pt-BR'
});
const bricksBuilder = mp.bricks();
const renderCardPaymentBrick = async (bricksBuilder) => {
    const settings = {
        initialization: {
            amount: 1, // total amount to be paid
            preferenceId: '<PREFERENCE_ID>'
        },
        customization: {
            paymentMethods: {
                creditCard: 'all',
                bankTransfer: 'all',
            },
        },
        callbacks: {
            onReady: () => {
                // callback called when the Brick is ready
            },
            onSubmit: ({ selectedPaymentMethod, formData }) => {
                //  callback called when the user clicks on the submit data button
                //  example of sending the data collected by our Brick to your server
                return new Promise((resolve, reject) => {
                    let paymentType = 0;

                    if (selectedPaymentMethod === 'credit_card') {
                        paymentType = 1;
                    } else if (selectedPaymentMethod === 'bank_transfer') {
                        paymentType = 2;
                    }
                    if (paymentType) {
                        abrirLoader();
                        ProcessPayment(formData, paymentType);
                        resolve();
                        
                    }
                   
                });
            },
            onError: (error) => {
                // callback called to all error cases related to the Brick
            },
        },
    };
    window.paymentBrickController = await bricksBuilder.create(
        'payment',
        'paymentBrick_container',
        settings
    );
};

renderCardPaymentBrick(bricksBuilder);

async function ProcessPayment(cardFormData, paymentType) {
    let dto = {
        Payment: cardFormData,
        TypePayment: paymentType
    };

    const config = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dto)
    }
    const response = await fetch("Payment/ProcessPayment", config)
    const data = await response.json();
    fecharLoader();
    ScreenBrick(data);
}

function ScreenBrick(id) {

    var elem = document.getElementById("info");
    if (elem == null) {
        document.location.reload(true);
    }
    else {
        elem.parentNode.removeChild(elem);

        const renderStatusScreenBrick = async (bricksBuilder) => {
            const settings = {
                initialization: {
                    paymentId: id, // id do pagamento gerado pelo mercado pago
                },
                callbacks: {
                    onReady: () => {
                        // callback chamado quando o Brick estiver pronto
                    },
                    onError: (error) => {
                        // callback chamado para todos os casos de erro do Brick
                    },
                },
            };
            window.statusScreenBrickController = await bricksBuilder.create('statusScreen', 'statusScreenBrick_container', settings);
        };
        renderStatusScreenBrick(bricksBuilder);
    }

    window.scrollTo(0, 0);
}
