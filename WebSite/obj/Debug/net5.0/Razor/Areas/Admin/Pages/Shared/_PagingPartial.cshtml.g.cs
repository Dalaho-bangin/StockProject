#pragma checksum "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be9365b0aea97f46db98aee7286498a586871a0c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(WebSite.Areas.Admin.Pages.Shared.Areas_Admin_Pages_Shared__PagingPartial), @"mvc.1.0.view", @"/Areas/Admin/Pages/Shared/_PagingPartial.cshtml")]
namespace WebSite.Areas.Admin.Pages.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be9365b0aea97f46db98aee7286498a586871a0c", @"/Areas/Admin/Pages/Shared/_PagingPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"20b6fd7ecaf75c2ed1ab58f16650d8ce3b980bdc", @"/Areas/Admin/Pages/_ViewImports.cshtml")]
    public class Areas_Admin_Pages_Shared__PagingPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Common.Paging.BasePaging>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div class=\"shop_toolbar t_bottom\">\r\n    <div class=\"pagination\">\r\n        <ul>\r\n");
#nullable restore
#line 7 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
             if (Model.StartPage < Model.PageId)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"previous cursor-pointer\"><a");
            BeginWriteAttribute("onclick", " onclick=\"", 238, "\"", 279, 3);
            WriteAttributeValue("", 248, "FillPageId(", 248, 11, true);
#nullable restore
#line 9 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
WriteAttributeValue("", 259, Model.PageId - 1, 259, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 278, ")", 278, 1, true);
            EndWriteAttribute();
            WriteLiteral(">قبلی</a></li>\r\n");
#nullable restore
#line 10 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
             for (int i = Model.StartPage; i <= Model.EndPage; i++)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li");
            BeginWriteAttribute("class", " class=\"", 414, "\"", 475, 2);
#nullable restore
#line 13 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
WriteAttributeValue("", 422, Model.PageId == i ? "current" : "", 422, 37, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("  ", 459, "cursor-pointer", 461, 16, true);
            EndWriteAttribute();
            BeginWriteAttribute("onclick", " onclick=\"", 476, "\"", 500, 3);
            WriteAttributeValue("", 486, "FillPageId(", 486, 11, true);
#nullable restore
#line 13 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
WriteAttributeValue("", 497, i, 497, 2, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 499, ")", 499, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 13 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
                                                                                                      Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 14 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
             if (Model.EndPage > Model.PageId)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"next cursor-pointer\"><a");
            BeginWriteAttribute("onclick", " onclick=\"", 639, "\"", 680, 3);
            WriteAttributeValue("", 649, "FillPageId(", 649, 11, true);
#nullable restore
#line 17 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
WriteAttributeValue("", 660, Model.PageId + 1, 660, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 679, ")", 679, 1, true);
            EndWriteAttribute();
            WriteLiteral(">بعدی</a></li>\r\n");
#nullable restore
#line 18 "C:\StckProject\StockProject\WebSite\Areas\Admin\Pages\Shared\_PagingPartial.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Common.Paging.BasePaging> Html { get; private set; }
    }
}
#pragma warning restore 1591
