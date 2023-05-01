using Common;
using Heavenly_Residences_Client.Helper;
using Heavenly_Residences_Client.Model.ViewModel;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Heavenly_Residences_Client.Pages.HotelRooms
{
    public partial class HotelRooms
    {
        private HomeVM HomeModel { get; set; } = new HomeVM();
        public IEnumerable<HotelRoomDTO> Rooms { get; set; } = new List<HotelRoomDTO>();
        private bool IsProcessing { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                // await Task.Delay(5000); for debuging in this cycle
//#if DEBUG
//                //await Task.Delay(5000);
//#endif
                if (await localStorage.GetItemAsync<HomeVM>(SD.Local_InitialBooking) != null)
                {
                    HomeModel = await localStorage.GetItemAsync<HomeVM>(SD.Local_InitialBooking);
                }
                else
                {
                    HomeModel.NoOfNights = 1;
                }
                await LoadRooms();
            }
            catch (Exception e)
            {
                await jsRuntime.ToastrError(e.Message);
            }
        }

        private async Task LoadRooms()
        {
            Rooms = await hotelRoomService.GetHotelRooms(HomeModel.StartDate.ToString("MM/dd/yyyy"), HomeModel.EndDate.ToString("MM/dd/yyyy"));

            foreach (var room in Rooms)
            {
                room.TotalAmount = room.RegularRate * HomeModel.NoOfNights;
                room.TotalDays = HomeModel.NoOfNights;
            }

        }

        private async Task SaveBookingInfo()
        {
            IsProcessing = true;
            HomeModel.EndDate = HomeModel.StartDate.AddDays(HomeModel.NoOfNights);
            await localStorage.SetItemAsync(SD.Local_InitialBooking, HomeModel);
            await LoadRooms();
            IsProcessing = false;
        }
    }
}
