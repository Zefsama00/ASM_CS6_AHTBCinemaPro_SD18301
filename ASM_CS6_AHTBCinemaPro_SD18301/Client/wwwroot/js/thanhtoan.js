window.logPaymentMethod = (paymentMethod) => {
    console.log('Selected payment method:', paymentMethod);
};

window.initializeCheckout = (totalAmount) => {
    console.log('Initializing checkout with totalAmount:', totalAmount);
    paypal.Buttons({
        style: {
            layout: 'vertical',
            color: 'blue',
            shape: 'rect',
            label: 'paypal'
        },
        createOrder: function (data, actions) {
            var exchangeRate = 23000;
            var totalAmountUSD = (totalAmount / exchangeRate).toFixed(2);
            console.log('Creating order with amount in USD:', totalAmountUSD);

            return actions.order.create({
                purchase_units: [{
                    amount: {
                        currency_code: 'USD',
                        value: totalAmountUSD
                    }
                }]
            });
        },
        onApprove: function (data, actions) {
            return actions.order.capture().then(function (details) {
                console.log('Order approved:', details);
                DotNet.invokeMethodAsync('ASM_CS6_AHTBCinemaPro_SD18301', 'OnPaymentSuccess', details);
                Swal.fire({
                    title: 'Success!',
                    text: 'Payment has been processed successfully.',
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then(() => {
                    window.location.href = '/';
                });
            });
        }
    }).render('#paypal-button-container');
};


window.processPaypalPayment = (orderModel) => {
    console.log('Processing PayPal payment with orderModel:', orderModel);
    // Here you can send the orderModel to the server or process it further in JavaScript
};
