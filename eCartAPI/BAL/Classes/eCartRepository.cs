using BAL.BEEntities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class eCartRepository
    {
        private readonly IConfiguration _configuration;
        //var connString = configuration.GetConnectionString("ProductsDb");
        //public readonly IC
        //public static string ConnString = "Server=sql.bsite.net\\MSSQL2016;Database=akm0505_mahakal;User Id=akm0505_mahakal;Password=Abhishek@123;";

        public eCartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GetDBConnectionString()
        {
            return _configuration.GetConnectionString("ProdConnectionString");
        }
        

       
        public CategoryList GetCategoryList(string type = "")
        {
            var cList = new CategoryList();
            SqlDataReader reader ;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            List<CategoryDetail> cDetail = new List<CategoryDetail>();

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_CategoryDetails_GET";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@type", type);
                        sqlConnection.Open();

                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CategoryDetail cItem = new CategoryDetail();
                                cItem.CATEGORY_ID = Convert.ToInt64(reader["CATEGORY_ID"]);
                                cItem.CATEGORY_NAME = Convert.ToString(reader["CATEGORY_NAME"]);
                                cItem.IMAGE_URL = Convert.ToString(reader["IMAGE_URL"]);
                                cDetail.Add(cItem);
                            }
                        }
                    }



                }
                cList.CategoryDetails = cDetail;
                return cList;
            }catch(Exception ex)
            {
                throw;
            }

        }


        public UserDetail GetUserDetail(string mobileNumber, int userRoleId)
        {
            var cUser = new UserDetail();
            SqlDataReader reader;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_UserDetail_GET";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mobileNumber", mobileNumber);
                        cmd.Parameters.AddWithValue("@userRoleId", userRoleId);
                        sqlConnection.Open();

                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cUser.USER_NAME = Convert.ToString(reader["USER_NAME"]);
                                cUser.USER_DISPLAY_NAME = Convert.ToString(reader["USER_DISPLAY_NAME"]);
                                cUser.USER_ADDRESS = Convert.ToString(reader["USER_ADDRESS"]);
                                cUser.USER_PHONE = Convert.ToString(reader["USER_PHONE"]);
                                cUser.IS_SHOP_OWNER = Convert.ToBoolean(reader["IS_SHOP_OWNER"]);
                                try
                                {
                                    cUser.SHOP_ID = Convert.ToInt64(reader["SHOP_ID"]);
                                }
                                catch { }
                            }
                        }
                    }
                }
                return cUser;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool Validate(UserCredential oUser)
        {
            SqlDataReader reader;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_User_Validate";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mobileNumber", oUser.USER_PHONE);
                        cmd.Parameters.AddWithValue("@Password", oUser.USER_PASSWORD);
                        sqlConnection.Open();

                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if(Convert.ToInt32(reader["IS_VALID_USER"]).Equals(1))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }


        public CMessage UserRegisteration(UserDetail oUser)
        {
            SqlDataReader reader;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            CMessage oResponse = new CMessage();

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_User_Registeration";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USER_ROLE_ID", oUser.USER_ROLE_ID);
                        cmd.Parameters.AddWithValue("@USER_NAME", oUser.USER_NAME);
                        cmd.Parameters.AddWithValue("@USER_DISPLAY_NAME", oUser.USER_DISPLAY_NAME);
                        cmd.Parameters.AddWithValue("@USER_EMAIL", oUser.USER_EMAIL);
                        cmd.Parameters.AddWithValue("@USER_PHONE", oUser.USER_PHONE);
                        cmd.Parameters.AddWithValue("@USER_PASSWORD", oUser.USER_PASSWORD);

                        cmd.Parameters.AddWithValue("@USER_PHONE2", oUser.USER_PHONE2);
                        cmd.Parameters.AddWithValue("@USER_ADDRESS", oUser.USER_ADDRESS);
                        cmd.Parameters.AddWithValue("@USER_ADDRESS_1", oUser.USER_ADDRESS_1);

                        cmd.Parameters.AddWithValue("@USER_CITY", oUser.USER_CITY);
                        cmd.Parameters.AddWithValue("@USER_STATE", oUser.USER_STATE);
                        cmd.Parameters.AddWithValue("@USER_COUNTRY", oUser.USER_COUNTRY);

                        cmd.Parameters.AddWithValue("@USER_PINCODE", oUser.USER_PINCODE);
                        cmd.Parameters.AddWithValue("@USER_PROFILE_PIC", oUser.USER_PROFILE_PIC);


                        sqlConnection.Open();

                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oResponse.RESPONSE_TYPE = Convert.ToString(reader["RESPONSE_TYPE"]);
                                oResponse.RESPONSE_MESSAGE = Convert.ToString(reader["RESPONSE_MESSAGE"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return oResponse;
        }



        public ShopDetail GetInitialSetup(long shopId)
        {
            SqlDataReader reader;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            ShopDetail o = new  ShopDetail();

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_InitialSetup_GET";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SHOP_ID", shopId);
                        sqlConnection.Open();

                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                o.SHOP_ID = Convert.ToInt64(reader["SHOP_ID"]);
                                o.USER_ID = Convert.ToInt64(reader["USER_ID"]);
                                o.USER_NAME = Convert.ToString(reader["USER_NAME"]);
                                o.USER_DISPLAY_NAME = Convert.ToString(reader["USER_DISPLAY_NAME"]);
                                o.SHOP_NAME = Convert.ToString(reader["SHOP_NAME"]);
                                o.IMAGE_URL = Convert.ToString(reader["IMAGE_URL"]);
                                o.SHOP_CODE = Convert.ToString(reader["SHOP_CODE"]);
                                o.SHOP_OWNER_CONTACT_NUMBER = Convert.ToString(reader["SHOP_OWNER_CONTACT_NUMBER"]);
                                o.SHOP_OWNER = Convert.ToString(reader["SHOP_OWNER"]);
                                o.SHOP_BRAND_IMAGE = Convert.ToString(reader["SHOP_BRAND_IMAGE"]);
                                o.SHOP_CONTENT = Convert.ToString(reader["SHOP_CONTENT"]);
                                o.SHOP_HEADER = Convert.ToString(reader["SHOP_HEADER"]);
                                o.SHOP_FOOTER = Convert.ToString(reader["SHOP_FOOTER"]);
                            }
                        }
                    }



                }
                return o;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public ProductList GetProductList(long categoryId)
        {
            var oList = new ProductList();
            SqlDataReader reader;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            List<ProductDetail> oDetail = new List<ProductDetail>();

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_ProductList_GET";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CATEGORY_ID", categoryId);
                        sqlConnection.Open();

                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductDetail oItem = new ProductDetail();
                                oItem.PRODUCT_ID = Convert.ToInt64(reader["PRODUCT_ID"]);
                                oItem.PRODUCT_CODE = Convert.ToString(reader["PRODUCT_CODE"]);
                                oItem.PRODUCT_NAME = Convert.ToString(reader["PRODUCT_NAME"]);

                                oItem.PRODUCT_BRAND = Convert.ToString(reader["PRODUCT_BRAND"]);
                                oItem.PRODUCT_PRICE = Convert.ToDecimal(reader["PRODUCT_PRICE"]);
                                oItem.PRODUCT_DISCOUNT = Convert.ToDecimal(reader["PRODUCT_DISCOUNT"]);
                                oItem.PRODUCT_DISPLAY_ORDER = Convert.ToInt32(reader["PRODUCT_DISPLAY_ORDER"]);

                                oItem.IMAGE_URL = Convert.ToString(reader["IMAGE_URL"]);
                                oDetail.Add(oItem);
                            }
                        }
                    }



                }
                oList.ProductDetails = oDetail;
                return oList;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public ProductDetail GetProductDetail(long productId, long customerId)
        {
            SqlDataReader reader;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            ProductDetail oItem = new ProductDetail();

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_ProductDetail_GET";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PRODUCT_ID", productId);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", customerId);
                        sqlConnection.Open();

                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
                                oItem.PRODUCT_ID = Convert.ToInt64(reader["PRODUCT_ID"]);
                                oItem.PRODUCT_CODE = Convert.ToString(reader["PRODUCT_CODE"]);
                                oItem.PRODUCT_NAME = Convert.ToString(reader["PRODUCT_NAME"]);

                                oItem.PRODUCT_BRAND = Convert.ToString(reader["PRODUCT_BRAND"]);
                                oItem.PRODUCT_PRICE = Convert.ToDecimal(reader["PRODUCT_PRICE"]);
                                oItem.PRODUCT_DISCOUNT = Convert.ToDecimal(reader["PRODUCT_DISCOUNT"]);
                                oItem.PRODUCT_DISPLAY_ORDER = Convert.ToInt32(reader["PRODUCT_DISPLAY_ORDER"]);

                                oItem.IMAGE_URL = Convert.ToString(reader["IMAGE_URL"]);
                            }
                        }
                    }



                }
                return oItem;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
