using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTurtle : EnemyController {

    public float minDistToGround;

    private bool falling = false;
    private bool ceiling = true;

	// Update is called once per frame
	void Update () {
        base.Update();

        if (fadeOut)
            fadeEnemy();

        if (base.Dead())
            return;

        if (falling)
            return;

        if (facingRight)
            MoveRight();
        else
            MoveLeft();

        if (ceiling && OverTheEdgeCeiling())
            TurnAround();
        else if (!ceiling && OverTheEdgeGround())
            TurnAround();
    }

    bool OverTheEdgeCeiling()
    {
        Vector2 position = transform.position;
        position.y = GetComponent<Collider2D>().bounds.max.y - 0.1f;

        if (facingRight)
            position.x = GetComponent<Collider2D>().bounds.max.x + 0.1f;
        else
            position.x = GetComponent<Collider2D>().bounds.min.x + 0.1f;

        float length = minDistToGround + 0.1f;
        Debug.DrawRay(position, Vector2.up * length);
        bool grounded = Physics2D.Raycast(position, Vector2.up, length, layerMaskForGrounded.value);

        return !grounded;
    }

    bool OverTheEdgeGround()
    {
        Vector2 position = transform.position;
        position.y = GetComponent<Collider2D>().bounds.min.y + 0.1f;

        if (facingRight)
            position.x = GetComponent<Collider2D>().bounds.max.x + 0.1f;
        else
            position.x = GetComponent<Collider2D>().bounds.min.x + 0.1f;

        float length = minDistToGround + 0.1f;
        Debug.DrawRay(position, Vector2.down * length);
        bool grounded = Physics2D.Raycast(position, Vector2.down, length, layerMaskForGrounded.value);

        return !grounded;
    }

    void MoveRight()
    {
        transform.position += new Vector3(speed, 0) * Time.deltaTime;
    }

    void MoveLeft()
    {
        transform.position -= new Vector3(speed, 0) * Time.deltaTime;
    }

    void TurnAround()
    {
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
        facingRight = !facingRight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" && falling)
            WalkOnGround();

        if (col.gameObject.tag == "Wall")
            TurnAround();
    }

    public void DropOnPlayer()
    {
        falling = true;
        rb2d.gravityScale = 3;
        anim.SetTrigger("Fall");
        ceiling = false;
    }

    void WalkOnGround()
    {
        falling = false;
        anim.SetTrigger("Ground");
    }
}
