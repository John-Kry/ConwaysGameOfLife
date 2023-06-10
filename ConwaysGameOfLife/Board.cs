using System.Diagnostics;

namespace ConwaysGameOfLife;

public class Board
{
    private const int Width = 100;
    private const int Height = 30;
    private readonly bool[,] _state = new bool[Width,Height];
    private readonly Rule _rule = new();

    public void Main()
    {
        SetInitialState();
        while (true)
        {
            var stopwatch = new Stopwatch();
            Render();
            Tick();    
            stopwatch.Stop();
            if (stopwatch.Elapsed < TimeSpan.FromMilliseconds(250))
            {
                Task.Delay(TimeSpan.FromMilliseconds(250) - stopwatch.Elapsed).Wait();
            }
        }
    }

    private void SetInitialState()
    {
        // for (int x = 0; x < _state.GetLength(0); x++)
        // {
        //     for (int y = 0; y < _state.GetLength(1); y++)
        //     {
        //         _state[x, y] = Random.Shared.Next(40) <= 4;
        //     }
        // }
        _state[50, 15] = true;
        _state[50, 16] = true;
        _state[50, 17] = true;

        _state[51, 17] = true;
        _state[49, 16] = true;
    }

    private void Tick()
    {
        var initialState = (bool[,]) _state.Clone();
        for (var x = 0; x < initialState.GetLength(0); x++)
        {
            for (var y = 0; y < initialState.GetLength(1); y++)
            {
                _state[x,y] = _rule.ShouldLive(initialState, new Position(x, y));
            }
        }
    }

    private void Render()
    {
        var s = "";
        for (int y = 0; y < _state.GetLength(1); y++)
        {
            s += "=";
            for (int x = 0; x < _state.GetLength(0); x++)
            {
                var c = _state[x, y] == true ? '█' : ' ';
                s += c;
            }

            s += "=";
            s += "\n";
        }
        Console.Clear();
        Console.WriteLine(s);
    }
}