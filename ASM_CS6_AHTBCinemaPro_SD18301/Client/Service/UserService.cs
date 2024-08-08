namespace ASM_CS6_AHTBCinemaPro_SD18301.Client.Service
{
    using System.Threading.Tasks;
    using ASM_CS6_AHTBCinemaPro_SD18301.Data;
    using ASM_CS6_AHTBCinemaPro_SD18301.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserService
    {
        private readonly DBCinemaContextshare _context;

        public UserService(DBCinemaContextshare context)
        {
            _context = context;
        }

        public async Task<KhachHang> GetCustomerByUserIdAsync(string userId)
        {
            return await _context.KhachHangs
                .FirstOrDefaultAsync(kh => kh.IDUser == userId);
        }
    }

}
