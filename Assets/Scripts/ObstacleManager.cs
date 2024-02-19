using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;
    public Transform gridParent;

    void Awake()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        for (int i =  obstacleData.obstacles.Length-1; i >= 0; i--)
        {
            if (obstacleData.obstacles[i])
            {
                int x = i % 10;
                int y = i / 10;
                Vector3 position = new Vector3(x - 4.5f, 0.5f, y - 4.5f); // Adjust height as needed
                GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, gridParent);
                obstacle.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
