using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public static int width = 10;
    public static int height = 17;
    public static Transform[,] grid = new Transform[width, height];

    /*
     * 判断边界
     */
    public static bool InsideBorder(Transform block)
    {
        return block.position.y >= 0 && block.position.x >= 0 && block.position.x <= 9;
    }

    public static bool IsValidPosition(GameObject shape)
    {
        foreach (Transform block in shape.transform)
        {
            int x = (int)block.position.x;
            int y = (int)block.position.y;
            if (!InsideBorder(block))
            {
                return false;
            }
            if (grid[x, y] != null && grid[x, y].parent != shape.transform)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsFullRow(int y)
    {
        for(int x = 0; x < 10; x++)
        {
            if(grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void DestoryFullRow(int y)
    {
        for(int x = 0; x < 10; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void MoveDownRow(int y)
    {
        for(int x = 0; x < 10; x++)
        {
            if(grid[x, y] != null)
            {
                grid[x, y].position += Vector3.down;
            }
        }
    }

    public static void MoveDownRowAbove(int y)
    {
        for(int i = y; i < 17; i++)
        {
            MoveDownRow(i);
        }
    }
}
