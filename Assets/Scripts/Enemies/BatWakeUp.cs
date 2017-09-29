using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatWakeUp : MonoBehaviour {

    Bat bat;

	void Start () {
        bat = GetComponentInParent<Bat>();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            WakeBat(col.gameObject.transform);
    }

    private void WakeBat(Transform playerTrans)
    {
        bat.WakeUpBat(playerTrans);
        Destroy(gameObject);
    }
}
