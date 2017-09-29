using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController {

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

    public void WakeUpBat(Transform playerTrans)
    {
        anim.SetTrigger("Wake");
        awake = true;
        player = playerTrans;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (awake)
            base.OnTriggerEnter2D(col);
    }
}
