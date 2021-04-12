using GillowWebApp.ModelViews;
using GillowWebApp.Result;
using GillowWebApp.Models;
using GillowWebApp.ModelViews;
using GillowWebApp2.Models;
using Hangfire;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendingPushNotifications.Logics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Repositories.Profiles
{
    public class ProfileRepositories : IProfileRepositories
    {
        private readonly GillowContext _context;
        private IHostingEnvironment _environment;
        public ProfileRepositories(GillowContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
      
       
        const int size = 200;
        const int quality = 80;
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

        public FullProfile GetProfile(int ProfileID)
        {
            try
            {
                FullProfile pro = new FullProfile();

                var profile =  _context.Profile.FirstOrDefault(a => a.ProfileID == ProfileID);

                if (profile != null)
                {
                    // registrationResult = new RegistrationResult();
                    pro.AccountName = profile.AccountName;
                    pro.AccountNumber = profile.AccountNumber;
                    pro.BankName = profile.BankName;
                    pro.CompanyName = profile.CompanyName;
                    pro.DateRegistered = profile.DateRegistered;
                    pro.Email = profile.Email;
                    pro.ProfilePic = profile.ImageUrl;
                    pro.FullName = profile.FullName;
                    pro.Lat = profile.Lat;
                    pro.Location = profile.Location;
                    pro.Lon = profile.Lon;
                    pro.Password = profile.Password;
                    pro.PhoneNumber = profile.PhoneNumber;

                    pro.Status = profile.Status;

                    pro.VerificationStatus = profile.VerificationStatus;
                    pro.WhatsappPhone = profile.WhatsappPhone;



                }

                var propprofile =  _context.PropertySubscription.FirstOrDefault(a => a.Profile.ProfileID == ProfileID);

                if (propprofile != null)
                {
                    pro.SubscriptionPlan = propprofile.SubscriptionPlan;
                    pro.SubscriptionPlanID = propprofile.SubscriptionPlanID;
                    pro.PropertyBlast = propprofile.PropertyBlast;
                    pro.PropertyBoost = propprofile.PropertyBoost;
                    pro.PropertyListing = propprofile.PropertyListing;
                    pro.RealtorSpecialist = propprofile.RealtorSpecialist;
                    pro.StarPremium = propprofile.StarPremium;
                    pro.ExpiryDate = propprofile.ExpiryDate;
                    pro.ZoneSpecialist = propprofile.ZoneSpecialist;
                    pro.NumberofMonths = propprofile.NumberofMonths;
                    pro.VirtualDisplay = propprofile.VirtualDisplay;

                }
                else
                {
                    pro.SubscriptionPlan = "";
                    pro.SubscriptionPlanID = 0;
                    pro.PropertyBlast = 0;
                    pro.PropertyBoost = 0;
                    pro.PropertyListing = 0;
                    pro.RealtorSpecialist = 0;
                    pro.StarPremium = 0;
                    pro.ExpiryDate = DateTime.Now;
                    pro.ZoneSpecialist = 0;
                    pro.NumberofMonths = 0;
                    pro.VirtualDisplay = 0;
                }

                return pro;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public IActionResult GetProfileImage(int ProfileID)
        {

            try
            {
                Profile register = _context.Profile.FirstOrDefault(e => e.ProfileID == ProfileID);

                string filename = register.ImageUrl;

                var filePath = Path.Combine(_environment.WebRootPath, "profiles\\" + filename);

                if (!System.IO.File.Exists(filePath))
                {
                    var filePath2 = Path.Combine(_environment.WebRootPath, "NoImage.png");

                    var fileBytes2 = System.IO.File.ReadAllBytes(filePath2);


                    var fileMemStream2 =
                        new MemoryStream(fileBytes2);


                    return File(fileMemStream2, "application/octet-stream", "NoImage.png");
                }
                var fileBytes = System.IO.File.ReadAllBytes(filePath);


                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", filename);
            }
            catch (Exception e)
            {
                var filePath = Path.Combine(_environment.WebRootPath, "NoImage.png");


                var fileBytes = System.IO.File.ReadAllBytes(filePath);


                var fileMemStream =
                    new MemoryStream(fileBytes);


                return File(fileMemStream, "application/octet-stream", "NoImage.png");
            }
        }

        private IActionResult File(MemoryStream fileMemStream2, string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public int Login(LoginVM loginVM)
        {
            try
            {
                var prof =  _context.Profile.FirstOrDefault(c => c.Email == loginVM.Email && c.Password == loginVM.Password);
                if (prof != null)
                {
                    return prof.ProfileID ?? default(int);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public async Task<ActionResult<ProfileResult>> PostProfile(ProfileView profileView)
        {
            try
            {
                Profile profile = new Profile();
                profile.AccountName = profileView.AccountName;
                profile.AccountNumber = profileView.AccountNumber;
                profile.BankName = profileView.BankName;
                profile.BankCode = profileView.BankCode;
                profile.CompanyName = profileView.CompanyName;
                profile.ReferalID = profileView.RefID;
                profile.DateRegistered = profileView.DateRegistered;
                profile.Email = profileView.Email;
                profile.ExpiryDate = profileView.ExpiryDate;
                profile.FullName = profileView.FullName;
                profile.Lat = profileView.Lat;
                profile.Location = profileView.Location;
                profile.Lon = profileView.Lon;
                profile.Password = profileView.Password;
                profile.PhoneNumber = profileView.PhoneNumber;
                profile.Status = profileView.Status;
                profile.SubscriptionPlan = profileView.SubscriptionPlan;
                profile.SubscriptionPlanID = profileView.SubscriptionPlanID;
                profile.VerificationStatus = profileView.VerificationStatus;
                profile.WhatsappPhone = profileView.WhatsappPhone;


                _context.Profile.Add(profile);

                //Add Refereals



                await _context.SaveChangesAsync();

                if (profileView.RefID != 0)
                {
                    Referals referals = new Referals();
                    referals.AgentID = profile.ProfileID ?? default(int);
                    referals.RefID = profileView.RefID;

                    _context.Referals.Add(referals);

                    Tokens token = await _context.Tokens.Include(c => c.Profile).FirstOrDefaultAsync(x => x.Profile.ProfileID == profileView.RefID);
                    if (token != null)
                    {
                        List<string> tokens = new List<string>();
                        tokens.Add(token.Token);
                        Data data = new Data();
                        data.category = "Referal";

                        var pushSent = PushNotificationLogic.SendPushNotification(tokens.ToArray(), "New Referal", "Someone just registered on Gillow with your referal code. Congratulations!!!", data);
                    }

                    await _context.SaveChangesAsync();

                }


                //////Send Email to Seller
                //SmtpClient client = new SmtpClient("smtp.apekflux.com");
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("info@apekflux.com", "Superbrains1@");
                //client.Port = 25;
                //client.EnableSsl = false;

                //MailMessage mailMessage = new MailMessage();
                //mailMessage.From = new MailAddress("info@apekflux.com");
                //mailMessage.To.Add(buyerRequest.Email);
                //mailMessage.IsBodyHtml = true;

                //var filePath = Path.Combine(_environment.WebRootPath, "images\\" + "Logo.png");

                //mailMessage.Body = "<b>Hello, </b><br/><h5>You've received an order from Fluxworks, Kindly open the fluxworks app to view the details of the order</h5><br/><br/><h4>Fluxworks</h4><br/> <img src=" + filePath.Replace("\\", @"\") + "/>";

                //mailMessage.Subject = "Fluxworks: New Order";
                //client.Send(mailMessage);



                //await SendPush("New Order", "Dear Sir/Ma, you've just received a new order from Fluxworks, kindly respond swiftly to continue to attracts clients. Thanks", buyerRequest.ProfileID);


                //for (int i = 0; i < profileView.BusinessAreas.Count; i++)
                //{
                //    BusinessArea businessArea = new BusinessArea();
                //    businessArea.BizArea = profileView.BusinessAreas[i].ToString();
                //    businessArea.Profile.ProfileID = profile.ProfileID;
                //    _context.BusinessArea.Add(businessArea);

                //    _context.Entry(businessArea.Profile).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                //}

                //await _context.SaveChangesAsync();

                ProfileResult profileResult = new ProfileResult();
                profileResult.message = "Success";

                profileResult.id = profile.ProfileID;
                return (profileResult);

            }
            catch (Exception e)
            {


                ProfileResult profileResult = new ProfileResult();
                profileResult.message = e.Message;

                profileResult.id = null;
                return (profileResult);
            }
        }

        public async Task<string> Subscribe(SubscriptionView subscriptionView)
        {
            try
            {

                var propsub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);
                if (propsub == null)
                {
                    PropertySubscription propertySubscription = new PropertySubscription();
                    propertySubscription.ExpiryDate = DateTime.Now;
                    propertySubscription.NumberofMonths = 0;
                    propertySubscription.Profile.ProfileID = subscriptionView.ProfileID;
                    propertySubscription.PropertyBlast = 0;
                    propertySubscription.PropertyBoost = 0;
                    propertySubscription.PropertyListing = 10;
                    propertySubscription.RealtorSpecialist = 0;
                    propertySubscription.StarPremium = 0;

                    propertySubscription.SubscriptionPlan = "Free";
                    propertySubscription.SubscriptionPlanID = 0;
                    propertySubscription.ZoneSpecialist = 0;

                    _context.PropertySubscription.Add(propertySubscription);

                    _context.Entry(propertySubscription.Profile).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;


                }
                await _context.SaveChangesAsync();

                //Check if he has a referal and Add 10% to the Referals Wallet
                var referal = await _context.Referals.FirstOrDefaultAsync(c => c.AgentID == subscriptionView.ProfileID);
                if (referal != null)
                {
                    //Check if the Referal is a Special Agent
                    var specialAgent = await _context.SpecialAgents.Include(p => p.Profile).FirstOrDefaultAsync(s => s.Profile.ProfileID == referal.RefID);
                    if (specialAgent != null)
                    {
                        //Special Agent
                        //Give 10% of Amount Subscribed
                        ReferalSubscription referalSubscription = new ReferalSubscription();
                        referalSubscription.AgentID = subscriptionView.ProfileID;
                        referalSubscription.AmountSubscribed = subscriptionView.Amount;
                        referalSubscription.DateSubscribed = DateTime.Now;
                        referalSubscription.Percentge = subscriptionView.Amount * 0.10;
                        referalSubscription.ReferalID = referal.RefID;
                        referalSubscription.Status = "Not Paid";

                        Tokens token = await _context.Tokens.Include(c => c.Profile).FirstOrDefaultAsync(x => x.Profile.ProfileID == referal.RefID);
                        if (token != null)
                        {
                            List<string> tokens = new List<string>();
                            tokens.Add(token.Token);
                            Data data = new Data();
                            data.category = "Referal";

                            var pushSent = PushNotificationLogic.SendPushNotification(tokens.ToArray(), "New Referal Subscription", "One of your referals just subscribed on Gillow. Congratulations!!!", data);
                        }

                        _context.ReferalSubscriptions.Add(referalSubscription);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //Not A Special Agent
                        var refSub = await _context.ReferalSubscriptions.FirstOrDefaultAsync(x => x.AgentID == subscriptionView.ProfileID && x.ReferalID == referal.RefID);
                        if (refSub == null)
                        {
                            //Then Add Bonus to Refereal Once
                            //Give 10% of Amount Subscribed
                            ReferalSubscription referalSubscription = new ReferalSubscription();
                            referalSubscription.AgentID = subscriptionView.ProfileID;
                            referalSubscription.AmountSubscribed = subscriptionView.Amount;
                            referalSubscription.DateSubscribed = DateTime.Now;
                            referalSubscription.Percentge = subscriptionView.Amount * 0.10;
                            referalSubscription.ReferalID = referal.RefID;
                            referalSubscription.Status = "Not Paid";

                            Tokens token = await _context.Tokens.Include(c => c.Profile).FirstOrDefaultAsync(x => x.Profile.ProfileID == referal.RefID);
                            if (token != null)
                            {
                                List<string> tokens = new List<string>();
                                tokens.Add(token.Token);
                                Data data = new Data();
                                data.category = "Referal";

                                var pushSent = PushNotificationLogic.SendPushNotification(tokens.ToArray(), "New Referal Subscription", "One of your referals just subscribed on Gillow. Congratulations!!!", data);
                            }

                            _context.ReferalSubscriptions.Add(referalSubscription);
                            await _context.SaveChangesAsync();
                        }
                    }
                }




                ////Monthly,Bi-Annual, Annual
                //sub.SuscriptionPlan = subscriptionView.SubscriptionPlan;
                if (subscriptionView.SubscriptionPlan == "Basic")
                {
                    if (subscriptionView.Type == "Product")
                    {
                        var sub = await _context.Products.FirstOrDefaultAsync(c => c.ProductID == subscriptionView.TypeID);
                        sub.ExpiryDate = DateTime.Now.AddDays(7);
                        sub.BoostPlan = "Basic";
                        sub.BoostPlanID = 1;
                    }
                    else if (subscriptionView.Type == "Service")
                    {
                        var sub = await _context.Services.FirstOrDefaultAsync(c => c.ServiceID == subscriptionView.TypeID);
                        sub.ExpiryDate = DateTime.Now.AddDays(7);
                        sub.BoostPlan = "Basic";
                        sub.BoostPlanID = 1;
                    }
                    else if (subscriptionView.Type == "Property")
                    {

                        var sub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);

                        int daysleft = (sub.ExpiryDate - DateTime.Now).Days;
                        if (daysleft <= 0)
                        {
                            daysleft = 0;
                        }

                        if (subscriptionView.NumberofMonths == 1)
                        {
                            sub.ExpiryDate = DateTime.Now.AddDays(30 + daysleft);
                        }
                        if (subscriptionView.NumberofMonths == 3)
                        {
                            sub.ExpiryDate = DateTime.Now.AddDays(90 + daysleft);
                        }
                        if (subscriptionView.NumberofMonths == 6)
                        {
                            sub.ExpiryDate = DateTime.Now.AddDays(180 + daysleft);
                        }
                        if (subscriptionView.NumberofMonths == 12)
                        {
                            sub.ExpiryDate = DateTime.Now.AddDays(365 + daysleft);
                        }
                        sub.NumberofMonths = subscriptionView.NumberofMonths;
                        sub.PropertyBlast = 0;
                        sub.PropertyBoost = sub.PropertyBoost + 10;
                        sub.PropertyListing = 0;
                        sub.RealtorSpecialist = 0;
                        sub.StarPremium = 0;
                        sub.ZoneSpecialist = 0;
                        sub.SubscriptionPlan = subscriptionView.SubscriptionPlan;
                        sub.SubscriptionPlanID = 1;

                    }

                }
                else if (subscriptionView.SubscriptionPlan == "Standard")
                {
                    if (subscriptionView.Type == "Product")
                    {
                        var sub = await _context.Products.FirstOrDefaultAsync(c => c.ProductID == subscriptionView.TypeID);
                        sub.ExpiryDate = DateTime.Now.AddDays(15);
                        sub.BoostPlan = "Standard";
                        sub.BoostPlanID = 2;
                    }
                    else if (subscriptionView.Type == "Service")
                    {
                        var sub = await _context.Services.FirstOrDefaultAsync(c => c.ServiceID == subscriptionView.TypeID);
                        sub.ExpiryDate = DateTime.Now.AddDays(15);
                        sub.BoostPlan = "Standard";
                        sub.BoostPlanID = 2;
                    }
                    else if (subscriptionView.Type == "Property")
                    {
                        var sub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);

                        int daysleft = (sub.ExpiryDate - DateTime.Now).Days;
                        if (daysleft <= 0)
                        {
                            daysleft = 0;
                        }

                        if (subscriptionView.NumberofMonths == 1)
                        {
                            sub.ExpiryDate = DateTime.Now.AddDays(30 + daysleft);
                        }
                        if (subscriptionView.NumberofMonths == 3)
                        {
                            sub.ExpiryDate = DateTime.Now.AddDays(90 + daysleft);
                        }
                        if (subscriptionView.NumberofMonths == 6)
                        {
                            sub.ExpiryDate = DateTime.Now.AddDays(180 + daysleft);
                        }
                        if (subscriptionView.NumberofMonths == 12)
                        {
                            sub.ExpiryDate = DateTime.Now.AddDays(365 + daysleft);
                        }
                        sub.NumberofMonths = subscriptionView.NumberofMonths;
                        sub.PropertyBlast = sub.PropertyBlast + 15;
                        sub.PropertyBoost = sub.PropertyBoost + 15;
                        sub.PropertyListing = 0;
                        sub.RealtorSpecialist = 0;
                        sub.StarPremium = 0;
                        sub.ZoneSpecialist = 0;
                        sub.SubscriptionPlan = subscriptionView.SubscriptionPlan;
                        sub.SubscriptionPlanID = 2;

                    }
                }
                else if (subscriptionView.SubscriptionPlan == "Premium")
                {
                    if (subscriptionView.Type == "Product")
                    {
                        var sub = await _context.Products.FirstOrDefaultAsync(c => c.ProductID == subscriptionView.TypeID);
                        sub.ExpiryDate = DateTime.Now.AddDays(30);
                        sub.BoostPlan = "Premium";
                        sub.BoostPlanID = 3;
                    }
                    else if (subscriptionView.Type == "Service")
                    {
                        var sub = await _context.Services.FirstOrDefaultAsync(c => c.ServiceID == subscriptionView.TypeID);
                        sub.ExpiryDate = DateTime.Now.AddDays(30);
                        sub.BoostPlan = "Premium";
                        sub.BoostPlanID = 3;
                    }
                }
                else if (subscriptionView.SubscriptionPlan == "Bronze")
                {

                    var sub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);

                    int daysleft = (sub.ExpiryDate - DateTime.Now).Days;
                    if (daysleft <= 0)
                    {
                        daysleft = 0;
                    }

                    if (subscriptionView.NumberofMonths == 1)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(30 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 3)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(90 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 6)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(180 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 12)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(365 + daysleft);
                    }
                    sub.NumberofMonths = subscriptionView.NumberofMonths;
                    sub.PropertyBlast = sub.PropertyBlast + 30;
                    sub.PropertyBoost = sub.PropertyBoost + 30;
                    sub.PropertyListing = 0;
                    sub.RealtorSpecialist = 0;
                    sub.StarPremium = 0;
                    sub.ZoneSpecialist = 0;
                    sub.SubscriptionPlan = subscriptionView.SubscriptionPlan;
                    sub.SubscriptionPlanID = 4;


                }
                else if (subscriptionView.SubscriptionPlan == "Silver")
                {

                    var sub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);

                    int daysleft = (sub.ExpiryDate - DateTime.Now).Days;
                    if (daysleft <= 0)
                    {
                        daysleft = 0;
                    }

                    if (subscriptionView.NumberofMonths == 1)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(30 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 3)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(90 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 6)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(180 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 12)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(365 + daysleft);
                    }
                    sub.NumberofMonths = subscriptionView.NumberofMonths;
                    sub.PropertyBlast = sub.PropertyBlast + 40;
                    sub.PropertyBoost = sub.PropertyBoost + 35;
                    sub.PropertyListing = 0;
                    sub.RealtorSpecialist = 0;
                    sub.StarPremium = 0;
                    sub.ZoneSpecialist = 0;
                    sub.SubscriptionPlan = subscriptionView.SubscriptionPlan;
                    sub.SubscriptionPlanID = 5;


                }
                else if (subscriptionView.SubscriptionPlan == "Gold")
                {

                    var sub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);

                    int daysleft = (sub.ExpiryDate - DateTime.Now).Days;
                    if (daysleft <= 0)
                    {
                        daysleft = 0;
                    }

                    if (subscriptionView.NumberofMonths == 1)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(30 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 3)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(90 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 6)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(180 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 12)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(365 + daysleft);
                    }
                    sub.NumberofMonths = subscriptionView.NumberofMonths;
                    sub.PropertyBlast = sub.PropertyBlast + 55;
                    sub.PropertyBoost = sub.PropertyBoost + 50;
                    sub.PropertyListing = 0;
                    sub.RealtorSpecialist = 0;
                    sub.StarPremium = 0;
                    sub.ZoneSpecialist = 0;
                    sub.SubscriptionPlan = subscriptionView.SubscriptionPlan;
                    sub.SubscriptionPlanID = 6;


                }

                else if (subscriptionView.SubscriptionPlan == "Platinum Bronze")
                {

                    var sub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);

                    int daysleft = (sub.ExpiryDate - DateTime.Now).Days;
                    if (daysleft <= 0)
                    {
                        daysleft = 0;
                    }

                    if (subscriptionView.NumberofMonths == 1)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(30 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 3)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(90 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 6)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(180 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 12)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(365 + daysleft);
                    }

                    sub.NumberofMonths = subscriptionView.NumberofMonths;
                    sub.PropertyBlast = sub.PropertyBlast + 100;
                    sub.PropertyBoost = sub.PropertyBoost + 100;
                    sub.PropertyListing = 0;
                    sub.RealtorSpecialist = 1;
                    sub.StarPremium = sub.StarPremium + 10;
                    sub.ZoneSpecialist = 0;
                    sub.SubscriptionPlan = subscriptionView.SubscriptionPlan;
                    sub.SubscriptionPlanID = 7;


                }
                else if (subscriptionView.SubscriptionPlan == "Platinum Silver")
                {

                    var sub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);

                    int daysleft = (sub.ExpiryDate - DateTime.Now).Days;
                    if (daysleft <= 0)
                    {
                        daysleft = 0;
                    }

                    if (subscriptionView.NumberofMonths == 1)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(30 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 3)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(90 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 6)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(180 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 12)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(365 + daysleft);
                    }

                    sub.NumberofMonths = subscriptionView.NumberofMonths;
                    sub.PropertyBlast = sub.PropertyBlast + 150;
                    sub.PropertyBoost = sub.PropertyBoost + 150;
                    sub.PropertyListing = 0;
                    sub.RealtorSpecialist = 2;
                    sub.StarPremium = sub.StarPremium + 20;
                    sub.ZoneSpecialist = 1;
                    sub.SubscriptionPlan = subscriptionView.SubscriptionPlan;
                    sub.SubscriptionPlanID = 8;


                }

                else if (subscriptionView.SubscriptionPlan == "Platinum Gold")
                {

                    var sub = await _context.PropertySubscription.FirstOrDefaultAsync(c => c.Profile.ProfileID == subscriptionView.ProfileID);

                    int daysleft = (sub.ExpiryDate - DateTime.Now).Days;
                    if (daysleft <= 0)
                    {
                        daysleft = 0;
                    }

                    if (subscriptionView.NumberofMonths == 1)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(30 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 3)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(90 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 6)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(180 + daysleft);
                    }
                    if (subscriptionView.NumberofMonths == 12)
                    {
                        sub.ExpiryDate = DateTime.Now.AddDays(365 + daysleft);
                    }

                    sub.NumberofMonths = subscriptionView.NumberofMonths;
                    sub.PropertyBlast = sub.PropertyBlast + 150;
                    sub.PropertyBoost = sub.PropertyBoost + 150;
                    sub.PropertyListing = 0;
                    sub.RealtorSpecialist = 3;
                    sub.StarPremium = sub.StarPremium + 25;
                    sub.ZoneSpecialist = 3;
                    sub.SubscriptionPlan = subscriptionView.SubscriptionPlan;
                    sub.SubscriptionPlanID = 9;


                }

                await _context.SaveChangesAsync();

                return "Success";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public async Task<ActionResult<int>> UpdateProfile(ProfileView profileView)
        {
            try
            {
                Profile profile = _context.Profile.FirstOrDefault(e => e.ProfileID == profileView.ProfileID);


                profile.AccountName = profileView.AccountName;
                profile.AccountNumber = profileView.AccountNumber;
                profile.BankName = profileView.BankName;
                profile.BankCode = profileView.BankCode;
                profile.CompanyName = profileView.CompanyName;
                profile.SubscriptionPlan = "Free";
                profile.SubscriptionPlanID = 0;
                profile.ExpiryDate = DateTime.Now;
                profile.DateRegistered = DateTime.Now;
                //    profile.VerificationStatus = profileView.VerificationStatus;

                profile.FullName = profileView.FullName;

                profile.Location = profileView.Location;


                profile.PhoneNumber = profileView.PhoneNumber;
                profile.Status = profileView.Status;

                profile.WhatsappPhone = profileView.WhatsappPhone;


                await _context.SaveChangesAsync();


                //var businessarea = _context.BusinessArea.FirstOrDefault(x => x.Profile.ProfileID == profileView.ProfileID);
                //if (businessarea == null) { 
                //for (int i = 0; i < profileView.BusinessAreas.Count; i++)
                //{
                //    BusinessArea businessArea = new BusinessArea();
                //    businessArea.BizArea = profileView.BusinessAreas[i].ToString();
                //    businessArea.Profile.ProfileID = profile.ProfileID;
                //    _context.BusinessArea.Add(businessArea);

                //    _context.Entry(businessArea.Profile).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                //}
                //}
                //else
                //{
                //    if (profileView.BusinessAreas.Count > 0) { 
                //    businessarea.BizArea = profileView.BusinessAreas[0];
                //    }
                //}

                await _context.SaveChangesAsync();



                ProfileResult profileResult = new ProfileResult();
                profileResult.message = "Success";

                profileResult.id = profile.ProfileID;
                return (1);
            }
            catch (Exception e)
            {


                ProfileResult profileResult = new ProfileResult();
                profileResult.message = e.Message;

                profileResult.id = null;
                return (0);
            }
        }

        public async Task<string> UploadProfile([FromForm] UploadProfileView uploadViewModel)
        {
            // var files = HttpContext.Request.Form.Files;
            var file = uploadViewModel.files[0];


            if (file != null)
            {

                try
                {

                    var extension = Path.GetExtension(file.FileName);
                    var fileName = "profpic" + uploadViewModel.ProfileID.ToString() + extension;

                    var filePath = Path.Combine(_environment.WebRootPath, "profiles\\" + fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var jobId = BackgroundJob.Enqueue(() => compressImage(filePath));

                    var picURL = fileName;

                    var InitialMember = _context.Profile.FirstOrDefault(c => c.ProfileID == uploadViewModel.ProfileID);


                    InitialMember.ImageUrl = picURL;


                    _context.SaveChanges();

                    return ("Success");

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return ("File Uploaded");
        }
    }
}
