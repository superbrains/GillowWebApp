using GillowWebApp.ModelViews;
using GillowWebApp.Result;
using GillowWebApp.Models;
using GillowWebApp.ModelViews;
using GillowWebApp.Result;
using GillowWebApp2.Models;
using Google.Apis.Sheets.v4.Data;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;


using Hangfire;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GillowWebApp.Repositories.AllProperties
{
    public class PropertiesRepositories : IPropertiesRepositories
    {
        private readonly GillowContext _context;
        private IHostingEnvironment _environment;
        public PropertiesRepositories(GillowContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        const int size = 400;
        const int quality = 80;


        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string SpreadsheetId = "1B8i8CcpcKbM9k2mrjtrxWP-IEAFAoOOAG5upItgMJU8";
        private const string GoogleCredentialsFileName = "google-credentials.json";


        private const string ReadRange = "Sheet1!A:L";
        public string WriteRange;

        public string  BoostProperty(BoostView boostView)
        {

            var propertysub = _context.PropertySubscription.Include(p => p.Profile).FirstOrDefault(c => c.Profile.ProfileID == boostView.ProfileID);

            var property =  _context.Properties.FirstOrDefault(c => c.PropertyID == boostView.PropertyID);

            if (boostView.BoostOption == "Boost")
            {
                propertysub.PropertyBoost = propertysub.PropertyBoost - 1;

            }

            if (boostView.BoostOption == "Blast")
            {
                propertysub.PropertyBlast = propertysub.PropertyBlast - 1;

            }

            if (boostView.BoostOption == "Star")
            {
                propertysub.StarPremium = propertysub.StarPremium - 1;

            }

            if (boostView.BoostOption == "Virtual Request")
            {
                propertysub.VirtualDisplay = propertysub.VirtualDisplay - 1;

                //Virtual Display Request
                VirtualTourRequests vrRequests = new VirtualTourRequests();
                vrRequests.Profile.ProfileID = propertysub.Profile.ProfileID;
                vrRequests.Properties.PropertyID = property.PropertyID;
                vrRequests.RequestDate = DateTime.Now;
                vrRequests.Status = "New";

                _context.VirtualTourRequests.Add(vrRequests);

                _context.Entry(vrRequests.Profile).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                _context.Entry(vrRequests.Properties).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                ////Send Email to Seller

                var jobId = BackgroundJob.Enqueue(() => SendVirtualServiceEmail(propertysub.Profile.Email, property.Title, property.Location, propertysub.Profile.FullName));

            }

            //Update Properties

            property.BoostPlan = boostView.BoostOption;

            if (boostView.BoostOption == "Blast")
            {
                property.BoostPlanID = 5;
            }
            if (boostView.BoostOption == "Boost")
            {
                property.BoostPlanID = 4;
            }
            if (boostView.BoostOption == "Star")
            {
                property.BoostPlanID = 6;
            }

             _context.SaveChangesAsync();

            return "Success";
        }

        public string CompressAll()
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\Obi Azubike\Documents\GillowDB\photos\profiles");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles(); //Getting Text files

            foreach (FileInfo file in Files)
            {
                var jobId = BackgroundJob.Enqueue(() => compressImage(file.FullName));
            }

            return "Done";
        }

        public string compressImage(string filePath)
        {
            using (var image = new MagickImage(filePath))
            {

                int width, height;
                if (image.Width > image.Height)
                {
                    width = size;
                    height = Convert.ToInt32(image.Height * size / (double)image.Width);
                }
                else
                {
                    width = Convert.ToInt32(image.Width * size / (double)image.Height);
                    height = size;
                }
                image.AutoOrient();
                image.Resize(width, height);

                image.Strip();
                image.Quality = quality;
                image.Write(filePath);


            }

            return "Done";
        }

        public async Task<ActionResult<string>> DeleteVirtualRequests(VirtualRequestVM virtualRequestVM)
        {
            try
            {
                var VR = await _context.VirtualTourRequests.Include(p => p.Properties).Include(c => c.Profile).FirstOrDefaultAsync(o => o.ID == virtualRequestVM.VirtualRequestID);

                _context.VirtualTourRequests.Remove(VR);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception)
            {

                return "Failed";
            }

        }

        public IActionResult GetProperty3DImage(int PropertyID)
        {
            try
            {
                Property3D property3DImages = _context.Property3D.FirstOrDefault(e => e.Properties.PropertyID == PropertyID);

                string filename = property3DImages.ImageURL;

                var filePath = Path.Combine(_environment.WebRootPath, "property3D\\" + filename);


                var fileBytes = System.IO.File.ReadAllBytes(filePath);


                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", filename);
            }
            catch (Exception e)
            {
                return null;
               
            }

        }


        //Implement!
        private IActionResult File(MemoryStream fileMemStream, string v, string filename)
        {
            throw new NotImplementedException();
        }

        public SearchResult GetPropertyByOwner(int ProfileID, int Skip)
        {
            SearchResult searchResult = new SearchResult();
            List<Searches> searches = new List<Searches>();
            try
            {

                var properties = _context.Properties.Include(c => c.Profile)
             .Where(p => p.Profile.ProfileID == ProfileID && p.Status == "Active").OrderByDescending(p => p.PropertyID).Skip(Skip).Take(10).ToList();//((p.ProductDescription.Contains(searchText)) || (p.ProductCategory.Contains(searchText)) || (p.SubCategory.Contains(searchText))  )).OrderByDescending(c=>c.Profile.ExpiryDate) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)

                try
                {
                    foreach (var item in properties)
                    {

                        Searches searchItem = new Searches();
                        searchItem.Baths = item.Bathroom;
                        searchItem.Negotiable = item.Negotiable;
                        searchItem.Beds = item.Bedroom;
                        searchItem.Description = item.Description;
                        searchItem.Lat = item.Profile.Lat;
                        searchItem.Location = item.Location;
                        searchItem.Lon = item.Profile.Lon;
                        searchItem.Parking = item.Parking;
                        searchItem.Phone = item.Profile.PhoneNumber;
                        searchItem.Price = item.Amount;

                        searchItem.Rating = 5;//need to be calculated
                        searchItem.SellerID = item.Profile.ProfileID;
                        searchItem.SellerName = item.Profile.FullName;
                        searchItem.Category = item.Category;

                        //var propsub = _context.PropertySubscription.FirstOrDefault(x => x.Profile.ProfileID == item.p.Profile.ProfileID && x.ExpiryDate >= DateTime.Now);
                        //if (propsub != null)
                        //{
                        searchItem.Subscription = item.BoostPlan;
                        searchItem.SubscriptionID = item.BoostPlanID;
                        //}
                        //else
                        //{
                        //    searchItem.Subscription ="Free";
                        //    searchItem.SubscriptionID = 0; 
                        //}

                        searchItem.Title = item.Title;
                        searchItem.Toilets = item.Toilet;
                        searchItem.Type = "Property";
                        searchItem.TypeID = item.PropertyID;
                        searchItem.VerificationStatus = item.Profile.VerificationStatus;

                        searchItem.ImageURL = _context.PropertyImages.FirstOrDefault(x => x.Properties.PropertyID == item.PropertyID).ImageURL;
                       
                        searchItem.ImageURL3D = item.VirtualURL; 
                       // searchItem.ImageURL = "https://www.gillow.ng/app/api/PropertyImages/GetPropertyImage?PropertyID=" + item.PropertyID.ToString();
                        searchItem.VirtualURL = item.VirtualURL;

                        searchItem.ImageList = new List<string>();

                        var imagelist = _context.PropertyImages.Where(x => x.Properties.PropertyID == item.PropertyID).ToList();
                        if (imagelist != null)
                        {
                            foreach (var itemImg in imagelist)
                            {
                                searchItem.ImageList.Add("https://www.gillow.ng/app/api/PropertyImages/GetPropertyImageByImageID?ImageID=" + itemImg.ImageID.ToString());
                            }
                        }


                        // searchItem.Location = item.p.Profile.

                        searches.Add(searchItem);
                    }
                }
                catch (Exception)
                {


                }

                searchResult.searches = searches;// await _context.Service.Include(b => b.Profile).Where(c => c.Profile.SuscriptionPlan != "Free" && c.Profile.ExpiryDate>DateTime.Now).OrderBy(d=>d.Profile.Lat).Skip(Skip).Take(10).ToListAsync();



                searchResult.message = "Success";

                return (searchResult);

            }
            catch (Exception e)
            {

                searchResult.searches = null;// await _context.Service.Include(b => b.Profile).Where(c => c.Profile.SuscriptionPlan != "Free" && c.Profile.ExpiryDate>DateTime.Now).OrderBy(d=>d.Profile.Lat).Skip(Skip).Take(10).ToListAsync();



                searchResult.message = e.InnerException.Message;

                return (searchResult);
            }
        }

        public async Task<ActionResult<propertiesdetailsResult>> GetPropertyByProfile(int ProfileID, int Skip)
        {
            try
            {
                var property = await _context.Properties.Where(x => x.Profile.ProfileID == ProfileID && x.Status == "Active").OrderByDescending(p => p.PropertyID).Skip(Skip).Take(10).ToListAsync();
                List<propertiesdetails> propertiesdetails = new List<propertiesdetails>();

                foreach (var item in property)
                {
                    propertiesdetails details = new propertiesdetails();
                    details.Amount = item.Amount;
                    details.Bathroom = item.Bathroom;
                    details.Bedroom = item.Bedroom;
                    details.BoostPlan = item.BoostPlan;
                    details.BoostPlanID = item.BoostPlanID;
                    details.Category = item.Category;
                    details.Country = item.Country;
                    details.Currency = item.Currency;
                    details.Negotiable = item.Negotiable;
                    details.DatePosted = item.DatePosted;
                    details.Description = item.Description;
                    var features = await _context.PropertyFeatures.FirstOrDefaultAsync(x => x.Properties.PropertyID == item.PropertyID);/// work on
                    if (features != null)
                    {
                        details.Features = features.Feature;

                    }
                    else
                    {
                        details.Features = "";

                    }

                    var imagelist = await _context.PropertyImages.Where(x => x.Properties.PropertyID == item.PropertyID).ToListAsync();
                    details.ImageList = new List<string>();

                    foreach (var itemimg in imagelist)
                    {
                        details.ImageList.Add("https://www.gillow.ng/app/api/PropertyImages/GetPropertyImageByImageID?ImageID=" + itemimg.ImageID.ToString());
                    }

                    details.ImageURL3D = "https://www.gillow.ng/app/api/Property3D/GetProperty3DImage?PropertyID=" + item.PropertyID.ToString();

                    details.Location = item.Location;
                    details.Parking = item.Parking;
                    details.PropertyID = item.PropertyID ?? default(int);
                    details.State = item.State;
                    details.Title = item.Title;
                    details.Toilet = item.Toilet;
                    details.Type = item.Type;
                    details.Views = item.Views;
                    details.VirtualURL = item.VirtualURL;

                    details.YoutubeLink = item.YoutubeLink;

                    propertiesdetails.Add(details);
                }

                propertiesdetailsResult result = new propertiesdetailsResult();
                result.propertiesdetails = propertiesdetails;
                result.message = "Sucess";
                result.id = null;

                return result;

            }
            catch (Exception e)
            {

                propertiesdetailsResult result = new propertiesdetailsResult();
                result.propertiesdetails = null;
                result.message = e.InnerException.Message;
                result.id = null;

                return result;
            }
        }

        public  string GetPropertyFeatures(int PropertyID)
        {
            try
            {

                //AlsoImplement this based on Location Later
                var features = _context.PropertyFeatures.Where(c => c.Properties.PropertyID == PropertyID).ToList() ;
                if (features != null)
                {
                    return features[0].Feature;
                }
                return "";



            }
            catch (Exception e)
            {

                return "Not Found";
            }
        }

        public IActionResult GetPropertyImage(int PropertyID)
        {
            try
            {
                PropertyImages propertyImages = _context.PropertyImages.FirstOrDefault(e => e.Properties.PropertyID == PropertyID);

                string filename = propertyImages.ImageURL;

                var filePath = Path.Combine(_environment.WebRootPath, "properties\\" + filename);


                var fileBytes = System.IO.File.ReadAllBytes(filePath);


                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", filename);
            }
            catch (Exception e)
            {
                string filename = "NoImage.png";

                var filePath = Path.Combine(_environment.WebRootPath, filename);


                var fileBytes = System.IO.File.ReadAllBytes(filePath);


                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", filename);
            }
        }


        public IActionResult GetPropertyImageByImageID(int ImageID)
        {
            try
            {
                PropertyImages propertyImages = _context.PropertyImages.FirstOrDefault(e => e.ImageID == ImageID);

                string filename = propertyImages.ImageURL;

                var filePath = Path.Combine(_environment.WebRootPath, "properties\\" + filename);


                var fileBytes = System.IO.File.ReadAllBytes(filePath);


                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", filename);
            }
            catch (Exception e)
            {
                string filename = "NoImage.png";

                var filePath = Path.Combine(_environment.WebRootPath, filename);


                var fileBytes = System.IO.File.ReadAllBytes(filePath);


                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", filename);
            }
        }

        public async Task<ActionResult<PropertyImageResult>> GetPropertyImageList(int PropertyID)
        {
            try
            {
                PropertyImageResult productResult = new PropertyImageResult();

                productResult.propertyImages = await _context.PropertyImages.Include(c => c.Properties).Where(b => b.Properties.PropertyID == PropertyID).ToListAsync();
                productResult.message = "Success";

                return (productResult);
            }
            catch (Exception e)
            {

                PropertyImageResult productResult = new PropertyImageResult();

                productResult.propertyImages = null;// await _context.ProductImages.Include(c => c.Products).Where(b => b.Products.ProductID == ProductID).ToListAsync();
                productResult.message = e.Message;
                //  registrationResult.id = "Success";

                return (productResult);
            }
        }

        public async Task<ActionResult<List<VirtualTourRequestDetails>>> GetVirtualRequests()
        {
            List<VirtualTourRequestDetails> virtualTourRequests = new List<VirtualTourRequestDetails>();
            try
            {
                var vr = await _context.VirtualTourRequests.Include(p => p.Profile).Include(l => l.Properties).Where(x => x.Status == "New").ToListAsync();
                if (vr != null)
                {
                    foreach (var item in vr)
                    {
                        VirtualTourRequestDetails vrReq = new VirtualTourRequestDetails();
                        vrReq.Email = item.Profile.Email;
                        vrReq.ID = item.ID ?? default(int);
                        vrReq.Location = item.Properties.Location;
                        vrReq.Name = item.Profile.FullName;
                        vrReq.Phone = item.Profile.PhoneNumber;
                        vrReq.ProfileID = item.Profile.ProfileID ?? default(int);
                        vrReq.PropertyID = item.Properties.PropertyID ?? default(int);
                        vrReq.RequestDate = item.RequestDate;
                        vrReq.Status = item.Status;
                        vrReq.Title = item.Properties.Title;

                        virtualTourRequests.Add(vrReq);
                    }
                }

                return virtualTourRequests;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public PropertyResult PostProperty([FromForm] PropertyView propertyView)
        {
            try
            {
                Properties property = new Properties();


                property.Amount = propertyView.Amount;
                property.Bathroom = propertyView.Bathroom;
                property.Bedroom = propertyView.Bedroom;
                property.Category = propertyView.Category;
                property.Country = propertyView.Country;
                property.Currency = propertyView.Currency;
                property.DatePosted = DateTime.UtcNow;
                property.Description = propertyView.Description;
                property.Location = propertyView.Location;
                property.Parking = propertyView.Parking;
                property.Negotiable = propertyView.Negotiable;
                property.Profile.ProfileID = propertyView.ProfileID;
                property.State = propertyView.State;
                property.Status = "Active";
                property.Title = propertyView.Title;
                property.Toilet = propertyView.Toilet;
                property.Type = propertyView.Type;
                property.Views = 0;
                property.YoutubeLink = propertyView.YoutubeLink;
                property.BoostPlan = "Free";
                property.BoostPlanID = 0;
                property.VirtualURL = propertyView.VirtualURL;


                _context.Properties.Add(property);

                _context.Entry(property.Profile).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                 _context.SaveChangesAsync();

                //Upload Product Images
                var numberoffiles = propertyView.files.Count;

                for (int i = 0; i < numberoffiles; i++)
                {
                    var file = propertyView.files[i];

                    var extension = Path.GetExtension(file.FileName);
                    var fileName = "propertypic" + i + property.PropertyID + propertyView.ProfileID.ToString() + extension;

                    var filePath = Path.Combine(_environment.WebRootPath, "properties\\" + fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(stream);

                    }

                    //Send to Hangfire
                    var jobId = BackgroundJob.Enqueue(() => compressImage(filePath));


                    var picURL = fileName;

                    PropertyImages propertyImages = new PropertyImages();
                    propertyImages.Properties.PropertyID = property.PropertyID;
                    propertyImages.ImageURL = picURL;


                    _context.PropertyImages.Add(propertyImages);
                    _context.Entry(propertyImages.Properties).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                }

                int numberof3Dfiles;

                if (propertyView.file3D != null)
                {
                    numberof3Dfiles = propertyView.file3D.Count;
                }
                else
                {
                    numberof3Dfiles = 0;
                }


                for (int i = 0; i < numberof3Dfiles; i++)
                {
                    var file = propertyView.file3D[i];

                    var extension = Path.GetExtension(file.FileName);
                    var fileName = "property3Dpic" + i + property.PropertyID + propertyView.ProfileID.ToString() + extension;

                    var filePath = Path.Combine(_environment.WebRootPath, "property3D\\" + fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                         file.CopyToAsync(stream);



                    }



                    var picURL = fileName;

                    Property3D propertyImages = new Property3D();
                    propertyImages.Properties.PropertyID = property.PropertyID;
                    propertyImages.ImageURL = picURL;


                    _context.Property3D.Add(propertyImages);
                    _context.Entry(propertyImages.Properties).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                }

                for (int i = 0; i < propertyView.Features.Count; i++)
                {
                    PropertyFeatures propertyFeatures = new PropertyFeatures();
                    propertyFeatures.Feature = propertyView.Features[i].ToString();
                    propertyFeatures.Properties.PropertyID = property.PropertyID;
                    _context.PropertyFeatures.Add(propertyFeatures);

                    _context.Entry(propertyFeatures.Properties).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                }

                //Send to GoogleSheet


                var val = new ValueRange { Values = new List<IList<object>> { new List<object> { property.PropertyID, propertyView.Title + " at " + propertyView.Location, propertyView.Description, "https://gillow.ng/propertiesDetails?ID=" + property.PropertyID + "&SellerId=" + propertyView.ProfileID, "New", propertyView.Amount + " NGN", "In Stock", "https://www.gillow.ng/app/api/PropertyImages/GetPropertyImage?PropertyID=" + property.PropertyID, "", "", "Gillow", "Home & Garden" } } };
                var jobId2 = BackgroundJob.Enqueue(() => WriteToGoogleSheet(val));
                //WriteToGoogleSheet

                var propsub =  _context.PropertySubscription.FirstOrDefault(c => c.Profile.ProfileID == propertyView.ProfileID);
                if (propsub == null)
                {
                    PropertySubscription propertySubscription = new PropertySubscription();
                    propertySubscription.ExpiryDate = DateTime.Now;
                    propertySubscription.NumberofMonths = 0;
                    propertySubscription.Profile.ProfileID = propertyView.ProfileID;
                    propertySubscription.PropertyBlast = 0;
                    propertySubscription.PropertyBoost = 0;
                    propertySubscription.PropertyListing = 10;
                    propertySubscription.RealtorSpecialist = 0;
                    propertySubscription.StarPremium = 0;
                    propertySubscription.VirtualDisplay = 0;
                    propertySubscription.SubscriptionPlan = "Free";
                    propertySubscription.SubscriptionPlanID = 0;
                    propertySubscription.ZoneSpecialist = 0;
                    _context.PropertySubscription.Add(propertySubscription);
                    _context.Entry(propertySubscription.Profile).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;


                }
                else
                {
                    if (propsub.SubscriptionPlan == "Free")
                    {
                        propsub.PropertyListing = propsub.PropertyListing - 1;
                    }
                }



                 _context.SaveChangesAsync();


                PropertyResult profileResult = new PropertyResult();
                profileResult.message = "Success";

                profileResult.id = property.PropertyID;



                return (profileResult);
            }
            catch (Exception e)
            {


                PropertyResult profileResult = new PropertyResult();
                profileResult.message = e.Message + " , " + e.InnerException.Message;

                profileResult.id = null;
                return (profileResult);
            }
        }

        public Task<IActionResult> PostProperty3D([FromBody] Property3D property3D)
        {
            throw new NotImplementedException();

        }

       
        public string PostRequests(VirtualRequestVM virtualRequestVM)
        {
            throw new NotImplementedException();
        }


        public string RemoveProperty(UpdateViewVM updateView)
        {
            try
            {
                var property = _context.Properties.FirstOrDefault(c => c.PropertyID == updateView.PropertyID);

                if (property != null)
                {
                    property.Status = "Removed";

                    //Take allOther Agent Info
                    _context.SaveChangesAsync();
                    return "Success";
                }

                return "Not Found";


            }
            catch (Exception e)
            {

                return e.InnerException.Message + ", " + e.Message;
            }
        }

        public string SendVirtualServiceEmail(string CustomerEmail, string PropertyTitle, string PropertyLocation, string CustomerName)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //set the addresses 
                mail.From = new MailAddress("virtualservice@gillow.ng"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(CustomerEmail);
                mail.To.Add("virtualservice@gillow.ng");

                //set the content 
                mail.Subject = "Gillow: Virtual Coverage Request Acknowleded!";
                mail.Body = "<b>Dear " + CustomerName + ", </b><br/><h5>We have received your request for a Virtual Tour coverage of your property (" + PropertyTitle + ")  located at " + PropertyLocation + "</h5><br/> A Gillow Agent will contact you shortly to proceed further. Thanks<br/><h4>GILLOW NG</h4>";

                //send the message 
                SmtpClient smtp = new SmtpClient("mail.gillow.ng");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("virtualservice@gillow.ng", "Password1@");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 25;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mail);

                return "Success";
            }
            catch (Exception)
            {

                return "Fail";
            }

        }

        public string SendVirtualServiceEmailCompleted(string CustomerEmail, string PropertyTitle, string PropertyLocation, string CustomerName)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //set the addresses 
                mail.From = new MailAddress("virtualservice@gillow.ng"); //IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(CustomerEmail);


                //set the content 
                mail.Subject = "Gillow: Virtual Coverage Request Updated!";
                mail.Body = "<b>Dear " + CustomerName + ", </b><br/><h5>We have updated your request for a Virtual Tour coverage of your property (" + PropertyTitle + ")  located at " + PropertyLocation + "</h5><br/> Thank you for choosing GILLOW NG.";

                //send the message 
                SmtpClient smtp = new SmtpClient("mail.gillow.ng");

                //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
                NetworkCredential Credentials = new NetworkCredential("virtualservice@gillow.ng", "Password1@");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = Credentials;
                smtp.Port = 25;    //alternative port number is 8889
                smtp.EnableSsl = false;
                smtp.Send(mail);

                return "Success";
            }
            catch (Exception)
            {

                return "Fail";
            }
        }

        public string UpdateView(UpdateViewVM updateView)
        {
            try
            {
                var property = _context.Properties.FirstOrDefault(c => c.PropertyID == updateView.PropertyID);

                if (property != null)
                {
                    property.Views = property.Views + 1;

                    //Take allOther Agent Info
                    _context.SaveChangesAsync();
                    return "Success";
                }

                return "Not Found";


            }
            catch (Exception e)
            {

                return e.InnerException.Message + ", " + e.Message;
            }
        }

        public async Task<ActionResult<string>> UpdateVirtualRequests(VirtualRequestVM virtualRequestVM)
        {
            try
            {
                var VR = await _context.VirtualTourRequests.Include(p => p.Properties).Include(c => c.Profile).FirstOrDefaultAsync(o => o.ID == virtualRequestVM.VirtualRequestID);

                var prop = await _context.Properties.FirstOrDefaultAsync(p => p.PropertyID == VR.Properties.PropertyID);
                prop.VirtualURL = virtualRequestVM.VirtualURL;

                VR.Status = "Done";

                var jobId = BackgroundJob.Enqueue(() => SendVirtualServiceEmailCompleted(VR.Profile.Email, prop.Title, prop.Location, VR.Profile.FullName));

                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception)
            {

                return "Failed";
            }
        }

        public SheetsService GetSheetsService()
        {
            using (var stream = new FileStream(GoogleCredentialsFileName, FileMode.Open, FileAccess.Read))
            {
                var serviceInitializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
                };
                return new SheetsService(serviceInitializer);
            }
        }

        public async Task<string> WriteAllToGoogleSheet(int skip)
        {
            var property = await _context.Properties.Include(p => p.Profile).OrderBy(p => p.PropertyID).Skip(skip).Take(100).ToListAsync();
            var serviceValues = GetSheetsService().Spreadsheets.Values;
            foreach (var item in property)
            {
                var val = new ValueRange { Values = new List<IList<object>> { new List<object> { item.PropertyID, item.Title + " at " + item.Location, item.Description, "https://gillow.ng/propertiesDetails?ID=" + item.PropertyID + "&SellerId=" + item.Profile.ProfileID, "New", item.Amount + " NGN", "In Stock", "https://www.gillow.ng/app/api/PropertyImages/GetPropertyImage?PropertyID=" + item.PropertyID, "", "", "Gillow", "Home & Garden" } } };

                await Write(serviceValues, val);
            }


            return "Sucess";
        }

        public async Task<string> WriteToGoogleSheet(ValueRange value)
        {
            var serviceValues = GetSheetsService().Spreadsheets.Values;

            await Write(serviceValues, value);

            return "Sucess";
        }

        public async Task Write(SpreadsheetsResource.ValuesResource valuesResource, ValueRange value)
        {
            var response = await valuesResource.Get(SpreadsheetId, ReadRange).ExecuteAsync();
            var values = response.Values;

            var str = "A" + (values.Count + 1) + ":L" + (values.Count + 1);

            if (values == null || !values.Any())
            {
                Console.WriteLine("No data found.");
                return;
            }

            var valueRange = value;//new ValueRange { Values = new List<IList<object>> { new List<object> { 35, "Property Title", "Property Title", "https://gillow.ng/propertiesDetails?ID=176&SellerId=125", "New", "38000000 NGN", "In Stock", "https://www.gillow.ng/app/api/PropertyImages/GetPropertyImage?PropertyID=34", "", "", "Gillow", "Home & Garden" } } };


            var update = valuesResource.Update(valueRange, SpreadsheetId, str);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            await update.ExecuteAsync();

            //var header = string.Join(" ", values.First().Select(r => r.ToString()));
            //Console.WriteLine($"Header: {header}");

            //foreach (var row in values.Skip(1))
            //{
            //    var res = string.Join(" ", row.Select(r => r.ToString()));
            //    Console.WriteLine(res);
            //}
        }

        public SearchResult Filter(SearchModel query)
        {
            SearchResult searchResult = new SearchResult();
            List<Searches> searches = new List<Searches>();

            if (query.Keywords == null || string.IsNullOrEmpty(query.Keywords))
            {
                query.Keywords = "";
            }

            if (query.Location == null || string.IsNullOrEmpty(query.Location))
            {
                query.Location = "Nigeria";
            }
            else
            {
                string[] loc = query.Location.Split(",");
                query.Location = loc[0];
            }

            if (query.MaxPrice == 0 )
            {
                query.MaxPrice = 50000000000;
            }

            if (query.MinPrice == 0)
            {
                query.MinPrice = 0;
            }

            if (query.Category == null)
            {
                query.Category = "For Rent";
            }

            if (query.Type == null)
            {
                query.Type = "Houses";
            }

          
            if (query.Keywords.ToLower().Contains("glw"))
            {
                query.Keywords = query.Keywords.Replace(" ", "");
            }

            query.Keywords = query.Keywords.Replace(",", " ");

            var searchArray = query.Keywords.ToLower().Split(" ");


            string propidstr;
            int propid=0;
            int propidpub=0;
            if (searchArray[0].ToString().ToLower().Contains("glw"))
            {
                propidstr = Regex.Replace(searchArray[0], "[^0-9]+", string.Empty);
                propid = int.Parse(propidstr);
                propidpub = propid;
            }
            else
            {
                
                searchArray = searchArray.Where(o => o.Length >= 3).ToArray();
            }


            IQueryable<Properties> prop;

            if (propid > 0)
            {
                prop = _context.Properties.Include(c => c.Profile)
                     .Where(p => p.PropertyID == propidpub);//.Contains(query.Location) && p.Amount <= query.MaxPrice && p.Amount >= query.MinPrice && p.Category == query.Category && p.Toilet == query.Toilet && searchArray.All(s => p.Description.ToLower().Contains(s.ToLower()) || p.PropertyID == propidpub || p.Type.ToLower().Contains(s.ToLower()) || p.Title.ToLower().Contains(s.ToLower()) || p.Location.ToLower().Contains(s.ToLower()) || p.Profile.FullName.ToLower().Contains(s.ToLower()))).OrderByDescending(d => d.BoostPlanID).Skip(query.Skip).Take(20);//((p.ProductDescription.Contains(searchText)) || (p.ProductCategory.Contains(searchText)) || (p.SubCategory.Contains(searchText))  )).OrderByDescending(c=>c.Profile.ExpiryDate) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)

            }
            else if (query.Type == "Land")
            {
                prop = _context.Properties.Include(c => c.Profile)
                    .Where(p => p.Location.Contains(query.Location) && p.Amount <= query.MaxPrice && p.Amount >= query.MinPrice && p.Category == query.Category && searchArray.All(s => p.Description.ToLower().Contains(s.ToLower()) || p.PropertyID == propidpub || p.Type.ToLower().Contains(s.ToLower()) || p.Title.ToLower().Contains(s.ToLower()) || p.Location.ToLower().Contains(s.ToLower()) || p.Profile.FullName.ToLower().Contains(s.ToLower()))).OrderByDescending(d => d.BoostPlanID).Skip(query.Skip).Take(10);//((p.ProductDescription.Contains(searchText)) || (p.ProductCategory.Contains(searchText)) || (p.SubCategory.Contains(searchText))  )).OrderByDescending(c=>c.Profile.ExpiryDate) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)

            }
            else
            {
                if(query.Bedroom==0 && query.Toilet > 0)// Get based on Toilet
                {
                    prop = _context.Properties.Include(c => c.Profile)
                     .Where(p => p.Location.Contains(query.Location) && p.Amount <= query.MaxPrice && p.Amount >= query.MinPrice && p.Category == query.Category && p.Type==query.Type  && p.Toilet == query.Toilet && searchArray.All(s => p.Description.ToLower().Contains(s.ToLower()) || p.PropertyID == propidpub || p.Type.ToLower().Contains(s.ToLower()) || p.Title.ToLower().Contains(s.ToLower()) || p.Location.ToLower().Contains(s.ToLower()) || p.Profile.FullName.ToLower().Contains(s.ToLower()))).OrderByDescending(d => d.BoostPlanID).Skip(query.Skip).Take(10);//((p.ProductDescription.Contains(searchText)) || (p.ProductCategory.Contains(searchText)) || (p.SubCategory.Contains(searchText))  )).OrderByDescending(c=>c.Profile.ExpiryDate) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)

                }
                else if(query.Bedroom>0 && query.Toilet == 0) //Get based on Bedroom
                {
                    prop = _context.Properties.Include(c => c.Profile)
                    .Where(p => p.Location.Contains(query.Location) && p.Amount <= query.MaxPrice && p.Amount >= query.MinPrice && p.Category == query.Category  && p.Type == query.Type && p.Bedroom == query.Bedroom && searchArray.All(s => p.Description.ToLower().Contains(s.ToLower()) || p.PropertyID == propidpub || p.Type.ToLower().Contains(s.ToLower()) || p.Title.ToLower().Contains(s.ToLower()) || p.Location.ToLower().Contains(s.ToLower()) || p.Profile.FullName.ToLower().Contains(s.ToLower()))).OrderByDescending(d => d.BoostPlanID).Skip(query.Skip).Take(10);//((p.ProductDescription.Contains(searchText)) || (p.ProductCategory.Contains(searchText)) || (p.SubCategory.Contains(searchText))  )).OrderByDescending(c=>c.Profile.ExpiryDate) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)

                }
                else if(query.Bedroom>0 && query.Toilet>0)                 {
                    prop = _context.Properties.Include(c => c.Profile)
                    .Where(p => p.Location.Contains(query.Location) && p.Amount <= query.MaxPrice && p.Amount >= query.MinPrice && p.Category == query.Category && p.Type == query.Type && p.Toilet == query.Toilet && p.Bedroom == query.Bedroom && searchArray.All(s => p.Description.ToLower().Contains(s.ToLower()) || p.PropertyID == propidpub || p.Type.ToLower().Contains(s.ToLower()) || p.Title.ToLower().Contains(s.ToLower()) || p.Location.ToLower().Contains(s.ToLower()) || p.Profile.FullName.ToLower().Contains(s.ToLower()))).OrderByDescending(d => d.BoostPlanID).Skip(query.Skip).Take(10);//((p.ProductDescription.Contains(searchText)) || (p.ProductCategory.Contains(searchText)) || (p.SubCategory.Contains(searchText))  )).OrderByDescending(c=>c.Profile.ExpiryDate) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)

                }
                else
                {
                    prop = _context.Properties.Include(c => c.Profile)
                  .Where(p => p.Location.Contains(query.Location) && p.Amount <= query.MaxPrice && p.Amount >= query.MinPrice && p.Category == query.Category && p.Type == query.Type && searchArray.All(s => p.Description.ToLower().Contains(s.ToLower()) || p.PropertyID == propidpub || p.Type.ToLower().Contains(s.ToLower()) || p.Title.ToLower().Contains(s.ToLower()) || p.Location.ToLower().Contains(s.ToLower()) || p.Profile.FullName.ToLower().Contains(s.ToLower()))).OrderByDescending(d => d.BoostPlanID).Skip(query.Skip).Take(10);//((p.ProductDescription.Contains(searchText)) || (p.ProductCategory.Contains(searchText)) || (p.SubCategory.Contains(searchText))  )).OrderByDescending(c=>c.Profile.ExpiryDate) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)

                    // && searchArray.All(s => p.Description.ToLower().Contains(s.ToLower()) || p.PropertyID == propidpub || p.Type.ToLower().Contains(s.ToLower()) || p.Title.ToLower().Contains(s.ToLower()) || p.Location.ToLower().Contains(s.ToLower()) || p.Profile.FullName.ToLower().Contains(s.ToLower()))
                }

            }

           

            try
            {
                foreach (var item in prop)
                {

                    Searches searchItem = new Searches();
                    searchItem.Baths = item.Bathroom;
                    searchItem.Beds = item.Bedroom;
                    searchItem.Description = item.Description;
                    searchItem.Lat = item.Profile.Lat;
                    searchItem.Negotiable = item.Negotiable;
                    searchItem.Location = item.Location;
                    searchItem.Lon = item.Profile.Lon;
                    searchItem.Parking = item.Parking;
                    searchItem.Phone = item.Profile.PhoneNumber;
                    searchItem.Price = item.Amount;
                    searchItem.Rating = 5;//need to be calculated
                    searchItem.SellerID = item.Profile.ProfileID;
                    searchItem.SellerName = item.Profile.FullName;
                    searchItem.Category = item.Category;

                    searchItem.Subscription = item.BoostPlan;
                    searchItem.SubscriptionID = item.BoostPlanID;
                    searchItem.YoutubeLink = item.YoutubeLink;
                    searchItem.VirtualURL = item.VirtualURL;
                    searchItem.Title = item.Title;
                    searchItem.Toilets = item.Toilet;
                    searchItem.Type = "Property";
                    searchItem.TypeID = item.PropertyID;
                    searchItem.VerificationStatus = item.Profile.VerificationStatus;
                    searchItem.ImageURL3D = item.VirtualURL;
                   

                    searchItem.ImageList = new List<string>();

                    int i = 0;
                    var imagelist = _context.PropertyImages.Where(x => x.Properties.PropertyID == item.PropertyID).ToList();
                    if (imagelist != null)
                    {
                        foreach (var itemImg in imagelist)
                        {
                            if (i == 0)
                            {
                                searchItem.ImageURL = itemImg.ImageURL.ToString();
                            }
                            searchItem.ImageList.Add(itemImg.ImageURL.ToString());
                            i++;
                        }
                    }

                    searchItem.Features = GetPropertyFeatures(item.PropertyID ?? default(int));


                    // searchItem.Location = item.p.Profile.

                    searches.Add(searchItem);
                }

                searchResult.searches = searches;

                return searchResult;
            }
            catch (Exception)
            {

                return null;
            }


            //    if (bed > 0)
            //    {
            //        searches = searches.Where(p => p.Beds <= bed).ToList();
            //    }

            //    if (bath > 0)
            //    {
            //        searches = searches.Where(p => p.Baths == bath).ToList();
            //    }

            //    if (toilet > 0)
            //    {
            //        searches = searches.Where(p => p.Toilets == toilet).ToList();
            //    }

            //    if (park > 0)
            //    {
            //        searches = searches.Where(p => p.Parking == park).ToList();
            //    }

            //    if (maxprice > 0)
            //    {
            //        searches = searches.Where(p => p.Price <= maxprice).ToList();
            //    }




            //searchResult.searches = searches;// await _context.Service.Include(b => b.Profile).Where(c => c.Profile.SuscriptionPlan != "Free" && c.Profile.ExpiryDate>DateTime.Now).OrderBy(d=>d.Profile.Lat).Skip(Skip).Take(12).ToListAsync();



            //searchResult.message = "Success";

            return (searchResult);
        }

        public double distanceInMiles(double lon1d, double lat1d, double lon2d, double lat2d)
        {
            var lon1 = ToRadians(lon1d);
            var lat1 = ToRadians(lat1d);
            var lon2 = ToRadians(lon2d);
            var lat2 = ToRadians(lat2d);

            var deltaLon = lon2 - lon1;
            var c = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(deltaLon));
            var earthRadius = 3958.76;
            var distInMiles = earthRadius * c;

            return distInMiles;
        }

        public double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0; 
        }

        int propidpub;
        public ActionResult<SearchResult> FilterByText(string searchText, int Skip, double lat, double lon, int km)
        {
            string propidstr;
            int propid;
            try
            {
                var myLat = lat;
                var myLon = lon;
                var radiusInMile = km * 0.621371;

                var minMilePerLat = 68.703;
                var milePerLon = Math.Cos(myLat) * 69.172;
                var minLat = myLat - radiusInMile / minMilePerLat;
                var maxLat = myLat + radiusInMile / minMilePerLat;
                var minLon = myLon - radiusInMile / milePerLon;
                var maxLon = myLon + radiusInMile / milePerLon;

                SearchResult searchResult = new SearchResult();

                if (searchText == null)
                {
                    searchText = "";
                }


                if (searchText.ToLower().Contains("glw"))
                {
                    searchText = searchText.Replace(" ", "");
                }

                var searchArray = searchText.ToLower().Split(" ");



                if (searchArray[0].ToString().ToLower().Contains("glw"))
                {
                    propidstr = Regex.Replace(searchArray[0], "[^0-9]+", string.Empty);
                    propid = int.Parse(propidstr);
                    propidpub = propid;
                }
                else
                {
                    searchArray = searchArray.Where(o => o.Length >= 3).ToArray();
                }



                List<Searches> searches = new List<Searches>();


                if (searchArray.Length > 0)
                {
                    var properties = _context.Properties.Include(c => c.Profile)
                       .Where(p => (minLat <= p.Profile.Lat && p.Profile.Lat <= maxLat) && (minLon <= p.Profile.Lon && p.Profile.Lon <= maxLon) && (p.Status == "Active") && searchArray.All(s => p.Description.ToLower().Contains(s.ToLower()) || p.PropertyID == propidpub || p.Type.ToLower().Contains(s.ToLower()) || p.Title.ToLower().Contains(s.ToLower()) || p.Location.ToLower().Contains(s.ToLower()) || p.Profile.FullName.ToLower().Contains(s.ToLower()))).OrderByDescending(d => d.BoostPlanID).Skip(Skip).Take(12)//((p.ProductDescription.Contains(searchText)) || (p.ProductCategory.Contains(searchText)) || (p.SubCategory.Contains(searchText))  )).OrderByDescending(c=>c.Profile.ExpiryDate) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)
                       .AsEnumerable()
                       .Select(p => new { p, Dist = distanceInMiles(myLon, myLat, p.Profile.Lon, p.Profile.Lat) / 0.621371 })
                       .Where(p => p.Dist <= radiusInMile).OrderBy(p => p.Dist);

                    try
                    {
                        foreach (var item in properties)
                        {

                            Searches searchItem = new Searches();
                            searchItem.Baths = item.p.Bathroom;
                            searchItem.Beds = item.p.Bedroom;
                            searchItem.Description = item.p.Description;
                            searchItem.Lat = item.p.Profile.Lat;
                            searchItem.Location = item.p.Location;
                            searchItem.Lon = item.p.Profile.Lon;
                            searchItem.Parking = item.p.Parking;
                            searchItem.Negotiable = item.p.Negotiable;
                            searchItem.Phone = item.p.Profile.PhoneNumber;
                            searchItem.Price = item.p.Amount;
                            searchItem.Rating = 5;//need to be calculated
                            searchItem.SellerID = item.p.Profile.ProfileID;
                            searchItem.SellerName = item.p.Profile.FullName;
                            searchItem.Category = item.p.Category;
                            searchItem.YoutubeLink = item.p.YoutubeLink;
                            searchItem.VirtualURL = item.p.VirtualURL;
                            //var propsub = _context.PropertySubscription.FirstOrDefault(x => x.Profile.ProfileID == item.p.Profile.ProfileID && x.ExpiryDate >= DateTime.Now);
                            //if (propsub != null)
                            //{
                            searchItem.Subscription = item.p.BoostPlan;
                            searchItem.SubscriptionID = item.p.BoostPlanID;
                            //}
                            //else
                            //{
                            //    searchItem.Subscription ="Free";
                            //    searchItem.SubscriptionID = 0; 
                            //}

                            searchItem.Title = item.p.Title;
                            searchItem.Toilets = item.p.Toilet;
                            searchItem.Type = "Property";
                            searchItem.TypeID = item.p.PropertyID;
                            searchItem.VerificationStatus = item.p.Profile.VerificationStatus;
                            searchItem.ImageURL3D = "https://www.gillow.ng/app/api/Property3D/GetProperty3DImage?PropertyID=" + item.p.PropertyID.ToString();
                            searchItem.ImageURL = "https://www.gillow.ng/app/api/PropertyImages/GetPropertyImage?PropertyID=" + item.p.PropertyID.ToString();

                            searchItem.ImageList = new List<string>();

                            var imagelist = _context.PropertyImages.Where(x => x.Properties.PropertyID == item.p.PropertyID).ToList();
                            if (imagelist != null)
                            {
                                foreach (var itemImg in imagelist)
                                {
                                    searchItem.ImageList.Add("https://www.gillow.ng/app/api/PropertyImages/GetPropertyImageByImageID?ImageID=" + itemImg.ImageID.ToString());
                                }
                            }


                            // searchItem.Location = item.p.Profile.

                            searches.Add(searchItem);
                        }
                    }
                    catch (Exception e)
                    {


                    }
                }
                else
                {
                    var properties = _context.Properties.Include(c => c.Profile)
                         .Where(p => (minLat <= p.Profile.Lat && p.Profile.Lat <= maxLat) && (minLon <= p.Profile.Lon && p.Profile.Lon <= maxLon) && (p.Status == "Active")).OrderByDescending(d => d.BoostPlanID).Skip(Skip).Take(12) //&&  (p.Profile.SuscriptionPlan != "Free") && (p.Profile.ExpiryDate > DateTime.Now)
                         .AsEnumerable()
                         .Select(p => new { p, Dist = distanceInMiles(myLon, myLat, p.Profile.Lon, p.Profile.Lat) / 0.621371 })
                         .Where(p => p.Dist <= radiusInMile).OrderBy(p => p.Dist);

                    try
                    {
                        foreach (var item in properties)
                        {

                            Searches searchItem = new Searches();
                            searchItem.Baths = item.p.Bathroom;
                            searchItem.Beds = item.p.Bedroom;
                            searchItem.Description = item.p.Description;
                            searchItem.Lat = item.p.Profile.Lat;
                            searchItem.Location = item.p.Location;
                            searchItem.Lon = item.p.Profile.Lon;
                            searchItem.Parking = item.p.Parking;
                            searchItem.Negotiable = item.p.Negotiable;
                            searchItem.Phone = item.p.Profile.PhoneNumber;
                            searchItem.Price = item.p.Amount;
                            searchItem.Rating = 5;//need to be calculated
                            searchItem.SellerID = item.p.Profile.ProfileID;
                            searchItem.SellerName = item.p.Profile.FullName;
                            searchItem.Category = item.p.Category;

                            searchItem.Subscription = item.p.BoostPlan;
                            searchItem.SubscriptionID = item.p.BoostPlanID;
                            searchItem.YoutubeLink = item.p.YoutubeLink;
                            searchItem.VirtualURL = item.p.VirtualURL;

                            searchItem.Title = item.p.Title;
                            searchItem.Toilets = item.p.Toilet;
                            searchItem.Type = "Property";
                            searchItem.TypeID = item.p.PropertyID;
                            searchItem.VerificationStatus = item.p.Profile.VerificationStatus;
                            searchItem.ImageURL3D = "https://www.gillow.ng/app/api/Property3D/GetProperty3DImage?PropertyID=" + item.p.PropertyID.ToString();
                            searchItem.ImageURL = "https://www.gillow.ng/app/api/PropertyImages/GetPropertyImage?PropertyID=" + item.p.PropertyID.ToString();

                            searchItem.ImageList = new List<string>();

                            var imagelist = _context.PropertyImages.Where(x => x.Properties.PropertyID == item.p.PropertyID).ToList();
                            if (imagelist != null)
                            {
                                foreach (var itemImg in imagelist)
                                {
                                    searchItem.ImageList.Add("https://www.gillow.ng/app/api/PropertyImages/GetPropertyImageByImageID?ImageID=" + itemImg.ImageID.ToString());
                                }
                            }
                            // searchItem.Location = item.p.Profile.

                            searches.Add(searchItem);
                        }
                    }
                    catch (Exception ex)
                    {


                    }
                }




                searchResult.searches = searches;// await _context.Service.Include(b => b.Profile).Where(c => c.Profile.SuscriptionPlan != "Free" && c.Profile.ExpiryDate>DateTime.Now).OrderBy(d=>d.Profile.Lat).Skip(Skip).Take(12).ToListAsync();



                searchResult.message = "Success";

                return (searchResult);
            }

            catch (Exception e)
            {
                SearchResult searchResult = new SearchResult();

                searchResult.searches = null;
                searchResult.message = e.Message;

                return (searchResult);
            }
        }

        public SearchResult FilterByProperty(int PropertyID)
        {
            try
            {

                SearchResult searchResult = new SearchResult();

                List<Searches> searches = new List<Searches>();

                var properties = _context.Properties.Include(c => c.Profile)
                   .Where(p => p.PropertyID == PropertyID);

                try
                {
                    foreach (var item in properties)
                    {

                        Searches searchItem = new Searches();
                        searchItem.Baths = item.Bathroom;
                        searchItem.Beds = item.Bedroom;
                        searchItem.Description = item.Description;
                        searchItem.Lat = item.Profile.Lat;
                        searchItem.Location = item.Location;
                        searchItem.Lon = item.Profile.Lon;
                        searchItem.Parking = item.Parking;
                        searchItem.Negotiable = item.Negotiable;
                        searchItem.Phone = item.Profile.PhoneNumber;
                        searchItem.Price = item.Amount;
                        searchItem.Rating = 5;//need to be calculated
                        searchItem.SellerID = item.Profile.ProfileID;
                        searchItem.SellerProfilePic = item.Profile.ImageUrl;
                        searchItem.SellerName = item.Profile.FullName;
                        searchItem.Category = item.Category;
                        searchItem.YoutubeLink = item.YoutubeLink;
                        searchItem.VirtualURL = item.VirtualURL;
                        //var propsub = _context.PropertySubscription.FirstOrDefault(x => x.Profile.ProfileID == item.p.Profile.ProfileID && x.ExpiryDate >= DateTime.Now);
                        //if (propsub != null)
                        //{
                        searchItem.Subscription = item.BoostPlan;
                        searchItem.SubscriptionID = item.BoostPlanID;
                        //}
                        //else
                        //{
                        //    searchItem.Subscription ="Free";
                        //    searchItem.SubscriptionID = 0; 
                        //}

                        searchItem.Title = item.Title;
                        searchItem.Toilets = item.Toilet;
                        searchItem.Type = "Property";
                        searchItem.TypeID = item.PropertyID;
                        searchItem.VerificationStatus = item.Profile.VerificationStatus;
                        searchItem.ImageURL3D =  item.VirtualURL;
        

                        searchItem.ImageList = new List<string>();

                        int i = 0;
                        var imagelist = _context.PropertyImages.Where(x => x.Properties.PropertyID == item.PropertyID).ToList();
                        if (imagelist != null)
                        {
                            foreach (var itemImg in imagelist)
                            {
                                if (i == 0)
                                {
                                    searchItem.ImageURL = itemImg.ImageURL.ToString();
                                }
                                searchItem.ImageList.Add(itemImg.ImageURL.ToString());
                                i++;
                            }
                        }

                        searchItem.Features = GetPropertyFeatures(item.PropertyID ?? default(int));

                        searches.Add(searchItem);
                    }
                }
                catch (Exception e)
                {


                }

                searchResult.searches = searches;// await _context.Service.Include(b => b.Profile).Where(c => c.Profile.SuscriptionPlan != "Free" && c.Profile.ExpiryDate>DateTime.Now).OrderBy(d=>d.Profile.Lat).Skip(Skip).Take(12).ToListAsync();

                searchResult.message = "Success";

                return (searchResult);
            }

            catch (Exception e)
            {
                SearchResult searchResult = new SearchResult();

                searchResult.searches = null;
                searchResult.message = e.Message;

                return (searchResult);
            }
        }

        public SearchResult Top10Properties()
        {
            SearchResult searchResult = new SearchResult();
            try
            {
               

                List<Searches> searches = new List<Searches>();


              
                    var properties = _context.Properties.Include(c => c.Profile).OrderByDescending(p=>p.BoostPlanID).OrderByDescending(d=>d.DatePosted).Take(10);
                      

                    try
                    {
                        foreach (var item in properties)
                        {

                            Searches searchItem = new Searches();
                            searchItem.Baths = item.Bathroom;
                            searchItem.Beds = item.Bedroom;
                            searchItem.Description = item.Description;
                            searchItem.Lat = item.Profile.Lat;
                            searchItem.Location = item.Location;
                            searchItem.Lon = item.Profile.Lon;
                            searchItem.Parking = item.Parking;
                            searchItem.Negotiable = item.Negotiable;
                            searchItem.Phone = item.Profile.PhoneNumber;
                            searchItem.Price = item.Amount;
                            searchItem.Rating = 5;//need to be calculated
                            searchItem.SellerID = item.Profile.ProfileID;
                            searchItem.SellerName = item.Profile.FullName;
                            searchItem.Category = item.Category;
                            searchItem.YoutubeLink = item.YoutubeLink;
                            searchItem.VirtualURL = item.VirtualURL;
                            //var propsub = _context.PropertySubscription.FirstOrDefault(x => x.Profile.ProfileID == item.p.Profile.ProfileID && x.ExpiryDate >= DateTime.Now);
                            //if (propsub != null)
                            //{
                            searchItem.Subscription = item.BoostPlan;
                            searchItem.SubscriptionID = item.BoostPlanID;
                            //}
                            //else
                            //{
                            //    searchItem.Subscription ="Free";
                            //    searchItem.SubscriptionID = 0; 
                            //}

                            searchItem.Title = item.Title;
                            searchItem.Toilets = item.Toilet;
                            searchItem.Type = "Property";
                            searchItem.TypeID = item.PropertyID;
                            searchItem.VerificationStatus = item.Profile.VerificationStatus;
                            searchItem.ImageURL3D = "https://www.gillow.ng/app/api/Property3D/GetProperty3DImage?PropertyID=" + item.PropertyID.ToString();
                           // searchItem.ImageURL = "https://www.gillow.ng/app/api/PropertyImages/GetPropertyImage?PropertyID=" + item.PropertyID.ToString();

                            searchItem.ImageList = new List<string>();

                            var imagelist = _context.PropertyImages.Where(x => x.Properties.PropertyID == item.PropertyID).ToList();
                            if (imagelist != null)
                            {
                            int i = 0;
                            foreach (var itemImg in imagelist)
                                {
                                if (i == 0)
                                {
                                    searchItem.ImageURL = itemImg.ImageURL.ToString();

                                }
                                searchItem.ImageList.Add(itemImg.ImageURL.ToString());
                                i++;
                                }
                            }


                            // searchItem.Location = item.p.Profile.

                            searches.Add(searchItem);
                        }
                    }
                    catch (Exception e)
                    {


                    }
                



                searchResult.searches = searches;// await _context.Service.Include(b => b.Profile).Where(c => c.Profile.SuscriptionPlan != "Free" && c.Profile.ExpiryDate>DateTime.Now).OrderBy(d=>d.Profile.Lat).Skip(Skip).Take(12).ToListAsync();



                searchResult.message = "Success";

                return (searchResult);
            }

            catch (Exception e)
            {
                

                searchResult.searches = null;
                searchResult.message = e.Message;

                return (searchResult);
            }
        }

      
    }
}
