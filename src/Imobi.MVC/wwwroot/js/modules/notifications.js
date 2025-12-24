export function initToasts() {
    const toastElList = [].slice.call(document.querySelectorAll('.toast'));

    toastElList.forEach(function (toastEl) {
        if (typeof bootstrap !== 'undefined') {
            const toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    });
}