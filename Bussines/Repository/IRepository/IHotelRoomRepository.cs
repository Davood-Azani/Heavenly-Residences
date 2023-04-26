using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId,HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetHotelRoom(int roomId);
        public Task<int> DeleteHotelRoom(int roomId);
        public Task<IEnumerable<HotelRoomDTO>>GetAllHotelRooms();
        public Task<HotelRoomDTO> IsRoomUniq(string name);

    }
}