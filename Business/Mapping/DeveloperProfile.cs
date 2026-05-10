using AutoMapper;
using Core.DTOs.DeveloperDtos;
using Core.Entities;
using Core.Helpers;

namespace Business.Mapping
{
    public class DeveloperProfile : Profile
    {
        public DeveloperProfile()
        {
            CreateMap<TaskDevelopment, DeveloperTaskListDto>()
                .ForMember(d => d.Id,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.Id))
                .ForMember(d => d.DevelopmentId,
                    o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.Title))
                .ForMember(d => d.OpenerName,
                    o => o.MapFrom(s =>
                        $"{s.TaskAnalysis.Task.CreatedByPersonnel.FirstName} {s.TaskAnalysis.Task.CreatedByPersonnel.LastName}"))
                .ForMember(d => d.AnalystName,
                    o => o.MapFrom(s =>
                        $"{s.TaskAnalysis.AnalystPersonnel.FirstName} {s.TaskAnalysis.AnalystPersonnel.LastName}"))
                .ForMember(d => d.DifficultyLevel,
                    o => o.MapFrom(s => s.TaskAnalysis.DifficultyLevel))
                .ForMember(d => d.Priority,
                    o => o.MapFrom(s => s.TaskAnalysis.Priority))
                .ForMember(d => d.DueDate,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.DueDate))
                .ForMember(d => d.AssignedAt,
                    o => o.MapFrom(s => s.CreatedAt));

            CreateMap<TaskDevelopment, DeveloperTaskDetailDto>()
                .ForMember(d => d.TaskId,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.Id))
                .ForMember(d => d.DevelopmentId,
                    o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.Title))
                .ForMember(d => d.Description,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.Description))
                .ForMember(d => d.ExpectationNotes,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.ExpectationNotes))
                .ForMember(d => d.OpenerName,
                    o => o.MapFrom(s =>
                        $"{s.TaskAnalysis.Task.CreatedByPersonnel.FirstName} {s.TaskAnalysis.Task.CreatedByPersonnel.LastName}"))
                .ForMember(d => d.DueDate,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.DueDate))
                .ForMember(d => d.CreatedAt,
                    o => o.MapFrom(s => s.TaskAnalysis.Task.CreatedAt))
                .ForMember(d => d.AnalystName,
                    o => o.MapFrom(s =>
                        $"{s.TaskAnalysis.AnalystPersonnel.FirstName} {s.TaskAnalysis.AnalystPersonnel.LastName}"))
                .ForMember(d => d.DifficultyName,
                    o => o.MapFrom(s => s.TaskAnalysis.DifficultyLevel.GetDisplayName()))
                .ForMember(d => d.PriorityName,
                    o => o.MapFrom(s => s.TaskAnalysis.Priority.GetDisplayName()))
                .ForMember(d => d.AnalystNotes,
                    o => o.MapFrom(s => s.TaskAnalysis.AnalystNotes))
                .ForMember(d => d.RequirementsDetail,
                    o => o.MapFrom(s => s.TaskAnalysis.RequirementsDetail));
        }
    }
}
