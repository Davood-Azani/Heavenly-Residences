using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            var mapModel = _mapper.Map<HotelRoom>(hotelRoomDTO);
            mapModel.CreatedDate = DateTime.Now;
            mapModel.CreatedBy = "";
            var addedHootelRoom = await _db.AddAsync(mapModel);
            await _db.SaveChangesAsync();
            var returnMappedModel = _mapper.Map<HotelRoomDTO>(addedHootelRoom.Entity);
            return returnMappedModel;

        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    var hotelRoom = await _db.HotelRooms.FindAsync(roomId);
                    if (hotelRoom != null && !hotelRoom.IsDeleted)
                    {
                        var mapModel = _mapper.Map
                            <HotelRoomDTO, HotelRoom>(hotelRoomDTO, hotelRoom);
                        mapModel.UpdatedBy = "";
                        mapModel.UpdatedDate = DateTime.Now;
                        var updatedhotelroom =
                            _db.HotelRooms.Update(mapModel);
                        await _db.SaveChangesAsync();
                        var returnMappedModel = _mapper.Map<HotelRoomDTO>(mapModel);
                        return returnMappedModel;


                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
        {
            try
            {
                var mappedModel =
                    _mapper.Map<HotelRoomDTO>(await _db.HotelRooms
                        .FirstOrDefaultAsync(a => a.Id == roomId && !a.IsDeleted));

                return mappedModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            try
            {
                var hotelroom = await GetHotelRoom(roomId);
                if (hotelroom != null)
                {
                    hotelroom.IsDeleted = true;
                    return await _db.SaveChangesAsync();

                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        {
            try
            {
                var mappedModel =
                    _mapper.Map<IEnumerable<HotelRoomDTO>>(_db.HotelRooms.Include(a=>a.HotelRoomImages.Where(a=>!a.IsDeleted)).Where(a => !a.IsDeleted));
                return mappedModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //if not it returns null otherwise obj
        public async Task<HotelRoomDTO> IsRoomUniq(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    var mappedModel =
                    _mapper.Map<HotelRoomDTO>(await _db.HotelRooms.Include(a=>a.HotelRoomImages.Where(a=>!a.IsDeleted))
                        .FirstOrDefaultAsync(a => !a.IsDeleted && a.Name.ToLower() == name.ToLower()));


                    return mappedModel;
                }
                else
                {
                    var mappedModel =
                        _mapper.Map<HotelRoomDTO>(await _db.HotelRooms
                            .FirstOrDefaultAsync(a => !a.IsDeleted && a.Name.ToLower() == name.ToLower() && a.Id != roomId));


                    return mappedModel;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
