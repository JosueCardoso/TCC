using Estimatz.UI.Entities.Story;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;

namespace Estimatz.UI.ExternalServices.EstimatzApi.GetStory
{
    public class GetStoryResponse : CommonResponse
    {
        public UserStory Data { get; set; }
    }
}
