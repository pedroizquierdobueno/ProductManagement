using System;
using Volo.Abp.Application.Dtos;

namespace ProductManagement.Categories;

public class CategoryDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }
}