using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WK.Technology.Teste.Domain.Entities;
using WK.Technology.Teste.Domain.Filters;
using WK.Technology.Teste.Domain.Interfaces.Services;
using WK.Technology.Teste.Domain.ViewModel.Product;
using WK.Technology.Teste.Infra.Data.FakeData.Product;
using WK.Technology.Teste.Infra.Results;
using WK.Technology.Teste.WebApi.Controllers.v1;

namespace WK.Technology.Teste.Tests.Controllers
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _productService;
        private readonly Mock<IValidator<ProductViewModel>> _productValidator;
        private readonly Mock<IValidator<CreateProductViewModel>> _createProductValidator;
        private readonly Mock<IValidator<UpdateProductViewModel>> _updateProductValidator;
        private readonly Mock<ILogger<ProductController>> _logger;
        private readonly ProductController _controller;
        private readonly Product _product;
        private readonly ProductFilter _productFilter;
        private readonly List<ProductViewModel> _productList;
        private readonly ProductViewModel _productViewModel;
        private readonly CreateProductViewModel _createProductViewModel;
        private readonly UpdateProductViewModel _updateProductViewModel;

        public ProductControllerTest()
        {
            _productService = new Mock<IProductService>();
            _productValidator = new Mock<IValidator<ProductViewModel>>();
            _createProductValidator = new Mock<IValidator<CreateProductViewModel>>();
            _updateProductValidator = new Mock<IValidator<UpdateProductViewModel>>();
            _logger = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_productService.Object, _productValidator.Object, _createProductValidator.Object, _updateProductValidator.Object);
            _product = new ProductFaker().Generate();
            _productList = new ProductViewModelFaker().Generate(3);
            _productFilter = new ProductFilterFaker().Generate();
            _productViewModel = new ProductViewModelFaker().Generate();
            _createProductViewModel = new CreateProductViewModelFaker().Generate();
            _updateProductViewModel = new UpdateProductViewModelFaker().Generate();
        }

        [Fact(DisplayName = "Get product paged (200 - OK)")]
        [Trait("Product", "ProductController")]
        public async Task Get_Product_Paged_Ok()
        {
            //Arranje
            var controle = new List<ProductViewModel>();
            _productList.ForEach(p => controle.Add(p.CloneTipado()));
            _productService.Setup(x => x.GetPagedAsync(It.IsAny<ProductFilter>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult(_productList.ToPagedList(_productFilter.PageIndex, _productFilter.PageSize)));

            //Act
            var response = (ObjectResult)await _controller.GetProductsPaged();

            //Assert
            _productService.Verify(x => x.GetPagedAsync(It.IsAny<ProductFilter>(), It.IsAny<CancellationToken>()), Times.Once());
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.Should().BeEquivalentTo(new SuccessDataResult<IPagedList<ProductViewModel>>(controle.ToPagedList(_productFilter.PageIndex, _productFilter.PageSize)));
        }

        [Fact(DisplayName = "Get product paged (404 - Not Found)")]
        [Trait("Product", "ProductController")]
        public async Task Get_Product_Paged_NotFound()
        {
            //Arranje
            _productService.Setup(x => x.GetPagedAsync(It.IsAny<ProductFilter>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult((IPagedList<ProductViewModel>)null));

            //Act
            var response = (NotFoundObjectResult)await _controller.GetProductsPaged();

            //Assert
            _productService.Verify(x => x.GetPagedAsync(It.IsAny<ProductFilter>(), It.IsAny<CancellationToken>()), Times.Once());
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            response.Value.Should().BeEquivalentTo(new ErrorResult("Não foi encontrada nenhum produto com os parâmetros informados"));
        }

        [Fact(DisplayName = "Get product by Id (200 - OK)")]
        [Trait("Product", "ProductController")]
        public async Task Get_Product_By_Id_Ok()
        {
            //Arranje
            _productService.Setup(x => x.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult(_productViewModel.CloneTipado()));

            //Act
            var response = (ObjectResult)await _controller.GetProductById(10);

            //Assert
            _productService.Verify(x => x.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once());
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.Should().BeEquivalentTo(new SuccessDataResult<ProductViewModel>(_productViewModel));
        }

        [Fact(DisplayName = "Get product by Id (404 - Not Found)")]
        [Trait("Product", "ProductController")]
        public async Task Get_Product_By_Id_NotFound()
        {
            //Arranje
            _productService.Setup(x => x.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult((ProductViewModel)null));

            //Act
            var response = (NotFoundObjectResult)await _controller.GetProductById(10);

            //Assert
            _productService.Verify(x => x.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once());
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            response.Value.Should().BeEquivalentTo(new ErrorResult("Não foi encontrada nenhum produto com o id informado"));
        }

        [Fact(DisplayName = "Add product (201 - Created)")]
        [Trait("Product", "ProductController")]
        public async Task Add_Product_Created()
        {
            //Arranje
            _productService.Setup(x => x.InsertAsync(It.IsAny<CreateProductViewModel>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult(_productViewModel));
            _createProductValidator.Setup(x => x.Validate(It.IsAny<CreateProductViewModel>()))
                                     .Returns(new ValidationResult());

            //Act
            var response = (ObjectResult)await _controller.AddProduct(_createProductViewModel);

            //Assert
            _productService.Verify(x => x.InsertAsync(It.IsAny<CreateProductViewModel>(), It.IsAny<CancellationToken>()), Times.Once);
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Value.Should().BeEquivalentTo(_productViewModel.CloneTipado());
        }

        [Fact(DisplayName = "Add product (400 - Bad Request)")]
        [Trait("Product", "ProductController")]
        public async Task Add_Product_Bad_Request()
        {
            //Arranje
            var validationResult = new ValidationResult(new List<ValidationFailure>()
            {
                new ValidationFailure("Name", "É necessário informar o nome do produto"),
                new ValidationFailure("Name", "O nome do produto não pode conter mais de 150 caracteres"),
                new ValidationFailure("Name", "O nome do produto não pode conter menos de 3 caracteres"),
            });

            _createProductValidator.Setup(x => x.Validate(It.IsAny<CreateProductViewModel>())).Returns(validationResult);

            //Act
            var response = (ObjectResult)await _controller.AddProduct(_createProductViewModel);

            //Assert
            _productService.Verify(x => x.InsertAsync(It.IsAny<CreateProductViewModel>(), It.IsAny<CancellationToken>()), Times.Never);
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            response.Value.Should().BeEquivalentTo(new ErrorDataResult<IList<ValidationFailure>>(validationResult.Errors));
        }

        [Fact(DisplayName = "Add product when ViewModel is null (400 - Bad Request)")]
        [Trait("Product", "ProductController")]
        public async Task Add_Product_Bad_Request_When_ViewModel_IsNull()
        {
            //Arranje 
            var validationResult = new ValidationFailure("CreateProductViewModel", "O objeto informado não pode ser nulo");

            //Act
            var response = (ObjectResult)await _controller.AddProduct(null);

            //Assert
            _productService.Verify(x => x.InsertAsync(It.IsAny<CreateProductViewModel>(), It.IsAny<CancellationToken>()), Times.Never);
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            response.Value.Should().BeEquivalentTo(new ErrorDataResult<ValidationFailure>(validationResult));
        }

        [Fact(DisplayName = "Edit product (200 - Ok)")]
        [Trait("Product", "ProductController")]
        public async Task Edit_Product_Ok()
        {
            //Arranje
            _productService.Setup(x => x.UpdateAsync(It.IsAny<UpdateProductViewModel>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult(_productViewModel));
            _updateProductValidator.Setup(x => x.Validate(It.IsAny<UpdateProductViewModel>())).Returns(new ValidationResult());

            //Act
            var response = (ObjectResult)await _controller.UpdateProduct(_updateProductViewModel);

            //Assert
            _productService.Verify(x => x.UpdateAsync(It.IsAny<UpdateProductViewModel>(), It.IsAny<CancellationToken>()), Times.Once);
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.Should().BeEquivalentTo(new SuccessDataResult<ProductViewModel>(_productViewModel.CloneTipado(), "Produto atualizado com sucesso"));
        }

        [Fact(DisplayName = "Edit product (404 - Not Found)")]
        [Trait("Product", "ProductController")]
        public async Task Edit_Product_NotFound()
        {
            //Arranje
            _productService.Setup(x => x.UpdateAsync(It.IsAny<UpdateProductViewModel>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult((ProductViewModel)null));
            _updateProductValidator.Setup(x => x.Validate(It.IsAny<UpdateProductViewModel>()))
                            .Returns(new ValidationResult());

            //Act
            var response = (NotFoundObjectResult)await _controller.UpdateProduct(_updateProductViewModel);

            //Assert
            _productService.Verify(x => x.UpdateAsync(It.IsAny<UpdateProductViewModel>(), It.IsAny<CancellationToken>()), Times.Once);
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            response.Value.Should().BeEquivalentTo(new ErrorResult("Não foi encontrada nenhum produto com o id informado"));
        }

        [Fact(DisplayName = "Edit product (400 - Bad Request)")]
        [Trait("Product", "ProductController")]
        public async Task Editproduct_Bad_Request()
        {
            //Arranje
            var validationResult = new ValidationResult(new List<ValidationFailure>()
            {
                new ValidationFailure("Name", "É necessário informar o nome do produto"),
                new ValidationFailure("Name", "O nome do produto não pode conter mais de 150 caracteres"),
                new ValidationFailure("Name", "O nome do produto não pode conter menos de 3 caracteres"),
            });

            _updateProductValidator.Setup(x => x.Validate(It.IsAny<UpdateProductViewModel>()))
                                     .Returns(validationResult);

            //Act
            var response = (ObjectResult)await _controller.UpdateProduct(_updateProductViewModel);

            //Assert
            _productService.Verify(x => x.UpdateAsync(It.IsAny<UpdateProductViewModel>(), It.IsAny<CancellationToken>()), Times.Never);
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            response.Value.Should().BeEquivalentTo(new ErrorDataResult<IList<ValidationFailure>>(validationResult.Errors));
        }

        [Fact(DisplayName = "Edit product when ViewModel is null (400 - Bad Request)")]
        [Trait("Product", "ProductController")]
        public async Task Edit_Product_Bad_Request_When_ViewModel_IsNull()
        {
            //Arranje 
            var validationResult = new ValidationFailure("UpdateProductModel", "O objeto informado não pode ser nulo");

            //Act
            var response = (ObjectResult)await _controller.UpdateProduct(null);

            //Assert
            _productService.Verify(x => x.UpdateAsync(It.IsAny<UpdateProductViewModel>(), It.IsAny<CancellationToken>()), Times.Never);
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            response.Value.Should().BeEquivalentTo(new ErrorDataResult<ValidationFailure>(validationResult));
        }

        [Fact(DisplayName = "Delete product (204 - No Content)")]
        [Trait("Product", "ProductController")]
        public async Task Delete_Product_NoContent()
        {
            //Arranje
            _productService.Setup(x => x.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            //Act
            var response = (NoContentResult)await _controller.DeleteProduct(1);

            //Assert
            _productService.Verify(x => x.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact(DisplayName = "Delete product (404 - Not Found)")]
        [Trait("Product", "ProductController")]
        public async Task NotFound_Product_NotFound()
        {
            //Arranje
            _productService.Setup(x => x.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(false));

            //Act
            var response = (NotFoundObjectResult)await _controller.DeleteProduct(1);

            //Assert
            _productService.Verify(x => x.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Theory(DisplayName = "Edit product when productId is less than 0 (400 - Bad Request)")]
        [Trait("Product", "ProductController")]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Delete_Product_Bad_Request_When_productId_Less_Than_0(long id)
        {
            //Arranje 
            var validationResult = new ValidationFailure("ProductId", "O id informado nao pode ser menor ou igual a zero");

            //Act
            var response = (ObjectResult)await _controller.DeleteProduct(id);

            //Assert
            _productService.Verify(x => x.UpdateAsync(It.IsAny<UpdateProductViewModel>(), It.IsAny<CancellationToken>()), Times.Never);
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            response.Value.Should().BeEquivalentTo(new ErrorDataResult<ValidationFailure>(validationResult));
        }
    }
}
