﻿using Application.Tasks.Dtos;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.Read;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Read
{
    public class TaskReadRepository : ReadRepository<ToDoTask>, ITaskReadRepository
    {
        public TaskReadRepository(IConnectionFactory connectionFactory) : base("Tasks", connectionFactory) { }

        private string mainSql = @"
                            select t.*,a.*,ut.*,u.*,hr.*,c.* 
                            from ToDoTask t inner join
                            Applicants a on t.ApplicantId=a.Id left outer join
                            UserToTask ut on ut.ToDoTaskId = t.Id left outer join
                            Users u on u.Id = ut.UserId inner join
                            Users hr on hr.Id = t.CreatedById inner join
                            Companies c on c.Id = t.CompanyId";

        
        private static ToDoTask queryMapper(ToDoTask task, Applicant applicant, UserToTask utt, User user, User hr, Company company, Dictionary<string, ToDoTask> TaskDictionary, Dictionary<string, UserToTask> UserToTaskDictionary)
        {
            if (!TaskDictionary.TryGetValue(task.Id, out ToDoTask TaskEntry))
            {

                TaskEntry = task;
                TaskEntry.TeamMembers = new List<UserToTask>();
                TaskEntry.Applicant = applicant;
                TaskEntry.Company = company;
                TaskEntry.CreatedBy = hr;

                TaskDictionary.Add(TaskEntry.Id, TaskEntry);
            }

            if (utt != null && !UserToTaskDictionary.TryGetValue($"{utt.ToDoTaskId}{utt.UserId}", out UserToTask userToTaskEntry))
            {
                userToTaskEntry = utt;
                TaskEntry.TeamMembers.Add(userToTaskEntry);
                UserToTaskDictionary.Add($"{utt.ToDoTaskId}{utt.UserId}", userToTaskEntry);
            }

            if (user != null)
            {
                utt.User = user;
            }

            return TaskEntry;
        }

        public async Task<List<ToDoTask>> GetTasksWithTeamMembersAsync()
        {
            var connection = _connectionFactory.GetSqlConnection();            
            
            await connection.OpenAsync();
            string sql = mainSql;
                            

            var TaskDictionary = new Dictionary<string, ToDoTask>();
            var UserToTaskDictionary = new Dictionary<string, UserToTask>();
            List<Task> allTasks = new List<Task>();

            var Task = (await connection.QueryAsync<ToDoTask, Applicant, UserToTask, User, User, Company, ToDoTask>(
                sql,
                (task, applicant, utt, user, hr, company) =>
                {
                    return queryMapper(task, applicant, utt, user, hr, company, TaskDictionary, UserToTaskDictionary);
                },
                splitOn: "Id,Id,UserId,Id,Id,Id"
                ));

            await connection.CloseAsync();

            return TaskDictionary.Values.ToList();
        }

        public async Task<List<ToDoTask>> GetTasksWithTeamMembersByUserAsync(string userId)
        {
            var connection = _connectionFactory.GetSqlConnection();            

            await connection.OpenAsync();
            string sql = $@"
                            {mainSql}
                            where hr.id = @userId or u.id = @userId";


            var TaskDictionary = new Dictionary<string, ToDoTask>();
            var UserToTaskDictionary = new Dictionary<string, UserToTask>();
            List<Task> allTasks = new List<Task>();

            var Task = (await connection.QueryAsync<ToDoTask, Applicant, UserToTask, User, User, Company, ToDoTask>(
                sql,
                (task, applicant, utt, user, hr, company) =>
                {
                    return queryMapper(task, applicant, utt, user, hr, company, TaskDictionary, UserToTaskDictionary);
                },
                new { userID = @userId },
                splitOn: "Id,Id,UserId,Id,Id,Id"
                ));

            await connection.CloseAsync();

            return TaskDictionary.Values.ToList();
        }

        public async Task<ToDoTask> GetTaskWithTeamMembersByIdAsync(string id)
        {
            var connection = _connectionFactory.GetSqlConnection();

            await connection.OpenAsync();
            string sql = $@"{mainSql}                            
                            where t.id = @id";

            var TaskDictionary = new Dictionary<string, ToDoTask>();
            var UserToTaskDictionary = new Dictionary<string, UserToTask>();

            var Task = (await connection.QueryAsync(
                sql,
                (Func<ToDoTask, Applicant, UserToTask, User, User, Company, ToDoTask>)((task, applicant, utt, user, hr, company) =>
                {
                    return queryMapper(task, applicant, utt, user, hr, company, TaskDictionary, UserToTaskDictionary);
                }),
                new { id = id },
                splitOn: "Id,Id,UserId,Id,Id,Id"
                ))
            .Distinct()
            .SingleOrDefault();

            await connection.CloseAsync();

            return Task;
        }

    }
}
