using System.ComponentModel;

namespace Estimatz.Entities.Room
{
    public enum Decks
    {
        [Description("Scrum")]
        Scrum = 0,

        [Description("Fibonacci")]
        Fibonacci = 1,

        [Description("Sequencial")]
        Sequencial = 2,

        [Description("T-Shirt")]
        TShirt = 3
    }
}
