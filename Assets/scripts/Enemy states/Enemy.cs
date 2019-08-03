using System.Collections;
using UnityEngine;

public class Enemy : Abstruct
{
    private IEnemyState currentState;
    public bool canAttack;

    public GameObject Target { get; set; }

    public override bool isDead
    {
        get
        {
            return health <= 0;
        }
    }
    public void Start()

    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
        ChangeState(new IdleState());
    }

    public void Update()
    {
        currentState.Execute();
        LookAtTarget();
        isGrounded = IsGrounded();
      
    }


    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }


    public void Move()
    {
        if (isGrounded)
        {
            
            transform.Translate(GetDirection() * (moveSpeed * Time.deltaTime));
        }
    }


    public Vector2 GetDirection()
    {
        return IsFacingRight ? Vector2.right : Vector2.left;
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }


    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && IsFacingRight || xDir > 0 && !IsFacingRight)
            {
                Flip();
            }
        }
    }


    public void JumpAttack()  // вызывает анимация
    {
        Vector2 JumpAttackForce = new Vector2();

        if (IsFacingRight)
        {
            JumpAttackForce = new Vector2(200f, 300f);
        }
        else if (!IsFacingRight)
        {
            JumpAttackForce = new Vector2(-200f, 300f);
        }
        if (isGrounded && MyRigidbody2D.velocity.y <= 0)
        {
        this.GetComponent<Rigidbody2D>().AddForce(JumpAttackForce);
        }
    }


    public override IEnumerator TakeDamage()
    {
        health -= 10;

        if (!isDead)
        {
            Debug.Log("10 damage to mushroom");
        }
        else
        {
            MyAnimator.SetTrigger("die");
        }
        yield return null;
    }


    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void MakeGhost()
    {
        MyRigidbody2D.isKinematic = true;
        MyRigidbody2D.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
        GameObject obj = transform.Find("damage collider").gameObject;
        obj.SetActive(false);
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }
}

