using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;

    private bool attachedToPlayer = false;
	
	void Update () {
        Vector3 cameraPos;

        if (attachedToPlayer)
        {
            cameraPos = player.transform.position;
            cameraPos.z = -10;
            transform.position = cameraPos;
        }
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


}
