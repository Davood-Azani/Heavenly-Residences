using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateStr, string checkOutDatestr)
        {
            try
            {
                var mappedModel =
                    _mapper.Map<HotelRoomDTO>(await _db.HotelRooms.Include(a => a.HotelRoomImages.Where(a => !a.IsDeleted))
                        .FirstOrDefaultAsync(a => a.Id == roomId && !a.IsDeleted));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDatestr))
                {
                    mappedModel.IsBooked = await IsRoomBooked(roomId, checkInDateStr, checkOutDatestr);
                }

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
                var hotelroom = await _db.HotelRooms.FindAsync(roomId);


                if (hotelroom != null)
                {

                    hotelroom.IsDeleted = true;
                    //await _db.SaveChangesAsync();
                    var allimages = await _db.HotelRoomImages
                        .Where(a => a.RoomId == roomId &&
                                    !a.IsDeleted).ToListAsync();
                    if (allimages.Any())
                    {
                        foreach (var image in allimages)
                        {
                            //if (File.Exists(image.RoomImageUrl))
                            //{
                            //    File.Delete(image.RoomImageUrl);
                            //}

                            image.IsDeleted = true;
                            //await _db.SaveChangesAsync();
                        }
                    }



                    return await _db.SaveChangesAsync();

                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkInDate = null, string checkOutDate = null)
        {
            try
            {
                //var mappedModel =
                //    _mapper.Map<IEnumerable<HotelRoomDTO>>(_db.HotelRooms.Include(a => a.HotelRoomImages.Where(a => !a.IsDeleted)).Where(a => !a.IsDeleted));
                //return mappedModel;
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                    _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                        (_db.HotelRooms.Include(x => x.HotelRoomImages)).Where(a => !a.IsDeleted);
                if (!string.IsNullOrEmpty(checkInDate) && !string.IsNullOrEmpty(checkOutDate))
                {
                    foreach (HotelRoomDTO hotelRoom in hotelRoomDTOs)
                    {
                        hotelRoom.IsBooked = await IsRoomBooked(hotelRoom.Id, checkInDate, checkOutDate);
                    }
                }
                return hotelRoomDTOs;

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
                    _mapper.Map<HotelRoomDTO>(await _db.HotelRooms.Include(a => a.HotelRoomImages.Where(a => !a.IsDeleted))
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

        public async Task<bool> IsRoomBooked(int RoomId, string checkInDatestr, string checkOutDatestr)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkOutDatestr) && !string.IsNullOrEmpty(checkInDatestr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDatestr, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDatestr, "MM/dd/yyyy", null);

                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == RoomId && x.IsPaymentSuccessful &&
                        //check if checkin date that user wants does not fall in between any dates for room that is booked
                        ((checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate)
                         //check if checkout date that user wants does not fall in between any dates for room that is booked
                         || (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)
                        )).FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
