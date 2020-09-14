using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        //protected Settings Settings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BaseController()
        {
            //Settings = Settings.Instance;
        }
    }
}
