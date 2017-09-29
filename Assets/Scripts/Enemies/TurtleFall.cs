using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleFall : MonoBehaviour {

    SpikeTurtle turtle;

	void Start () {
        turtle = GetComponentInParent<SpikeTurtle>();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            FallDown();
    }

    private void FallDown()
    {
        turtle.DropOnPlayer();
        Destroy(gameObject);
    }
}
