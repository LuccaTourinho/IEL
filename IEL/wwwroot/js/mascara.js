document.addEventListener('DOMContentLoaded', function () {
    const cpfInputs = document.querySelectorAll('.cpf-mascara');

    cpfInputs.forEach(input => {
        input.addEventListener('input', function (e) {
            // Remove tudo que não é dígito
            let value = e.target.value.replace(/\D/g, '');

            if (value.length <= 11) {
                // Adiciona a máscara
                value = value.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
            }

            // Atualiza o campo com a máscara
            e.target.value = value;
        });
    });
});