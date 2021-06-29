namespace Game_of_Life
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleExtension.ApplyWindowAction(ConsoleExtension.WindowActions.MAXIMIZE);

            Board<Cell> board = new((40, 40), (5, 5));
            BoardManager<Cell> boardManager = new(board);

            boardManager.PlaceFigureAt(TempFigures.GetFlower(), 10, 10);
            boardManager.EditBoard();

            GameOfLife game = new(board);
            game.Play();
        }
    }
}