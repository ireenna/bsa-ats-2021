using Domain.Interfaces.Read;
using Domain.Interfaces.Write;
using FileInfo = Domain.Entities.FileInfo;
using Infrastructure.Files.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Write
{
    public class CandidateCvWriteRepository : ICandidateCvWriteRepository
    {
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IVacancyCandidateReadRepository _candidateReadRepository;
        private readonly IVacancyCandidateWriteRepository _candidateWriteRepository;

        public CandidateCvWriteRepository(
            IFileWriteRepository fileWriteRepository,
            IVacancyCandidateReadRepository candidateReadRepository,
            IVacancyCandidateWriteRepository candidateWriteRepository)
        {
            _fileWriteRepository = fileWriteRepository;
            _candidateReadRepository = candidateReadRepository;
            _candidateWriteRepository = candidateWriteRepository;
        }
        public async Task<FileInfo> UploadAsync(string candidateId, Stream cvFileContent)
        {
            var fileInfo = await _fileWriteRepository.UploadPrivateFileAsync(
                GetFilePath(),
                GetFileName(candidateId),
                cvFileContent);

            var candidate = await _candidateReadRepository.GetByPropertyAsync("Id", candidateId);
            candidate.
            candidate.CvFileInfoId = fileInfo.Id;

            await _candidateWriteRepository.UpdateAsync(candidate);

            return fileInfo;
        }

        public async Task UpdateAsync(string candidateId, Stream cvFileContent)
        {
            var cvFileInfo = await _candidateReadRepository.GetCvFileInfoAsync(candidateId);
            await _fileWriteRepository.UpdateFileAsync(cvFileInfo, cvFileContent);
        }

        public async Task DeleteAsync(FileInfo cvFileInfo)
        {
            await _fileWriteRepository.DeleteFileAsync(cvFileInfo);
        }
        private static string GetFilePath()
        {
            return "candidates";
        }

        private static string GetFileName(string candidateId)
        {
            return $"{candidateId}-cv.pdf";
        }
    }
}
