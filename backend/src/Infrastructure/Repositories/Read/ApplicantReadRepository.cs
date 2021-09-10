using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using Domain.Interfaces.Read;
using Domain.Entities;
using Application.Common.Exceptions;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Repositories.Abstractions;
using System;
using Application.Common.Exceptions.Applicants;
using Application.Interfaces;

namespace Infrastructure.Repositories.Read
{
    public class ApplicantReadRepository : ReadRepository<Applicant>, IApplicantReadRepository
    {
        private readonly ICurrentUserContext _currentUserContext;

        public ApplicantReadRepository(IConnectionFactory connectionFactory, ICurrentUserContext currentUserContext) : base("Applicants", connectionFactory)
        {
            _currentUserContext = currentUserContext;
        }

        public async Task<IEnumerable<FileInfo>> GetCvFileInfosAsync(string applicantId)
        {
            SqlConnection connection = _connectionFactory.GetSqlConnection();

            var query = @"SELECT fi.* FROM FileInfos fi WHERE fi.ApplicantId = @applicantId;";

            var fileInfos = await connection.QueryAsync<FileInfo>(query, param: new { applicantId });

            if (fileInfos == null)
            {
                throw new ApplicantCvNotFoundException(applicantId);
            }

            await connection.CloseAsync();

            return fileInfos;
        }

        public async Task<IEnumerable<Applicant>> GetCompanyApplicants()
        {
            var companyId = (await _currentUserContext.GetCurrentUser()).CompanyId;

            SqlConnection connection = _connectionFactory.GetSqlConnection();

            string sql = @$"SELECT a.*, fi.* FROM {_tableName} a
                           LEFT JOIN FileInfos fi ON a.Id = fi.ApplicantId
                           WHERE a.CompanyId = @companyId";

            await connection.OpenAsync();

            var applicantDictionary = new Dictionary<string, Applicant>();

            var entities = (await connection.QueryAsync<Applicant, FileInfo, Applicant>(sql,
                (a, fi) =>
                {
                    Applicant applicantEntry;

                    if (!applicantDictionary.TryGetValue(a.Id, out applicantEntry))
                    {
                        applicantEntry = a;
                        applicantEntry.CvFileInfos = new List<FileInfo>();
                        applicantDictionary.Add(applicantEntry.Id, applicantEntry);
                    }

                    if (fi != null)
                    {
                        applicantEntry.CvFileInfos.Add(fi);
                    }
                    return applicantEntry;
                },
                splitOn: "Id,Id",
                param: new { companyId })).Distinct();

            await connection.CloseAsync();

            return entities;
        }

        public async Task<IEnumerable<ApplicantVacancyInfo>> GetApplicantVacancyInfoListAsync(string applicantId)
        {
            SqlConnection connection = _connectionFactory.GetSqlConnection();

            string sql = "SELECT Vacancies.Id, Vacancies.Title, Stages.Id, Stages.Name, " +
                         "Projects.Id, Projects.Name, CandidateToStages.StageId, VacancyCandidates.Id FROM Vacancies " +
                         "JOIN Stages ON Vacancies.Id = Stages.VacancyId " +
                         "JOIN Projects ON Vacancies.ProjectId = Projects.Id " +
                         "JOIN CandidateToStages ON CandidateToStages.StageId = Stages.Id " +
                         "JOIN VacancyCandidates ON CandidateToStages.CandidateId = VacancyCandidates.Id " +
                         $"WHERE VacancyCandidates.ApplicantId = @applicantId";

            await connection.OpenAsync();

            var applicantVacancyInfos = await connection
                .QueryAsync<Vacancy, Stage, Project, CandidateToStage, VacancyCandidate, ApplicantVacancyInfo>(sql,
                    (v, s, p, cs, vc) =>
                    {
                        return new ApplicantVacancyInfo()
                        {
                            Title = v.Title,
                            Stage = s.Name,
                            Project = p.Name,
                        };
                    },
                    new { applicantId = @applicantId },
                    splitOn: "Id,Id,StageId,Id"
                );

            await connection.CloseAsync();

            return applicantVacancyInfos;
        }

