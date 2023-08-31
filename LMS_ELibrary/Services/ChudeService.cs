using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public class ChudeService : IChudeService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public ChudeService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Chude_Model> editChude(int id,Chude_Model chude)
        {
            
            var cd = await _context.chude_Dbs.SingleOrDefaultAsync(p => p.ChudeID == id);
            if (cd != null)
            {
                
                cd.Tenchude = chude.Tenchude;
                await _context.SaveChangesAsync();
            }

            Chude_Model model = new Chude_Model();
            model = _mapper.Map<Chude_Model>(cd);



            return model;
        }
    }
}
