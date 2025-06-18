using AutoMapper;
using Estimatz.UI.Commands.AddStory;
using Estimatz.UI.Commands.Common;
using Estimatz.UI.Commands.ConfirmRecoverPassword;
using Estimatz.UI.Commands.RecoverPassword;
using Estimatz.UI.Commands.RemoveStory;
using Estimatz.UI.Commands.SignIn;
using Estimatz.UI.Commands.UpdateStatusStory;
using Estimatz.UI.ExternalServices.EstimatzApi.AddStory;
using Estimatz.UI.ExternalServices.EstimatzApi.RemoveStory;
using Estimatz.UI.ExternalServices.EstimatzApi.UpdateStoryStatus;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.RecoverPassword;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Register;
using Estimatz.UI.Resources;

namespace Estimatz.UI.Commands.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SignInCommand, RegisterRequest>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name));

            CreateMap<CommonResponse, CommonCommandResponse>()
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => TranslateMessages(src.Messages)));

            CreateMap<ConfirmRecoverPasswordCommand, ConfirmRecoverPasswordRequest>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.PasswordRecoveryToken));

            CreateMap<RecoverPasswordCommand, RecoverPasswordRequest>();
            CreateMap<AddStoryCommand, AddStoryRequest>();
            CreateMap<RemoveStoryCommand, RemoveStoryRequest>();
            CreateMap<UpdateStatusStoryCommand, UpdateStoryStatusRequest>();
        }

        private List<string> TranslateMessages(List<Message> messagesWithCodes)
        {
            List<string> messages = new();

            if(messagesWithCodes is null)
                return messages;

            var selectedculture = Language.GetSelectedCulture();

            foreach (var message in messagesWithCodes)
            {
                if(selectedculture == "en-us")                
                    messages.Add(message.Description);                
                else if(message?.Code is not null)
                    messages.Add(Language.GetString(message?.Code));                              
            }

            return messages;
        }
    }
}
