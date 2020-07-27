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
    [Route("api/Template")]
    public class TemplateController : Controller
    {
        private DocumentConfiguration _documentConfig { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        private ILogger<AccountController> _logger { get; set; }

        public TemplateController(
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var templates = _unitOfWork.TemplateRepository.GetMultiple(t => t.IsDeleted == false);
                    return Ok(templates);
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Error when creating uow transaction, thereby reverting back. Error: {ex.Message}";
                    _logger.LogError(errorMessage);
                    await _unitOfWork.Rollback();
                    return BadRequest(errorMessage);
                }
            }
            return BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody]CreateTemplateForm createTemplateForm)
        {
            if (ModelState.IsValid)
            {
                var template = new Template
                {
                    Name = createTemplateForm.Name
                };
                try
                {
                    _unitOfWork.TemplateRepository.Add(template);
                    await _unitOfWork.Commit();
                    return Ok(template);
                } catch (Exception ex)
                {
                    var errorMessage = $"Error when creating uow transaction, thereby reverting back. Error: {ex.Message}";
                    _logger.LogError(errorMessage);
                    await _unitOfWork.Rollback();
                    return BadRequest(errorMessage);
                }
            }
            return BadRequest();
        }

        [HttpDelete("Delete/{templateId?}")]
        public async Task<IActionResult> DeleteAsync([FromBody]DeleteTemplateForm deleteTemplateForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var template = _unitOfWork.TemplateRepository.GetSingle(t => t.Id == deleteTemplateForm.TemplateId);
                    template.IsDeleted = true;
                    _unitOfWork.TemplateRepository.Update(template);
                    await _unitOfWork.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Error when creating uow transaction, thereby reverting back. Error: {ex.Message}";
                    _logger.LogError(errorMessage);
                    await _unitOfWork.Rollback();
                    return BadRequest(errorMessage);
                }
            }
            return BadRequest();
        }

        [HttpPost("{templateId?}/SampleDocument/Upload")]
        public async Task<IActionResult> UploadSampleDocumentAsync(int templateId, [FromForm]UploadSampleDocumentsForm uploadSampleDocumentsForm)
        {
            List<string> supportedFileExtensions = new List<string>
            {
                "PDF",
                "TIFF",
                "JPEG",
                "PNG"
            };
            if (ModelState.IsValid)
            {
                try
                {
                    List<SampleDocument> sampleDocuments = new List<SampleDocument>();
                    foreach (var file in uploadSampleDocumentsForm.SampleDocuments)
                    {
                        var guid = Guid.NewGuid();
                        var extension = Path.GetExtension(file.FileName).Substring(1);
                        if (!supportedFileExtensions.Contains(extension.ToUpper()))
                        {
                            return BadRequest(new { errorMessage = "File format is not supported." });
                        }
                        var sampleDocument = new SampleDocument
                        {
                            Guid = guid,
                            TemplateId = templateId,
                            FilenameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName),
                            Extension = extension,
                            Fullpath = _documentConfig.RootPath + guid.ToString() + "." + extension
                        };
                        sampleDocuments.Add(sampleDocument);
                        _unitOfWork.SampleDocumentRepository.Add(sampleDocument);

                        // Upload File Content
                        //file.CopyTo(new FileStream(sampleDocument.Fullpath, FileMode.Create));
                        using (FileStream fs = System.IO.File.Create(sampleDocument.Fullpath))
                        {
                            await file.CopyToAsync(fs);
                            fs.Flush();
                        }
                    }

                    await _unitOfWork.Commit();

                    return Ok(sampleDocuments);
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Error when creating uow transaction, thereby reverting back. Error: {ex.Message}";
                    _logger.LogError(errorMessage);
                    await _unitOfWork.Rollback();
                    return BadRequest(new { errorMessage = errorMessage });
                }
            }
            return BadRequest();
        }
    }
}
