using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LMS_ELibrary.Services
{
    public interface IThongbaoService
    {
        Task<IEnumerable<Thongbao_Model>> getallThongbao(int id);

        Task<IEnumerable<Thongbao_Model>> searchThongbao(int user_id, string keyword);

        Task<object> chitietThongbao(int idthongbao);

        Task<KqJson> xoaThongbao(List<int> listid);

        // 0 => chua doc ; 1 => da doc
        Task<object> locThongBao(int user_id,int status); 

        Task<KqJson> danhDauThongBao(int thongbao_id,int status);
        


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

        

        //Status = 0 => chua doc ; 1 => da doc
        public async Task<object> chitietThongbao(int idthongbao)
        {
            try
            {
                Thongbao_Model thongbao = new Thongbao_Model();
                var result = await _context.thongbao_Dbs.SingleOrDefaultAsync(p=>p.ThongbaoID==idthongbao);
                if (result != null)
                {
                    
                    if (result.Status == 0)
                    {
                        result.Status = 1;
                    }
                    thongbao = _mapper.Map<Thongbao_Model>(result);

                    return thongbao;
                }
                else
                {
                    KqJson kq = new KqJson();
                    kq.Status = false;
                    kq.Message = "Not found";

                    return kq; 
                }

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> danhDauThongBao(int thongbao_id, int status)
        {
            try
            {
                KqJson kq = new KqJson();
                var result = await _context.thongbao_Dbs.SingleOrDefaultAsync(p=>p.ThongbaoID==thongbao_id);
                if (result!=null)
                {
                    if (status == 0||status==1)
                    {
                        result.Status = status;
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Update successful";
                        }
                        else
                        {
                            kq.Status = false;
                            kq.Message = "Update Failed";
                        }
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Bad Request";
                    }
                }
                else
                {
                    
                    kq.Status = false;
                    kq.Message = "Not found";
                    
                }
                return kq;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
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

        public async Task<object> locThongBao(int user_id, int status)
        {
            try
            {
                if(status != 0 && status != 1)
                {
                    KqJson kq = new KqJson();
                    kq.Status = false;
                    kq.Message = "Bad Request";
                    return kq;
                }
                else
                {
                    var result = await (from tb in _context.thongbao_Dbs 
                                        where tb.Status == status && tb.UserID==user_id 
                                        select tb).ToListAsync();
                    if (result != null)
                    {
                        List<Thongbao_Model> listtb = new List<Thongbao_Model>();
                        listtb = _mapper.Map<List<Thongbao_Model>>(result);
                        return listtb;
                    }
                    else
                    {
                        KqJson kq = new KqJson();
                        kq.Status = false;
                        kq.Message = "Not found";
                        return kq;
                    }
                   
                }
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

        public async Task<KqJson> xoaThongbao(List<int> listid)
        {
            try
            {
                KqJson kq = new KqJson();
                List<Thongbao_Db>listthongbao=new List<Thongbao_Db>();
                foreach (int id in listid)
                {
                    var tb = await _context.thongbao_Dbs.SingleOrDefaultAsync(p=>p.ThongbaoID==id);
                    if (tb != null)
                    {
                        listthongbao.Add(tb);
                    }
                }
                _context.thongbao_Dbs.RemoveRange(listthongbao);
                int row = await _context.SaveChangesAsync();
                if (row > 0)
                {
                    kq.Status = true;
                    kq.Message = "Xoa thanh cong!";
                }
                else
                {
                    kq.Status=false;
                    kq.Message = "Xoa that bai!";
                }

                return kq;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
