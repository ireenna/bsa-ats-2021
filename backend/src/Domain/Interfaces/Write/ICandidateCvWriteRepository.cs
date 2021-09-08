using FileInfo = Domain.Entities.FileInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Write
{
    public interface ICandidateCvWriteRepository
    {
        Task<FileInfo> UploadAsync(string applicantId, Stream cvFileContent);
        Task UpdateAsync(string applicantId, Stream cvFileContent);
        Task DeleteAsync(FileInfo fileInfo);
    }
}
