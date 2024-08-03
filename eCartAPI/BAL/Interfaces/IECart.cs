using BAL.BEEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IECart
    {
        CategoryList GetCategoryList(string type = "");
        UserDetail GetUserDetail(string mobileNumber, int userRoleId);
        bool Validate(UserCredential oUser);
        CMessage UserRegisteration(UserDetail oUser);
        ShopDetail GetInitialSetup(long shopId);
        ProductList GetProductList(long categoryId);
        ProductDetail GetProductDetail(long productId , long customerId);

    }
}
