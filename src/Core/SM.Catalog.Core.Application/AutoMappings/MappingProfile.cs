using AutoMapper;
using SM.Catalog.Core.Application.Commands.Category;
using SM.Catalog.Core.Application.Commands.Product;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Domain.Entities;
using SM.MQ.Models.Category;
using SM.MQ.Models.Product;
using SM.MQ.Models.Supplier;

namespace SM.Catalog.Core.Application.AutoMappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Add Category Command
            CreateMap<CategoryModel, AddCategoryCommand>().ReverseMap();
            CreateMap<AddCategoryCommand, Category>().ReverseMap();

            //Update Category Command
            CreateMap<CategoryModel, UpdateCategoryCommand>().ReverseMap();
            CreateMap<UpdateCategoryCommand, Category>().ReverseMap();

            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Category, ResponseCategoryOut>().ReverseMap();
            CreateMap<CategoryModel, ResponseCategoryOut>().ReverseMap();


            //Add Product Command
            CreateMap<ProductModel, AddProductCommand>().ReverseMap();
            CreateMap<AddProductCommand, Product>().ReverseMap();

            //Update Product Command
            CreateMap<ProductModel, UpdateProductCommand>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();

            CreateMap<Supplier, SupplierModel>().ReverseMap();
            CreateMap<Supplier, ResponseSupplierOut>().ReverseMap();

            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.ResponseCategoryOut, act => act.MapFrom(src => src.Category))
                .ForMember(dest => dest.ResponseSupplierOut, act => act.MapFrom(src => src.Supplier))
                .ReverseMap();

            CreateMap<ProductModel, ResponseProductOut>()
                .ForMember(dest => dest.ResponseCategoryOut, act => act.MapFrom(src => src.ResponseCategoryOut))
                .ForMember(dest => dest.ResponseSupplierOut, act => act.MapFrom(src => src.ResponseSupplierOut))
                .ReverseMap();
        }
    }
}
