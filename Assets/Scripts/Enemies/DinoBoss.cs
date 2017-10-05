using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBoss : EnemyController {

    GameObject player;
    bool fightStarted = false;
    int attacking = 0;
    bool doAttack = false;
    bool battleStarted = false;
    bool spit = false;
    bool charge = false;
    bool charging = false;
    float attackTimer;
    bool attackCooldown = false;
    float attackCooldownTimer;
    bool chargeForward = false;
    bool notDead = true;

    public DinoBossFightController bossController;
    public GameObject firePrefab;
    public Transform mouthTrans;

    public float walkSpeed;
    public float chargeSpeed;

    public float distanceToStartBattle;
    public float distanceToAttack;

    public float roarTime;
    public float attackCooldownTime;

    private AudioSource roarSound;
    private AudioSource fireballSound;
    private AudioSource dieSound;
    private AudioSource crashSound;

    private void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        roarSound = GetComponents<AudioSource>()[0];
        fireballSound = GetComponents<AudioSource>()[1];
        dieSound = GetComponents<AudioSource>()[2];
        crashSound = GetComponents<AudioSource>()[3];
    }

    private void Update()
    {
        if (fadeOut)
            fadeEnemy();

        if (base.Dead() && notDead)
            DinoBossDead();

        if (base.Dead())
            return;

        if (!battleStarted)
        {
            CheckIfBattleStart();
            return;
        }

        if (chargeForward)
        {
            ChargePlayer();
            return;
        }

        base.facingRight = CheckDirectionToPlayer();

        if (attackCooldown)
        {
            anim.SetFloat("Speed", 0);
            attackCooldownTimer += Time.deltaTime;
            if(attackCooldownTimer >= attackCooldownTime)
            {
                attackCooldown = false;
            }
            else
            {
                return;
            }
        }

        if (attacking == 0)
        {
            if (base.facingRight)
            {
                Vector3 scale = transform.localScale;
                scale.x = -1;
                transform.localScale = scale;
            }
            else
            {
                Vector3 scale = transform.localScale;
                scale.x = 1;
                transform.localScale = scale;
            }

            WalkTowardsPlayer();
            if (DistanceToPlayer() <= distanceToAttack)
                attacking = 1;
        }

        if (attacking == 1)
            AttackPlayer();

        if (attacking == 2)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= roarTime)
            {
                if (charge)
                {
                    chargeForward = true;
                }
                else if (spit)
                {
                    SpitFire();
                }
            }
        }
    }

    void CheckIfBattleStart()
    {
        if (DistanceToPlayer() <= distanceToStartBattle)
        {
            battleStarted = true;
            bossController.StartBattle();
        }
    }

    bool CheckDirectionToPlayer()
    {
        float bossX = transform.position.x;
        float playerX = player.transform.position.x;

        return bossX < playerX;
    }

    float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    void WalkTowardsPlayer()
    {
        int direction;
        Vector2 pos = transform.position;

        if (facingRight)
            direction = 1;
        else
            direction = -1;

        pos.x = pos.x + (walkSpeed * direction * Time.deltaTime);
        transform.position = pos;
        anim.SetFloat("Speed", walkSpeed);
    }

    void AttackPlayer()
    {
        anim.SetBool("Roar", true);
        roarSound.Play();

        float value = Random.value;

        if (value <= 0.5f)
        {
            attacking = 2;
            attackTimer = 0;
            charge = true;
            spit = false;
        }
        else
        {
            attacking = 2;
            attackTimer = 0;
            spit = true;
            charge = false;
        }
    }

    void ChargePlayer()
    {
        int direction;
        Vector2 pos = transform.position;

        if (facingRight)
            direction = 1;
        else
            direction = -1;

        pos.x = pos.x + (chargeSpeed * direction * Time.deltaTime);
        transform.position = pos;
        anim.SetBool("Roar", false);
        anim.SetFloat("Speed", chargeSpeed);
    }

    void SpitFire()
    {
        anim.SetBool("Roar", false);
        anim.SetTrigger("SpitFire");
        GameObject fireObj;

        fireballSound.Play();

        fireObj = Instantiate(firePrefab, mouthTrans);

        fireObj.GetComponent<FireBallController>().TravelRight(facingRight);

        FinishAttack();
    }

    void FinishAttack()
    {
        spit = false;
        charge = false;
        chargeForward = false;
        attacking = 0;
        attackCooldown = true;
        attackCooldownTimer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            crashSound.Play();
            FinishAttack();
        }
    }

    void DinoBossDead()
    {
        notDead = false;
        dieSound.Play();
        bossController.DinoBossDead();
    }

    void Footstep()
    {
    }

}
