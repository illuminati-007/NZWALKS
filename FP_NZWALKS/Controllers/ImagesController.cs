using FP_NZWALKS.Models.Domain;
using FP_NZWALKS.Models.DTO;
using FP_NZWALKS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace FP_NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository) {
            this.imageRepository = imageRepository;
        }


        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
         {
            
                ValidateFileUpload(request);
                if (ModelState.IsValid)
                {
                    //convert dto to domain model 
                    var ImageDomainModel = new Image
                    {
                        File = request.File,
                        FileExtension = Path.GetExtension(request.File.FileName),
                        FileSizeInBytes = request.File.Length,
                        FileName = request.FileName,
                        FileDescription = request.FileDescription
                    };


                    //User repository to upload image
                    await imageRepository.Upload(ImageDomainModel);


                    return Ok(ImageDomainModel);
                }
                return BadRequest(ModelState);
            
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jped", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10mb!Please upload smaller size file.");
            }
        }
    }
}
