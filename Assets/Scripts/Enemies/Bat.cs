using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController {

    //private Animator batAnim;
    private bool awake = false;
    private Transform player;
	
    void Start()
    {
        base.Start();
        rb2d.bodyType = RigidbodyType2D.Static;
    }

	// Update is called once per frame
	void Update () {
        base.Update();

        if (fadeOut)
            fadeEnemy();

        if (base.Dead())
            return;

        if (awake)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        if (col.gameObject.tag == "Player")
            WakeUpBat(col.gameObject.transform);
    }

    void WakeUpBat(Transform playerTrans)
    {
        anim.SetTrigger("Wake");
        awake = true;
        player = playerTrans;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }
}
