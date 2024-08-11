window.showModal = (isEditMode) => {
    const modal = new bootstrap.Modal(document.getElementById(isEditMode ? 'editModal' : 'addModal'), {});
    modal.show();
};

window.hideModal = (isEditMode) => {
    const modal = bootstrap.Modal.getInstance(document.getElementById(isEditMode ? 'editModal' : 'addModal'));
    modal.hide();
};

window.handleAlertAndReload = () => {
    setTimeout(() => {
        window.location.reload();
    }, 1500);
};