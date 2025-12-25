$(function () {
    let imgIndex = 0;
    const MAX_IMAGENS = 20;

    initImageCropper('modalEmpreendimento', {
        aspectRatio: 16 / 9,
        onConfirm: function (base64) {
            const atuais = $('#imagensExistentesContainer .item-galeria').length;
            const novas = $('#previewNovasImagens .card-preview-nova').length;

            if ((atuais + novas) >= MAX_IMAGENS) {
                alert("Limite de 20 imagens atingido!");
                return;
            }

            const card = `
                <div class="card card-preview-nova shadow-sm" style="min-width: 180px;">
                    <div class="card-img-wrapper" style="height: 120px; overflow: hidden;">
                        <img src="${base64}" class="card-img-top" style="object-fit: cover; height: 100%; width: 100%;">
                    </div>
                    <input type="hidden" name="ImagensBase64" value="${base64}" />
                    <div class="card-body p-2">
                        <button type="button" class="btn btn-sm btn-outline-danger w-100 btn-remover-nova">
                            <i class="bi bi-trash"></i>
                        </button>
                    </div>
                </div>`;

            $('#previewNovasImagens').append(card);
        }
    });

    $(document).on('click', '.btn-remover-nova', function () {
        $(this).closest('.card').remove();
    });

    $(document).on('click', '.btn-remover-logico', function () {
        const item = $(this).closest('.item-galeria');
        item.remove();
        reordenarIndices();
    });
});

function reordenarIndices() {
    $('#imagensExistentesContainer .item-galeria').each(function (i, el) {
        $(el).find('input[name^="Imagens"]').each(function () {
            const name = $(this).attr('name');
            const newName = name.replace(/\[\d+\]/, '[' + i + ']');
            $(this).attr('name', newName);
        });
        $(el).find('.badge-ordem').text('# ' + (i + 1));
        $(el).find('.input-ordem').val(i + 1);
    });
}