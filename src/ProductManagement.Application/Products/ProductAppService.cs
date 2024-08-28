using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Products;

public class ProductAppService(IRepository<Product, Guid> productRepository) : ProductManagementAppService, IProductAppService
{
	private readonly IRepository<Product, Guid> _productRepository = productRepository;

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
}