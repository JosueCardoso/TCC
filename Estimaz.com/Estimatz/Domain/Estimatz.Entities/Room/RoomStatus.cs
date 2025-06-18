using System.ComponentModel;

namespace Estimatz.Entities.Room
{
	public enum RoomStatus
	{
        [Description("Não iniciada")]
        NotStarted = 0,

        [Description("Não finalizada")]
        Unfinished = 1,

        [Description("Finalizada")]
        Finished = 2,

        [Description("Votação livre")]
        FreeVoting = 3 //Não tem controle de US
    }
}
