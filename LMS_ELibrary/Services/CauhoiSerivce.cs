using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    
    public class CauhoiService : ICauhoiService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;

        public CauhoiService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KqJson> addCauhoi(QA_Model cauhoi)
        {
            try
            {
                KqJson kq = new KqJson();
                QA_Db ch = new QA_Db();
                ch.Cauhoi = cauhoi.Cauhoi;
                ch.Cautrl = cauhoi.Cautrl;
                ch.MonhocID=cauhoi.MonhocID;
                ch.Lancuoisua = DateTime.Now;

                _context.qA_Dbs.Add(ch);
                int row =await _context.SaveChangesAsync();
                if (row > 0)
                {
                    kq.Status = true;
                    kq.Message = "Them thanh cong!";
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Them that bai!";
                }

                return kq;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<QA_Model> chitietCauhoi(int idcauhoi)
        {
            try
            {
                QA_Model cauhoi_model = new QA_Model();
                var result = await _context.qA_Dbs.SingleOrDefaultAsync(p => p.QAID == idcauhoi);
                if (result != null)
                {
                    var reff= _context.Entry(result);
                    reff.Reference(p => p.Monhoc).Load();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.MaMonhoc = result.Monhoc.MaMonhoc;
                    monhoc.TenMonhoc = result.Monhoc.TenMonhoc;
                    monhoc.Mota = result.Monhoc.Mota;
                    monhoc.Tinhtrang = result.Monhoc.Tinhtrang;
                    monhoc.TobomonId = result.Monhoc.TobomonId;

                    result.Monhoc = monhoc;

                    cauhoi_model = _mapper.Map<QA_Model>(result);

                }
                else
                {
                    throw new Exception("khong tim thay");
                }
                

                return cauhoi_model;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> editCauhoi(int idcauhoi, QA_Model cauhoi)
        {
            try
            {
                KqJson kq = new KqJson();
                var result = await _context.qA_Dbs.SingleOrDefaultAsync(p=>p.QAID==idcauhoi);
                if (result != null)
                {

                    result.Cauhoi = cauhoi.Cauhoi!=null?cauhoi.Cauhoi: result.Cauhoi;
                    result.Cautrl = cauhoi.Cautrl!=null?cauhoi.Cautrl: result.Cautrl;
                    result.Lancuoisua = DateTime.Now;
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Sua thanh cong!";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Sua that bai!";
                    }
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Khong tim thay!";
                }


                return kq;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<QA_Model>> getAllCauhoi()
        {
            try
            {
                var result = await (from cauhoi in _context.qA_Dbs orderby cauhoi.Lancuoisua descending select cauhoi).ToListAsync();
                foreach (var item in result)
                {
                    var col = _context.Entry(item);
                    col.Reference(p => p.Monhoc).Load();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                    monhoc.Mota = item.Monhoc.Mota;
                    monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                    monhoc.TobomonId = item.Monhoc.TobomonId;

                    item.Monhoc=monhoc;
                    
                }
                List<QA_Model> listcauhoi = new List<QA_Model>();
                listcauhoi = _mapper.Map<List<QA_Model>>(result);


                return listcauhoi;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<QA_Model>> xemCauhoitheoMon(int idmon)
        {
            try
            {
                var result = await (from cauhoi in _context.qA_Dbs where cauhoi.MonhocID==idmon orderby cauhoi.Lancuoisua descending select cauhoi).ToListAsync();
                foreach (var item in result)
                {
                    var col = _context.Entry(item);
                    col.Reference(p => p.Monhoc).Load();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                    monhoc.Mota = item.Monhoc.Mota;
                    monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                    monhoc.TobomonId = item.Monhoc.TobomonId;

                    item.Monhoc = monhoc;

                }
                List<QA_Model> listcauhoi = new List<QA_Model>();
                listcauhoi = _mapper.Map<List<QA_Model>>(result);


                return listcauhoi;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<QA_Model>> xemCauHoitheoToMon(int idtomon)
        {
            try
            {
                var result = await(from cauhoi in _context.qA_Dbs 
                                   join monhoc in _context.monhoc_Dbs
                                   on cauhoi.MonhocID equals monhoc.MonhocID
                                   where monhoc.TobomonId==idtomon
                                   orderby cauhoi.Lancuoisua descending
                                   select cauhoi).ToListAsync();
                foreach (var item in result)
                {
                    var col = _context.Entry(item);
                    col.Reference(p => p.Monhoc).Load();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                    monhoc.Mota = item.Monhoc.Mota;
                    monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                    monhoc.TobomonId = item.Monhoc.TobomonId;

                    item.Monhoc = monhoc;

                }
                List<QA_Model> listcauhoi = new List<QA_Model>();
                listcauhoi = _mapper.Map<List<QA_Model>>(result);


                return listcauhoi;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> xoaCauhoi(int idcauhoi)
        {
            try
            {
                KqJson kq = new KqJson();
                var result = await _context.qA_Dbs.SingleOrDefaultAsync(p=>p.QAID==idcauhoi);
                if (result != null)
                {
                    _context.qA_Dbs.Remove(result);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Xoa thanh cong!";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Xoa that bai!";
                    }
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Khong tim thay!";
                }

                return kq;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
