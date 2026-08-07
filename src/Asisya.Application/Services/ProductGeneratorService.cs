using Asisya.Application.Interfaces.Repositories;
using Asisya.Application.Interfaces.Services;
using Asisya.Domain.Entities;

namespace Asisya.Application.Services;

public class ProductGeneratorService : IProductGeneratorService
{
    private readonly IProductRepository _productRepository;

    private readonly ICategoryRepository _categoryRepository;

    private readonly ISupplierRepository _supplierRepository;

    public ProductGeneratorService(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _supplierRepository = supplierRepository;
    }

    public async Task<int> GenerateAsync(int quantity)
    {
        const int batchSize = 1000;

        var random = new Random();

        var batch = new List<Product>(batchSize);

        var serverProducts = new[]
        {
            "Dell PowerEdge R760",
            "HP ProLiant DL380",
            "Lenovo ThinkSystem SR650",
            "Cisco UCS C240",
            "IBM Power System S1022",
            "Dell PowerEdge R660",
            "HPE Apollo 4200",
            "Lenovo ThinkAgile HX",
            "Cisco UCS X210c",
            "Supermicro SuperServer"
        };

        var cloudProducts = new[]
        {
            "Azure Virtual Machine",
            "AWS EC2 Instance",
            "Google Compute Engine",
            "Azure Kubernetes Service",
            "AWS Elastic Kubernetes Service",
            "Google Kubernetes Engine",
            "Azure SQL Database",
            "Amazon RDS",
            "Google Cloud Storage",
            "Azure App Service"
        };

        var categories = (await _categoryRepository.GetAllAsync()).ToList();

        var suppliers = await _supplierRepository.GetAllAsync();

        if (!categories.Any())
            throw new Exception("No existen categorías.");

        if (!suppliers.Any())
            throw new Exception("No existen proveedores.");

        int inserted = 0;

        for (int i = 1; i <= quantity; i++)
        {
            var category = categories[random.Next(categories.Count)];

            var supplier = suppliers[random.Next(suppliers.Count)];

            string productName;

            string quantityPerUnit;

            decimal unitPrice;

            if (category.CategoryName.Equals("SERVIDORES", StringComparison.OrdinalIgnoreCase))
            {
                productName = serverProducts[random.Next(serverProducts.Length)];

                quantityPerUnit = "1 Servidor";

                unitPrice = random.Next(5000, 30001);
            }
            else
            {
                productName = cloudProducts[random.Next(cloudProducts.Length)];

                quantityPerUnit = "1 Suscripción";

                unitPrice = random.Next(50, 2001);
            }

            batch.Add(new Product
            {
                ProductName = $"{productName} {random.Next(1000, 9999)}",

                SupplierID = supplier.SupplierID,

                CategoryID = category.CategoryID,

                QuantityPerUnit = quantityPerUnit,

                UnitPrice = unitPrice,

                UnitsInStock = (short)random.Next(0, 100),

                UnitsOnOrder = (short)random.Next(0, 50),

                ReorderLevel = (short)random.Next(0, 10),

                Discontinued = false
            });

            if (batch.Count == batchSize)
            {
                await _productRepository.AddRangeAsync(batch);

                inserted += batch.Count;

                batch.Clear();
            }
        }

        if (batch.Any())
        {
            await _productRepository.AddRangeAsync(batch);

            inserted += batch.Count;
        }

        return inserted;
    }
}