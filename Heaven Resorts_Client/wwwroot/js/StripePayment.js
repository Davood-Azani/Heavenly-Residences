redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51N2UxGBihCQa8eMHBfyTSghirWtaGhlQ1y37dygBnCK7wlRz3dYixYVhj4DMlFe9baMxXlsCEYidyFU4MVBi7d7b00V8pnKlUK');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};