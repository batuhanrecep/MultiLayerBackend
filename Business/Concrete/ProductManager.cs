using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }


        [CacheAspect(duration:10)]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }


        //Cross Cutting Concerns Examples: Validation (before), Cache (usually before from list operations), Log (before or after), Performance (before and after), Auth, Transaction
        //AOP - Aspect Oriented Programming  should only use for cross-cutting concerns 
        //For aspects, I will use Autofac. Alternative of autofac is Postsharp. Its also good for developing aspects
        [ValidationAspect(typeof(ProductValidator),Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //These are not the best ways. Use cross-cutting concerns
            //ValidationTool.Validate(new ProductValidator(),product);
            //or
            //ProductValidator productValidator=new ProductValidator();
            //var result = productValidator.Validate(product);
            //if(...) etc.



            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        //TEST OPERATİON
        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            // _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
  
}
