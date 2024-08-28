using ProductManagement.Categories;
using ProductManagement.Products;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Data;

public class ProductManagementDataSeedContributor(IRepository<Category, Guid> categoryRepository, IRepository<Product, Guid> productRepository) :
	IDataSeedContributor, ITransientDependency
{
	private readonly IRepository<Category, Guid> _categoryRepository = categoryRepository;
	private readonly IRepository<Product, Guid> _productRepository = productRepository;

	public async Task SeedAsync(DataSeedContext context)
	{
		if (await _categoryRepository.CountAsync() > 0)
		{
			return;
		}

		Category monitors = new() { Name = "Monitors" };
		Category printers = new() { Name = "Printers" };

		await _categoryRepository
			.InsertManyAsync([monitors, printers]);

		Product monitor1 = new()
		{
			Category = monitors,
			Name = "XP VH240a 23.8-Inch Full HD 1080p IPS LED Monitor",
			Price = 163,
			ReleaseDate = new DateTime(2019, 05, 24),
			StockState = ProductStockState.InStock
		};

		Product monitor2 = new()
		{
			Category = monitors,
			Name = "Clips 328E1CA 32-Inch Curved Monitor, 4K UHD",
			Price = 349,
			IsFreeCargo = true,
			ReleaseDate = new DateTime(2022, 02, 01),
			StockState = ProductStockState.PreOrder
		};

		Product printer1 = new()
		{
			Category = printers,
			Name = "Acme Monochrome Laser Printer, Compact All-In One",
			Price = 199,
			ReleaseDate	= new DateTime(2020, 11, 19),
			StockState = ProductStockState.NotAvailable
		};

		await _productRepository
			.InsertManyAsync([monitor1, monitor2, printer1]);
	}
}