using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWorm : EnemyController {

    public float minDistToGround;

    void Update ()
    {
        base.Update();

        if (fadeOut)
            fadeEnemy();

        if (base.Dead())
            return;

        if (facingRight)
            MoveRight();
        else
            MoveLeft();

        if (OverTheEdge())
            TurnAround();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
            TurnAround();
    }

    bool OverTheEdge()
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
}
