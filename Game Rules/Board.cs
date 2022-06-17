namespace GameRules;

public class Board
{
    private const int ColumnCount = 7;
    private const int RowCount = 6;

    private Cell[][] _cells;

    public Board()
    {
        Reset();
    }

    public GameState Add(int columnIndex, CellState cellState)
    {
        if (columnIndex < 0 || columnIndex >= ColumnCount)
        {
            return GameState.Incorrent;
        }

        var column = _cells[columnIndex];

        var nextCellIndex = column.Count(c => c != null);

        if (nextCellIndex >= RowCount)
        {
            return GameState.Incorrent;
        }

        var newCell = new Cell { State = cellState };
        column[nextCellIndex] = newCell;

        return CheckWinCondition(columnIndex, nextCellIndex, newCell);
    }

    public void Print()
    {
        Console.Clear();
        Console.WriteLine();

        for (var j = RowCount - 1; j >= 0; j--)
        {
            for (var i = 0; i < ColumnCount; i++)
            {
                var cell = _cells[i][j];

                if (cell != null)
                {

                    if (cell.State == CellState.Yellow)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write("O ");

                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.Write("| ");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private GameState CheckWinCondition(int columnIndex, int rowIndex, Cell newCell)
    {
        var column = _cells[columnIndex];

        var state = CheckWinInRow(column, newCell);
        if (state is GameState.YellowWin or GameState.RedWin)
        {
            return state;
        }

        var row = _cells.Select(c => c[rowIndex]).ToArray();

        state = CheckWinInRow(row, newCell);
        if (state is GameState.YellowWin or GameState.RedWin)
        {
            return state;
        }

        state = CheckWinInRightDiagonal(columnIndex, rowIndex, newCell);
        if (state is GameState.YellowWin or GameState.RedWin)
        {
            return state;
        }

        state = CheckWinInLeftDiagonal(columnIndex, rowIndex, newCell);
        if (state is GameState.YellowWin or GameState.RedWin)
        {
            return state;
        }

        return GameState.Game;
    }

    private GameState CheckWinInRow(Cell[] row, Cell newCell)
    {
        var count = 0;

        foreach (var cell in row)
        {
            if (cell?.State == newCell.State)
            {
                count++;
            }
            else
            {
                count = 0;
            }

            if (count == 4)
            {
                break;
            }
        }

        if (count >= 4)
        {
            return newCell.State == CellState.Yellow ? GameState.YellowWin : GameState.RedWin;
        }

        return GameState.Game;
    }

    private GameState CheckWinInRightDiagonal(int columnIndex, int rowIndex, Cell newCell)
    {
        var count = 0;

        var i = columnIndex;
        var j = rowIndex;

        while (i > 0 && j > 0)
        {
            i--;
            j--;
        }

        while (i < ColumnCount && j < RowCount)
        {
            if (_cells[i][j]?.State == newCell.State)
            {
                count++;
            }
            else
            {
                count = 0;
            }

            i++;
            j++;

            if (count == 4)
            {
                break;
            }
        }

        if (count >= 4)
        {
            return newCell.State == CellState.Yellow ? GameState.YellowWin : GameState.RedWin;
        }

        return GameState.Game;
    }

    private GameState CheckWinInLeftDiagonal(int columnIndex, int rowIndex, Cell newCell)
    {
        var count = 0;

        var i = columnIndex;
        var j = rowIndex;

        while (i < ColumnCount && j > 0)
        {
            i++;
            j--;
        }

        while (i >= 0 && j < RowCount)
        {
            if (_cells[i][j]?.State == newCell.State)
            {
                count++;
            }
            else
            {
                count = 0;
            }

            i--;
            j++;

            if (count == 4)
            {
                break;
            }
        }

        if (count >= 4)
        {
            return newCell.State == CellState.Yellow ? GameState.YellowWin : GameState.RedWin;
        }

        return GameState.Game;
    }

    private void Reset()
    {
        _cells = new Cell[ColumnCount][];

        for (var i = 0; i < _cells.Length; i++)
        {
            _cells[i] = new Cell[RowCount];
        }
    }
}