using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data.Models
{
    public class HotelRoomImage
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomImageUrl { get; set; }
        [ForeignKey("RoomId")]
        public virtual HotelRoom HotelRoom { get; set; }
    }
}
