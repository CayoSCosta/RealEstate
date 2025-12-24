import { Loading } from './loader.js';

export function initViaCep() {
    const cepInput = document.getElementById("Endereco_Cep");

    if (cepInput) {
        let lastCep = "";

        cepInput.addEventListener("blur", async function () {
            const cep = this.value.replace(/\D/g, '');

            if (cep === lastCep || cep.length === 0) return;
            lastCep = cep;

            if (cep.length !== 8) return;

            Loading.show("Consultando CEP...");

            try {
                const res = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
                const data = await res.json();

                if (!data.erro) {
                    const setVal = (id, val) => {
                        const el = document.getElementById(id);
                        if (el) el.value = val;
                    };

                    setVal("Endereco_Logradouro", data.logradouro || '');
                    setVal("Endereco_Bairro", data.bairro || '');
                    setVal("Endereco_Cidade", data.localidade || '');
                    setVal("Endereco_Uf", data.uf || '');

                    document.getElementById("Endereco_Numero")?.focus();
                } else {
                    alert("CEP não encontrado.");
                }
            } catch (e) {
                console.error("Erro ao buscar CEP:", e);
                alert("Erro ao consultar CEP.");
            } finally {
                Loading.hide();
            }
        });
    }
}