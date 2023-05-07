// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Heavenly_Residences_Client.Pages.Stripe
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Heavenly_Residences_Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Heavenly_Residences_Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Heavenly_Residences_Client.Helper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Blazored.LocalStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Heavenly_Residences_Client.Model.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Heavenly_Residences_Client.Service.IService;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Heavenly_Residences_Client.Pages.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Radzen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\_Imports.razor"
using Radzen.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\Pages\Stripe\Success_Payment.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\Pages\Stripe\Success_Payment.razor"
           [Authorize(Roles = SD.Role_Customer)]

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/success-payment")]
    public partial class Success_Payment : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 28 "D:\Projects\Heavenly Residences\Heavenly Residences_Client\Pages\Stripe\Success_Payment.razor"
       
    private bool IsPaymentComplete { get; set; } = false;
    private int OrderId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var orderDetails = await localStorage.GetItemAsync<RoomOrderDetailsDTO>(SD.Local_RoomOrderDetails);
        OrderId = orderDetails.Id;

        var FinalRoomOrderDetails = await roomOrderDetailService.MarkPaymentSuccessful(orderDetails);
        if (FinalRoomOrderDetails.Status == SD.Status_Booked)
        {
            await localStorage.RemoveItemAsync(SD.Local_RoomOrderDetails);
            await localStorage.RemoveItemAsync(SD.Local_InitialBooking);

            IsPaymentComplete = true;
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IRoomOrderDetailsService roomOrderDetailService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ILocalStorageService localStorage { get; set; }
    }
}
#pragma warning restore 1591