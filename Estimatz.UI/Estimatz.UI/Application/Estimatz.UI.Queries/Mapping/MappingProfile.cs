using AutoMapper;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Login;
using Estimatz.UI.Queries.Login;

namespace Estimatz.UI.Queries.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginQuery, LoginRequest>();
            CreateMap<LoginResponse, LoginQueryResponse>()
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => MappingData(src.Messages)))
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.Success))
                .ForMember(dest => dest.SignInUser, opt => opt.MapFrom(src => src.Data));
        }     
        
        private List<string> MappingData(List<Message> messagesResponse)
        {
            var messages = new List<string>();

            if (messagesResponse is null)
                return messages;

            foreach(var messageInData in messagesResponse)
            {
                messages.Add(messageInData.Description);
            }

            return messages;
        }
    }
}
