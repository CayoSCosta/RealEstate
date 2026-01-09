import { GaleriaManager } from './components/galeria-manager.js';

export const ImobiUI = {
    setupGaleria: function (config) {
        GaleriaManager.init({
            containerId: config.containerId,
            modalId: config.modalId,
            optionsHtml: config.optionsHtml,
            aspectRatio: config.aspectRatio || (16 / 9)
        });
    },
    setupValidation: function (formSelector) {
        if (typeof window.initValidation === 'function') {
            window.initValidation(formSelector);
        }
    }
};