// wwwroot/js/components/galeria-manager.js

const GaleriaManager = {
    init: function (config) {
        const containerId = config.containerId;
        const modalId = config.modalId;
        const optionsHtml = config.optionsHtml;
        const aspectRatio = config.aspectRatio || 16 / 9;

        const container = document.getElementById(containerId);
        if (!container) return;

        if (typeof initImageCropper === 'function') {
            initImageCropper(modalId, {
                aspectRatio: aspectRatio,
                onConfirm: (base64) => {
                    this.adicionarCard(container, base64, optionsHtml);
                }
            });
        }

        $(container).on('click', '.btn-remover-galeria', (e) => {
            $(e.currentTarget).closest('.item-galeria').remove();
            this.reordenarIndices(container);
        });

        if (typeof Sortable !== 'undefined') {
            new Sortable(container, {
                animation: 150,
                ghostClass: 'sortable-ghost',
                onEnd: () => {
                    this.reordenarIndices(container);
                }
            });
        }
    },

    adicionarCard: function (container, base64, optionsHtml) {
        const index = $(container).find('.item-galeria').length;
        const card = `
            <div class="col-md-3 item-galeria">
                <div class="card h-100 shadow-sm border-primary">
                    <input type="hidden" name="Imagens[${index}].Id" value="00000000-0000-0000-0000-000000000000" />
                    <input type="hidden" name="Imagens[${index}].Base64" value="${base64}" />
                    <div class="position-relative">
                        <img src="${base64}" class="card-img-top" style="height: 140px; object-fit: cover;">
                        <span class="badge bg-success position-absolute top-0 start-0 m-2">Nova</span>
                    </div>
                    <div class="card-body p-2">
                        <select name="Imagens[${index}].Tipo" class="form-select form-select-sm mb-1">
                            ${optionsHtml}
                        </select>
                        <input type="text" name="Imagens[${index}].Legenda" class="form-control form-control-sm mb-1" placeholder="Legenda..." />
                        <input type="hidden" name="Imagens[${index}].Ordem" class="input-ordem" value="${index + 1}" />
                        <button type="button" class="btn btn-sm btn-outline-danger w-100 mt-1 btn-remover-galeria">
                            <i class="bi bi-trash-fill"></i> Remover
                        </button>
                    </div>
                </div>
            </div>`;
        $(container).append(card);
        this.reordenarIndices(container);
    },

    reordenarIndices: function (container) {
        $(container).find('.item-galeria').each(function (i, el) {
            $(el).find('input, select').each(function () {
                const name = $(this).attr('name');
                if (name) {
                    const newName = name.replace(/Imagens\[\d+\]/, 'Imagens[' + i + ']');
                    $(this).attr('name', newName);
                }
            });
            $(el).find('.badge-ordem').text('# ' + (i + 1));
            $(el).find('.input-ordem').val(i + 1);
        });
    }
};

export { GaleriaManager };