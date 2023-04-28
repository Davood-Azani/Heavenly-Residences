using AutoMapper;
using Business.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Data.Models;

namespace Business.Repository
{
    public class HotelImagesRepository : IHotelImagesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelImagesRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO)
        {
            var image = _mapper.Map<HotelRoomImage>(imageDTO);
            await _db.HotelRoomImages.AddAsync(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelImageByImageUrl(string imageUrl)
        {
            var allImages = await _db.HotelRoomImages.FirstOrDefaultAsync
                                (x => x.RoomImageUrl.ToLower() == imageUrl.ToLower() && !x.IsDeleted);
            if (allImages == null)
            {
                return 0;
            }

            allImages.IsDeleted = true;

            //_db.HotelRoomImages.Remove(allImages);
            return await _db.SaveChangesAsync();

        }

        public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
        {
            var image = await _db.HotelRoomImages.FirstOrDefaultAsync(x => x.Id == imageId && !x.IsDeleted);
            if (image == null)
            {
                return 0;
            }

            image.IsDeleted = true;
            // _db.HotelRoomImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            var imageList = await _db.HotelRoomImages.Where(x => x.RoomId == roomId && !x.IsDeleted).ToListAsync();
            if (!imageList.Any())
            {
                return 0;
            }

            foreach (var image in imageList)
            {
                image.IsDeleted = true;
                await _db.SaveChangesAsync();
            }

            //_db.HotelRoomImages.RemoveRange(imageList);
            //return await _db.SaveChangesAsync();
            return 1;
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId)
        {
            return _mapper.Map<IEnumerable<HotelRoomImageDTO>>(
            await _db.HotelRoomImages.Where(x => x.RoomId == roomId && !x.IsDeleted).ToListAsync());
        }
    }
}
