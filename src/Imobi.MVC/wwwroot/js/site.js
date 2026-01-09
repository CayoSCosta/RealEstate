/* ==================== site.js ==================== */
import { initToasts } from './modules/notifications.js';
import { initSidebar } from './modules/layout.js';
import { initViaCep } from './modules/viacep.js';
import { initImageUpload } from './modules/image-upload.js';
import { initFormHandling } from './modules/forms.js';

document.addEventListener("DOMContentLoaded", function () {
    // 1. UI Básica
    initToasts();
    initSidebar();
    initViaCep();
    initFormHandling();
    initImageUpload();

    // 2. Validation base form
    $('.form-check-input[role="switch"]').on('change', function () {
        const isChecked = $(this).is(':checked');
        const $label = $(this).siblings('.form-check-label');
        if ($label.length) {
            if ($(this).attr('id') === 'chkStatus') {
                const text = isChecked ? 'Ativo' : 'Inativo';
                $label.text(text).toggleClass('text-success', isChecked).toggleClass('text-danger', !isChecked);
            } else {
                const text = isChecked ? 'Ativo' : 'Inativo';
                $label.text(text);
            }
        }
    });

    console.log("Imobi System Scripts Loaded 🚀");
});