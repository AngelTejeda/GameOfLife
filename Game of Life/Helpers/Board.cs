using System;
using System.Collections.Generic;

namespace Game_of_Life
{
    #region Board Explanation
    /*
     ███  ███  ██   ██ ████     ███  █████    █     █████ █████ █████
    █    █   █ █ █ █ █ █       █   █ █        █       █   █     █
    █ ██ █████ █  █  █ ███     █   █ ███      █       █   ███   ███
    █  █ █   █ █  █  █ █       █   █ █        █       █   █     █
     ██  █   █ █     █ ████     ███  █        █████ █████ █     █████

    topOffset = 0;
    leftOffset = 0;
    ┌───────────────────────────────────────────────┐ ← Window
    │                               ┬               │
    │                               │               │
    │                               │ topMargin     │
    │          Board                │               │
    │            ↓                  ┴               │
    │            ┌───────────────────────┐ ┬        │
    │            │X ← Cell (0, 0)        │ │        │
    │ leftMargin │    Board (0, 0)       │ │ height │
    │├──────────┤│                       │ │        │
    │            │                       │ │        │
    │            └───────────────────────┘ ┴        │
    │            ├───────────────────────┤          │
    │                      width                    │
    └───────────────────────────────────────────────┘


    topOffset = 1;
    leftOffset = 2;
    ┌───────────────────────────────────────────────┐ ← Window
    │                               ┬               │
    │                               │               │
    │                               │ topMargin     │
    │          Board                │               │
    │            ↓                  ┴               │
    │            ┌───────────────────────┐ ┬        │
    │            │                       │ │        │
    │ leftMargin │  X ← Cell (0,0)       │ │ height │
    │├──────────┤│      Board (1, 2)     │ │        │
    │            │                       │ │        │
    │            └───────────────────────┘ ┴        │
    │            ├───────────────────────┤          │
    │                      width                    │
    └───────────────────────────────────────────────┘
    
    Even if the offset changes, the coordinate (0, 0) for the board will always be the same.
    */
    #endregion

    public class Board
    {
        public HashSet<Coordinate> CellsCoordinates { get; }

        private readonly int _height;
        private readonly int _width;
        private readonly int _leftMargin;
        private readonly int _topMargin;
        private int _leftOffset = 0;
        private int _topOffset = 0;

        public Board((int height, int width) dimensions, (int topMargin, int leftMargin) margins)
        {
            CellsCoordinates = new();
            _height = dimensions.height;
            _width = dimensions.width;
            _leftMargin = margins.leftMargin;
            _topMargin = margins.topMargin;
        }

        #region Getters
        public (int height, int width) GetDimensions()
        {
            return (_height, _width);
        }

        public (int topMargin, int leftMargin) GetMargins()
        {
            return (_topMargin, _leftMargin);
        }
        #endregion

        #region Coordinates
        // Given the coordinate (y, x) of a cell, it calculates the position inside the board where
        // the cell should be displayed considering the current offset and the position of the board.
        private Coordinate CalculateBoardCoordinates(Coordinate cellCoordinate)
        {
            Coordinate boardCoordinate = new();

            boardCoordinate.y = cellCoordinate.y + _topOffset;
            boardCoordinate.x = cellCoordinate.x + _leftOffset;

            return boardCoordinate;
        }

        // Given the coordinate (yPos, xPos) inside the board, it calculates the coordinate of the cell
        // in the given position considering the current offset and the position of the board.
        private Coordinate CalculateCellCoordinates(Coordinate boardCoordinate)
        {
            Coordinate cellCoordinate = new();

            cellCoordinate.y = boardCoordinate.y - _topOffset;
            cellCoordinate.x = boardCoordinate.x - _leftOffset;

            return cellCoordinate;
        }

        // Verifies if the cell in the coordinate (y, x) should be displayed in the board, considering
        // the current offset and the position of the board.
        private bool IsCoordinateDisplayed(Coordinate cellCoordinate)
        {
            Coordinate boardCoord = CalculateBoardCoordinates(cellCoordinate);

            // Check y
            if (boardCoord.y < 0 || boardCoord.y > _height - 1)
                return false;

            // Check x
            if (boardCoord.x < 0 || boardCoord.x > _width - 1)
                return false;

            return true;
        }
        #endregion

