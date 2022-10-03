using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK.Technology.Teste.Domain.ViewModel.Category;

namespace WK.Technology.Teste.Domain.Validations.Category
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryViewModel>
    {
        public CreateCategoryValidator()
        {
            ValidateName();
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
