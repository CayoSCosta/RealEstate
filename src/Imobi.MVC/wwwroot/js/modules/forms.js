import { Loading } from './loader.js';

export function initFormHandling() {
    const forms = document.querySelectorAll("form");

    const validationRules = {
        cpf: {
            mask: (v) => v.replace(/\D/g, '').replace(/(\d{3})(\d)/, '$1.$2').replace(/(\d{3})(\d)/, '$1.$2').replace(/(\d{3})(\d{1,2})/, '$1-$2').slice(0, 14),
            validate: (v) => /^\d{3}\.\d{3}\.\d{3}-\d{2}$/.test(v)
        },
        cnpj: {
            mask: (v) => v.replace(/\D/g, '').replace(/^(\d{2})(\d)/, '$1.$2').replace(/^(\d{2})\.(\d{3})(\d)/, '$1.$2.$3').replace(/\.(\d{3})(\d)/, '.$1/$2').replace(/(\d{4})(\d)/, '$1-$2').slice(0, 18),
            validate: (v) => /^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$/.test(v)
        },
        cep: {
            mask: (v) => v.replace(/\D/g, '').replace(/(\d{5})(\d)/, '$1-$2').slice(0, 9),
            validate: (v) => /^\d{5}-\d{3}$/.test(v)
        },
        currency: {
            mask: (v) => {
                let val = v.replace(/\D/g, '');
                return (val / 100).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            },
            validate: (v) => v !== "" && v !== "0,00"
        },
        number: {
            mask: (v) => v.replace(/\D/g, ''),
            validate: (v, input) => {
                const max = parseInt(input.dataset.max) || 12;
                return /^\d+$/.test(v) && v.length <= max;
            }
        },
        email: {
            mask: (v) => v.trim(),
            validate: (v) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v)
        },
        text: {
            mask: (v) => v,
            validate: (v, input) => {
                const max = parseInt(input.dataset.max) || 50;
                return v.trim().length > 0 && v.length <= max;
            }
        },
        textbox: {
            mask: (v) => v,
            validate: (v, input) => {
                const max = parseInt(input.dataset.max) || 500;
                return v.length <= max;
            }
        },
        title: {
            mask: (v) => {
                let val = v.replace(/\s\s+/g, ' ');
                return val.toLowerCase().replace(/(?:^|\s)\S/g, (a) => a.toUpperCase());
            },
            validate: (v, input) => {
                const min = parseInt(input.dataset.min) || 3;
                const max = parseInt(input.dataset.max) || 100;
                const val = v.trim();
                return val.length >= min && val.length <= max;
            }
        },
    };

    const validateInput = (input) => {
        const type = input.dataset.validate || (input.hasAttribute('required') ? 'text' : null);
        if (!type || !validationRules[type]) return true;

        const rule = validationRules[type];
        const isValid = rule.validate(input.value, input);
        const errorSpan = document.querySelector(`[data-valmsg-for="${input.name}"]`);

        if (!isValid) {
            input.classList.add('is-invalid');
            input.classList.remove('is-valid');
            input.setCustomValidity("Invalido");

            if (errorSpan && errorSpan.textContent === "") {
                const min = input.dataset.min;
                const max = input.dataset.max;
                if (min && input.value.length < min) errorSpan.textContent = `Mínimo de ${min} caracteres`;
                else if (max && input.value.length > max) errorSpan.textContent = `Máximo de ${max} caracteres`;
                else errorSpan.textContent = "Entrada inválida";
            }
            return false;
        } else {
            input.classList.remove('is-invalid');
            input.classList.add('is-valid');
            input.setCustomValidity("");
            if (errorSpan) errorSpan.textContent = "";
            return true;
        }
    };

    document.querySelectorAll('[data-validate]').forEach(input => {
        const rule = validationRules[input.dataset.validate];
        if (rule && input.value) {
            input.value = rule.mask(input.value);
        }
    });

    document.querySelectorAll('[data-validate]').forEach(input => {
        const rule = validationRules[input.dataset.validate];
        input.addEventListener('input', (e) => { e.target.value = rule.mask(e.target.value); });
        input.addEventListener('blur', () => validateInput(input));
    });

    forms.forEach(form => {
        form.addEventListener("submit", function (e) {
            let formIsValid = true;

            this.querySelectorAll('[data-validate], [required]').forEach(input => {
                if (!validateInput(input)) formIsValid = false;
            });

            const isJQueryValid = (typeof jQuery !== 'undefined' && $(this).valid) ? $(this).valid() : true;

            if (!formIsValid || !this.checkValidity() || !isJQueryValid) {
                e.preventDefault();
                e.stopPropagation();
                if (window.Loading) window.Loading.hide();
                this.classList.add('was-validated');
                return false;
            }

            this.querySelectorAll('[data-validate]').forEach(input => {
                if (input.value) {
                    if (input.dataset.validate === "currency") {
                        input.value = input.value.replace(/\./g, '').replace(',', '.');
                    }
                    else if (input.dataset.validate === "cep") {
                        input.value = input.value.replace(/\D/g, '');
                    }
                }
            });

            if (this.method.toLowerCase() === "post" && this.target !== "_blank") {
                const btn = this.querySelector('button[type="submit"]');
                if (btn) {
                    btn.disabled = true;
                    btn.style.opacity = '0.5';
                }
                if (window.Loading) window.Loading.show("Processando...");
            }
        });
    });
}