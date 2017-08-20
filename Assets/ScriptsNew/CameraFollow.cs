using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float timeToCamera = 3;

    private Vector3 playerPosition;
    private Vector3 currentPosition;
    private Vector3 delta;

	// Use this for initialization
	void Start ()
    {
        playerPosition = playerTransform.position;
        currentPosition = transform.position;
        delta = currentPosition - playerPosition;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        playerPosition = playerTransform.position;
        currentPosition = transform.position;
        //currentPosition = new Vector3 (playerPosition.x, playerPosition.y, playerPosition.z - delta);

        transform.position = Vector3.Lerp(currentPosition, playerPosition + delta, timeToCamera * Time.deltaTime);
	}
}
