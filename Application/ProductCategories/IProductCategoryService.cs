using Application.Interfaces.Context;
using AutoMapper;
using Common;
using Domain.ProductCategories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductCategories
{
    public interface IProductCategoryService
    {
        #region Get All Product Categories

        List<TreeViewProductCategory> GetAllProductCategories();


        void BindSubProductCategories(TreeViewProductCategory productCategories);


        #endregion

        #region Is Exist Product Category

        bool IsExistProductCategory(string productCategoryName);

        #endregion

        #region Find Product Category With Id

        ProductCategory FindProductCategoryWithId(long productCategoryId);

        #endregion

        #region Find Product Category By Product Category Name

        ProductCategory FindProductCategoryByProductCategoryName(string productCategoryName);


        #endregion

        #region Create

        CreateProductCategoryResult Create(CreateProductCategoryDto dto);

        #endregion

        #region List  Product Categories

        List<ProductCategoryDto> ListProductCategories();


        #endregion

        #region Find Product Category For Edit
        CreateProductCategoryDto FindProductCategoryForEdit(long productCategoryId);
        #endregion

        #region Edit

        EditProductCategoryResult Edit(CreateProductCategoryDto dto);

        #endregion

        #region Find Product Category For Delete

        DeleteProductCategory FindProductCategoryForDelete(long productCategoryId);

        #endregion

        #region Delete

        DeleteProductCategoryResult Delete(DeleteProductCategory dto);
  

        #endregion
    }

    public class ProductCategoryService : IProductCategoryService
    {

        #region Constructor

        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        #endregion

        #region Get All Product Categories

        public List<TreeViewProductCategory> GetAllProductCategories()
        {
            var productCategories = _context.ProductCategories.Where(x => x.ParentProductCategoryId == null)
                .Select(x => new TreeViewProductCategory()
                {
                    id = x.ProductCategoryId,
                    title = x.ProductCategoryName,
                    Slug = x.Slug
                }
                ).ToList();

            foreach (var item in productCategories)
            {
                BindSubProductCategories(item);
            }

            return productCategories;
        }

        public void BindSubProductCategories(TreeViewProductCategory productCategories)
        {
            var subProductCategories = _context.ProductCategories.Where(x => x.ParentProductCategoryId == productCategories.id)
                .Select(x => new TreeViewProductCategory()
                {
                    id = x.ProductCategoryId,
                    title = x.ProductCategoryName,
                    Slug = x.Slug
                }).ToList();

            foreach (var item in subProductCategories)
            {
                BindSubProductCategories(item);
                productCategories.subs.Add(item);
            }
        }

        #endregion

        #region Create

        public CreateProductCategoryResult Create(CreateProductCategoryDto dto)
        {
            if (IsExistProductCategory(dto.ProductCategoryName))
                return CreateProductCategoryResult.IsExistProductCategory;
            if (dto.ParentProductCategoryName != null)
            {
                var parentProductCategory = FindProductCategoryByProductCategoryName(dto.ParentProductCategoryName);
                if (parentProductCategory == null)
                {

                    ProductCategory parent = new ProductCategory()
                    {
                        ProductCategoryName = dto.ParentProductCategoryName,
                        Slug = dto.ParentProductCategoryName
                    };
                    _context.ProductCategories.Add(parent);
                    _context.SaveChanges();

                    dto.ParentProductCategoryId = parent.ProductCategoryId;
                    dto.ParentProductCategoryName = parent.ProductCategoryName;
                }
                else
                {
                    dto.ParentProductCategoryId = parentProductCategory.ProductCategoryId;
                }

            }

            var data = _mapper.Map<ProductCategory>(dto);
            _context.ProductCategories.Add(data);
            _context.SaveChanges();

            return CreateProductCategoryResult.CreateSuccess;
        }

        #endregion

        #region Is Exist Product Category

        public bool IsExistProductCategory(string productCategoryName)
        {
            return _context.ProductCategories.Any(x => x.ProductCategoryName == productCategoryName);
        }

        #endregion

        #region Find Product Category By Product Category Name

        public ProductCategory FindProductCategoryByProductCategoryName(string productCategoryName)
        {
            return _context.ProductCategories.FirstOrDefault(x => x.ProductCategoryName == productCategoryName);
        }

        #endregion

        #region List  Product Categories

        public List<ProductCategoryDto> ListProductCategories()
        {

            var data = _context.ProductCategories.AsQueryable();

            var result = _mapper.ProjectTo<ProductCategoryDto>(data).ToList();

            return result;

        }

        #endregion

        #region Find Product Category For Edit

        public CreateProductCategoryDto FindProductCategoryForEdit(long productCategoryId)
        {
            var productCategory = _context.ProductCategories.FirstOrDefault(x => x.ProductCategoryId == productCategoryId);
            if (productCategory == null)
                return null;
            var result = _mapper.Map<CreateProductCategoryDto>(productCategory);

            return result;
        }


        #endregion

        #region Find Product Category With Id

        public ProductCategory FindProductCategoryWithId(long productCategoryId)
        {
            return _context.ProductCategories.FirstOrDefault(x => x.ProductCategoryId == productCategoryId);
        }

        #endregion

        #region Edit

        public EditProductCategoryResult Edit(CreateProductCategoryDto dto)
        {
            var data = FindProductCategoryWithId(dto.ProductCategoryId);

            if (data == null)
                return EditProductCategoryResult.NotFound;

            if (IsExistProductCategory(dto.ProductCategoryName) && data.ProductCategoryId != dto.ProductCategoryId)
                return EditProductCategoryResult.IsExistProductCategory;



            if (dto.ParentProductCategoryName != null && data.ParentProductCategoryName != dto.ParentProductCategoryName)
            {
                var parentProductCategory = FindProductCategoryByProductCategoryName(dto.ParentProductCategoryName);
                if (parentProductCategory == null)
                {

                    ProductCategory parent = new ProductCategory()
                    {
                        ProductCategoryName = dto.ParentProductCategoryName,
                        Slug = dto.ParentProductCategoryName
                    };
                    _context.ProductCategories.Add(parent);
                    _context.SaveChanges();

                    dto.ParentProductCategoryId = parent.ProductCategoryId;
                    dto.ParentProductCategoryName = parent.ProductCategoryName;
                }
                else
                {
                    dto.ParentProductCategoryId = parentProductCategory.ProductCategoryId;
                }
            }
            dto.Slug = dto.Slug.Slugify();

            data.ProductCategoryName = dto.ProductCategoryName;
            data.Slug = dto.Slug;
            data.ParentProductCategoryId = dto.ParentProductCategoryId;
            data.ParentProductCategoryName = dto.ParentProductCategoryName;

            _context.ProductCategories.Update(data);
            _context.SaveChanges();

            return EditProductCategoryResult.EditSuccess;

        }


        #endregion

        #region Find Product Category For Delete

        public DeleteProductCategory FindProductCategoryForDelete(long productCategoryId)
        {
            var productCategory = _context.ProductCategories.FirstOrDefault(x => x.ProductCategoryId == productCategoryId);
            if (productCategory == null)
                return null;
            var result = _mapper.Map<DeleteProductCategory>(productCategory);

            return result;
        }


        #endregion

        #region Delete

        public DeleteProductCategoryResult Delete(DeleteProductCategory dto)
        {
            var data = FindProductCategoryWithId(dto.ProductCategoryId);

            if (data == null)
                return DeleteProductCategoryResult.NotFound;

            if (_context.ProductCategories.Any(x => x.ParentProductCategoryId == data.ProductCategoryId))
            {
                _context.ProductCategories.Where(x => x.ParentProductCategoryId == data.ProductCategoryId).ToList()
           .ForEach(x => _context.ProductCategories.Remove(x));
            };

            _context.ProductCategories.Remove(data);
            _context.SaveChanges();

            return DeleteProductCategoryResult.DeleteSuccess;

        }

        #endregion
    }


    public class ProductCategoryDto
    {

        public long ProductCategoryId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        public string ProductCategoryName { get; set; }


        [Display(Name = "دسته ی پدر")]
        public string ParentProductCategoryName { get; set; }


    }

    public class CreateProductCategoryDto
    {

        public long ProductCategoryId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string ProductCategoryName { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        public long ParentProductCategoryId { get; set; }

        [Display(Name = "دسته ی پدر")]
        public string ParentProductCategoryName { get; set; }


    }

    public enum CreateProductCategoryResult
    {
        CreateSuccess,
        IsExistProductCategory
    }

    public enum EditProductCategoryResult
    {
        EditSuccess,
        IsExistProductCategory,
        NotFound
    }

    public enum DeleteProductCategoryResult
    {
        DeleteSuccess,
        NotFound
    }

    public class DeleteProductCategory
    {
        public long ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }
    }

    public class TreeViewProductCategory
    {
        public TreeViewProductCategory()
        {
            subs = new List<TreeViewProductCategory>();
        }
        public long id { get; set; }
        public string title { get; set; }
        public string Slug { get; set; }
        public List<TreeViewProductCategory> subs { get; set; }
    }

}
