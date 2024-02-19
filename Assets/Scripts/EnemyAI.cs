using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    private MovementController movementController;
    public Transform playerUnit;
    private GameObject player;
    public float tileRange = 4.5f;

    private void Start()
    {
        movementController = GetComponent<MovementController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        DetermineNextMove();
    }

    public void DetermineNextMove()
    {
        if (!player.GetComponent<MovementController>().isMoving)
        {
            Vector3 playerPos = player.transform.position;

            Vector3[] adjacentTiles = new Vector3[]
            {
                new Vector3(playerPos.x + 1, playerPos.y, playerPos.z), // Right
                new Vector3(playerPos.x - 1, playerPos.y, playerPos.z), // Left
                new Vector3(playerPos.x, playerPos.y, playerPos.z + 1), // Up
                new Vector3(playerPos.x, playerPos.y, playerPos.z - 1) // Down
            };

            Vector3 nextTile = Vector3.zero;
            foreach (Vector3 tile in adjacentTiles)
            {
                if (tile != playerPos && Mathf.Abs(tile.x) <= tileRange && Mathf.Abs(tile.z) <= tileRange)
                {
                    nextTile = tile;
                    break;
                }
            }

            movementController.isMoving = false;
            // Request movement to the selected tile
            movementController.RequestMovement(nextTile, false);
        }
    }
}