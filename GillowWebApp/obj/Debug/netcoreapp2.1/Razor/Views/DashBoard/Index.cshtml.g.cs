#pragma checksum "C:\Users\samuel.ojekunle\source\repos\GillowWebApp\GillowWebApp\Views\DashBoard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "69bde21bdba278927535807dc8c0192616d42dc9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DashBoard_Index), @"mvc.1.0.view", @"/Views/DashBoard/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DashBoard/Index.cshtml", typeof(AspNetCore.Views_DashBoard_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\samuel.ojekunle\source\repos\GillowWebApp\GillowWebApp\Views\_ViewImports.cshtml"
using GillowWebApp;

#line default
#line hidden
#line 2 "C:\Users\samuel.ojekunle\source\repos\GillowWebApp\GillowWebApp\Views\_ViewImports.cshtml"
using GillowWebApp.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"69bde21bdba278927535807dc8c0192616d42dc9", @"/Views/DashBoard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bc32134cfd7f5767851f3bb324e890d6f9c8ffa7", @"/Views/_ViewImports.cshtml")]
    public class Views_DashBoard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GillowWebApp.ModelViews.FullProfile>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\samuel.ojekunle\source\repos\GillowWebApp\GillowWebApp\Views\DashBoard\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout2.cshtml";

#line default
#line hidden
            BeginContext(133, 389, true);
            WriteLiteral(@"<div class=""col-lg-9 col-md-12"">
    <div class=""row"">
        <br />
        <br />
        <div class=""col-lg-9 col-md-12"">

            <div class=""row"">
                <div class=""col-lg-12 col-md-12 col-sm-12"">
                    <br />
                    <br />
                    <h4>Your Referal Link: <span class=""pc-title theme-cl"">https://gillow.ng/Register?refid=");
            EndContext();
            BeginContext(523, 11, false);
#line 16 "C:\Users\samuel.ojekunle\source\repos\GillowWebApp\GillowWebApp\Views\DashBoard\Index.cshtml"
                                                                                                       Write(Model.Email);

#line default
#line hidden
            EndContext();
            BeginContext(534, 6690, true);
            WriteLiteral(@"</span></h4>
                </div>
            </div>

            <div class=""row"">

                <div class=""col-lg-4 col-md-6 col-sm-12"">
                    <div class=""dashboard-stat widget-1"">
                        <div class=""dashboard-stat-content""><h4>607</h4> <span>Listings Included</span></div>
                        <div class=""dashboard-stat-icon""><i class=""ti-location-pin""></i></div>
                    </div>
                </div>

                <div class=""col-lg-4 col-md-6 col-sm-12"">
                    <div class=""dashboard-stat widget-2"">
                        <div class=""dashboard-stat-content""><h4>102</h4> <span>Listings Remaining</span></div>
                        <div class=""dashboard-stat-icon""><i class=""ti-pie-chart""></i></div>
                    </div>
                </div>

                <div class=""col-lg-4 col-md-6 col-sm-12"">
                    <div class=""dashboard-stat widget-3"">
                        <div class=""dashboard-stat-conten");
            WriteLiteral(@"t""><h4>70</h4> <span>Featured Included</span></div>
                        <div class=""dashboard-stat-icon""><i class=""ti-user""></i></div>
                    </div>
                </div>

                <div class=""col-lg-4 col-md-6 col-sm-12"">
                    <div class=""dashboard-stat widget-4"">
                        <div class=""dashboard-stat-content""><h4>30</h4> <span>Featured Remaining</span></div>
                        <div class=""dashboard-stat-icon""><i class=""ti-location-pin""></i></div>
                    </div>
                </div>

                <div class=""col-lg-4 col-md-6 col-sm-12"">
                    <div class=""dashboard-stat widget-5"">
                        <div class=""dashboard-stat-content""><h4>Unlimited</h4> <span>Images / per listing</span></div>
                        <div class=""dashboard-stat-icon""><i class=""ti-pie-chart""></i></div>
                    </div>
                </div>

                <div class=""col-lg-4 col-md-6 col-sm-12"">
      ");
            WriteLiteral(@"              <div class=""dashboard-stat widget-6"">
                        <div class=""dashboard-stat-content""><h4>2021-02-26</h4> <span>Ends On</span></div>
                        <div class=""dashboard-stat-icon""><i class=""ti-user""></i></div>
                    </div>
                </div>

            </div>

            <div class=""dashboard-wraper"">

                <!-- Basic Information -->
                <div class=""form-submit"">
                    <h4>My Account</h4>
                    <div class=""submit-section"">
                        <div class=""row"">

                            <div class=""form-group col-md-6"">
                                <label>Your Name</label>
                                <input type=""text"" class=""form-control"" value=""Shaurya Preet"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>Email</label>
                                <input type=""email"" class=");
            WriteLiteral(@"""form-control"" value=""preet77@gmail.com"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>Your Title</label>
                                <input type=""text"" class=""form-control"" value=""Web Designer"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>Phone</label>
                                <input type=""text"" class=""form-control"" value=""123 456 5847"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>Address</label>
                                <input type=""text"" class=""form-control"" value=""522, Arizona, Canada"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>City</label>
                                <input type=""text"" class=""form");
            WriteLiteral(@"-control"" value=""Montquebe"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>State</label>
                                <input type=""text"" class=""form-control"" value=""Canada"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>Zip</label>
                                <input type=""text"" class=""form-control"" value=""160052"">
                            </div>

                            <div class=""form-group col-md-12"">
                                <label>About</label>
                                <textarea class=""form-control"">Maecenas quis consequat libero, a feugiat eros. Nunc ut lacinia tortor morbi ultricies laoreet ullamcorper phasellus semper</textarea>
                            </div>

                        </div>
                    </div>
                </div>

                <div class=""f");
            WriteLiteral(@"orm-submit"">
                    <h4>Social Accounts</h4>
                    <div class=""submit-section"">
                        <div class=""row"">

                            <div class=""form-group col-md-6"">
                                <label>Facebook</label>
                                <input type=""text"" class=""form-control"" value=""https://facebook.com/"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>Twitter</label>
                                <input type=""email"" class=""form-control"" value=""https://twitter.com/"">
                            </div>

                            <div class=""form-group col-md-6"">
                                <label>Google Plus</label>
                                <input type=""text"" class=""form-control"" value=""https://googleplus.com/"">
                            </div>

                            <div class=""form-group col-md-6"">
                ");
            WriteLiteral(@"                <label>LinkedIn</label>
                                <input type=""text"" class=""form-control"" value=""https://linkedin.com/"">
                            </div>

                            <div class=""form-group col-lg-12 col-md-12"">
                                <button class=""btn btn-theme-light-2 rounded"" type=""submit"">Save Changes</button>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
</div>
</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GillowWebApp.ModelViews.FullProfile> Html { get; private set; }
    }
}
#pragma warning restore 1591
