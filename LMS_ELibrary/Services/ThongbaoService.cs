using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LMS_ELibrary.Services
{
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
        public async Task<object> chitietThongbao(int idthongbao, int user_id)
        {
            try
            {
                ThongbaoLop_Model thongbao = new ThongbaoLop_Model();
                var result = await (from tb in _context.thongbao_Dbs
                                    join tbl in _context.thongbaoLop_Dbs
                                    on tb.ThongbaoID equals tbl.Thongbao_Id
                                    where tbl.Thongbao_Id == idthongbao && tbl.User_Id == user_id
                                    select tbl).SingleOrDefaultAsync();
                if (result != null)
                {
                    result.Status = 1;
                    await _context.SaveChangesAsync();
                    var col = _context.Entry(result);
                    col.Reference(p => p.Thongbao).Load();
                    Thongbao_Db tb = new Thongbao_Db();
                    tb.Tieude = result.Thongbao.Tieude;
                    tb.Noidung = result.Thongbao.Noidung;
                    tb.Thoigian = result.Thongbao.Thoigian;
                    result.Thongbao = tb;

                    thongbao = _mapper.Map<ThongbaoLop_Model>(result);
                    if (thongbao.Status == "0")
                    {
                        thongbao.Status = "Chua Xem";
                    }else if (thongbao.Status == "1")
                    {
                        thongbao.Status = "Da Xem";
                    }
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
                                    join tbl in _context.thongbaoLop_Dbs
                                    on tb.ThongbaoID equals tbl.Thongbao_Id
                                    where tbl.User_Id == id orderby tb.Thoigian descending
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
                                        where tb.UserID==user_id 
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

        public async Task<KqJson> Taothongbao(Gui_Thongbao_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Tieude!="" && model.Noidung!="")
                {
                    Thongbao_Db tb = new Thongbao_Db();
                    tb.Tieude = model.Tieude;
                    tb.Noidung = model.Noidung;
                    tb.Thoigian = DateTime.Now;
                    tb.UserID = model.ID_Nguoigui;
                    await _context.thongbao_Dbs.AddAsync(tb);
                    await _context.SaveChangesAsync();
                    int thongbao_Id = tb.ThongbaoID;
                    //co chon lop de gui thong bao
                    if (model.list_ID_Lopgiang.Count > 0)
                    {
                        //Neu co chon hoc vien -> Lớp đó chỉ gửi cho học viên được chọn
                        if (model.list_ID_Nguoinhan.Count > 0)
                        {
                            foreach(var id_lop in model.list_ID_Lopgiang)
                            {
                                List<int?> list_nguoinhan_Id = new List<int?>();
                                //duyet xem hoc vien duoc chon o lop nao
                                foreach (var id_nguoinhan in model.list_ID_Nguoinhan)
                                {
                                    var hocvien = await (from hvl in _context.hocvien_Lop_Dbs
                                                         where hvl.Lopgiang_Id == id_lop && hvl.User_Id == id_nguoinhan
                                                         select hvl).SingleOrDefaultAsync();
                                    if (hocvien != null)//neu tim thay hoc vien thi luu vao list
                                    {
                                        list_nguoinhan_Id.Add(hocvien.User_Id);
                                    }
                                }
                                //Gui thong bao den cac hoc vien duoc chon cua lop
                                if (list_nguoinhan_Id.Count > 0)
                                {
                                    List<ThongbaoLop_Db> list_add_tbl = new List<ThongbaoLop_Db>();
                                    foreach(int idnn in list_nguoinhan_Id)
                                    {
                                        ThongbaoLop_Db tbl = new ThongbaoLop_Db();
                                        tbl.Thongbao_Id=thongbao_Id;
                                        tbl.Lopgiang_Id = id_lop;
                                        tbl.User_Id = idnn;
                                        tbl.Status = 0;
                                        list_add_tbl.Add(tbl);
                                    }
                                    await _context.thongbaoLop_Dbs.AddRangeAsync(list_add_tbl);
                                }
                                //khong tim thay hoc vien nao thi gui den tat ca hoc vien cua lop
                                else
                                {
                                    var listnguoinhan = await (from nn in _context.hocvien_Lop_Dbs
                                                               where nn.Lopgiang_Id == id_lop
                                                               select nn).ToListAsync();
                                    List<ThongbaoLop_Db> list_add_tbl = new List<ThongbaoLop_Db>();
                                    foreach (var nn in listnguoinhan)
                                    {
                                        ThongbaoLop_Db tbl = new ThongbaoLop_Db();
                                        tbl.Thongbao_Id = thongbao_Id;
                                        tbl.Lopgiang_Id = id_lop;
                                        tbl.User_Id = nn.User_Id;
                                        tbl.Status = 0;
                                        list_add_tbl.Add(tbl);
                                    }
                                    await _context.thongbaoLop_Dbs.AddRangeAsync(list_add_tbl);
                                }
                            }
                        }
                        //khong chon hoc vien -> gui cho tat ca hoc vien trong lop
                        else
                        {
                            foreach (var id_lop in model.list_ID_Lopgiang)
                            {
                                var listhocvien = await (from hvl in _context.hocvien_Lop_Dbs
                                                         where hvl.Lopgiang_Id == id_lop
                                                         select hvl).ToListAsync();
                                List<ThongbaoLop_Db> list_tb_lop_add = new List<ThongbaoLop_Db>();
                                foreach (var hv in listhocvien)
                                {
                                    ThongbaoLop_Db tbl = new ThongbaoLop_Db();
                                    tbl.Thongbao_Id = thongbao_Id;
                                    tbl.Lopgiang_Id = id_lop;
                                    tbl.User_Id = hv.User_Id;
                                    tbl.Status = 0;
                                    list_tb_lop_add.Add(tbl);
                                }
                                await _context.thongbaoLop_Dbs.AddRangeAsync(list_tb_lop_add);
                            }
                        }
                    }
                    //khong the gui thong bao cho hoc vien ma khong chon lop
                    else if(model.list_ID_Lopgiang.Count==0 && model.list_ID_Nguoinhan.Count > 0)
                    {
                        throw new Exception("Hay chon lop co hoc vien de gui");
                    }
                    //khong chon lop -> gui cho tat ca hoc vien o tat ca cac lop
                    else
                    {
                        var listlop = await _context.lopgiangday_Dbs.ToListAsync();
                        foreach(var lop in listlop)
                        {
                            var listhocvien = await (from hvl in _context.hocvien_Lop_Dbs
                                                     where hvl.Lopgiang_Id == lop.LopgiangdayID
                                                     select hvl).ToListAsync();
                            List<ThongbaoLop_Db> list_tb_lop_add = new List<ThongbaoLop_Db>();
                            foreach(var hv in listhocvien)
                            {
                                ThongbaoLop_Db tbl = new ThongbaoLop_Db();
                                tbl.Thongbao_Id= thongbao_Id;
                                tbl.Lopgiang_Id = lop.LopgiangdayID;
                                tbl.User_Id = hv.User_Id;
                                tbl.Status = 0;
                                list_tb_lop_add.Add(tbl);
                            }
                            await _context.thongbaoLop_Dbs.AddRangeAsync(list_tb_lop_add);
                        }
                    }
                    int row = await _context.SaveChangesAsync();

                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Gui thong bao thanh cong";
                        return kq;
                    }
                    else
                    {
                        throw new Exception("Gui thong bao that bai");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
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
