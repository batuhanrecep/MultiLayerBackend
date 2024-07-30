using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList());
        }

        //public IDataResult<Category> GetById(int productId)
        //{
        //    return new SuccessDataResult<Category>(_categoryDal.Get(p => p.CategoryId == productId));
        //}



        //public IDataResult<List<Category>> GetListByCategory(int categoryId)
        //{
        //    return new SuccessDataResult<List<Category>>(_categoryDal.GetList(p => p.CategoryId == categoryId).ToList());
        //}


        //public IResult Add(Category category)
        //{
        //    _categoryDal.Add(category);
        //    return new SuccessResult(Messages.ProductAdded);

        //}


        //public IResult Update(Category category)
        //{
        //    _categoryDal.Update(category);
        //    return new SuccessResult(Messages.ProductUpdated);
        //}


        //public IResult Delete(Category category)
        //{
        //    _categoryDal.Delete(category);
        //    return new SuccessResult(Messages.ProductDeleted);
        //}
    }
}
