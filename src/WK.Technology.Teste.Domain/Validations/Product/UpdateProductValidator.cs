using FluentValidation;
using WK.Technology.Teste.Domain.ViewModel.Product;

namespace WK.Technology.Teste.Domain.Validations.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductViewModel>
    {
        public UpdateProductValidator()
        {
            ValidateId();
            Include(new CreateProductValidator());
        }

        protected void ValidateId()
        {
            RuleFor(viewModel => viewModel.Id)
                .NotEmpty().WithMessage("É necessário informar o Id do produto")
                .Must(x => x != 0).WithMessage("O Id informado deve ser maior que 0");
        }
    }
}
