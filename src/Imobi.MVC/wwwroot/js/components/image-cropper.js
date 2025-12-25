function initImageCropper(modalId, options = {}) {
    const modalEl = document.getElementById(modalId);
    if (!modalEl) return;

    const inputFile = modalEl.querySelector('.input-file-cropper');
    const imageElement = modalEl.querySelector('.image-to-crop');
    const placeholder = modalEl.querySelector('.placeholder-crop');
    const btnConfirm = modalEl.querySelector('.btn-confirm-crop');
    const avatarContainer = modalEl.querySelector('.avatar-grid-container');

    let cropper = null;
    let selectedAvatarUrl = null;
    let activeTab = 'upload';

    if (avatarContainer && options.avatars && options.avatars.length > 0) {
        options.avatars.forEach(url => {
            const img = document.createElement('img');
            img.src = url;
            img.classList.add('avatar-option', 'rounded-circle', 'bg-light');

            img.addEventListener('click', function () {
                modalEl.querySelectorAll('.avatar-option').forEach(el => el.classList.remove('selected'));
                this.classList.add('selected');
                selectedAvatarUrl = url;
                activeTab = 'avatar';
            });
            avatarContainer.appendChild(img);
        });

        const tabAvatarBtn = modalEl.querySelector(`[data-bs-target="#content-avatars-${modalId}"]`);
        const tabUploadBtn = modalEl.querySelector(`[data-bs-target="#content-upload-${modalId}"]`);

        if (tabAvatarBtn) tabAvatarBtn.addEventListener('shown.bs.tab', () => activeTab = 'avatar');
        if (tabUploadBtn) tabUploadBtn.addEventListener('shown.bs.tab', () => activeTab = 'upload');
    }

    inputFile.addEventListener('change', function (e) {
        const files = e.target.files;
        if (files && files.length > 0) {
            const file = files[0];
            const url = URL.createObjectURL(file);
            inputFile.value = '';

            imageElement.src = url;
            imageElement.classList.remove('d-none');
            placeholder.classList.add('d-none');

            if (cropper) cropper.destroy();

            cropper = new Cropper(imageElement, {
                aspectRatio: options.aspectRatio || 1,
                viewMode: 1,
                autoCropArea: 1
            });
            activeTab = 'upload';
        }
    });

    btnConfirm.addEventListener('click', function () {
        let resultBase64 = null;

        if (activeTab === 'upload' && cropper) {
            const canvas = cropper.getCroppedCanvas({
                width: options.outputWidth || 400,
                height: options.outputHeight || 400,
                fillColor: '#fff'
            });
            resultBase64 = canvas.toDataURL('image/jpeg', 0.85);
        }
        else if (activeTab === 'avatar' && selectedAvatarUrl) {
            resultBase64 = selectedAvatarUrl;
        }

        if (resultBase64 && options.onConfirm) {
            options.onConfirm(resultBase64);
            const modalInstance = bootstrap.Modal.getInstance(modalEl);
            modalInstance.hide();
        }
    });


    modalEl.addEventListener('hidden.bs.modal', function () {
        if (cropper) {
            cropper.destroy();
            cropper = null;
        }
        imageElement.classList.add('d-none');
        placeholder.classList.remove('d-none');
        modalEl.querySelectorAll('.avatar-option').forEach(el => el.classList.remove('selected'));
        selectedAvatarUrl = null;
    });
}