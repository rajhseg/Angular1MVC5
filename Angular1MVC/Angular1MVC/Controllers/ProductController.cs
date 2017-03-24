using Angular1MVC.Filters;
using Angular1MVC.Handlers;
using Angular1MVC.Models;
using Angular1MVC.Providers;
using PurchaseEntities;
using PurchaseModel.Repositories;
using PurchaseModel.Services;
using PurchaseService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Angular1MVC.Controllers
{
    [RoutePrefix("api/product")]
    [Authorization(Roles ="Admin")]
    public class ProductController:BaseController
    {
        private IUserService userService;

        public ProductController(IUnitOfWork unitOfwork, IEncryptService encService, IUserService userService) : base(unitOfwork, encService)
        {
            this.userService = userService;
        }

        [CheckMimeMultiPart]
        [Route("updateimage")]
        public async Task<FilePostResult> Updateimage()
        {
            var response = new FilePostResult();

            try
            {
                var filepath = HttpContext.Current.Server.MapPath("~/files");
                MultipartFormStreamProvider provider = new MultipartFormStreamProvider(filepath);
                await Request.Content.ReadAsMultipartAsync(provider);


                var filename = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
                var fileinfo = new FileInfo(filename);
                

                response.DownloadUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/files/" + response.FileName; ;
                ImageFile ifile = new ImageFile() { FileName = filename, FileSize = fileinfo.Length, CreatedDate = DateTime.Now, IsActive = true, IsDeleted = false, DownloadUrl = response.DownloadUrl };

                unitOfWork.ImageFiles.Insert(ifile);
                unitOfWork.Commit();

                response.Size = fileinfo.Length.ToString();
                response.TrackingId = ifile.Id.ToString();
                                           
            }
            catch (Exception ex)
            {

            }

            return response;

        }


    }
}