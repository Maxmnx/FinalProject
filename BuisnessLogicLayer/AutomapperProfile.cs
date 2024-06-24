using AutoMapper;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using static System.Net.WebRequestMethods;

namespace BuisnessLogicLayer
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() {

            CreateMap<FileInformation, FileModel>()
                .ForMember(fm => fm.Id, fi => fi.MapFrom(x => x.Id))
                .ForMember(fm => fm.Name, fi => fi.MapFrom(x => x.Name))
                .ForMember(fm => fm.Size, fi => fi.MapFrom(x => x.Size))
                .ForMember(fm => fm.AccessLevel, fi => fi.MapFrom(x => x.AccessLevel))
                .ForMember(fm => fm.FileTypeExtension, fi => fi.MapFrom(x => x.FileType.Extension))
                .ForMember(fm => fm.ContentType, fi => fi.MapFrom(x => x.FileType.MIMEType))
                .ForMember(fm => fm.Description, fi => fi.MapFrom(x => x.Description))
                .ForMember(fm => fm.CreatorId, fi => fi.MapFrom(x => x.CreatorId))
                .ForMember(fm => fm.CreatorUserName, fi => fi.MapFrom(x => x.Creator.UserName))
                .ForMember(fm => fm.CreationDate, fi => fi.MapFrom(x => x.CreationDate))
                .ReverseMap();


            CreateMap<FileType, FileTypeModel>()
                .ForMember(ftm => ftm.Id, ft => ft.MapFrom(x => x.Id))
                .ForMember(ftm => ftm.Extension, ft => ft.MapFrom(x => x.Extension))
                .ForMember(ftm => ftm.MIMEType, ft => ft.MapFrom(x => x.MIMEType))
                .ForMember(ftm => ftm.FileInformationIds, ft => ft.MapFrom(x => x.FileInformation.Select(x => x.Id)))
                .ReverseMap();
        }
    }
}