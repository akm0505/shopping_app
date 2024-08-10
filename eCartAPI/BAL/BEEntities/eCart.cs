using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.BEEntities
{
    public class eCart
    {
    }

    public class CategoryList
    {
        public List<CategoryDetail>? CategoryDetails { get; set; }
    }

    public class CategoryDetail
    {
        public long CATEGORY_ID { get; set; }
        public string? CATEGORY_NAME { get; set; }
        public string? IMAGE_URL { get; set; }
    }

    public class CustomerDetail
    {
        public long CUSTOMER_ID { get; set; }
        public int CUSTOMER_ROLE_ID { get; set; }
        public string? CUSTOMER_ROLE_TYPE { get; set; }  
        public bool IS_SHOP_OWNER { get; set; }
        public long? SHOP_ID { get; set; }   
        public string? CUSTOMER_NAME { get; set; }
        public string? CUSTOMER_DISPLAY_NAME { get; set; }
        public string? CUSTOMER_EMAIL { get; set; }
        public string? CUSTOMER_PHONE { get; set; }
        public string? CUSTOMER_PASSWORD { get; set; }
        public string? CUSTOMER_PHONE2 { get; set; }
        public string? CUSTOMER_ADDRESS { get; set; }
        public string? CUSTOMER_ADDRESS_1 { get; set; }
        public string? CUSTOMER_CITY { get; set; }
        public string? CUSTOMER_STATE { get; set; }
        public string? CUSTOMER_COUNTRY { get; set; }
        public string? CUSTOMER_PINCODE { get; set; }
        public string? CUSTOMER_PROFILE_PIC    { get; set; }
        
    }

    public class CMessage
    {
        public string? RESPONSE_TYPE { get; set; }
        public string? RESPONSE_MESSAGE { get; set; }
    }

    public class CustomerCredential
    {
        public string? CUSTOMER_LOGIN_ID { get; set; }  
        public string? CUSTOMER_PASSWORD { get; set; }
        public int CUSTOMER_ROLE_ID { get; set; }
        public long SHOP_ID { get; set; }
        public string? OAUTH_TOKEN { get; set; }
    }


    public class ShopDetail
    {
        public long SHOP_ID { get; set; }
        public long CUSTOMER_ID { get; set; }
        public string? CUSTOMER_NAME { get; set; }
        public string? CUSTOMER_DISPLAY_NAME { get; set; }
        public string? SHOP_CODE { get; set; }
        public string? SHOP_NAME { get; set; }
        public string? IMAGE_URL { get; set; }
        public string? SHOP_ADDRESS { get; set; }
        public string? SHOP_OWNER { get;set; }
        public string? SHOP_OWNER_CONTACT_NUMBER { get; set; }
        public string? SHOP_FOOTER {  get; set; }   
        public string? SHOP_HEADER { get; set; }
        public string? SHOP_CONTENT { get; set; }
        public string? SHOP_BRAND_IMAGE { get; set; }

    }


    public class ProductList
    {
        public List<ProductDetail>? ProductDetails { get; set; }
    }

    public class ProductDetail
    {
        public long CATEGORY_ID { get; set; }
        public long PRODUCT_ID { get; set; }
        public string? PRODUCT_CODE { get; set; }
        public string? PRODUCT_NAME { get; set; }
        public string? IMAGE_URL { get; set; }
        public string? PRODUCT_BRAND { get; set; }
        public decimal PRODUCT_PRICE { get; set; }
        public decimal PRODUCT_DISCOUNT { get; set; }
        public int PRODUCT_DISPLAY_ORDER { get; set; }
    }
}
