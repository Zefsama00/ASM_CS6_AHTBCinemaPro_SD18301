window.showModal = (modalId) => {
    var modal = new bootstrap.Modal(document.querySelector(modalId));
    modal.show();
};

window.hideModal = (modalId) => {
    var modal = bootstrap.Modal.getInstance(document.querySelector(modalId));
    if (modal) {
        modal.hide();
    }
};

window.showAlert = (title, message, type) => {
    const alertPlaceholder = document.getElementById('alertPlaceholder');
    const wrapper = document.createElement('div');
    wrapper.innerHTML = `
        <div class="alert alert-${type} alert-dismissible" role="alert">
            <strong>${title}</strong> ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;
    alertPlaceholder.append(wrapper);
};
