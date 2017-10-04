using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBossFightController : MonoBehaviour {

    public DinoBoss dinoBoss;
    public CameraController cam;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject leftDoorClosed;

    public float doorSpeed;
    public float bossCameraSize;
    public Vector2 bossCameraLocation;

    bool raiseDoors = false;
    bool lowerDoors = false;
    float closedDoorHeight;
    float openDoorHeight;

    private void Start()
    {
        closedDoorHeight = leftDoorClosed.transform.position.y;
        openDoorHeight = leftDoor.transform.position.y;
    }

    private void Update()
    {
        if (raiseDoors)
            RaiseDoors();

        if (lowerDoors)
            LowerDoors();
    }

    public void StartBattle()
    {
        raiseDoors = true;
        cam.MoveCameraLocation(bossCameraLocation, bossCameraSize);
    }

    private void RaiseDoors()
    {
        Vector2 pos = leftDoor.transform.position;
        pos.y += doorSpeed * Time.deltaTime;
        leftDoor.transform.position = pos;
        pos.x = rightDoor.transform.position.x;
        rightDoor.transform.position = pos;

        if(pos.y >= closedDoorHeight)
        {
            raiseDoors = false;
        }
    }

    private void LowerDoors()
    {
        Vector2 pos = leftDoor.transform.position;
        pos.y -= doorSpeed * Time.deltaTime;
        leftDoor.transform.position = pos;
        pos.x = rightDoor.transform.position.x;
        rightDoor.transform.position = pos;

        if (pos.y <= openDoorHeight)
        {
            raiseDoors = false;
        }
    }

    public void DinoBossDead()
    {
        lowerDoors = true;
        cam.ResetCamera();
    }
}
