using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	float speed = 5;
	Vector3[] path;
	int targetIndex;
	public bool isMoving = false;
	private bool moveToTarget = false;

	public void RequestMovement(Vector3 targetPos, bool isPlayer) {
		if (!isMoving)
		{
			moveToTarget = isPlayer;
			PathRequestManager.RequestPath(transform.position,targetPos, OnPathFound);
		}
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
			isMoving = true;
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length)
				{ 
					isMoving = false;
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			currentWaypoint.y = 1.5f;
			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			yield return null;

		}

	}

}
