using Microsoft.AspNetCore.Authorization;
using ProductManagement.Categories;
using ProductManagement.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Products;

public class ProductAppService(IRepository<Product, Guid> productRepository, IRepository<Category, Guid> categoryRepository
	, IAuthorizationService authorizationService)
	: ProductManagementAppService, IProductAppService
{
	private readonly IRepository<Product, Guid> _productRepository = productRepository;
    private readonly IRepository<Category, Guid> _categoryRepository = categoryRepository;
	private readonly IAuthorizationService _authorizationService = authorizationService;

    /// <summary>
    /// This method provides a way to get a list of products from the system
    /// </summary>
    /// <param name="input"> Specifies the paging and sorting options for the query </param>
    /// <returns> a PagedResultDto object containing the total count and the list of ProductDto objects </returns>
    public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
	{
		IQueryable<Product> queryable = await _productRepository
								.WithDetailsAsync(p => p.Category);

		queryable = queryable
						.Skip(input.SkipCount)
						.Take(input.MaxResultCount)
						.OrderBy(input.Sorting ?? nameof(Product.Name));

		List<Product> products = await AsyncExecuter.ToListAsync(queryable);

		long count = await _productRepository.GetCountAsync();

		return new PagedResultDto<ProductDto>(count, ObjectMapper.Map<List<Product>, List<ProductDto>>(products));
	}

	/// <summary>
	/// This method provides a way to create a product in the system
	/// </summary>
	/// <param name="input"> The product to be created </param>
	/// <returns> </returns>
	public async Task CreateAsync(CreateUpdateProductDto input)
	{
		if(await _authorizationService.IsGrantedAsync(ProductManagementPermissions.ProductCreation))
		{
            await _productRepository.InsertAsync(ObjectMapper.Map<CreateUpdateProductDto, Product>(input));
        }
		else
		{
			// TODO: Handle unauthorized exception case
		}
	}

	/// <summary>
	/// This method provides a way to get a list of categories from the system
	/// </summary>
	/// <returns> A task that represents the asynchronous operation </returns>
	public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
	{
		List<Category> categories = await _categoryRepository.GetListAsync();

		return new ListResultDto<CategoryLookupDto>(ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories));
	}

	/// <summary>
	/// This method provides a way to get a product from the system after ensuring that the user has the necessary permissions to do so.
	/// </summary>
	/// <param name="id"> The unique identifier of the product to be retrieved </param>
	/// <returns> A task that represents the asynchronous operation </returns>
	public async Task<ProductDto> GetAsync(Guid id)
	{
		return ObjectMapper.Map<Product, ProductDto>(await _productRepository.GetAsync(id));
	}

	/// <summary>
	/// This method provides a way to update a product in the system after ensuring that the user has the necessary permissions to do so.
	/// </summary>
	/// <param name="id"> The unique identifier of the product to be updated </param>
	/// <param name="input"></param>
	/// <returns> A task that represents the asynchronous operation </returns>
	[Authorize("ProductManagementPermissions.ProductUpdate")]
	public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
	{
		Product product = await _productRepository.GetAsync(id);

		ObjectMapper.Map(input, product);
	}

    /// <summary>
    /// This method provides a way to delete a product from the system after ensuring that the user has the necessary permissions to do so.
    /// </summary>
    /// <param name="id"> The unique identifier of the product to be deleted </param>
    /// <returns> A task that represents the asynchronous operation </returns>
    public async Task DeleteAsync(Guid id)
	{
		await _authorizationService.CheckAsync(ProductManagementPermissions.ProductDeletion);
        await _productRepository.DeleteAsync(id);
	}
}