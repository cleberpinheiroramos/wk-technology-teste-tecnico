using FluentValidation;
using WK.Technology.Teste.Domain.ViewModel.Product;

namespace WK.Technology.Teste.Domain.Validations.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductValidator()
        {
            ValidateName();
        }

        protected void ValidateName()
        {
            RuleFor(viewModel => viewModel.Name)
                .NotEmpty().WithMessage("É necessário informar o nome do produto")
                .MaximumLength(150).WithMessage("O nome do produto não pode conter mais de 150 caracteres")
                .MinimumLength(3).WithMessage("O nome do produto não pode conter menos de 3 caracteres");
        }
    }
}
