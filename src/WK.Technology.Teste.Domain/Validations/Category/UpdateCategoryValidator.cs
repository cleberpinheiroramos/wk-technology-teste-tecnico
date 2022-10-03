using FluentValidation;
using WK.Technology.Teste.Domain.ViewModel.Category;

namespace WK.Technology.Teste.Domain.Validations.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryViewModel>
    {
        public UpdateCategoryValidator()
        {
            ValidateId();
            Include(new CreateCategoryValidator());
        }

        protected void ValidateId()
        {
            RuleFor(viewModel => viewModel.Id)
                .NotEmpty().WithMessage("É necessário informar o Id da categoria")
                .Must(x => x != 0).WithMessage("O Id informado deve ser maior que 0");
        }
    }
}
