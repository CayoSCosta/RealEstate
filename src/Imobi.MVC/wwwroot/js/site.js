/* ==================== site.js ==================== */
import { Loading } from './modules/loader.js';
import { initToasts } from './modules/notifications.js';
import { initSidebar } from './modules/layout.js';
import { initViaCep } from './modules/viacep.js';
import { initImageUpload } from './modules/image-upload.js';
import { initFormHandling } from './modules/forms.js';

document.addEventListener("DOMContentLoaded", function () {

    initToasts();
    initSidebar();

    initViaCep();
    initImageUpload();
    initFormHandling();

    console.log("Imobi System Scripts Loaded 🚀");
});