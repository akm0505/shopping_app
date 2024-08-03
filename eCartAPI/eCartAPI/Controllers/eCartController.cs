using BAL.BEEntities;
using BAL.Classes;
using BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("GetCategoryList/{type?}")]
        public IActionResult GetCategoryList(string type = "")
        {
            var items = _eCartService.GetCategoryList(type);
            if(items != null)
            {
                return Ok(items);
            }
            return BadRequest();

        }

        [HttpPost]
        [Route("GetUserLoginDetail")]
        public IActionResult GetUserLoginDetail(UserCredential oUser)
        {
            if (oUser != null)
            {
                if (!String.IsNullOrEmpty(oUser.USER_PHONE) && !String.IsNullOrEmpty(oUser.USER_PASSWORD))
                {
                    if (Validate(oUser))
                    {
                        var items = _eCartService.GetUserDetail(oUser.USER_PHONE,oUser.USER_ROLE_ID);
                        if (items != null)
                        {
                            return Ok(items);
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
        [Route("UserRegisteration")]
        public IActionResult UserRegisteration(UserDetail oUser)
        {
            if (oUser != null)
            {
                if (!String.IsNullOrEmpty(oUser.USER_PHONE) && !String.IsNullOrEmpty(oUser.USER_PASSWORD))
                {
                    var oResponse = _eCartService.UserRegisteration(oUser);
                    return Ok(oResponse);
                }
                else
                {
                    return BadRequest("Please enter valid credential.");
                }
            }
            return BadRequest();

        }


        private bool Validate(UserCredential oUser)
        {
            return _eCartService.Validate(oUser);
        }

        [HttpGet]
        [Route("GetUserDetail/{mobileNumber}/{userRoleId}")]
        public IActionResult GetUserDetail(string mobileNumber,int userRoleId)
        {
            var items = _eCartService.GetUserDetail(mobileNumber, userRoleId);
            if (items != null)
            {
                return Ok(items);
            }
            return BadRequest();

        }


        [HttpGet]
        [Route("GetInitialSetup/{shopId}")]
        public IActionResult GetInitialSetup(long shopId)
        {
            var items = _eCartService.GetInitialSetup(shopId);
            if (items != null)
            {
                return Ok(items);
            }
            return BadRequest();

        }

        [HttpGet]
        [Route("GetProductList/{categoryId}")]
        public IActionResult GetProductList(long categoryId)
        {
            var items = _eCartService.GetProductList(categoryId);
            if (items != null)
            {
                return Ok(items);
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
                return Ok(items);
            }
            return BadRequest();

        }

    }
}
