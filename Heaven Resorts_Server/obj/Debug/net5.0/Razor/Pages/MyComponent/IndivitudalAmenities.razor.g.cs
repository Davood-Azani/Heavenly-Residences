#pragma checksum "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\Pages\MyComponent\IndivitudalAmenities.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4f0921e7282be816647908d20cb71e7d43ea6cb7"
// <auto-generated/>
#pragma warning disable 1591
namespace Heaven_Resorts_Server.Pages.MyComponent
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Heaven_Resorts_Server;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Heaven_Resorts_Server.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\_Imports.razor"
using Heaven_Resorts_Server.Pages.MyComponent;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\Pages\MyComponent\IndivitudalAmenities.razor"
using Heaven_Resorts_Server.Model;

#line default
#line hidden
#nullable disable
    public partial class IndivitudalAmenities : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddMarkupContent(1, "<h1>Amenities</h1>\r\n    ");
            __builder.OpenElement(2, "h3");
#nullable restore
#line 4 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\Pages\MyComponent\IndivitudalAmenities.razor"
__builder.AddContent(3, Amenity.Id);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(4, "\r\n    ");
            __builder.OpenElement(5, "h3");
            __builder.AddAttribute(6, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 5 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\Pages\MyComponent\IndivitudalAmenities.razor"
                  (args) =>  CheckedAmenitiesTheRoom(args,Amenity.Name)

#line default
#line hidden
#nullable disable
            ));
#nullable restore
#line 5 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\Pages\MyComponent\IndivitudalAmenities.razor"
__builder.AddContent(7, Amenity.Name);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(8, "\r\n    ");
            __builder.OpenElement(9, "h3");
#nullable restore
#line 6 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\Pages\MyComponent\IndivitudalAmenities.razor"
__builder.AddContent(10, Amenity.Description);

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 12 "D:\Heaven Resorts\Heaven Resorts\Heaven Resorts_Server\Pages\MyComponent\IndivitudalAmenities.razor"
 
    [Parameter]
    public BlazorAmenities Amenity { get; set; }

    
    [Parameter]
    public EventCallback<string> onSelectedAmenities { get; set; }

    protected async Task  CheckedAmenitiesTheRoom(MouseEventArgs e,string amenitiy)
    {
        await onSelectedAmenities.InvokeAsync((string)amenitiy);
    }


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591