
function initImageCropper(modalId, options = {}) {
    const modalEl = document.getElementById(modalId);
    if (!modalEl) {
        console.error(`Modal com id '${modalId}' não encontrado.`);
        return;
    }

    const inputFile = modalEl.querySelector('.input-file-cropper');
    const imageElement = modalEl.querySelector('.image-to-crop');
    const placeholder = modalEl.querySelector('.placeholder-crop');
    const btnConfirm = modalEl.querySelector('.btn-confirm-crop');


    const avatarContainer = modalEl.querySelector('.avatar-grid-container');
    const tabUploadBtn = modalEl.querySelector(`[data-bs-target="#content-upload-${modalId}"]`);
    const tabAvatarBtn = modalEl.querySelector(`[data-bs-target="#content-avatars-${modalId}"]`);

    let cropper = null;
    let selectedAvatarUrl = null;
    let activeTab = 'upload';

    if (avatarContainer && options.avatars && options.avatars.length > 0) {
        avatarContainer.innerHTML = '';
        options.avatars.forEach(url => {
            const img = document.createElement('img');
            img.src = url;
            img.classList.add('avatar-option', 'rounded-circle', 'bg-light');
            img.style.cursor = 'pointer';
            img.style.width = '60px';
            img.style.height = '60px';
            img.style.border = '3px solid transparent';
            img.style.margin = '5px';

            img.addEventListener('click', function () {
                modalEl.querySelectorAll('.avatar-option').forEach(el => {
                    el.style.borderColor = 'transparent';
                    el.classList.remove('selected');
                });
                this.style.borderColor = '#0d6efd';
                this.classList.add('selected');
                selectedAvatarUrl = url;
                activeTab = 'avatar';
            });
            avatarContainer.appendChild(img);
        });

 
        if (tabUploadBtn) {
            tabUploadBtn.addEventListener('click', () => { activeTab = 'upload'; });
        }
        if (tabAvatarBtn) {
            tabAvatarBtn.addEventListener('click', () => { activeTab = 'avatar'; });
        }
    }

    if (inputFile) {
        inputFile.addEventListener('change', function (e) {
            const files = e.target.files;
            if (files && files.length > 0) {
                const file = files[0];
                const url = URL.createObjectURL(file);

                imageElement.src = url;
                imageElement.classList.remove('d-none');
                placeholder.classList.add('d-none');

                if (cropper) {
                    cropper.destroy();
                }

                cropper = new Cropper(imageElement, {
                    aspectRatio: options.aspectRatio || 16 / 9,
                    viewMode: 1,
                    autoCropArea: 1,
                    responsive: true
                });

                activeTab = 'upload';
                if (tabUploadBtn) {

                }
            }
        });
    }

    if (btnConfirm) {
        const newBtn = btnConfirm.cloneNode(true);
        btnConfirm.parentNode.replaceChild(newBtn, btnConfirm);

        newBtn.addEventListener('click', function () {
            let resultBase64 = null;

            if (activeTab === 'upload') {
                if (cropper) {
                    const canvas = cropper.getCroppedCanvas({
                        width: options.outputWidth || 800,
                        height: options.outputHeight || 450,
                        fillColor: '#fff',
                        imageSmoothingEnabled: true,
                        imageSmoothingQuality: 'high',
                    });
                    resultBase64 = canvas.toDataURL('image/jpeg', 0.9);
                }
            }
            else if (activeTab === 'avatar' && selectedAvatarUrl) {
                resultBase64 = selectedAvatarUrl;
            }

            if (resultBase64) {
                if (options.onConfirm) options.onConfirm(resultBase64);

                const bsModal = bootstrap.Modal.getInstance(modalEl);
                if (bsModal) bsModal.hide();

                if (inputFile) inputFile.value = '';
            } else {
                alert('Por favor, selecione uma imagem ou avatar.');
            }
        });
    }

    modalEl.addEventListener('hidden.bs.modal', function () {
        if (cropper) {
            cropper.destroy();
            cropper = null;
        }
        if (inputFile) inputFile.value = '';
        if (imageElement) {
            imageElement.src = '';
            imageElement.classList.add('d-none');
        }
        if (placeholder) placeholder.classList.remove('d-none');
    });
}