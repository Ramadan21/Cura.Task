using AutoMapper;
using Cura.Task.DAL.Dtos.Mail;
using Cura.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Service.MappingProfile
{
    public class MailProfile:Profile
    {
        public MailProfile()
        {
            CreateMap<Mail, MailResponseDto>()
                .ForMember(a => a.IsStared, d => d.MapFrom(s => s.IsStared))
                .ForMember(a => a.body, d => d.MapFrom(s => s.Body))
                .ForMember(a => a.ReciveTime, d => d.MapFrom(s => s.SentDate))
                .ForMember(a => a.Subject, d => d.MapFrom(s => s.Subject))
                .ForMember(a => a.Id, d => d.MapFrom(s => s.Id))
                .ReverseMap();
        }
    }
}
