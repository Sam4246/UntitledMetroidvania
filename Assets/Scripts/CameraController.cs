using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float moveSpeed;
    public float sizeSpeed;

    private GameObject player;
    private Camera cam;

    private float defaultSize = 7;

    private bool attachedToPlayer = false;
    private bool staticLocation = false;

    private Vector2 location;
    private Vector2 originalLocation;
    private float camSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update () {
        Vector3 cameraPos;

        if (attachedToPlayer)
        {
            cameraPos = player.transform.position;
            cameraPos.z = -10;
            transform.position = cameraPos;
        }

        if (staticLocation)
            MoveToLocation();
	}

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
        attachedToPlayer = true;
    }

    public void Dead()
    {
        attachedToPlayer = false;
    }

    public void MoveCameraLocation(Vector2 newLocation, float size = 7)
    {
        staticLocation = true;
        attachedToPlayer = false;

        location = newLocation;
        originalLocation = transform.position;
        camSize = size;
    }

    private void MoveToLocation()
    {
        Vector3 pos = transform.position;

            if (originalLocation.x < location.x && transform.position.x < location.x)
                MoveRight();
            else if(transform.position.x < location.x)
                MoveLeft();

        if (originalLocation.y < location.y && transform.position.y < location.y)
            MoveUp();
        else if(transform.position.y < location.y)
            MoveDown();

        if (cam.orthographicSize < camSize)
            GrowCam();
        else
            ShrinkCam();
    }

    private void MoveRight()
    {
        Vector3 pos = transform.position;
        pos.z = -10;
        pos.x += moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    private void MoveLeft()
    {
        Vector3 pos = transform.position;
        pos.z = -10;
        pos.x -= moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    private void MoveUp()
    {
        Vector3 pos = transform.position;
        pos.z = -10;
        pos.y += moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    private void MoveDown()
    {
        Vector3 pos = transform.position;
        pos.z = -10;
        pos.y -= moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    private void GrowCam()
    {
        cam.orthographicSize += sizeSpeed * Time.deltaTime;
    }

    private void ShrinkCam()
    {
        cam.orthographicSize -= sizeSpeed * Time.deltaTime;
    }

    public void ResetCamera()
    {
        cam.orthographicSize = defaultSize;
        staticLocation = false;
        attachedToPlayer = true;
    }

}
