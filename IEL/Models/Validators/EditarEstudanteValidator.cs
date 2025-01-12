using FluentValidation;

namespace IEL.Models.Validators
{
    public class EditarEstudanteValidator : AbstractValidator<EditarEstudanteViewModel>
    {
        public EditarEstudanteValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .WithMessage("O campo Id é obrigatório para a edição.");
            RuleFor(e => e.Nome)
                .Must(nome => !string.IsNullOrWhiteSpace(nome))
                .WithMessage("O campo Nome não pode conter apenas espaços em branco.")
                .MaximumLength(100)
                .WithMessage("O campo Nome deve ter no máximo 100 caracteres.");
            RuleFor(e => e.CPF)
                .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")
                .WithMessage("O campo CPF deve estar no formato 000.000.000-00.");
            RuleFor(e => e.Endereco)
                .Must(endereco => !string.IsNullOrWhiteSpace(endereco))
                .WithMessage("O campo Endereço não pode conter apenas espaços em branco.")
                .MaximumLength(200)
                .WithMessage("O campo Endereço deve ter no máximo 200 caracteres.");
            RuleFor(e => e.DataConclusao)
                .NotEmpty()
                .WithMessage("Data de Conclusão é obrigatório.");
        }
    }
}
