using AutoMapper;
using Core.DTOs.OpenerDtos;
using Core.Entities;
using Core.Enums;
using Core.Helpers;

namespace Business.Mapping
{
    public class OpenerProfile : Profile
    {
        public OpenerProfile()
        {
            CreateMap<Core.Entities.Task, TaskListDto>()
                .ForMember(d => d.AnalystName,
                    o => o.MapFrom(s => s.TaskAnalysis != null
                        ? $"{s.TaskAnalysis.AnalystPersonnel.FirstName} {s.TaskAnalysis.AnalystPersonnel.LastName}"
                        : null))
                .ForMember(d => d.AnalysisStatus,
                    o => o.MapFrom(s => s.TaskAnalysis != null
                        ? s.TaskAnalysis.Status.GetDisplayName()
                        : null))
                .ForMember(d => d.DeveloperName,
                    o => o.MapFrom(s => s.TaskAnalysis != null && s.TaskAnalysis.TaskDevelopment != null
                        ? $"{s.TaskAnalysis.TaskDevelopment.DeveloperPersonnel.FirstName} {s.TaskAnalysis.TaskDevelopment.DeveloperPersonnel.LastName}"
                        : null))
                .ForMember(d => d.DevStatus,
                    o => o.MapFrom(s => s.TaskAnalysis != null && s.TaskAnalysis.TaskDevelopment != null
                        ? s.TaskAnalysis.TaskDevelopment.Status.GetDisplayName()
                        : null));

            CreateMap<Core.Entities.Task, TaskDetailDto>()
                .ForMember(d => d.Analysis,
                    o => o.MapFrom(s => s.TaskAnalysis))
                .ForMember(d => d.Development,
                    o => o.MapFrom(s => s.TaskAnalysis != null
                        ? s.TaskAnalysis.TaskDevelopment
                        : null));

            CreateMap<TaskAnalysis, TaskAnalysisDetailDto>()
                .ForMember(d => d.AnalystName,
                    o => o.MapFrom(s => $"{s.AnalystPersonnel.FirstName} {s.AnalystPersonnel.LastName}"))
                .ForMember(d => d.DifficultyLevel,
                    o => o.MapFrom(s => s.DifficultyLevel.GetDisplayName()))
                .ForMember(d => d.Priority,
                    o => o.MapFrom(s => s.Priority.GetDisplayName()))
                .ForMember(d => d.Status,
                    o => o.MapFrom(s => s.Status.GetDisplayName()));

            CreateMap<TaskDevelopment, TaskDevDetailDto>()
                .ForMember(d => d.DeveloperName,
                    o => o.MapFrom(s => $"{s.DeveloperPersonnel.FirstName} {s.DeveloperPersonnel.LastName}"))
                .ForMember(d => d.Status,
                    o => o.MapFrom(s => s.Status.GetDisplayName()));

            CreateMap<TaskCreateDto, Core.Entities.Task>()
                .ForMember(d => d.Stage, o => o.MapFrom(_ => TaskStage.Open))
                .ForMember(d => d.IsDeleted, o => o.MapFrom(_ => false));

            CreateMap<TaskUpdateDto, Core.Entities.Task>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.ExpectationNotes, o => o.MapFrom(s => s.ExpectationNotes))
                .ForMember(d => d.DueDate, o => o.MapFrom(s => s.DueDate));
        }
    }
}
