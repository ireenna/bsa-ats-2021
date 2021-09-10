using Domain.Entities;
using Domain.Interfaces.Abstractions;
using Infrastructure.EF;
using Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Write
{
    public class FileInfoWriteRepository : WriteRepository<FileInfo>
    {
        public FileInfoWriteRepository(ApplicationDbContext context) : base(context)
        { }

        public async override Task DeleteAsync(string id)
        {
            var candidates = _context.Set<VacancyCandidate>().Where(_ => _.CvFileInfoId == id);

            foreach (var candidate in candidates)
            {
                candidate.CvFileInfoId = null;
            }
            _context.UpdateRange(candidates);
            await _context.SaveChangesAsync();

            var fileInfo = await _context.Set<FileInfo>().FindAsync(id);
            _context.Remove(fileInfo);
            await _context.SaveChangesAsync();
        }
    }
}
