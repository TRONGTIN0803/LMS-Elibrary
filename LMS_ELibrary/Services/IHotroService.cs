using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public interface IHotroService
    {
        Task<IEnumerable<Help_Model>> getAlllisthotro();

        Task<KqJson> PostHelp(Help_Model help);
    }

    public class HotroService : IHotroService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;

        public HotroService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Help_Model>> getAlllisthotro()
        {
            try
            {
                var result = await _context.help_Dbs.ToListAsync();
                List<Help_Model> listhelp = new List<Help_Model>();
                listhelp = _mapper.Map<List<Help_Model>>(result);
                return listhelp;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> PostHelp(Help_Model help)
        {
            try
            {
                KqJson kq = new KqJson();
                Help_Db posthelp = new Help_Db();
                posthelp.Tieude = help.Tieude;
                posthelp.Noidung = help.Noidung;
                posthelp.UserID = help.UserID;
                posthelp.NgayGui = DateTime.Now;

                await _context.help_Dbs.AddAsync(posthelp);
                int row =await _context.SaveChangesAsync();
                if (row > 0)
                {
                    kq.Status = false;
                    kq.Message = "Add Successful";
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Add Failed";
                }
                return kq;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
