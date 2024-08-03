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


namespace BAL.Classes
{
    public class eCartServices : IECart
    {
        private readonly IConfiguration _configuration;
        public eCartServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public CategoryList GetCategoryList(string type = "")
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetCategoryList(type);

            if (items != null)
            {
                return items;
                //return Mapper.Map<CategoryList>(items);
            }
            return null;
        }

        public ShopDetail GetInitialSetup(long shopId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetInitialSetup(shopId);

            if (items != null)
            {
                return items;
            }
            return null;
        }

        public ProductDetail GetProductDetail(long productId, long customerId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetProductDetail(productId,customerId);

            if (items != null)
            {
                return items;
            }
            return null;
        }

        public ProductList GetProductList(long categoryId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetProductList(categoryId);

            if (items != null)
            {
                return items;
            }
            return null;
        }

        public UserDetail GetUserDetail(string mobileNumber, int userRoleId)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var items = oRepo.GetUserDetail(mobileNumber,userRoleId);

            if (items != null)
            {
                return items;
                //return Mapper.Map<CategoryList>(items);
            }
            return null;
        }

       
        public CMessage UserRegisteration(UserDetail oUser)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            var oResponse = oRepo.UserRegisteration(oUser);

            if (oResponse != null)
            {
                return oResponse;
                //return Mapper.Map<CategoryList>(items);
            }
            return null;
        }

        public bool Validate(UserCredential oUser)
        {
            eCartRepository oRepo = new eCartRepository(_configuration);
            return oRepo.Validate(oUser);
        }
    }
}
