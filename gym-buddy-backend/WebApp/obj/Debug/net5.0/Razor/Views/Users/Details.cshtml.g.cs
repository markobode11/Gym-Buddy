#pragma checksum "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "728a7fe53db93b102a3f72aceebe7b7739f53b57"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_Details), @"mvc.1.0.view", @"/Views/Users/Details.cshtml")]
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
#nullable restore
#line 1 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\_ViewImports.cshtml"
using WebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\_ViewImports.cshtml"
using WebApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"728a7fe53db93b102a3f72aceebe7b7739f53b57", @"/Views/Users/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e10e67c25c791a112e3e48e175cc51c69f2cfb6b", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Users_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DAL.App.DTO.Identity.AppUser>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>AppUser</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (14,14)-(14,59) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.Firstname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (17,14)-(17,55) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.Firstname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (20,14)-(20,58) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.Lastname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (23,14)-(23,54) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.Lastname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (26,14)-(26,60) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.HeightInCm));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (29,14)-(29,56) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.HeightInCm));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (32,14)-(32,60) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.WeightInKg));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (35,14)-(35,56) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.WeightInKg));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (38,14)-(38,56) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (41,14)-(41,52) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.Gender));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (44,14)-(44,59) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.UserSince));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (47,14)-(47,55) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.UserSince));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (50,14)-(50,58) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (53,14)-(53,54) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (56,14)-(56,68) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.NormalizedUserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (59,14)-(59,64) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.NormalizedUserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (62,14)-(62,55) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (65,14)-(65,51) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (68,14)-(68,65) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.NormalizedEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (71,14)-(71,61) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.NormalizedEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (74,14)-(74,64) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.EmailConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (77,14)-(77,60) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.EmailConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (80,14)-(80,62) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.PasswordHash));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (83,14)-(83,58) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.PasswordHash));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (86,14)-(86,63) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.SecurityStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (89,14)-(89,59) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.SecurityStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (92,14)-(92,66) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.ConcurrencyStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (95,14)-(95,62) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.ConcurrencyStamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (98,14)-(98,61) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (101,14)-(101,57) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (104,14)-(104,70) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.PhoneNumberConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (107,14)-(107,66) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.PhoneNumberConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (110,14)-(110,66) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.TwoFactorEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (113,14)-(113,62) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.TwoFactorEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (116,14)-(116,60) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.LockoutEnd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (119,14)-(119,56) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.LockoutEnd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (122,14)-(122,64) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.LockoutEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (125,14)-(125,60) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.LockoutEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line (128,14)-(128,67) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayNameFor(model => model.AccessFailedCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line (131,14)-(131,63) 6 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
Write(Html.DisplayFor(model => model.AccessFailedCount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "728a7fe53db93b102a3f72aceebe7b7739f53b5718211", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line (136,41)-(136,49) 13 "C:\Users\marko.bode\RiderProjects\Gym-Buddy2\gym-buddy-backend\WebApp\Views\Users\Details.cshtml"
WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "728a7fe53db93b102a3f72aceebe7b7739f53b5720362", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DAL.App.DTO.Identity.AppUser> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
