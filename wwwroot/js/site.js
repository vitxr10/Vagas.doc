let table = new DataTable('#minhasVagas');

var timeoutID;
function fecharPopup() {
    timeoutID = setTimeout(function () {
        $('.alerta').hide();
    }, 3000);
}

fecharPopup();

$('.close-alert').click(function () {
    clearTimeout(timeoutID);
    $('.alerta').hide();
});
