using FluentValidation;
using WK.Technology.Teste.Domain.ViewModel.Category;

namespace WK.Technology.Teste.Domain.Validations.Category
{
    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidator()
        {
            ValidateId();
            ValidateName();
        }

        protected void ValidateId()
        {
            RuleFor(viewModel => viewModel.Id)
                .GreaterThan(0).WithMessage("O Id informado deve ser maior que 0");
        }

        protected void ValidateName()
        {
            RuleFor(viewModel => viewModel.Name)
                .NotEmpty().WithMessage("É necessário informar o nome da categoria")
                .MaximumLength(150).WithMessage("O nome da categoria não pode conter mais de 150 caracteres")
                .MinimumLength(3).WithMessage("O nome da categoria não pode conter menos de 3 caracteres");
        }
    }
}
