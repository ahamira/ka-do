using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;

    public static Transform[,] grid = new Transform[10, 20];

    public static bool Inside(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public static void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void MoveDown(int fromY)
    {
        for (int y = fromY; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position += Vector3.down;
                }
            }
        }
    }
}
