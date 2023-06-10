using System.Diagnostics;

namespace ConwaysGameOfLife;

public class Rule
{
    public static IEnumerable<bool> GetNeighbors(bool[,] state, Position position)
    {
        var neighbors = new List<bool>();
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (i != 0 || j != 0)
                {
                    neighbors.Add(GetPosition(state, new Position(position.X + i, position.Y + j)));
                }
            }
        }
        Debug.Assert(neighbors.Count == 8);
        return neighbors;
    }

    public static bool GetPosition(bool[,] state, Position position)
    {
        if (position.Y >= state.GetLength(1)|| position.Y <0)
        {
            return false;
        }
        
        if (position.X >= state.GetLength(0)|| position.X <0)
        {
            return false;
        }

        return state[position.X, position.Y];
    }

    public bool ShouldLive(bool[,] state, Position position)
    {
        if (GetPosition(state, position) && LivingCells(state, position) >=2 && LivingCells(state, position) <=3)
        {
            return true;
        }

        if (!GetPosition(state, position) && LivingCells(state, position) == 3)
        {
            return true;
        }

        return false;
    }

    private static int LivingCells(bool[,] state, Position position)
    {
        return GetNeighbors(state, position).Count(i => i==true);
    }
    
}