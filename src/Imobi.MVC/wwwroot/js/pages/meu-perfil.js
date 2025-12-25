
$(function () {
    const $form = $('#form-perfil');
    const $inputBase64 = $('#inputFotoBase64');
    const $avatarPreview = $('#avatarPreview');

    const seeds = ["Felix", "Aneka", "Zoe", "Jack", "Max", "Bella", "Trouble", "Bandit", "Muffin", "Peanut"];
    const avatarUrls = seeds.map(s => `https://api.dicebear.com/7.x/avataaars/svg?seed=${s}`);

    if (typeof initImageCropper === 'function') {
        initImageCropper('modalPerfil', {
            aspectRatio: 1,
            outputWidth: 400,
            outputHeight: 400,
            avatars: avatarUrls,
            onConfirm: function (base64Data) {
                $avatarPreview.attr('src', base64Data);
                $inputBase64.val(base64Data);
            }
        });
    }

    $form.on("submit", function (e) {
        if ($(this).valid()) {
            if (typeof Loading !== 'undefined') {
                Loading.show("Salvando alterações...");
            }
        }
    });
});