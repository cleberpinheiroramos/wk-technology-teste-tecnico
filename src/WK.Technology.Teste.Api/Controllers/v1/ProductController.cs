using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WK.Technology.Teste.Domain.Filters;
using WK.Technology.Teste.Domain.Interfaces.Services;
using WK.Technology.Teste.Domain.ViewModel.Product;
using WK.Technology.Teste.Infra.Results;

namespace WK.Technology.Teste.WebApi.Controllers.v1
{
    /// <summary>
    /// Controlador de operações para Produto.
    /// </summary>
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductViewModel> _productValidator;
        private readonly IValidator<CreateProductViewModel> _createProductValidator;
        private readonly IValidator<UpdateProductViewModel> _updateProductValidator;

        public ProductController(IProductService productService,
                                 IValidator<ProductViewModel> productValidator,
                                 IValidator<CreateProductViewModel> createProductValidator,
                                 IValidator<UpdateProductViewModel> updateProductValidator)
        {
            _productService = productService;
            _productValidator = productValidator;
            _createProductValidator = createProductValidator;
            _updateProductValidator = updateProductValidator;
        }

        /// <summary>
        /// Retorna uma lista paginada de produtos.
        /// </summary>
        /// <param name="filter">Filtro para otimizar a consulta por produtos</param>
        /// <param name="tokenCancelamento"></param>
        /// <response code="200">Retorna uma lista paginada de produtos de acordo com o filtro informado.</response>
        /// <response code="404">Se não encontrar nenhum produto de acordo com o filtro informado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpGet]
        [ProducesResponseType(typeof(SuccessDataResult<IPagedList<ProductViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsPaged([FromQuery] ProductFilter filter = null, CancellationToken tokenCancelamento = default)
        {
            var products = await _productService.GetPagedAsync(filter, tokenCancelamento).ConfigureAwait(false);

            if (products is null)
                return NotFoundResult("Não foi encontrada nenhum produto com os parâmetros informados");

            return SuccessDataResult(products);
        }

        /// <summary>
        /// Retorna uma produto consultada pelo id.
        /// </summary>
        /// <param name="id" example="123">Id da produto.</param>
        /// <param name="tokenCancelamento"></param>
        /// <response code="200">Retorna o produto de acordo com o Id informado</response>
        /// <response code="404">Se não encontrar um produto com o Id informado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(SuccessDataResult<ProductViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById(long id, CancellationToken tokenCancelamento = default)
        {
            var products = await _productService.GetByIdAsync(id, tokenCancelamento).ConfigureAwait(false);

            if (products is null)
                return NotFoundResult("Não foi encontrada nenhum produto com o id informado");

            return SuccessDataResult(products);
        }

        /// <summary>
        /// Cria uma novo produto.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///    
        ///     {
        ///         "name": "Produto Teste",
        ///         "description": "Pen blue bic",
        ///         "price": 2.60,
        ///         "categoryId": 5
        ///     }
        ///
        /// </remarks>
        /// <param name="createProductModel">Objeto com dados do produto</param>
        /// <param name="cancellationToken"></param>	
        /// <response code="201">Retorna o produto criado</response>
        /// <response code="400">Se o produto não for criado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDataResult<CreateProductViewModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDataResult<IList<ValidationFailure>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductViewModel createProductModel, CancellationToken cancellationToken = default)
        {
            if (createProductModel is null)
                return ErrorDataResult(new ValidationFailure("CreateProductViewModel", "O objeto informado não pode ser nulo"));

            var validationResult = _createProductValidator.Validate(createProductModel);
            if (!validationResult.IsValid)
                return ErrorDataResult(validationResult.Errors);

            var productSavedData = await _productService.InsertAsync(createProductModel, cancellationToken).ConfigureAwait(false);

            return CreatedAtAction(nameof(GetProductById), new { id = productSavedData.Id }, productSavedData);
        }

        /// <summary>
        /// Altera um produto.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///    
        ///     {
        ///         "id": 11,
        ///         "name": "Produto Teste",
        ///         "description": "Pen blue bic",
        ///         "price": 2.60,
        ///         "categoryId": 6
        ///     }
        ///
        /// </remarks>
        /// <param name="updateProductModel">Objeto com dados do produto</param>
        /// <param name="cancellationToken"></param>	
        /// <response code="200">Retorna o produto alterado</response>
        /// <response code="400">Se o produto não for alterado</response>
        /// <response code="404">Se não for encontrado um produto de acordo com o Id informado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpPut]
        [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDataResult<IList<ValidationFailure>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDataResult<IList<ValidationFailure>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductViewModel updateProductModel, CancellationToken cancellationToken = default)
        {
            if (updateProductModel is null)
                return ErrorDataResult(new ValidationFailure("UpdateProductModel", "O objeto informado não pode ser nulo"));

            var validationResult = _updateProductValidator.Validate(updateProductModel);
            if (!validationResult.IsValid)
                return ErrorDataResult(validationResult.Errors);

            var result = await _productService.UpdateAsync(updateProductModel, cancellationToken).ConfigureAwait(false);

            if (result is null)
                return NotFoundResult("Não foi encontrada nenhum produto com o id informado");

            return SuccessDataResult(result, "Produto atualizado com sucesso");
        }

        /// <summary>
        /// Deleta uma produto.
        /// </summary>
        /// <param name="productId" example="123">Id do produto.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">Retorna se o produto foi deletado</response>
        /// <response code="400">Se o produto não for deletado</response>
        /// <response code="404">Se não for encontrado um produto de acordo com o Id informado</response>
        /// <response code="500">Erro de exceção</response>
        [HttpDelete("{productId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorDataResult<IList<ValidationFailure>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDataResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(long productId, CancellationToken cancellationToken = default)
        {
            if (productId <= 0)
                return ErrorDataResult(new ValidationFailure("ProductId", "O id informado nao pode ser menor ou igual a zero"));

            var result = await _productService.DeleteAsync(productId, cancellationToken).ConfigureAwait(false);
            if (!result)
                return NotFoundResult("Não foi encontrada nenhum produto com o id informado");

            return NoContent();
        }
    }
}
