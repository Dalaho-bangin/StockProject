using Application.ProductCategories;
using AutoMapper;
using Domain.ProductCategories;


namespace Infrastructure.MappingProfile
{
    public class ProductCategoryMapping:Profile
    {
        public ProductCategoryMapping()
        {
            CreateMap<ProductCategory, CreateProductCategoryDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<ProductCategory, DeleteProductCategory>().ReverseMap();
        }
    }
}
