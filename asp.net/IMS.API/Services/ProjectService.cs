﻿using AutoMapper;
using IMS.Api.Common.Helpers.Extensions;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Projects;
using IMS.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Services
{
    public class ProjectService : ServiceBase<Project>, IProjectService
    {
        private readonly IMapper _mapper;
        public ProjectService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<ProjectReponse> GetAllProjectAsync(ProjectRequest request)
        {
            var projectQuery = await context.Projects
                .Include(x => x.Milestones)
                .Include(x => x.IssueSettings)
                .Include(x => x.Issues)
                .Include(x => x.ProjectMembers)
                .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
            || u.Name.Contains(request.KeyWords)
            || u.Description.Contains(request.KeyWords)).ToListAsync();
            

            if (request.ClassId != null)
            {
                projectQuery = projectQuery.Where(x => x.ClassId == request.ClassId).ToList();
            }

            var projectDtos = mapper.Map<List<ProjectDto>>(projectQuery).Paginate(request).ToList();


            var response = new ProjectReponse
            {
                Projects = projectDtos,
                Page = GetPagingResponse(request, projectQuery.Count()),
            };

            return response;
        }

        public async Task<ProjectDto> GetProjectById(int Id)
        {
            var subject = await context.Projects
           .Include(x => x.Issues)
           .Include(x => x.Milestones)
           .Include(x => x.ProjectMembers)
           .Include(x => x.IssueSettings)
           .FirstOrDefaultAsync(u => u.Id == Id);

            var subjectDto = mapper.Map<ProjectDto>(subject);
            return subjectDto;
        }

        public async Task DeleteStudentAsyns(Guid studentId, int projectId)
        {
            var queryStudent = await context.ProjectMembers.FirstOrDefaultAsync(x => x.UserId.Equals(studentId) && x.ProjectId == projectId);
            if (queryStudent != null)
            {
                context.Remove(queryStudent);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("User or project doesn't exist");
            }
        }
    }
}
