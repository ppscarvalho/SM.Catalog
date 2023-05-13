using AutoMapper;
using SM.Catalog.Core.Application.Commands.Category;
using SM.Catalog.Core.Application.Models;
using SM.Catalog.Core.Domain.Entities;
using SM.MQ.Models.Categoria;

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
            CreateMap<CategoryModel, ResponseCategoriaOut>().ReverseMap();


            //Response Categoria Out
            //CreateMap<CategoryModel, ResponseCategoriaOut>().ReverseMap();
            //CreateMap<ResponseCategoriaOut, AdicionarCategoriaCommand>().ReverseMap();
            //CreateMap<ResponseCategoriaOut, AlterarCategoriaCommand>().ReverseMap();

            //Produto
            //CreateMap<ProdutoViewModel, AdicionarProdutoCommand>().ReverseMap();
            //CreateMap<AdicionarProdutoCommand, LojaInspiracao.Produto>().ReverseMap();

            //CreateMap<Product, ProdutoViewModel>().ForMember(
            //    dest => dest.CategoriaViewModel,
            //    opt => opt.MapFrom(b => b.Categoria)
            //    ).ReverseMap();
        }
    }
}
