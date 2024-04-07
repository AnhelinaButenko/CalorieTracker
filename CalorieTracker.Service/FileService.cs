using CalorieTracker.Data.Repository;
using CalorieTracker.Data;
using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;
using CalorieTracker.Api.Models;

namespace CalorieTracker.Service;

public class FileService : IFileService
{
    private readonly IProductRepository _productRepository;
    private readonly IManufacturerRepository _manufacturerRepository;

    public FileService(IProductRepository productRepository,
        IManufacturerRepository manufacturerRepository)
    {
        _productRepository = productRepository
            ?? throw new ArgumentNullException(nameof(productRepository));
        _manufacturerRepository = manufacturerRepository
            ?? throw new ArgumentNullException(nameof(manufacturerRepository));
    }

    public async Task WorkWithFileAsync(List<ImportProductDto> products)
    {
        foreach (ImportProductDto product in products)
        {
            Product productByName = await _productRepository.GetByName(product.ProductName);

            Manufacturer? manufacturerByName = null;

            if (!string.IsNullOrWhiteSpace(product.ManufacturerName))
            {
                manufacturerByName = await _manufacturerRepository.GetByName(product.ManufacturerName);

                if (manufacturerByName == null)
                {
                    manufacturerByName = new()
                    {
                        Name = product.ManufacturerName,
                    };

                    await _manufacturerRepository.Add(manufacturerByName);
                }
            }

            if (productByName != null)
            {
                productByName.Name = product.ProductName;
                productByName.CaloriePer100g = product.CaloryInfo.CaloriePer100g;
                productByName.ProteinPer100g = product.CaloryInfo.ProteinPer100g;
                productByName.FatPer100g = product.CaloryInfo.FatPer100g;
                productByName.CarbohydratePer100g = product.CaloryInfo.CarbohydratePer100g;

                if (product.ManufacturerName != null)
                {
                    productByName.Name = product.ManufacturerName;
                }

                await _productRepository.Update(productByName.Id, productByName);
            }
            else
            {
                Product newProduct = new()
                {
                    Name = product.ProductName,
                    CaloriePer100g = product.CaloryInfo.CaloriePer100g,
                    ProteinPer100g = product.CaloryInfo.ProteinPer100g,
                    FatPer100g = product.CaloryInfo.FatPer100g,
                    CarbohydratePer100g = product.CaloryInfo.CarbohydratePer100g,
                };

                if (product.ManufacturerName != null)
                {
                    newProduct.ManufacturerId = manufacturerByName?.Id;
                }

                await _productRepository.Add(newProduct);
            }
        }
    }
}
