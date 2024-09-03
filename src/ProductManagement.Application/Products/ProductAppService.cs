﻿using ProductManagement.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Products;

public class ProductAppService(IRepository<Product, Guid> productRepository, IRepository<Category, Guid> categoryRepository)
	: ProductManagementAppService, IProductAppService
{
	private readonly IRepository<Product, Guid> _productRepository = productRepository;
    private readonly IRepository<Category, Guid> _categoryRepository = categoryRepository;

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

	public async Task CreateAsync(CreateUpdateProductDto input)
	{
		await _productRepository.InsertAsync(ObjectMapper.Map<CreateUpdateProductDto, Product>(input));
	}

	public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
	{
		List<Category> categories = await _categoryRepository.GetListAsync();

		return new ListResultDto<CategoryLookupDto>(ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories));
	}

	public async Task<ProductDto> GetAsync(Guid id)
	{
		return ObjectMapper.Map<Product, ProductDto>(await _productRepository.GetAsync(id));
	}

	public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
	{
		Product product = await _productRepository.GetAsync(id);

		ObjectMapper.Map(input, product);
	}

	public async Task DeleteAsync(Guid id)
	{
        await _productRepository.DeleteAsync(id);
	}
}