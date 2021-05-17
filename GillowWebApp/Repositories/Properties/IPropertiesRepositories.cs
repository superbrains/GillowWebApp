using GillowWebApp.ModelViews;
using GillowWebApp.Result;
using GillowWebApp.Models;
using GillowWebApp.ModelViews;
using GillowWebApp.Result;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Repositories.AllProperties
{
    public interface IPropertiesRepositories
    {

        //Properties Methods
        Task<ActionResult<List<VirtualTourRequestDetails>>> GetVirtualRequests();
        string PostRequests(VirtualRequestVM virtualRequestVM);
        Task<ActionResult<string>> UpdateVirtualRequests(VirtualRequestVM virtualRequestVM);
        Task<ActionResult<string>> DeleteVirtualRequests(VirtualRequestVM virtualRequestVM);
        Task<ActionResult<propertiesdetailsResult>> GetPropertyByProfile(int ProfileID, int Skip);
        SearchResult GetPropertyByOwner(int ProfileID, int Skip);
        string SendVirtualServiceEmail(string CustomerEmail, string PropertyTitle, String PropertyLocation, String CustomerName);
        string SendVirtualServiceEmailCompleted(string CustomerEmail, string PropertyTitle, String PropertyLocation, String CustomerName);
        string BoostProperty(BoostView boostView);
        string UpdateView(UpdateViewVM updateView);
        string RemoveProperty(UpdateViewVM updateView);
        string CompressAll();
        string compressImage(String filePath);
     
        PropertyResult PostProperty([FromForm] PropertyView propertyView);
        IActionResult GetProperty3DImage(int PropertyID);
        Task<IActionResult> PostProperty3D([FromBody] Property3D property3D);
        string GetPropertyFeatures(int PropertyID);
        IActionResult GetPropertyImage(int PropertyID);
        IActionResult GetPropertyImageByImageID(int ImageID);
        Task<ActionResult<PropertyImageResult>> GetPropertyImageList(int PropertyID);

        SheetsService GetSheetsService();
        Task<string> WriteAllToGoogleSheet(int skip);
        Task<string> WriteToGoogleSheet(ValueRange value);
        Task Write(SpreadsheetsResource.ValuesResource valuesResource, ValueRange value);

        SearchResult Top10Properties();

       SearchResult Filter(SearchModel search);
        double distanceInMiles(double lon1d, double lat1d, double lon2d, double lat2d);
        double ToRadians(double degrees);
        ActionResult<SearchResult> FilterByText(string searchText, int Skip, double lat, double lon, int km);
        SearchResult FilterByProperty(int PropertyID);
    }
}
