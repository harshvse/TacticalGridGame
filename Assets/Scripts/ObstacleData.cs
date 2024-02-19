using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Obstacle/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public bool[] obstacles = new bool[100]; // Represents a 10x10 grid
}