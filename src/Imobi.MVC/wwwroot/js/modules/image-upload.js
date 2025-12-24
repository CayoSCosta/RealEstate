export function initImageUpload() {
    const imgInput = document.getElementById('imagensUpload');
    const previewContainer = document.getElementById('previewContainer');

    if (imgInput && previewContainer) {

        imgInput.addEventListener('change', () => {
            const files = Array.from(imgInput.files);
            previewContainer.innerHTML = '';

            if (files.length === 0) {
                previewContainer.innerHTML = '<p class="text-muted small">Nenhuma imagem selecionada</p>';
                return;
            }

            const carousel = document.createElement('div');
            carousel.classList.add('horizontal-carousel', 'd-flex', 'overflow-auto', 'py-2');

            files.forEach((file, index) => {
                if (!file.type.startsWith('image/')) return;

                const reader = new FileReader();
                reader.onload = (e) => {
                    const card = document.createElement('div');
                    card.classList.add('card', 'shadow-sm', 'me-3');
                    card.style.minWidth = '160px';

                    card.innerHTML = `
                        <div class="card-img-wrapper" style="height: 120px; overflow: hidden; display: flex; align-items: center; justify-content: center;">
                            <img src="${e.target.result}" class="card-img-top rounded" style="height: 100%; width: auto; object-fit: cover;" alt="preview" />
                        </div>
                        <div class="card-body text-center p-2">
                            <button type="button" class="btn btn-sm btn-danger remove-btn w-100" data-index="${index}">
                                <i class="bi bi-trash"></i> Remover
                            </button>
                        </div>
                    `;
                    carousel.appendChild(card);
                };
                reader.readAsDataURL(file);
            });

            previewContainer.appendChild(carousel);
        });

        previewContainer.addEventListener('click', (e) => {
            const btn = e.target.closest('.remove-btn');
            if (btn) {
                const index = btn.dataset.index;
                const dt = new DataTransfer();
                const currentFiles = Array.from(imgInput.files);

                currentFiles.splice(index, 1);
                currentFiles.forEach(f => dt.items.add(f));

                imgInput.files = dt.files;
                imgInput.dispatchEvent(new Event('change'));
            }
        });
    }
}