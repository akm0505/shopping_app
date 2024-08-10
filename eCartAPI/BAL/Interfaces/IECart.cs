using BAL.BEEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IECart
    {
        //CategoryList GetCategoryList(string type = "");
        DataSet GetCategoryList(long shopId);
        //CustomerDetail GetCustomerDetail(string customerLoginId, long shopId, int CustomerRoleId);
        DataSet GetCustomerDetail(string customerLoginId, long shopId, int CustomerRoleId);
        bool Validate(CustomerCredential oCustomer);
        CMessage CustomerRegisteration(CustomerDetail oCustomer);
        //ShopDetail GetInitialSetup(long shopId);
        //ProductList GetProductList(long categoryId);
        //ProductDetail GetProductDetail(long productId , long customerId);
        DataSet GetInitialSetup(long shopId);
        DataSet GetProductList(long categoryId);
        DataSet GetProductDetail(long productId, long customerId);

    }
}
