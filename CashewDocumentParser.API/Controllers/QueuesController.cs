using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using CashewDocumentParser.Models;
using CashewDocumentParser.ViewModels.Forms;
using CashewDocumentParser.API.Helpers;
using Microsoft.AspNetCore.Cors;
using CashewDocumentParser.Models.Infrastructure;
using System.IO;
using CashewDocumentParser.API.Configurations;
using System.Collections.Generic;

namespace CashewDocumentParser.API.Controllers
{
    [EnableCors("AllowOrigins")]
    [Authorize]
    public class QueuesController : Controller
    {
        private DocumentConfiguration _documentConfig { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        private ILogger<AccountController> _logger { get; set; }

        public QueuesController(
            DocumentConfiguration documentConfig,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            ILogger<AccountController> logger)
        {
            _documentConfig = documentConfig;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("api/templates/{templateId}/importqueue")]
        public async Task<IActionResult> GetAllImportQueuesAsync(int templateId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var templates = _unitOfWork.GetImportQueueRepository().GetMultiple(q => q.TemplateId == templateId);
                    return Ok(templates);
                }
                catch (Exception ex)
                {
                    var errorMessage = $"{ex.Message}";
                    _logger.LogError(errorMessage);
                    await _unitOfWork.Rollback();
                    return BadRequest(errorMessage);
                }
            }
            return BadRequest();
        }
    }
}
