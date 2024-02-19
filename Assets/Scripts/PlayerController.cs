using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text positionText;

    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Node node = hit.collider.GetComponent<Node>();
            if (node != null)
            {
                positionText.text = "Position: (" + node.gridX  + ", " + node.gridY + ")";
                if (Input.GetMouseButton(0))
                {
                    MovementController movementController = transform.GetComponent<MovementController>();
                    if (movementController)
                    {
                        Vector3 targetPos = hit.transform.position;
                        targetPos.y = 1.5f; 
                        if (Vector3.Distance(transform.position, targetPos) > 1f)
                        {
                            movementController.RequestMovement(targetPos,true);
                        }
                    }
                }
            }
        }
    }
}
