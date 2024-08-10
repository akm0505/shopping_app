using BAL.BEEntities;
using BAL.Classes;
using BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace eCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class eCartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public readonly IECart _eCartService;

        public eCartController(IConfiguration configuration, IECart eCartService)
        {
            _eCartService = eCartService;
            _configuration = configuration;
        }

        //[HttpGet]
        //[Route("GetCategoryList/{type?}")]
        //public IActionResult GetCategoryList(string type = "")
        //{
        //    var items = _eCartService.GetCategoryList(type);
        //    if(items != null)
        //    {
        //        return Ok(items);
        //    }
        //    return BadRequest();

        //}

        [HttpGet]
        [Route("GetCategoryList/{shopId}")]
        public IActionResult GetCategoryList(long shopId)
        {
            var items = _eCartService.GetCategoryList(shopId);
            if (items != null)
            {
                string jsonString = string.Empty;
                jsonString = JsonConvert.SerializeObject(items, Formatting.Indented);
                return Ok(jsonString);
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("GetCustomerLoginDetail")]
        public IActionResult GetCustomerLoginDetail(CustomerCredential oCustomer)
        {
            if (oCustomer != null)
            {
                if (!String.IsNullOrEmpty(oCustomer.CUSTOMER_LOGIN_ID) && !String.IsNullOrEmpty(oCustomer.CUSTOMER_PASSWORD))
                {
                    if (Validate(oCustomer))
                    {
                        var items = _eCartService.GetCustomerDetail(oCustomer.CUSTOMER_LOGIN_ID,oCustomer.SHOP_ID,oCustomer.CUSTOMER_ROLE_ID);
                        if (items != null)
                        {
                            string jsonString = string.Empty;
                            jsonString = JsonConvert.SerializeObject(items, Formatting.Indented);
                            return Ok(jsonString);

                            //return Ok(items);
                        }
                        else
                        {
                            return BadRequest("You are not authorized to access this application.");
                        }
                    }
                    else
                    {
                        return BadRequest("You are not authorized to access this application.");
                    }
                }
                else
                {
                    return BadRequest("Please enter valid credential.");
                }
            }
            return BadRequest();

        }


        [HttpPost]
        [Route("CustomerRegisteration")]
        public IActionResult CustomerRegisteration(CustomerDetail oCustomer)
        {
            if (oCustomer != null)
            {
                if (!String.IsNullOrEmpty(oCustomer.CUSTOMER_PHONE) && !String.IsNullOrEmpty(oCustomer.CUSTOMER_PASSWORD))
                {
                    var oResponse = _eCartService.CustomerRegisteration(oCustomer);
                    return Ok(oResponse);
                }
                else
                {
                    return BadRequest("Please enter valid credential.");
                }
            }
            return BadRequest();

        }


        private bool Validate(CustomerCredential oCustomer)
        {
            return _eCartService.Validate(oCustomer);
        }

        [HttpGet]
        [Route("GetCustomerDetail/{customerLoginId}/{shopId}/{CustomerRoleId}")]
        public IActionResult GetCustomerDetail(string customerLoginId,long shopId, int CustomerRoleId)
        {
            var items = _eCartService.GetCustomerDetail(customerLoginId, shopId, CustomerRoleId);
            if (items != null)
            {
                string jsonString = string.Empty;
                jsonString = JsonConvert.SerializeObject(items, Formatting.Indented);
                return Ok(jsonString);
                //return Ok(items);
            }
            return BadRequest();

        }


        //[HttpGet]
        //[Route("GetInitialSetup/{shopId}")]
        //public IActionResult GetInitialSetup(long shopId)
        //{
        //    var items = _eCartService.GetInitialSetup(shopId);
        //    if (items != null)
        //    {
        //        return Ok(items);
        //    }
        //    return BadRequest();

        //}

        [HttpGet]
        [Route("GetInitialSetup/{shopId}")]
        public IActionResult GetInitialSetup(long shopId)
        {
            var items = _eCartService.GetInitialSetup(shopId);
            if (items != null)
            {
                string jsonString = string.Empty;
                jsonString = JsonConvert.SerializeObject(items, Formatting.Indented);
                return Ok(jsonString);
            }
            return BadRequest();

        }


        //[HttpGet]
        //[Route("GetProductList/{categoryId}")]
        //public IActionResult GetProductList(long categoryId)
        //{
        //    var items = _eCartService.GetProductList(categoryId);
        //    if (items != null)
        //    {
        //        return Ok(items);
        //    }
        //    return BadRequest();

        //}

        [HttpGet]
        [Route("GetProductList/{categoryId}")]
        public IActionResult GetProductList(long categoryId)
        {
            var items = _eCartService.GetProductList(categoryId);
            if (items != null)
            {
                string jsonString = string.Empty;
                jsonString = JsonConvert.SerializeObject(items, Formatting.Indented);
                return Ok(jsonString);
            }
            return BadRequest();

        }


        [HttpGet]
        [Route("GetProductDetail/{productId}/{customerId}")]
        public IActionResult GetProductDetail(long productId, long customerId)
        {
            var items = _eCartService.GetProductDetail(productId, customerId);
            if (items != null)
            {
                string jsonString = string.Empty;

                if (items.Tables[0].Rows.Count  > 0)
                {
                    jsonString = JsonConvert.SerializeObject(items, Formatting.Indented);
                }
                return Ok(jsonString);
            }
            return BadRequest();

        }

    }
}