        public async Task<Applicant> GetByIdAsync(string applicantId)
        {
            SqlConnection connection = _connectionFactory.GetSqlConnection();
            
            string sql = @$"SELECT a.*, fi.* FROM {_tableName} a
                            LEFT JOIN FileInfos fi ON a.Id = fi.ApplicantId
                            WHERE a.Id = @applicantId";

            await connection.OpenAsync();

            var entities = await connection.QueryAsync<Applicant, FileInfo, Applicant>(sql,
            (a, fi) =>
            {
                if (fi != null)
                {
                    a.CvFileInfos = a.CvFileInfos == null
                        ? new List<FileInfo>().Append(fi).ToList()
                        : a.CvFileInfos.Append(fi).ToList();
                }
                return a;
            },
            splitOn: "Id,Id",
            param: new { applicantId });

            await connection.CloseAsync();

            var applicant = entities.FirstOrDefault();

            if (applicant == null)
            {
                throw new NotFoundException(typeof(Applicant), applicantId);
            }

            return applicant;
        }

        public async Task<IEnumerable<(Applicant, bool)>> GetApplicantsWithAppliedMark(string vacancyId)
        {
            string companyId = (await _currentUserContext.GetCurrentUser()).CompanyId;

            SqlConnection connection = _connectionFactory.GetSqlConnection();

            string sql = @$"SELECT DISTINCT AllApplicants.*, CASE WHEN Applied.[Index] IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END IsApplied FROM Applicants AS AllApplicants
                           LEFT OUTER JOIN
                           (SELECT VacancyCandidates.ApplicantId, Stages.[Index]
                           From Stages 
                           LEFT OUTER JOIN CandidateToStages ON CandidateToStages.StageId = Stages.Id AND (Stages.[Index]=0 OR Stages.[Index]=1)
                           LEFT OUTER JOIN VacancyCandidates ON CandidateToStages.CandidateId = VacancyCandidates.Id
                           WHERE Stages.VacancyId = @vacancyId) AS Applied ON AllApplicants.Id=Applied.ApplicantId
                           WHERE AllApplicants.CompanyId = @companyId";

            var result = await connection.QueryAsync<Applicant, bool, (Applicant, bool)>(sql,
                (applicant, isApplied) =>
                {
                    (Applicant, bool) pair;

                    pair.Item1 = applicant;
                    pair.Item2 = isApplied;

                    return pair;
                }, new
                {
                    vacancyId = @vacancyId,
                    companyId = @companyId
                },
            splitOn: "IsApplied");

            await connection.CloseAsync();

            return result;
        }

        public async Task<Applicant> GetByCompanyIdAsync(string id)
        {
            string companyId = (await _currentUserContext.GetCurrentUser()).CompanyId;

            SqlConnection connection = _connectionFactory.GetSqlConnection();
            await connection.OpenAsync();

            string sql = @$"
                    SELECT * FROM Applicants
                    WHERE Applicants.Id = @Id
                    AND Applicants.CompanyId = @companyId";

            var applicant = await connection.QueryFirstOrDefaultAsync<Applicant>(sql,
            new
            {
                Id = id,
                companyId = @companyId
            });

            if (applicant == null)
            {
                throw new NotFoundException(typeof(Applicant), id);
            }

            await connection.CloseAsync();

            return applicant;
        }

        public override async Task<IEnumerable<Applicant>> GetEnumerableAsync()
        {
            string companyId = (await _currentUserContext.GetCurrentUser()).CompanyId;

            var connection = _connectionFactory.GetSqlConnection();
            await connection.OpenAsync();
            string sql = @$"SELECT * FROM {_tableName} 
                            WHERE Applicants.CompanyId = @companyId";

            var entities = await connection.QueryAsync<Applicant>(sql,
                new
                {
                    companyId = @companyId
                });
            await connection.CloseAsync();

            return entities;
        }
    }
}
