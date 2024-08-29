using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Categories;

public class CategoryAppService(IRepository<Category, Guid> categoryRepository)
    : ProductManagementAppService, ICategoryAppService
{
    private readonly IRepository<Category, Guid> _categoryRepository = categoryRepository;

    public async Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        IQueryable<Category> queryable = await _categoryRepository
                                .GetQueryableAsync();

        queryable = queryable
                        .Skip(input.SkipCount)
                        .Take(input.MaxResultCount)
                        .OrderBy(input.Sorting ?? nameof(Category.Name));

        List<Category> categories = await AsyncExecuter.ToListAsync(queryable);

        long count = await _categoryRepository.GetCountAsync();

        return new PagedResultDto<CategoryDto>(count, ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories));
    }
}