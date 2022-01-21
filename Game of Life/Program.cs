namespace Game_of_Life
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleExtension.ApplyWindowAction(ConsoleExtension.WindowActions.MAXIMIZE);

            (int height, int width) = ConsoleExtension.GetWindowSize();
            int topMargin = 8;
            int bottomMargin = 5;
            int leftMargin = 5;
            int rightMargin = 5;

            height -= topMargin + bottomMargin + 2;
            width -= leftMargin + rightMargin + 2;
            

            Board board = new((height, width), (topMargin, leftMargin));
            GameManager boardManager = new(board);

            boardManager.DrawTitle();
            //boardManager.PlaceFigureAt(TempFigures.GetFlower(), 10, 10);
            boardManager.EditBoard();

            GameOfLife game = new(board);
            game.Play();
        }
    }
}