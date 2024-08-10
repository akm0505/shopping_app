using BAL.BEEntities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class eCartRepository
    {
        private readonly IConfiguration _configuration;
       
        public eCartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GetDBConnectionString()
        {
            return _configuration.GetConnectionString("ProdConnectionString");
        }



        //public CategoryList GetCategoryList(string type = "")
        //{
        //    var cList = new CategoryList();
        //    SqlDataReader reader ;
        //    SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
        //    List<CategoryDetail> cDetail = new List<CategoryDetail>();

        //    try
        //    {
        //        using (sqlConnection)
        //        {
        //            string strCommandText = "ECART_CategoryDetails_GET";

        //            using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@type", type);
        //                sqlConnection.Open();

        //                using (reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        CategoryDetail cItem = new CategoryDetail();
        //                        cItem.CATEGORY_ID = Convert.ToInt64(reader["CATEGORY_ID"]);
        //                        cItem.CATEGORY_NAME = Convert.ToString(reader["CATEGORY_NAME"]);
        //                        cItem.IMAGE_URL = Convert.ToString(reader["IMAGE_URL"]);
        //                        cDetail.Add(cItem);
        //                    }
        //                }
        //            }



        //        }
        //        cList.CategoryDetails = cDetail;
        //        return cList;
        //    }catch(Exception ex)
        //    {
        //        throw;
        //    }

        //}

        public DataSet GetCategoryList(long shopId)
        {
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            DataSet ds = new DataSet();
            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_CategoryDetails_GET";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SHOP_ID", shopId);
                        sqlConnection.Open();

                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                        {
                            sqlDataAdapter.Fill(ds);
                        }
                    }



                }
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        //public CustomerDetail GetCustomerDetail(string customerLoginId , long shopId, int CustomerRoleId)
        //{
        //    var cCustomer = new CustomerDetail();
        //    SqlDataReader reader;
        //    SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());

        //    try
        //    {
        //        using (sqlConnection)
        //        {
        //            string strCommandText = "ECART_CustomerDetail_GET";

        //            using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@CUSTOMER_LOGIN_ID", customerLoginId);
        //                cmd.Parameters.AddWithValue("@SHOP_ID", shopId);
        //                cmd.Parameters.AddWithValue("@CUSTOMER_ROLE_ID", CustomerRoleId);
        //                sqlConnection.Open();

        //                using (reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        cCustomer.CUSTOMER_NAME = Convert.ToString(reader["CUSTOMER_NAME"]);
        //                        cCustomer.CUSTOMER_DISPLAY_NAME = Convert.ToString(reader["CUSTOMER_DISPLAY_NAME"]);
        //                        cCustomer.CUSTOMER_ADDRESS = Convert.ToString(reader["CUSTOMER_ADDRESS"]);
        //                        cCustomer.CUSTOMER_PHONE = Convert.ToString(reader["CUSTOMER_PHONE"]);
        //                        cCustomer.IS_SHOP_OWNER = Convert.ToBoolean(reader["IS_SHOP_OWNER"]);
        //                        try
        //                        {
        //                            cCustomer.SHOP_ID = Convert.ToInt64(reader["SHOP_ID"]);
        //                        }
        //                        catch { }
        //                    }
        //                }
        //            }
        //        }
        //        return cCustomer;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}


        public DataSet GetCustomerDetail(string customerLoginId, long shopId, int CustomerRoleId)
        {
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            DataSet ds = new DataSet();

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_CustomerDetail_GET";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CUSTOMER_LOGIN_ID", customerLoginId);
                        cmd.Parameters.AddWithValue("@SHOP_ID", shopId);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ROLE_ID", CustomerRoleId);
                        sqlConnection.Open();

                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                        {
                            sqlDataAdapter.Fill(ds);
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool Validate(CustomerCredential oCustomer)
        {
            SqlDataReader reader;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_Customer_Validate";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CUSTOMER_LOGIN_ID", oCustomer.CUSTOMER_LOGIN_ID);
                        cmd.Parameters.AddWithValue("@PASSWORD", oCustomer.CUSTOMER_PASSWORD);
                        cmd.Parameters.AddWithValue("@SHOP_ID", oCustomer.SHOP_ID);
                        sqlConnection.Open();

                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if(Convert.ToInt32(reader["IS_VALID_CUSTOMER"]).Equals(1))
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


        public CMessage CustomerRegisteration(CustomerDetail oCustomer)
        {
            SqlDataReader reader;
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            CMessage oResponse = new CMessage();

            try
            {
                using (sqlConnection)
                {
                    string strCommandText = "ECART_Customer_Registeration";

                    using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SHOP_ID", oCustomer.SHOP_ID);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ROLE_ID", oCustomer.CUSTOMER_ROLE_ID);
                        cmd.Parameters.AddWithValue("@CUSTOMER_NAME", oCustomer.CUSTOMER_NAME);
                        cmd.Parameters.AddWithValue("@CUSTOMER_DISPLAY_NAME", oCustomer.CUSTOMER_DISPLAY_NAME);
                        cmd.Parameters.AddWithValue("@CUSTOMER_EMAIL", oCustomer.CUSTOMER_EMAIL);
                        cmd.Parameters.AddWithValue("@CUSTOMER_PHONE", oCustomer.CUSTOMER_PHONE);
                        cmd.Parameters.AddWithValue("@CUSTOMER_PASSWORD", oCustomer.CUSTOMER_PASSWORD);

                        cmd.Parameters.AddWithValue("@CUSTOMER_PHONE2", oCustomer.CUSTOMER_PHONE2);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ADDRESS", oCustomer.CUSTOMER_ADDRESS);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ADDRESS_1", oCustomer.CUSTOMER_ADDRESS_1);

                        cmd.Parameters.AddWithValue("@CUSTOMER_CITY", oCustomer.CUSTOMER_CITY);
                        cmd.Parameters.AddWithValue("@CUSTOMER_STATE", oCustomer.CUSTOMER_STATE);
                        cmd.Parameters.AddWithValue("@CUSTOMER_COUNTRY", oCustomer.CUSTOMER_COUNTRY);

                        cmd.Parameters.AddWithValue("@CUSTOMER_PINCODE", oCustomer.CUSTOMER_PINCODE);
                        cmd.Parameters.AddWithValue("@CUSTOMER_PROFILE_PIC", oCustomer.CUSTOMER_PROFILE_PIC);


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



        //public ShopDetail GetInitialSetup(long shopId)
        //{
        //    SqlDataReader reader;
        //    SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
        //    ShopDetail o = new  ShopDetail();

        //    try
        //    {
        //        using (sqlConnection)
        //        {
        //            string strCommandText = "ECART_InitialSetup_GET";

        //            using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@SHOP_ID", shopId);
        //                sqlConnection.Open();

        //                using (reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        o.SHOP_ID = Convert.ToInt64(reader["SHOP_ID"]);
        //                        o.CUSTOMER_ID = Convert.ToInt64(reader["CUSTOMER_ID"]);
        //                        o.CUSTOMER_NAME = Convert.ToString(reader["CUSTOMER_NAME"]);
        //                        o.CUSTOMER_DISPLAY_NAME = Convert.ToString(reader["CUSTOMER_DISPLAY_NAME"]);
        //                        o.SHOP_NAME = Convert.ToString(reader["SHOP_NAME"]);
        //                        o.IMAGE_URL = Convert.ToString(reader["IMAGE_URL"]);
        //                        o.SHOP_CODE = Convert.ToString(reader["SHOP_CODE"]);
        //                        o.SHOP_OWNER_CONTACT_NUMBER = Convert.ToString(reader["SHOP_OWNER_CONTACT_NUMBER"]);
        //                        o.SHOP_OWNER = Convert.ToString(reader["SHOP_OWNER"]);
        //                        o.SHOP_BRAND_IMAGE = Convert.ToString(reader["SHOP_BRAND_IMAGE"]);
        //                        o.SHOP_CONTENT = Convert.ToString(reader["SHOP_CONTENT"]);
        //                        o.SHOP_HEADER = Convert.ToString(reader["SHOP_HEADER"]);
        //                        o.SHOP_FOOTER = Convert.ToString(reader["SHOP_FOOTER"]);
        //                    }
        //                }
        //            }



        //        }
        //        return o;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

        public DataSet GetInitialSetup(long shopId)
        {
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            DataSet ds = new DataSet();
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
                        
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                        {
                            sqlDataAdapter.Fill(ds);
                        }
                    }



                }
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        //public ProductList GetProductList(long categoryId)
        //{
        //    var oList = new ProductList();
        //    SqlDataReader reader;
        //    SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
        //    List<ProductDetail> oDetail = new List<ProductDetail>();

        //    try
        //    {
        //        using (sqlConnection)
        //        {
        //            string strCommandText = "ECART_ProductList_GET";

        //            using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@CATEGORY_ID", categoryId);
        //                sqlConnection.Open();

        //                using (reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        ProductDetail oItem = new ProductDetail();
        //                        oItem.PRODUCT_ID = Convert.ToInt64(reader["PRODUCT_ID"]);
        //                        oItem.PRODUCT_CODE = Convert.ToString(reader["PRODUCT_CODE"]);
        //                        oItem.PRODUCT_NAME = Convert.ToString(reader["PRODUCT_NAME"]);

        //                        oItem.PRODUCT_BRAND = Convert.ToString(reader["PRODUCT_BRAND"]);
        //                        oItem.PRODUCT_PRICE = Convert.ToDecimal(reader["PRODUCT_PRICE"]);
        //                        oItem.PRODUCT_DISCOUNT = Convert.ToDecimal(reader["PRODUCT_DISCOUNT"]);
        //                        oItem.PRODUCT_DISPLAY_ORDER = Convert.ToInt32(reader["PRODUCT_DISPLAY_ORDER"]);

        //                        oItem.IMAGE_URL = Convert.ToString(reader["IMAGE_URL"]);
        //                        oDetail.Add(oItem);
        //                    }
        //                }
        //            }



        //        }
        //        oList.ProductDetails = oDetail;
        //        return oList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}


        //public ProductDetail GetProductDetail(long productId, long customerId)
        //{
        //    SqlDataReader reader;
        //    SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
        //    ProductDetail oItem = new ProductDetail();

        //    try
        //    {
        //        using (sqlConnection)
        //        {
        //            string strCommandText = "ECART_ProductDetail_GET";

        //            using (SqlCommand cmd = new SqlCommand(strCommandText, sqlConnection))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@PRODUCT_ID", productId);
        //                cmd.Parameters.AddWithValue("@CUSTOMER_ID", customerId);
        //                sqlConnection.Open();

        //                using (reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {

        //                        oItem.PRODUCT_ID = Convert.ToInt64(reader["PRODUCT_ID"]);
        //                        oItem.PRODUCT_CODE = Convert.ToString(reader["PRODUCT_CODE"]);
        //                        oItem.PRODUCT_NAME = Convert.ToString(reader["PRODUCT_NAME"]);

        //                        oItem.PRODUCT_BRAND = Convert.ToString(reader["PRODUCT_BRAND"]);
        //                        oItem.PRODUCT_PRICE = Convert.ToDecimal(reader["PRODUCT_PRICE"]);
        //                        oItem.PRODUCT_DISCOUNT = Convert.ToDecimal(reader["PRODUCT_DISCOUNT"]);
        //                        oItem.PRODUCT_DISPLAY_ORDER = Convert.ToInt32(reader["PRODUCT_DISPLAY_ORDER"]);

        //                        oItem.IMAGE_URL = Convert.ToString(reader["IMAGE_URL"]);
        //                    }
        //                }
        //            }



        //        }
        //        return oItem;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}


        public DataSet GetProductList(long categoryId)
        {
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            DataSet ds = new DataSet();
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

                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                        {
                            sqlDataAdapter.Fill(ds);
                        }
                    }



                }
               return ds;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataSet GetProductDetail(long productId, long customerId)
        {
            SqlConnection sqlConnection = new SqlConnection(GetDBConnectionString());
            DataSet ds = new DataSet();
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

                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd))
                        {
                            sqlDataAdapter.Fill(ds);
                        }
                    }

                }
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
