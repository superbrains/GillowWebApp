using GillowWebApp.ModelViews;
using GillowWebApp.Result;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Repositories.Profiles
{
   public interface IProfileRepositories
    {
        FullProfile GetProfile(int ProfileID);
        int Login(LoginVM loginVM);
        Task<string> Subscribe(SubscriptionView subscriptionView);
        Task<ActionResult<int>> UpdateProfile(ProfileView profileView);
        Task<ActionResult<ProfileResult>> PostProfile(ProfileView profileView);
        IActionResult GetProfileImage(int ProfileID);
        string compressImage(String filePath);
        Task<string> UploadProfile([FromForm] UploadProfileView uploadViewModel);
    }
}
