using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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



        //Status = 1 => chua doc ; 2 => da doc
        public async Task<object> chitietThongbao(int idthongbao, int user_id)
        {
            KqJson kq = new KqJson();
            try
            {
                if (idthongbao > 0 && user_id > 0)
                {
                    ThongbaoLop_Model thongbao = new ThongbaoLop_Model();
                    var result = await (from tb in _context.thongbao_Dbs
                                        join tbl in _context.thongbaoLop_Dbs
                                        on tb.ThongbaoID equals tbl.Thongbao_Id
                                        where tbl.Thongbao_Id == idthongbao && tbl.User_Id == user_id
                                        select tbl).SingleOrDefaultAsync();
                    if (result != null)
                    {
                        if (result.Status == 1)
                        {
                            result.Status = 2;
                            await _context.SaveChangesAsync();
                        }
                        var col = _context.Entry(result);
                        col.Reference(p => p.Thongbao).Load();
                        Thongbao_Db tb = new Thongbao_Db();
                        tb.Tieude = result.Thongbao.Tieude;
                        tb.Noidung = result.Thongbao.Noidung;
                        tb.Thoigian = result.Thongbao.Thoigian;
                        result.Thongbao = tb;

                        thongbao = _mapper.Map<ThongbaoLop_Model>(result);

                        thongbao.Status = "Da Xem";

                        return thongbao;
                    }
                    else
                    {

                        throw new Exception("Not Found");
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

        public async Task<KqJson> danhDauThongBao(Danhdauthongbao_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.List_Id_Thongbao.Count > 1 && model.User_Id > 0)
                {
                    if (model.Status == 1 || model.Status == 2)
                    {
                        foreach (var thongbao_id in model.List_Id_Thongbao)
                        {
                            var result = await (from tb in _context.thongbao_Dbs
                                                join tbl in _context.thongbaoLop_Dbs
                                                on tb.ThongbaoID equals tbl.Thongbao_Id
                                                where tbl.Thongbao_Id == thongbao_id && tbl.User_Id == model.User_Id
                                                select tbl).SingleOrDefaultAsync();
                            if (result != null)
                            {
                                if (model.Status == 1)
                                {
                                    if (result.Status == 2)
                                    {
                                        result.Status = 1;
                                    }
                                    else
                                    {
                                        throw new Exception("Thong bao co ID: " + thongbao_id + " khong the danh dau");
                                    }
                                }
                                else if (model.Status == 2)
                                {
                                    if (result.Status == 1)
                                    {
                                        result.Status = 2;
                                    }
                                    else
                                    {
                                        throw new Exception("Thong bao co ID: " + thongbao_id + " khong the danh dau");
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Result thongbao ID: " + thongbao_id + " is Not Found");
                            }
                        }
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Update successful";
                            return kq;
                        }
                        else
                        {
                            throw new Exception("Update Failed");
                        }
                    }
                    else
                    {
                        throw new Exception("Trang thai muon danh dau khong phu hop");
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

        public async Task<object> getallThongbao(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await (from tb in _context.thongbao_Dbs
                                        join tbl in _context.thongbaoLop_Dbs
                                        on tb.ThongbaoID equals tbl.Thongbao_Id
                                        where tbl.User_Id == id
                                        orderby tb.Thoigian descending
                                        select tb).ToListAsync();
                    List<Thongbao_Model> thongbao = new List<Thongbao_Model>();
                    thongbao = _mapper.Map<List<Thongbao_Model>>(result);

                    return thongbao;
                }
                else
                {
                    throw new Exception("Bad Request");
                }

            }
            catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<object> locThongBao(int user_id, int status)
        {
            KqJson kq = new KqJson();
            try
            {
                if (user_id > 0)
                {
                    if (status == 1 || status == 2)
                    {
                        var result = await (from tb in _context.thongbao_Dbs
                                            join tbl in _context.thongbaoLop_Dbs
                                            on tb.ThongbaoID equals tbl.Thongbao_Id
                                            where tbl.User_Id == user_id &&
                                            tbl.Status == status
                                            orderby tb.Thoigian descending
                                            select tb).ToListAsync();
                        if (result != null)
                        {
                            List<Thongbao_Model> listtb = new List<Thongbao_Model>();
                            listtb = _mapper.Map<List<Thongbao_Model>>(result);
                            return listtb;
                        }
                        else
                        {
                            throw new Exception("Not Found");
                        }
                    }
                    else
                    {
                        throw new Exception("Trang thai khong phu hop");
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

        public async Task<object> searchThongbao(int user_id, string keyword)
        {
            try
            {
                if (user_id > 0 && keyword != "")
                {
                    var result = await (from tb in _context.thongbao_Dbs
                                        join tbl in _context.thongbaoLop_Dbs
                                        on tb.ThongbaoID equals tbl.Thongbao_Id
                                        where tb.UserID == user_id &&
                                        tb.Tieude.Contains(keyword) || tb.Noidung.Contains(keyword)
                                        orderby tb.Thoigian descending
                                        select tbl).ToListAsync();
                    if (result.Count > 0)
                    {
                        List<Thongbao_Model> thongbao = new List<Thongbao_Model>();
                        thongbao = _mapper.Map<List<Thongbao_Model>>(result);

                        return thongbao;
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }

            }
            catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<KqJson> Taothongbao(Gui_Thongbao_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Tieude != "" && model.Noidung != "")
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
                            foreach (var id_lop in model.list_ID_Lopgiang)
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
                                    foreach (int idnn in list_nguoinhan_Id)
                                    {
                                        ThongbaoLop_Db tbl = new ThongbaoLop_Db();
                                        tbl.Thongbao_Id = thongbao_Id;
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
                    else if (model.list_ID_Lopgiang.Count == 0 && model.list_ID_Nguoinhan.Count > 0)
                    {
                        throw new Exception("Hay chon lop co hoc vien de gui");
                    }
                    //khong chon lop -> gui cho tat ca hoc vien o tat ca cac lop
                    else
                    {
                        var listlop = await _context.lopgiangday_Dbs.ToListAsync();
                        foreach (var lop in listlop)
                        {
                            var listhocvien = await (from hvl in _context.hocvien_Lop_Dbs
                                                     where hvl.Lopgiang_Id == lop.LopgiangdayID
                                                     select hvl).ToListAsync();
                            List<ThongbaoLop_Db> list_tb_lop_add = new List<ThongbaoLop_Db>();
                            foreach (var hv in listhocvien)
                            {
                                ThongbaoLop_Db tbl = new ThongbaoLop_Db();
                                tbl.Thongbao_Id = thongbao_Id;
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

        public async Task<KqJson> xoaThongbao(Delete_Entity_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.EntityId > 0 && model.User_Id > 0)
                {

                    var result = await (from tbl in _context.thongbaoLop_Dbs
                                        where tbl.User_Id == model.User_Id &&
                                        tbl.Thongbao_Id==model.EntityId
                                        select tbl).SingleOrDefaultAsync();

                    
                    if (result != null)
                    {
                        _context.thongbaoLop_Dbs.RemoveRange(result);
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Xoa thanh cong!";
                            return kq;
                        }
                        else
                        {
                            throw new Exception("Xoa that bai");
                        }
                    }
                    else
                    {
                        throw new Exception("Not Found");
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
    }
}
