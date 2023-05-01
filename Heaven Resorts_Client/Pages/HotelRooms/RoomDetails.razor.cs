using Common;
using Heaven_Resorts_Client.Helper;
using Heaven_Resorts_Client.Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using System.Threading.Tasks;
using System;
using Blazored.LocalStorage;
using Heaven_Resorts_Client.Service.IService;

namespace Heaven_Resorts_Client.Pages.HotelRooms
{
    public partial class RoomDetails
    {
        [Parameter]
        public int? Id { get; set; }

        public HotelRoomBookingVM HotelBooking { get; set; } = new HotelRoomBookingVM();
        private int NoOfNights { get; set; } = 1;
        [Inject] 
        public IJSRuntime jsRuntime { get; set; }
        [Inject]
        public ILocalStorageService localStorage { get; set; }
        [Inject]
        public IHotelRoomService hotelRoomService { get; set; }
        [Inject]
        public IStripePaymentService stripePaymentService { get; set; }
        [Inject]
        public IRoomOrderDetailsService roomOrderDetailsService { get; set; }
        


        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Task.Delay(5000);
                HotelBooking.OrderDetails = new RoomOrderDetailsDTO();
                if (Id != null)
                {
                    if (await localStorage.GetItemAsync<HomeVM>
        (SD.Local_InitialBooking) != null)
                    {
                        var roomInitialInfo = await localStorage.GetItemAsync<HomeVM>
                            (SD.Local_InitialBooking);
                        HotelBooking.OrderDetails.HotelRoomDTO = await hotelRoomService.GetHotelRoomDetails(Id.Value,
                        roomInitialInfo.StartDate.ToString("MM/dd/yyyy"), roomInitialInfo.EndDate.ToString("MM/dd/yyyy"));
                        NoOfNights = roomInitialInfo.NoOfNights;
                        HotelBooking.OrderDetails.CheckInDate = roomInitialInfo.StartDate;
                        HotelBooking.OrderDetails.CheckOutDate = roomInitialInfo.EndDate;
                        HotelBooking.OrderDetails.HotelRoomDTO.TotalDays = roomInitialInfo.NoOfNights;
                        HotelBooking.OrderDetails.HotelRoomDTO.TotalAmount =
                        roomInitialInfo.NoOfNights * HotelBooking.OrderDetails.HotelRoomDTO.RegularRate;
                    }
                    else
                    {
                        HotelBooking.OrderDetails.HotelRoomDTO = await hotelRoomService.GetHotelRoomDetails(Id.Value,
                        DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"));
                        NoOfNights = 1;
                        HotelBooking.OrderDetails.CheckInDate = DateTime.Now;
                        HotelBooking.OrderDetails.CheckOutDate = DateTime.Now.AddDays(1);
                        HotelBooking.OrderDetails.HotelRoomDTO.TotalDays = 1;
                        HotelBooking.OrderDetails.HotelRoomDTO.TotalAmount =
                        HotelBooking.OrderDetails.HotelRoomDTO.RegularRate;
                    }
                }

                if (await localStorage.GetItemAsync<UserDTO>
                    (SD.Local_UserDetails) != null)
                {
                    var userInfo = await localStorage.GetItemAsync<UserDTO>
                        (SD.Local_UserDetails);
                    HotelBooking.OrderDetails.UserId = userInfo.Id;
                    HotelBooking.OrderDetails.Name = userInfo.Name;
                    HotelBooking.OrderDetails.Email = userInfo.Email;
                    HotelBooking.OrderDetails.Phone = userInfo.PhoneNo;
                }
            }
            catch (Exception e)
            {
                await jsRuntime.ToastrError(e.Message);
            }
        }


        private async Task HandleNoOfNightsChange(ChangeEventArgs e)
        {
            NoOfNights = Convert.ToInt32(e.Value.ToString());
            HotelBooking.OrderDetails.HotelRoomDTO = await hotelRoomService.GetHotelRoomDetails(Id.Value,
            HotelBooking.OrderDetails.CheckInDate.ToString("MM/dd/yyyy"),
            HotelBooking.OrderDetails.CheckInDate.AddDays(NoOfNights).ToString("MM/dd/yyyy"));

            HotelBooking.OrderDetails.CheckOutDate = HotelBooking.OrderDetails.CheckInDate.AddDays(NoOfNights);
            HotelBooking.OrderDetails.HotelRoomDTO.TotalDays = NoOfNights;
            HotelBooking.OrderDetails.HotelRoomDTO.TotalAmount = NoOfNights * HotelBooking.OrderDetails.HotelRoomDTO.RegularRate;
        }

        private async Task HandleCheckout()
        {
            if (!await HandleValidation())
            {
                return;
            }

            try
            {
                var paymentDTO = new StripePaymentDTO()
                {
                    Amount = Convert.ToInt32(HotelBooking.OrderDetails.HotelRoomDTO.TotalAmount * 100),
                    ProductName = HotelBooking.OrderDetails.HotelRoomDTO.Name,
                    ReturnUrl = "/hotel/room-details/" + Id
                };
                await Task.Delay(2000);
                var result = await stripePaymentService.CheckOut(paymentDTO);
                await Task.Delay(2000);
                HotelBooking.OrderDetails.StripeSessionId = result.Data.ToString();
                HotelBooking.OrderDetails.RoomId = HotelBooking.OrderDetails.HotelRoomDTO.Id;
                HotelBooking.OrderDetails.TotalCost = HotelBooking.OrderDetails.HotelRoomDTO.TotalAmount;

                var roomOrderDetailsSaved = await roomOrderDetailsService.SaveRoomOrderDetails(HotelBooking.OrderDetails);

                await localStorage.SetItemAsync(SD.Local_RoomOrderDetails, roomOrderDetailsSaved);

                await jsRuntime.InvokeVoidAsync("redirectToCheckout", result.Data.ToString());
            }
            catch (Exception e)
            {
                await jsRuntime.ToastrError(e.Message);
            }

        }

        private async Task<bool>
            HandleValidation()
        {
            if (string.IsNullOrEmpty(HotelBooking.OrderDetails.Name))
            {
                await jsRuntime.ToastrError("Name cannot be empty");
                return false;
            }
            if (string.IsNullOrEmpty(HotelBooking.OrderDetails.Phone))
            {
                await jsRuntime.ToastrError("Phone cannot be empty");
                return false;
            }

            if (string.IsNullOrEmpty(HotelBooking.OrderDetails.Email))
            {
                await jsRuntime.ToastrError("Email cannot be empty");
                return false;
            }
            return true;

        }
    }
}
