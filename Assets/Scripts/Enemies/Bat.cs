using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyController {

    public float maxDistToPlayer;

    private bool awake = false;
    private Transform player;
	
    void Start()
    {
        base.Start();
        rb2d.bodyType = RigidbodyType2D.Static;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

	// Update is called once per frame
	void Update () {
        if (fadeOut)
            fadeEnemy();

        if (base.Dead())
            return;

        if (awake)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }

        if (CheckIfPlayerWake())
            WakeUpBat();
    }

    bool CheckIfPlayerWake()
    {
        if (Vector2.Distance(player.position, transform.position) < maxDistToPlayer)
            return true;
        else
            return false;
    }

    public void WakeUpBat()
    {
        anim.SetTrigger("Wake");
        awake = true;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }
}
