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
using CashewDocumentParser.Web.Helpers;
using Microsoft.AspNetCore.Cors;
using CashewDocumentParser.Models.Infrastructure;
using System.IO;
using CashewDocumentParser.Web.Configurations;
using System.Collections.Generic;

namespace CashewDocumentParser.API.Controllers
{
    [EnableCors("AllowOrigins")]
    [Authorize]
    public class QueuesController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        private ILogger<QueuesController> _logger { get; set; }

        public QueuesController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            ILogger<QueuesController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("api/templates/{templateId}/processedqueue")]
        public async Task<IActionResult> GetAllProcessedQueuesAsync(int templateId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var templates = _unitOfWork.GetProcessedQueueRepository().GetMultiple(q => q.TemplateId == templateId);
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

        [HttpGet("api/templates/{templateId}/preprocessingqueue")]
        public async Task<IActionResult> GetAllPreprocessingQueuesAsync(int templateId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var templates = _unitOfWork.GetPreprocessingQueueRepository().GetMultiple(q => q.TemplateId == templateId);
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

        [HttpGet("api/templates/{templateId}/extractqueue")]
        public async Task<IActionResult> GetAllExtractQueuesAsync(int templateId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var templates = _unitOfWork.GetExtractQueueRepository().GetMultiple(q => q.TemplateId == templateId);
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

        [HttpGet("api/templates/{templateId}/integrationqueue")]
        public async Task<IActionResult> GetAllIntegrationQueuesAsync(int templateId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var templates = _unitOfWork.GetIntegrationQueueRepository().GetMultiple(q => q.TemplateId == templateId);
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
