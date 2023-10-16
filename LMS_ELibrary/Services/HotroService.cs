using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public class HotroService : IHotroService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;

        public HotroService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KqJson> addHelp(Help_Model help)
        {
            KqJson kq = new KqJson();
            try
            {
                if (help.Tieude!="" && help.Noidung!="" && help.UserID>0)
                {
                    Help_Db helpdb = new Help_Db();
                    helpdb.Tieude = help.Tieude;
                    helpdb.Noidung = help.Noidung;
                    helpdb.NgayGui = DateTime.Now;
                    helpdb.UserID = help.UserID;

                    await _context.help_Dbs.AddAsync(helpdb);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        
                        kq.Status = true;
                        kq.Message = "Them thanh cong";
                        return kq;
                    }
                    else
                    {
                        throw new Exception("Them that bai");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }catch(Exception e)
            {
                
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<object> getAlllisthotro()
        {
            try
            {
                var result = await _context.help_Dbs.ToListAsync();
                if (result.Count > 0)
                {
                    List<Help_Model> listhelp = new List<Help_Model>();
                    listhelp = _mapper.Map<List<Help_Model>>(result);
                    return listhelp;
                }
                else
                {
                    throw new Exception("Not Found");
                }
                
            }catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<KqJson> PostHelp(Help_Model help)
        {
            KqJson kq = new KqJson();
            try
            {
                
                Help_Db posthelp = new Help_Db();
                posthelp.Tieude = help.Tieude;
                posthelp.Noidung = help.Noidung;
                posthelp.UserID = help.UserID;
                posthelp.NgayGui = DateTime.Now;

                await _context.help_Dbs.AddAsync(posthelp);
                int row =await _context.SaveChangesAsync();
                if (row > 0)
                {
                    kq.Status = true;
                    kq.Message = "Add Successful";
                    return kq;
                }
                else
                {
                    throw new Exception("Add Failed");
                }
                
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }
    }
}
