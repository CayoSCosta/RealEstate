export const Loading = {
    _timeoutId: null,

    show(text = "Carregando...", timeout = null, id = "app-loader") {
        const el = document.getElementById(id);
        const textEl = document.getElementById("loader-text");

        if (textEl) textEl.textContent = text;
        if (el) el.style.display = "flex";

        if (this._timeoutId) clearTimeout(this._timeoutId);

        if (timeout) {
            this._timeoutId = setTimeout(() => { this.hide(id); }, timeout * 1000);
        }
    },

    hide(id = "app-loader") {
        const el = document.getElementById(id);
        if (el) el.style.display = "none";
        if (this._timeoutId) {
            clearTimeout(this._timeoutId);
            this._timeoutId = null;
        }
    }
};

window.Loading = Loading;