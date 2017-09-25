using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum PlayerMovement { Up, Down, Left, Right, Neutral };

    public float speed;
    public float jumpSpeed;
    public float minDistToGround;
    public LayerMask layerMaskForGrounded;
    public float attackTime;
    public GameObject attackBeam;
    public GameObject attackBeamUp;
    public GameObject attackBeamDown;
    public CameraController mainCamera;
    public int attackDamage;

    private Rigidbody2D rb2d;
    private Animator anim;
    private Animator beamAnim;
    private Animator beamUpAnim;
    private Animator beamDownAnim;

    private GameController gameController;
    private bool isFacingRight = true;
    private float timePassed;
    private bool attacking;
    private bool attackingUp;
    private bool attackingDown;
    private bool isDead;
    private PolygonCollider2D attackTrigger;
    private PolygonCollider2D attackUpTrigger;
    private PolygonCollider2D attackDownTrigger;
    private bool paused = false;

    private static int playerHP;

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        beamAnim = attackBeam.GetComponent<Animator>();
        beamUpAnim = attackBeamUp.GetComponent<Animator>();
        beamDownAnim = attackBeamDown.GetComponent<Animator>();
        attackTrigger = attackBeam.GetComponent<PolygonCollider2D>();
        attackUpTrigger = attackBeamUp.GetComponent<PolygonCollider2D>();
        attackDownTrigger = attackBeamDown.GetComponent<PolygonCollider2D>();

        isDead = false;

        playerHP = 5;
	}

	void Update ()
    {
        if (paused)
            return;
        if (isDead)
            return;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");
        bool stopJump = Input.GetButtonUp("Jump");
        bool attack = Input.GetButtonDown("Attack");

        anim.SetBool("Grounded", IsGrounded());

        if (attack && vertInput > 0.3)
        {
            anim.SetTrigger("AttackUp");
            beamUpAnim.SetTrigger("Attack");
            attackUpTrigger.enabled = true;
            attackingUp = true;
        }
        else if (attack && vertInput < -0.3)
        {
            anim.SetTrigger("AttackDown");
            beamDownAnim.SetTrigger("Attack");
            attackDownTrigger.enabled = true;
            attackingDown = true;
        }
        else if (attack)
        {
            anim.SetTrigger("Attack");
            beamAnim.SetTrigger("Attack");
            attackTrigger.enabled = true;
            attacking = true;
        }

        if (attacking || attackingUp || attackingDown)
            AttackBeam();

        if(timePassed > attackTime)
            AttackBeamStop();

        transform.position = new Vector2(transform.position.x + speed * horInput * Time.deltaTime,transform.position.y);
        anim.SetBool("Walking", horInput != 0);
        anim.SetFloat("Speed", Mathf.Abs(horInput));

        FacingDirection(horInput);

        if (jump && IsGrounded())
            PlayerJump();

        if (stopJump && VerticalMovement() == PlayerMovement.Up)
            StopPlayerJump();

        if (rb2d.velocity.y > 0.1)
            anim.SetBool("MovingUp", true);
        else
            anim.SetBool("MovingUp", false);

        if (rb2d.velocity.y < -0.1)
            anim.SetBool("MovingDown", true);
        else
            anim.SetBool("MovingDown", false);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy" && !col.gameObject.GetComponent<EnemyController>().Dead())
        {
            TakeDamage(col.gameObject.GetComponent<EnemyController>().Damage());
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            anim.SetBool("Grounded", IsGrounded());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "HealthDrop")
            HealPlayer();

        if (col.gameObject.tag == "Exit" && gameController.CanPlayerExitScene())
            gameController.ChangeArea(col.gameObject.GetComponent<ExitController>().fromArea, col.gameObject.GetComponent<ExitController>().toArea, col.gameObject.GetComponent<ExitController>().toSpawn);
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        position.y = GetComponent<Collider2D>().bounds.min.y + 0.1f;
        float length = minDistToGround + 0.1f;
        Debug.DrawRay(position, Vector2.down * length);
        bool grounded = Physics2D.Raycast(position, Vector2.down, length, layerMaskForGrounded.value);

        return grounded;
    }

    void PlayerJump()
    {
        rb2d.AddForce(new Vector2(0, jumpSpeed));
    }

    void StopPlayerJump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
    }

    void FacingDirection(float direction)
    {
        if(direction > 0)
        {
            transform.localScale = new Vector2(1,1);
            isFacingRight = true;
        }
        else if(direction < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            isFacingRight = false;
        }
    }

    public bool FacingRight()
    {
        return isFacingRight;
    }

    public PlayerMovement VerticalMovement()
    {
        if (rb2d.velocity.y > 0)
            return PlayerMovement.Up;
        else if (rb2d.velocity.y < 0)
            return PlayerMovement.Down;
        else
            return PlayerMovement.Neutral;
    }

    public PlayerMovement HorizontalMovement()
    {
        if (Input.GetAxis("Horizontal") > 0.1)
            return PlayerMovement.Right;
        else if (Input.GetAxis("Horizontal") < -0.1)
            return PlayerMovement.Left;
        else
            return PlayerMovement.Neutral;
    }

    void AttackBeam()
    {
        timePassed += Time.deltaTime;
    }

    void AttackBeamStop()
    {
        attacking = false;
        attackingUp = false;
        attackingDown = false;
        attackTrigger.enabled = false;
        attackUpTrigger.enabled = false;
        attackDownTrigger.enabled = false;
        timePassed = 0f;
    }

    void TakeDamage(int damage)
    {
        Vector2 vel = rb2d.velocity;
        vel.y = 0;

        playerHP -= damage;
        rb2d.velocity = vel;
        rb2d.AddForce(new Vector2(0, 400));
        if (playerHP <= 0)
            Dead();
    }

    void Dead()
    {
        mainCamera.Dead();
        GetComponent<PolygonCollider2D>().enabled = false;
        isDead = true;
    }

    public static int RemainingHP()
    {
        return playerHP;
    }

    private void HealPlayer()
    {
        playerHP++;
        if (playerHP > 5)
            playerHP = 5;
    }

    public void TogglePaused()
    {
        paused = !paused;
    }

    public void SetGameController(GameController gc)
    {
        gameController = gc;
    }

    public void SetMainCamera(CameraController cam)
    {
        mainCamera = cam;
    }
}
