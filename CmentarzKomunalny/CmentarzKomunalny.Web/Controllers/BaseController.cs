using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CmentarzKomunalny.Web.Controllers
{
    public class BaseController<TController> : Controller
    {
        //      protected readonly IStringLocalizer<TController> _localizer; not needed, everything by default is in PL language
        protected readonly ILogger _logger;
        public BaseController(ILoggerFactory loggerFactory)
        {
        //  _localizer = localizer;
            _logger = loggerFactory.CreateLogger(GetType());
        }
    }
}