        #region Display
        // Writes a certain character inside the board. If the coordinate (y, x) is outside the
        // board range, it will  not be displayed.
        private void DisplayCharAt(char c, Coordinate coord)
        {
            if (!IsCoordinateDisplayed(coord))
                return;

            Coordinate boardCoord = CalculateBoardCoordinates(coord);

            SetCursorInsideBoard(boardCoord.x, boardCoord.y);
            Console.Write(c);
            SetCursorInsideBoard(boardCoord.x, boardCoord.y);
        }

        public void SetCursorInsideBoard(int x, int y)
        {
            Console.SetCursorPosition(_leftMargin + 1 + x, _topMargin + 1 + y);
        }

        public void SetCursorInsideBoard(Coordinate boardCoordinate)
        {
            SetCursorInsideBoard(boardCoordinate.y, boardCoordinate.x);
        }

        private void DrawBoarder()
        {
            // Corners
            SetCursorInsideBoard(-1, -1);
            Console.Write("┌");
            SetCursorInsideBoard(_width, -1);
            Console.Write("┐");
            SetCursorInsideBoard(-1, _height);
            Console.Write("└");
            SetCursorInsideBoard(_width, _height);
            Console.Write("┘");

            // Horizontal Lines
            for (int i = 1; i < _width + 1; i++)
            {
                SetCursorInsideBoard(i - 1, -1);
                Console.Write("─");
                SetCursorInsideBoard(i - 1, _height);
                Console.Write("─");
            }

            // Vertical Lines
            for (int i = 1; i < _height + 1; i++)
            {
                SetCursorInsideBoard(-1, i - 1);
                Console.Write("│");
                SetCursorInsideBoard(_width, i - 1);
                Console.Write("│");
            }
        }

        private void DrawCells()
        {
            foreach (Coordinate cellCoordinate in CellsCoordinates)
                DisplayCharAt('█', cellCoordinate);
        }

        public void ClearBoard()
        {
            foreach (Coordinate cellCoordinate in CellsCoordinates)
            {
                DisplayCharAt(' ', cellCoordinate);
            }
        }

        public void RefreshBoard()
        {
            ClearBoard();
            DrawCells();
        }

        public void DisplayBoard(bool clearBoard = true)
        {
            if (clearBoard)
                ClearBoard();

            DrawBoarder();
            DrawCells();

            SetCursorInsideBoard(0, 0);
        }
        #endregion

        #region Cell Management
        // Adds a coordinate to the Dictionary of selected coordinates.
        // If desired and posible to do so, the cell will be displayed in the board.
        public void PlaceCellWithKey(Coordinate cellCoordinate, bool display = true)
        {
            CellsCoordinates.Add(cellCoordinate);

            if (display)
                DisplayCharAt('█', cellCoordinate);
        }

        // Removes a coordinate from the Dictionary of selected coordinates.
        // If desired and posible to do so, the cell will be removed from the board.
        public void RemoveCellWithKey(Coordinate cellCoordinate, bool display = true)
        {
            CellsCoordinates.Remove(cellCoordinate);

            if (display)
                DisplayCharAt(' ', cellCoordinate);
        }

        // Recieves a coordinate (yPos, xPos) inside the board and adds its corresponding value to the
        // Dictionary.
        public void PlaceCellAtBoard(Coordinate boardCoordinate, bool display = true)
        {
            Coordinate cellCoordinate = CalculateCellCoordinates(boardCoordinate);

            PlaceCellWithKey(cellCoordinate, display);
        }

        // Recieves a coordinate (yPos, xPos) inside the board and removes its corresponding value from the
        // Dictionary.
        public void RemoveCellAtBoard(Coordinate boardCoordinate, bool display = true)
        {
            Coordinate cellCoordinate = CalculateCellCoordinates(boardCoordinate);

            RemoveCellWithKey(cellCoordinate, display);
        }
        #endregion

        #region Offsets
        public void MoveLeft(bool displayChanges = true)
        {
            if (displayChanges) ClearBoard();

            _leftOffset--;

            if (displayChanges) DrawCells();
        }

        public void MoveRight(bool displayChanges = true)
        {
            if (displayChanges) ClearBoard();

            _leftOffset++;

            if (displayChanges) DrawCells();
        }

        public void MoveUp(bool displayChanges = true)
        {
            if (displayChanges) ClearBoard();

            _topOffset--;

            if (displayChanges) DrawCells();
        }

        public void MoveDown(bool displayChanges = true)
        {
            if (displayChanges) ClearBoard();

            _topOffset++;

            if (displayChanges) DrawCells();
        }
        #endregion
    }
}