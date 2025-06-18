namespace Estimatz.Entities.Deck
{
    public static class DeckValues
    {
        private static Dictionary<int, List<string>> _deckValues = new Dictionary<int, List<string>>
        {
            { 0 , new List<string> { "0","meio", "1", "2", "3", "5", "8", "13", "20", "40", "100", "interrogation", "coffee" } }, //Scrum
            { 1 , new List<string> { "0", "1", "2", "3", "5", "8", "13", "21", "34", "55", "89", "interrogation", "coffee" } }, //Fibonacci
            { 2 , new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "interrogation", "coffee" } }, //Sequencial
            { 3 , new List<string> { "XS","S", "M", "L", "XL", "XXL", "interrogation", "coffee", } }, //T-Shirt
        };

        public static List<string> Get(int deckId) => _deckValues[deckId];
    }
}
