using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] pieces;

    private List<GameObject> bag = new List<GameObject>();

    void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        if (bag.Count == 0)
            FillBag();

        Instantiate(bag[0], transform.position, Quaternion.identity);
        bag.RemoveAt(0);
    }

    void FillBag()
    {
        List<GameObject> temp = new List<GameObject>(pieces);

        while (temp.Count > 0)
        {
            int i = Random.Range(0, temp.Count);
            bag.Add(temp[i]);
            temp.RemoveAt(i);
        }
    }

    public void CheckLines()
    {
        for (int y = 0; y < GridManager.height; y++)
        {
            if (IsFull(y))
            {
                GridManager.DeleteLine(y);
                GridManager.MoveDown(y + 1);
                y--;
            }
        }
    }

    bool IsFull(int y)
    {
        for (int x = 0; x < GridManager.width; x++)
        {
            if (GridManager.grid[x, y] == null)
                return false;
        }
        return true;
    }
}
