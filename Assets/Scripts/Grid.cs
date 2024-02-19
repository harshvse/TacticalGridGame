using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector3 gridWorldSize;
    public GameObject tilePrefab;
    public float nodeRadius;
    GameObject[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY; //how many nodes are there in an axis

    private void Awake()
    {
        nodeDiameter = 2 * nodeRadius;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }
    void CreateGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        grid = new GameObject[gridSizeX, gridSizeY];
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                grid[i, j] = Instantiate(tilePrefab,worldPoint,Quaternion.identity,this.transform);
                Node node = grid[i, j].GetComponent<Node>();
                node.walkable = walkable;
                node.worldPostion = worldPoint;
                node.gridX = i;
                node.gridY = j;
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int CheckX = node.gridX + x;
                int CheckY = node.gridY + y;

                if (CheckX >= 0 && CheckX < gridSizeX && CheckY >= 0 && CheckY < gridSizeY) // check if the neighbour nodes are out of map
                {
                    neighbours.Add(grid[CheckX, CheckY].GetComponent<Node>());
                }
            }
        }
        return neighbours;
    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentageX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentageY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y; //notice how we need to use z as y here
        percentageX = Mathf.Clamp01(percentageX);
        percentageY = Mathf.Clamp01(percentageY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentageX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentageY);

        return grid[x, y].GetComponent<Node>();
    }

    public List<Node> path;

    private void Update()
    {
        if (grid != null)
        {
            foreach (GameObject n in grid)
            {
                n.GetComponent<Renderer>().material.color = (n.GetComponent<Node>().walkable) ? Color.white : Color.red;
                if (path != null)
                {
                    if (path.Contains(n.GetComponent<Node>()))
                    {
                        n.GetComponent<Renderer>().material.color = Color.black;
                    }
                }
            }
    
        }
    }

}
