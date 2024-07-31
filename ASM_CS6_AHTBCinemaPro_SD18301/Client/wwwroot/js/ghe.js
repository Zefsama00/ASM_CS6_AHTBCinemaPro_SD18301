function showModal() {
    var myModal = new bootstrap.Modal(document.getElementById('gheModal'), {
        keyboard: false
    });
    myModal.show();
}

function hideModal() {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('gheModal'));
    if (myModal) {
        myModal.hide();
    }
}

function showAlert(title, message, type) {
    var alertPlaceholder = document.getElementById('alertPlaceholder');
    var wrapper = document.createElement('div');
    wrapper.innerHTML = [
        `<div class="alert alert-${type} alert-dismissible fade show" role="alert">`,
        `<strong>${title}</strong> ${message}`,
        '</div>'
    ].join('');
    alertPlaceholder.append(wrapper);
    setTimeout(() => {
        var alert = bootstrap.Alert.getOrCreateInstance(wrapper.querySelector('.alert'));
        alert.close();
    }, 5000);
}
