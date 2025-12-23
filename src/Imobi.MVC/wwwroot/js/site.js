/* ==================== site.js ==================== */

document.addEventListener("DOMContentLoaded", function () {

    // ----------------------------------------------------------------
    // 1. SISTEMA DE NOTIFICAÇÕES (TOASTS)
    // ----------------------------------------------------------------
    // Procura por toasts gerados pelo _Alerta.cshtml via TempData
    var toastElList = [].slice.call(document.querySelectorAll('.toast'));
    toastElList.forEach(function (toastEl) {
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    });

    // ----------------------------------------------------------------
    // 2. SISTEMA DE LOADING (GLOBAL)
    // ----------------------------------------------------------------
    window.Loading = {
        _timeoutId: null,
        show(text = "Carregando...", timeout = null, id = "app-loader") {
            const el = document.getElementById(id);
            const textEl = document.getElementById("loader-text");
            if (textEl) textEl.textContent = text;
            if (el) el.style.display = "flex";
            if (this._timeoutId) clearTimeout(this._timeoutId);
            if (timeout) {
                this._timeoutId = setTimeout(() => { this.hide(id); }, timeout * 1000); // timeout em segundos
            }
        },
        hide(id = "app-loader") {
            const el = document.getElementById(id);
            if (el) el.style.display = "none";
            if (this._timeoutId) { clearTimeout(this._timeoutId); this._timeoutId = null; }
        }
    };

    // ----------------------------------------------------------------
    // 3. MENU SIDEBAR
    // ----------------------------------------------------------------
    const toggleBtn = document.getElementById("sidebarToggle");
    const sidebar = document.getElementById("sidebar");
    if (toggleBtn && sidebar) {
        toggleBtn.addEventListener("click", function (e) {
            e.preventDefault();
            sidebar.classList.toggle("collapsed");
        });
    }

    // ----------------------------------------------------------------
    // 4. MÓDULO: CONSULTA DE CEP
    // ----------------------------------------------------------------
    const cepInput = document.getElementById("Endereco_Cep");
    if (cepInput) {
        let lastCep = "";
        cepInput.addEventListener("blur", async function () {
            const cep = this.value.replace(/\D/g, '');

            if (cep === lastCep || cep.length === 0) return;
            lastCep = cep;

            if (cep.length !== 8) {
                // Opcional: Avisar CEP incompleto
                return;
            }

            Loading.show("Consultando CEP...");

            try {
                const res = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
                const data = await res.json();

                if (!data.erro) {
                    document.getElementById("Endereco_Logradouro").value = data.logradouro || '';
                    document.getElementById("Endereco_Bairro").value = data.bairro || '';
                    document.getElementById("Endereco_Cidade").value = data.localidade || '';
                    document.getElementById("Endereco_Uf").value = data.uf || '';
                    // Foca no número após preencher
                    document.getElementById("Endereco_Numero")?.focus();
                } else {
                    // CEP não encontrado na base
                    alert("CEP não encontrado."); // Pode substituir por um Toast JS se preferir
                }
            } catch (e) {
                console.error("Erro ao buscar CEP:", e);
                alert("Erro ao consultar CEP. Verifique sua conexão.");
            } finally {
                // O finally garante que o Loading suma, dando erro ou sucesso!
                Loading.hide();
            }
        });
    }

    // ----------------------------------------------------------------
    // 5. MÓDULO: UPLOAD DE IMAGENS (PREVIEW)
    // ----------------------------------------------------------------
    const imgInput = document.getElementById('imagensUpload');
    const previewContainer = document.getElementById('previewContainer');

    if (imgInput && previewContainer) {
        imgInput.addEventListener('change', () => {
            const files = Array.from(imgInput.files);
            previewContainer.innerHTML = '';

            if (files.length === 0) {
                previewContainer.innerHTML = '<p class="text-muted">Nenhuma imagem selecionada</p>';
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
                    card.style.minWidth = '200px';
                    card.innerHTML = `
                        <div class="card-img-wrapper">
                            <img src="${e.target.result}" class="card-img-top rounded" style="height: 150px; object-fit: cover;" alt="preview" />
                        </div>
                        <div class="card-body text-center">
                            <button type="button" class="btn btn-sm btn-danger mt-2 remove-btn" data-index="${index}">
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

        // Event Delegation para remover imagens
        previewContainer.addEventListener('click', (e) => {
            if (e.target.closest('.remove-btn')) {
                const btn = e.target.closest('.remove-btn');
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

    // ----------------------------------------------------------------
    // 6. MÓDULO: SUBMIT GERAL (Correção do Loading Infinito)
    // ----------------------------------------------------------------
    const forms = document.querySelectorAll("form");
    forms.forEach(form => {
        form.addEventListener("submit", function (e) {

            // 1. Verifica validação Nativa (HTML5)
            if (!this.checkValidity()) {
                // Deixa o navegador mostrar os balões de erro padrão se houver
                return;
            }

            // 2. Verifica validação do jQuery (ASP.NET MVC)
            // Isso é crucial! Se o jQuery barrar (ex: CEP inválido), não podemos mostrar o loader.
            if (typeof $(this).valid === 'function' && !$(this).valid()) {
                e.preventDefault(); // Garante que pare
                // Opcional: Rolar até o primeiro erro
                return;
            }

            // Só ativa loading se for POST e estiver tudo VÁLIDO
            if (this.method.toLowerCase() === "post") {
                const btn = this.querySelector('button[type="submit"]');

                // Evita duplo clique
                if (btn && !btn.classList.contains('disabled-loading')) {
                    btn.disabled = true;
                    // Salva o texto original
                    btn.dataset.originalText = btn.innerHTML;
                    btn.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Salvando...';
                }

                Loading.show("Processando dados...");
            }
        });
    });
});