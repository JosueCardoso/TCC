using System.ComponentModel;

namespace Estimatz.UI.Entities.Story
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