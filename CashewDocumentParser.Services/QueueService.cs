using CashewDocumentParser.Common;
using CashewDocumentParser.Enumerations;
using CashewDocumentParser.Models;
using CashewDocumentParser.Models.Infrastructure;
using CashewDocumentParser.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using IronPdf;

namespace CashewDocumentParser.Services
{
    public interface IQueueService
    {
        public Task MoveFromProcessedToImport();
        public Task MoveFromImportToOCR();
        public Task ProcessOCRQueue();
        public Task MoveFromOCRToClassification();
        public Task MoveFromClassificationToPreprocessing();
        public Task MoveFromPreprocessingToExtract();
        public Task MoveFromExtractToScripting();
        public Task MoveFromScriptingToIntegration();
    }
    public class QueueService : IQueueService
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public QueueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task MoveFromProcessedToImport()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var processedQueueItems = _unitOfWork.GetProcessedQueueRepository().GetMultiple(q => q.ProcessStage == EnumProcessStage.PostProcess);
                    foreach (var processedQueueItem in processedQueueItems)
                    {
                        ImportQueue importQueue = new ImportQueue
                        {
                            Guid = processedQueueItem.Guid,
                            TemplateId = processedQueueItem.TemplateId,
                            FilenameWithoutExtension = processedQueueItem.FilenameWithoutExtension,
                            Extension = processedQueueItem.Extension,
                            Fullpath = processedQueueItem.Fullpath,
                            ProcessStage = EnumProcessStage.Preprocess
                        };
                        _unitOfWork.GetImportQueueRepository().Add(importQueue);
                        _unitOfWork.GetProcessedQueueRepository().Delete(pq => pq.Id == processedQueueItem.Id);
                    }
                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        public async Task MoveFromImportToOCR()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var importQueueItems = _unitOfWork.GetImportQueueRepository().GetMultiple(q => q.ProcessStage == EnumProcessStage.PostProcess);
                    foreach (var importQueueItem in importQueueItems)
                    {
                        OCRQueue ocrQueue = new OCRQueue
                        {
                            Guid = importQueueItem.Guid,
                            TemplateId = importQueueItem.TemplateId,
                            FilenameWithoutExtension = importQueueItem.FilenameWithoutExtension,
                            Extension = importQueueItem.Extension,
                            Fullpath = importQueueItem.Fullpath,
                            ProcessStage = EnumProcessStage.Preprocess
                        };
                        _unitOfWork.GetOCRQueueRepository().Add(ocrQueue);
                        _unitOfWork.GetImportQueueRepository().Delete(pq => pq.Id == importQueueItem.Id);
                    }
                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        public async Task ProcessOCRQueue()
        {
            var ocrQueueItems = _unitOfWork.GetOCRQueueRepository().GetMultiple(q => q.ProcessStage == EnumProcessStage.Preprocess);
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var ocrQueueItem in ocrQueueItems)
                    {
                        //ocrQueueItem.ProcessStage = EnumProcessStage.InProcess;
                        _unitOfWork.GetOCRQueueRepository().Update(ocrQueueItem);
                    }
                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var ocrQueueItem in ocrQueueItems)
                    {
                        var pdfPath = ocrQueueItem.Fullpath;
                        PdfDocument mPdfDocument = PdfDocument.FromFile(pdfPath);
                        string[] imagePaths = mPdfDocument.ToJpegImages($@"{DocumentConfiguration.RootPath}\Jpg\{ocrQueueItem.Guid}_*.jpg");
                        for (var i=0; i<imagePaths.Length; i++)
                        {
                            var imagePath = imagePaths[i];
                            var newImagePath = $@"{DocumentConfiguration.RootPath}\Jpg\{ocrQueueItem.Guid}_{i}.jpg";
                            if (File.Exists(newImagePath)) File.Delete(newImagePath);
                            File.Move(imagePath, newImagePath);
                        }
                        /*var client = new HttpClient();
                        MultipartFormDataContent form = new MultipartFormDataContent();
                        var queryString = HttpUtility.ParseQueryString(string.Empty);

                        // Request headers
                        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b96af7aa360f455184efa22465f426d9");

                        // Request parameters
                        queryString["language"] = "en";
                        queryString["detectOrientation"] = "true";
                        var uri = "https://kyoceraformextractor.cognitiveservices.azure.com/vision/v3.1/ocr?" + queryString;

                        // Request body
                        string filePath = ocrQueueItem.Fullpath;
                        byte[] byteData = File.ReadAllBytes(filePath);

                        using (var content = new ByteArrayContent(byteData))
                        {
                            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                            response = await client.PostAsync(uri, content);
                        }
                        form.Add(new ByteArrayContent(byteData));
                        var result = await client.PostAsync(uri, form);
                        var response = await result.Content.ReadAsStringAsync();*/
                    }
                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        public async Task MoveFromOCRToClassification()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var ocrQueueItems = _unitOfWork.GetOCRQueueRepository().GetMultiple(q => q.ProcessStage == EnumProcessStage.PostProcess);
                    foreach (var ocrQueueItem in ocrQueueItems)
                    {
                        ClassificationQueue classificationQueue = new ClassificationQueue
                        {
                            Guid = ocrQueueItem.Guid,
                            TemplateId = ocrQueueItem.TemplateId,
                            FilenameWithoutExtension = ocrQueueItem.FilenameWithoutExtension,
                            Extension = ocrQueueItem.Extension,
                            Fullpath = ocrQueueItem.Fullpath,
                            ProcessStage = EnumProcessStage.Preprocess
                        };
                        _unitOfWork.GetClassificationQueueRepository().Add(classificationQueue);
                        _unitOfWork.GetOCRQueueRepository().Delete(pq => pq.Id == ocrQueueItem.Id);
                    }

                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        public async Task MoveFromClassificationToPreprocessing()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var classificationQueueItems = _unitOfWork.GetClassificationQueueRepository().GetMultiple(q => q.ProcessStage == EnumProcessStage.PostProcess);
                    foreach (var classificationQueueItem in classificationQueueItems)
                    {
                        PreprocessingQueue preprocessingQueue = new PreprocessingQueue
                        {
                            Guid = classificationQueueItem.Guid,
                            TemplateId = classificationQueueItem.TemplateId,
                            FilenameWithoutExtension = classificationQueueItem.FilenameWithoutExtension,
                            Extension = classificationQueueItem.Extension,
                            Fullpath = classificationQueueItem.Fullpath,
                            ProcessStage = EnumProcessStage.Preprocess
                        };
                        _unitOfWork.GetPreprocessingQueueRepository().Add(preprocessingQueue);
                        _unitOfWork.GetClassificationQueueRepository().Delete(pq => pq.Id == classificationQueueItem.Id);
                    }
                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        public async Task MoveFromPreprocessingToExtract()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var preprocessingQueueItems = _unitOfWork.GetPreprocessingQueueRepository().GetMultiple(q => q.ProcessStage == EnumProcessStage.PostProcess);
                    foreach (var preprocessingQueueItem in preprocessingQueueItems)
                    {
                        PreprocessingQueue preprocessingQueue = new PreprocessingQueue
                        {
                            Guid = preprocessingQueueItem.Guid,
                            TemplateId = preprocessingQueueItem.TemplateId,
                            FilenameWithoutExtension = preprocessingQueueItem.FilenameWithoutExtension,
                            Extension = preprocessingQueueItem.Extension,
                            Fullpath = preprocessingQueueItem.Fullpath,
                            ProcessStage = EnumProcessStage.Preprocess
                        };
                        _unitOfWork.GetPreprocessingQueueRepository().Add(preprocessingQueue);
                        _unitOfWork.GetImportQueueRepository().Delete(pq => pq.Id == preprocessingQueueItem.Id);
                    }
                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        public async Task MoveFromExtractToScripting()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var extractQueueItems = _unitOfWork.GetExtractQueueRepository().GetMultiple(q => q.ProcessStage == EnumProcessStage.PostProcess);
                    foreach (var extractQueueItem in extractQueueItems)
                    {
                        ScriptingQueue scriptingQueue = new ScriptingQueue
                        {
                            Guid = extractQueueItem.Guid,
                            TemplateId = extractQueueItem.TemplateId,
                            FilenameWithoutExtension = extractQueueItem.FilenameWithoutExtension,
                            Extension = extractQueueItem.Extension,
                            Fullpath = extractQueueItem.Fullpath,
                            ProcessStage = EnumProcessStage.Preprocess
                        };
                        _unitOfWork.GetScriptingQueueRepository().Add(scriptingQueue);
                        _unitOfWork.GetExtractQueueRepository().Delete(pq => pq.Id == extractQueueItem.Id);
                    }
                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }

        public async Task MoveFromScriptingToIntegration()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var scriptingQueueItems = _unitOfWork.GetScriptingQueueRepository().GetMultiple(q => q.ProcessStage == EnumProcessStage.PostProcess);
                    foreach (var scriptingQueueItem in scriptingQueueItems)
                    {
                        IntegrationQueue integrationQueue = new IntegrationQueue
                        {
                            Guid = scriptingQueueItem.Guid,
                            TemplateId = scriptingQueueItem.TemplateId,
                            FilenameWithoutExtension = scriptingQueueItem.FilenameWithoutExtension,
                            Extension = scriptingQueueItem.Extension,
                            Fullpath = scriptingQueueItem.Fullpath,
                            ProcessStage = EnumProcessStage.Preprocess
                        };
                        _unitOfWork.GetIntegrationQueueRepository().Add(integrationQueue);
                        _unitOfWork.GetScriptingQueueRepository().Delete(pq => pq.Id == scriptingQueueItem.Id);
                    }
                    await _unitOfWork.Commit();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }
    }
}
