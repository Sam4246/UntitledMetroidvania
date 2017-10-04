using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    public LayerMask layerMaskForGrounded;
    public int attackPower;
    public float healthDropRate;
    public bool facingRight = false;
    public Rigidbody2D rb2d;
    public Animator anim;
    public bool fadeOut = false;
    public int health;
    public float hitforce;

    private bool isDead = false;
    private DropController dc;
    private bool paused = false;

    public void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dc = GetComponent<DropController>();
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Attack")
            TakeDamage(col.gameObject.GetComponentInParent<PlayerController>());
    }

    void TakeDamage(PlayerController playerCon)
    {
        health -= playerCon.attackDamage;
        if (health <= 0)
            Die();

        if (!fadeOut)
        {
            if(playerCon.FacingRight())
                rb2d.AddForce(new Vector2(hitforce, hitforce));
            else
                rb2d.AddForce(new Vector2(-hitforce, hitforce));
        }
    }

    public void Die()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
        isDead = true;
        anim.SetTrigger("Dead");

        fadeOut = true;

        Drop();
    }

    public bool Dead()
    {
        return isDead;
    }

    public void fadeEnemy()
    {
        Color enemyColour;
        enemyColour = GetComponent<SpriteRenderer>().color;
        enemyColour.a -= 0.01f;
        GetComponent<SpriteRenderer>().color = enemyColour;

        if (enemyColour.a <= 0)
            Destroy(gameObject);
    }

    public int Damage()
    {
        return attackPower;
    }

    private void Drop()
    {
        float randomRoll;

        randomRoll = Random.Range(0, 100);
        if(randomRoll < healthDropRate)
        {
            DropHealth();
        }
    }

    private void DropHealth()
    {
        dc.DropHealth(transform.position);
    }

    public void TogglePause()
    {
        paused = !paused;
    }
}
