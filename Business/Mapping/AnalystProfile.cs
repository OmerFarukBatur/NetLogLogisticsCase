using AutoMapper;
using Core.DTOs.AnalystDtos;
using Core.Entities;

namespace Business.Mapping
{
    public class AnalystProfile : Profile
    {
        public AnalystProfile()
        {
            CreateMap<Core.Entities.Task, TaskPendingListDto>()
                .ForMember(d => d.OpenerName,
                    o => o.MapFrom(s =>
                        $"{s.CreatedByPersonnel.FirstName} {s.CreatedByPersonnel.LastName}"));

            CreateMap<TaskAnalysis, AnalystTaskListDto>()
                .ForMember(d => d.Id,
                    o => o.MapFrom(s => s.Task.Id))
                .ForMember(d => d.AnalysisId,
                    o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title,
                    o => o.MapFrom(s => s.Task.Title))
                .ForMember(d => d.OpenerName,
                    o => o.MapFrom(s =>
                        $"{s.Task.CreatedByPersonnel.FirstName} {s.Task.CreatedByPersonnel.LastName}"))
                .ForMember(d => d.DueDate,
                    o => o.MapFrom(s => s.Task.DueDate))
                .ForMember(d => d.AssignedAt,
                    o => o.MapFrom(s => s.CreatedAt));

            CreateMap<TaskAnalysis, AnalystTaskDetailDto>()
                .ForMember(d => d.TaskId,
                    o => o.MapFrom(s => s.Task.Id))
                .ForMember(d => d.AnalysisId,
                    o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title,
                    o => o.MapFrom(s => s.Task.Title))
                .ForMember(d => d.Description,
                    o => o.MapFrom(s => s.Task.Description))
                .ForMember(d => d.ExpectationNotes,
                    o => o.MapFrom(s => s.Task.ExpectationNotes))
                .ForMember(d => d.OpenerName,
                    o => o.MapFrom(s =>
                        $"{s.Task.CreatedByPersonnel.FirstName} {s.Task.CreatedByPersonnel.LastName}"))
                .ForMember(d => d.DueDate,
                    o => o.MapFrom(s => s.Task.DueDate))
                .ForMember(d => d.CreatedAt,
                    o => o.MapFrom(s => s.Task.CreatedAt))
                .ForMember(d => d.CanUpdate,
                    o => o.Ignore());
        }
    }
}
