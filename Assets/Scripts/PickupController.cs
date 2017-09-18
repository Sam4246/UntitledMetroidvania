using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public float bobAmount;
    public float speed;

    private Vector2 startLocation;
    private bool bobUp = true;

	void Start () {
        startLocation = transform.position;
	}

	void Update () {
        Vector2 currLocation = transform.position;

        if (Mathf.Abs(currLocation.y - startLocation.y) > bobAmount)
            bobUp = !bobUp;

        if (bobUp)
        {
            currLocation.y += speed * Time.deltaTime;
        }
        else if (!bobUp)
        {
            currLocation.y -= speed * Time.deltaTime;
        }

        transform.position = currLocation;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
