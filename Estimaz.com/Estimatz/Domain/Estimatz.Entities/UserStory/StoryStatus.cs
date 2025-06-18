using System.ComponentModel;

namespace Estimatz.Entities.UserStory
{
    public enum StoryStatus
    {
        [Description("Votação não iniciada")]
        Unfinished = 0,

        [Description("Votação em andamento")]
        InProgress = 1,

        [Description("Votação finalizada")]
        Finished = 2        
    }
}
