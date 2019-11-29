using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMove : MonoBehaviour
{
    public GameObject [] shapes;   
    public float moveSpeed = 0.5f;
    private float internalTime = 0f;

    private GameObject newShape;

    void Start()
    {
        newShape = GenerateShape();
    }

    void Update()
    {
        if (internalTime >= moveSpeed)
        {
            newShape.transform.position += Vector3.down;
            if (!GameControler.IsValidPosition(newShape))
            {
                newShape.transform.position += Vector3.up;
                foreach (Transform block in newShape.transform)
                {
                    int x = (int)block.position.x;
                    int y = (int)block.position.y;
                    GameControler.grid[x, y] = block;
                }
                for(int y = 0; y < 17; y++)
                {
                    if (GameControler.IsFullRow(y))
                    {
                        GameControler.DestoryFullRow(y);
                        GameControler.MoveDownRowAbove(y);
                    }
                }
                newShape = GenerateShape();
                moveSpeed = 0.5f;
            }
            internalTime = 0f;
        }
        else
        {
            internalTime += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            newShape.transform.position += Vector3.right;
            if (!GameControler.IsValidPosition(newShape))
            {
                newShape.transform.position -= Vector3.right;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            newShape.transform.position += Vector3.left;
            if (!GameControler.IsValidPosition(newShape))
            {
                newShape.transform.position -= Vector3.left;
            }
        }else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveSpeed /= 10;
        }
    }

    /*
     * 随机生成方块
     */
    GameObject GenerateShape()
    {
        int i = Random.Range(0, shapes.Length);
        return Instantiate(shapes[i], transform.position, transform.rotation);
    }
}
