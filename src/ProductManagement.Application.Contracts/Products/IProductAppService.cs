using System;
using System.Threading.Tasks;
using ProductManagement.Categories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductManagement.Products;

public interface IProductAppService : IApplicationService
{
    /// <summary>
    /// Retrieves a paged list of ProductDto objects based on the provided input.
    /// </summary>
    /// <param name="input">
    ///	Input
    /// </param>
    /// <returns>
    /// Task <see cref="PagedResultDto{ProductDto}"/>
    /// </returns>
    Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input);

    /// <summary>
    ///  Creates a new product based on the provided input.
    /// </summary>
    /// <param name="input">
    /// Input
    /// </param>
    /// <returns>
    /// Task
    /// </returns>
    Task CreateAsync(CreateUpdateProductDto input);

    /// <summary>
    /// Retrieves a list of CategoryLookupDto objects, which are likely used for lookup purposes.
    /// </summary>
    /// <returns>
    /// Task <see cref="ListResultDto{CategoryLookupDto}"/>
    /// </returns>
    Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync();

    /// <summary>
    ///  Retrieves a single ProductDto object based on the provided id.
    /// </summary>
    /// <param name="id">
    /// Product id
    /// </param>
    /// <returns>
    /// Task <see cref="ProductDto"/>
    /// </returns>
    Task<ProductDto> GetAsync(Guid id);

    /// <summary>
    /// Updates an existing product with the provided id using the provided input.
    /// </summary>
    /// <param name="id">
    /// Product id
    /// </param>
    /// <param name="input">
    /// Input
    /// </param>
    /// <returns>
    /// Task
    /// </returns>
    Task UpdateAsync(Guid id, CreateUpdateProductDto input);

    /// <summary>
    /// Deletes a product with the provided id.
    /// </summary>
    /// <param name="id">
    /// Product id
    /// </param>
    /// <returns>
    /// Task
    /// </returns>
    Task DeleteAsync(Guid id);
}