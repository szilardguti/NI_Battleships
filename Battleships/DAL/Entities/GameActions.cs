namespace Battleships.DAL.Entities
{
    public class GameActions
    {
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public string ActionString { get; set; }

        public GameResult GameResult { get; set; }
    }
}