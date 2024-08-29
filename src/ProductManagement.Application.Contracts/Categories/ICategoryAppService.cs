using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductManagement.Categories;

public interface ICategoryAppService : IApplicationService
{
    Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input);
}