using FluentValidation;
using WK.Technology.Teste.Domain.ViewModel.Product;

namespace WK.Technology.Teste.Domain.Validations.Product
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
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
                .NotEmpty().WithMessage("É necessário informar o nome do produto")
                .MaximumLength(150).WithMessage("O nome do produto não pode conter mais de 150 caracteres")
                .MinimumLength(3).WithMessage("O nome do produto não pode conter menos de 3 caracteres");
        }
    }
}
