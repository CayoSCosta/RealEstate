import { Loading } from './loader.js';

export function initFormHandling() {
    const forms = document.querySelectorAll("form");

    forms.forEach(form => {
        form.addEventListener("submit", function (e) {

            if (!this.checkValidity()) return;

            if (typeof $ !== 'undefined' && typeof $(this).valid === 'function' && !$(this).valid()) {
                e.preventDefault();
                return;
            }

            if (this.method.toLowerCase() === "post" && this.target !== "_blank") {
                const btn = this.querySelector('button[type="submit"]');

                if (btn && !btn.classList.contains('disabled-loading')) {
                    btn.style.pointerEvents = 'none';
                    btn.style.opacity = '0.75';
                }

                Loading.show("Processando dados...");
            }
        });
    });
}