using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetList();
        //IDataResult<Category> GetById(int productId);
        //IDataResult<List<Category>> GetListByCategory(int categoryId);
        //IResult Add(Category product);
        //IResult Update(Category product);
        //IResult Delete(Category product);
    }
}
