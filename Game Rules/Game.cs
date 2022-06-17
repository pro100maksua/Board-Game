namespace GameRules;

public class Game
{
    private CellState _currentPlayer;
    private GameState _gameState;
    private Board _board;

    public Game()
    {
        Reset();
    }

    public void Start()
    {
        _board.Print();

        while (_gameState == GameState.Game)
        {
            Console.WriteLine($"{_currentPlayer} turn");
            Console.Write("Select column: ");

            if (int.TryParse(Console.ReadLine(), out var column))
            {
                MakeTurn(column);
            }
            else
            {
                Console.WriteLine("Incorrect input.");
            }
        }
    }

    private void MakeTurn(int column)
    {
        var state = _board.Add(column - 1, _currentPlayer);
        if (state == GameState.Incorrent)
        {
            return;
        }

        _board.Print();

        _gameState = state;

        if (state != GameState.Game)
        {
            Console.WriteLine();
            Console.WriteLine(state == GameState.Draw ? "Draw!" : $"{_currentPlayer} wins!");
            Console.WriteLine();
            Console.WriteLine();
        }

        _currentPlayer = _currentPlayer == CellState.Yellow ? CellState.Red : CellState.Yellow;
    }

    private void Reset()
    {
        _currentPlayer = CellState.Yellow;
        _gameState = GameState.Game;

        _board = new Board();
    }
}