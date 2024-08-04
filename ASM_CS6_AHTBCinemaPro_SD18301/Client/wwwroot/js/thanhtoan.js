window.logPaymentMethod = (paymentMethod) => {
    console.log('Selected payment method:', paymentMethod);
};

window.initializeCheckout = (totalAmount) => {
    console.log('initializeCheckout called with totalAmount:', totalAmount);
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
                DotNet.invokeMethodAsync('ASM_CS6_AHTBCinemaPro_SD18301', 'OnPaypalPaymentSuccess', details);
                // Hiển thị thông báo thành công và chuyển hướng về trang chủ
                Swal.fire({
                    title: 'Thành công!',
                    text: 'Thanh toán đã được xử lý thành công.',
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = '/';  // Đổi thành URL trang chủ của bạn nếu khác
                    }
                });
            });
        }
    }).render('#paypal-button-container');
};




window.processPaypalPayment = (orderModel) => {
    console.log('Processing PayPal payment with orderModel:', orderModel);
    // Here you can send the orderModel to the server or process it further in JavaScript
};
