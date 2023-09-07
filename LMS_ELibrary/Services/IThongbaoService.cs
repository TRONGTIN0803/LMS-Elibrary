using AutoMapper;
using LMS_ELibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public interface IThongbaoService
    {
        Task<IEnumerable<Thongbao_Model>> getallThongbao(int id);

        Task<IEnumerable<Thongbao_Model>> searchThongbao(int user_id, string keyword);
    }
    public class ThongbaoService : IThongbaoService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;

        public ThongbaoService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Thongbao_Model>> getallThongbao(int id)
        {
            try
            {
                var result = await (from tb in _context.thongbao_Dbs 
                                    where tb.UserID == id orderby tb.Thoigian descending 
                                    select tb).ToListAsync();
                List<Thongbao_Model> thongbao = new List<Thongbao_Model>();
                thongbao = _mapper.Map<List<Thongbao_Model>>(result);
                
                return thongbao;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Thongbao_Model>> searchThongbao(int user_id, string keyword)
        {
            try
            {
                var result = await (from tb in _context.thongbao_Dbs
                                    where tb.UserID == user_id && tb.Tieude.Contains(keyword) || tb.Noidung.Contains(keyword)
                                    orderby tb.Thoigian descending
                                    select tb).ToListAsync();
                List<Thongbao_Model> thongbao = new List<Thongbao_Model>();
                thongbao = _mapper.Map<List<Thongbao_Model>>(result);

                return thongbao;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
