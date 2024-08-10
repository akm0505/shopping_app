using BAL.BEEntities;
using BAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data;


namespace BAL.Classes
{
    public class eCartServices : IECart
    {
        private readonly IConfiguration _configuration;
        public eCartServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public CategoryList GetCategoryList(string type = "")
        //{
        //    eCartRepository oRepo = new eCartRepository(_configuration);
        //    var items = oRepo.GetCategoryList(type);

        //    if (items != null)
        //    {
        //        return items;
        //        //return Mapper.Map<CategoryList>(items);
        //    }
        //    return null;
        //}

        public DataSet GetCategoryList(long shopId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetCategoryList(shopId);

            if (items != null)
            {
                return items;
                //return Mapper.Map<CategoryList>(items);
            }
            return null;
        }

        //public ShopDetail GetInitialSetup(long shopId)
        //{
        //    eCartRepository oRepo = new eCartRepository(_configuration);
        //    var items = oRepo.GetInitialSetup(shopId);

        //    if (items != null)
        //    {
        //        return items;
        //    }
        //    return null;
        //}

        public DataSet GetInitialSetup(long shopId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetInitialSetup(shopId);

            if (items != null)
            {
                return items;
            }
            return null;
        }

        public DataSet GetProductDetail(long productId, long customerId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetProductDetail(productId, customerId);

            if (items != null)
            {
                return items;
            }
            return null;
        }

        //public ProductDetail GetProductDetail(long productId, long customerId)
        //{
        //    eCartRepository oRepo = new eCartRepository(_configuration);
        //    var items = oRepo.GetProductDetail(productId,customerId);

        //    if (items != null)
        //    {
        //        return items;
        //    }
        //    return null;
        //}

        //public ProductList GetProductList(long categoryId)
        //{
        //    eCartRepository oRepo = new eCartRepository(_configuration);
        //    var items = oRepo.GetProductList(categoryId);

        //    if (items != null)
        //    {
        //        return items;
        //    }
        //    return null;
        //}

        public DataSet GetProductList(long categoryId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetProductList(categoryId);

            if (items != null)
            {
                return items;
            }
            return null;
        }

        //public CustomerDetail GetCustomerDetail(string customerLoginId, long shopId, int CustomerRoleId)
        //{
        //    eCartRepository oRepo = new eCartRepository(_configuration);
        //    var items = oRepo.GetCustomerDetail(customerLoginId , shopId, CustomerRoleId);

        //    if (items != null)
        //    {
        //        return items;
        //        //return Mapper.Map<CategoryList>(items);
        //    }
        //    return null;
        //}

        public DataSet GetCustomerDetail(string customerLoginId, long shopId, int CustomerRoleId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetCustomerDetail(customerLoginId, shopId, CustomerRoleId);

            if (items != null)
            {
                return items;
                //return Mapper.Map<CategoryList>(items);
            }
            return null;
        }


        public CMessage CustomerRegisteration(CustomerDetail oCustomer)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var oResponse = oRepo.CustomerRegisteration(oCustomer);

            if (oResponse != null)
            {
                return oResponse;
                //return Mapper.Map<CategoryList>(items);
            }
            return null;
        }

        public bool Validate(CustomerCredential oCustomer)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            return oRepo.Validate(oCustomer);
        }
    }
}
