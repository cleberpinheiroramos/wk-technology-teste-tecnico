using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WK.Technology.Teste.Domain.Filters;
using WK.Technology.Teste.Domain.Interfaces.Services;
using WK.Technology.Teste.Domain.ViewModel.Category;
using WK.Technology.Teste.Infra.Results;

namespace WK.Technology.Teste.WebApi.Controllers.v1
{
    /// <summary>
    /// Controlador de operações para Categoria.
    /// </summary>
    public class CategoryController : ApiControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryViewModel> _categoryValidator;
        private readonly IValidator<CreateCategoryViewModel> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryViewModel> _updateCategoryValidator;

        public CategoryController(ICategoryService categoryService,
                                  IValidator<CategoryViewModel> categoryValidator,
                                  IValidator<CreateCategoryViewModel> createCategoryValidator,
                                  IValidator<UpdateCategoryViewModel> updateCategoryValidator)
        {
            _categoryService = categoryService;
            _categoryValidator = categoryValidator;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        /// <summary>
        /// Retorna uma lista páginada de categorias.
        /// </summary>
        /// <param name="filter">Filtro para otimizar a consulta por categoria</param>
        /// <param name="tokenCancelamento"></param>
        /// <response code="200">Retorna uma lista paginada de categorias de acordo com o filtro informado.</response>
        /// <response code="404">Se não encontrar nenhuma categoria de acordo com o filtro informado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessDataResult<IPagedList<CategoryViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategorysPaged([FromQuery] CategoryFilter filter = null, CancellationToken tokenCancelamento = default)
        {
            var categorys = await _categoryService.GetPagedAsync(filter, tokenCancelamento).ConfigureAwait(false);

            if (categorys is null)
                return NotFoundResult("Não foi encontrada nenhuma categoria com os parâmetros informados");

            return SuccessDataResult(categorys);
        }

        /// <summary>
        /// Retorna uma categoria consultada pelo id.
        /// </summary>
        /// <param name="id" example="123">Id da categoria.</param>
        /// <param name="tokenCancelamento"></param>
        /// <response code="200">Retorna a categoria de acordo com o Id informado</response>
        /// <response code="404">Se não encontrar uma categoria com o Id informado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(SuccessDataResult<CategoryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryById(long id, CancellationToken tokenCancelamento = default)
        {
            var categorys = await _categoryService.GetByIdAsync(id, tokenCancelamento).ConfigureAwait(false);

            if (categorys is null)
                return NotFoundResult("Não foi encontrada nenhuma categoria com o id informado");

            return SuccessDataResult(categorys);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <remarks>
        /// 
        /// Exemplo:
        ///
        ///     {
        ///         "name": "Categoria Teste"
        ///     }
        ///
        /// </remarks>
        /// <param name="createCategoryModel">Objeto com dados da categoria</param>
        /// <param name="cancellationToken"></param>	
        /// <response code="201">Retorna a categoria criada</response>
        /// <response code="400">Se a categoria não for criada</response>
        /// <response code="500">Erro de exceção</response>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDataResult<CreateCategoryViewModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDataResult<IList<ValidationFailure>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryViewModel createCategoryModel, CancellationToken cancellationToken = default)
        {
            if (createCategoryModel is null)
                return ErrorDataResult(new ValidationFailure("CreateCategoryViewModel", "O objeto informado não pode ser nulo"));

            var validationResult = _createCategoryValidator.Validate(createCategoryModel);
            if (!validationResult.IsValid)
                return ErrorDataResult(validationResult.Errors);

            var categorySavedData = await _categoryService.InsertAsync(createCategoryModel, cancellationToken).ConfigureAwait(false);

            return CreatedAtAction(nameof(GetCategoryById), new { id = categorySavedData.Id }, categorySavedData);
        }

        /// <summary>
        /// Altera uma categoria.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///    
        ///     {
        ///         "id": 1,
        ///         "name": "Categoria Teste"
        ///     }
        ///
        /// </remarks>
        /// <param name="updateCategoryModel">Objeto com dados da categoria</param>
        /// <param name="cancellationToken"></param>	
        /// <response code="200">Retorna a categoria alterada</response>
        /// <response code="400">Se a categoria não for alterada</response>
        /// <response code="404">Se não for encontrada uma categoria de acordo com o Id informado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpPut]
        [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<IList<ValidationFailure>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<IList<ValidationFailure>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryViewModel updateCategoryModel, CancellationToken cancellationToken = default)
        {
            if (updateCategoryModel is null)
                return ErrorDataResult(new ValidationFailure("UpdateCategoryModel", "O objeto informado não pode ser nulo"));

            var validationResult = _updateCategoryValidator.Validate(updateCategoryModel);
            if (!validationResult.IsValid)
                return ErrorDataResult(validationResult.Errors);

            var result = await _categoryService.UpdateAsync(updateCategoryModel, cancellationToken).ConfigureAwait(false);

            if (result is null)
                return NotFoundResult("Não foi encontrada nenhuma categoria com o id informado");

            return SuccessDataResult(result, "Categoria atualizada com sucesso");
        }

        /// <summary>
        /// Deleta uma categoria.
        /// </summary>
        /// <param name="categoryId" example="123">Id da categoria.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">Retorna se a categoria foi deletada</response>
        /// <response code="400">Se a categoria não for deletada</response>
        /// <response code="404">Se não for encontrada uma categoria de acordo com o Id informado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpDelete("{categoryId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDataResult<IList<ValidationFailure>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(long categoryId, CancellationToken cancellationToken = default)
        {
            if (categoryId <= 0)
                return ErrorDataResult(new ValidationFailure("CategoryId", "O id informado nao pode ser menor ou igual a zero"));

            var result = await _categoryService.DeleteAsync(categoryId, cancellationToken).ConfigureAwait(false);
            if (!result)
                return NotFoundResult("Não foi encontrada nenhuma categoria com o id informado");

            return NoContent();
        }
    }
}
