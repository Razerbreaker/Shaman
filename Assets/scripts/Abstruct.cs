using System.Collections;
using UnityEngine;

public abstract class Abstruct : MonoBehaviour
{

    public Animator MyAnimator { get; set; }
    public Rigidbody2D MyRigidbody2D { get; set; }
    public bool isFacingRight = true;
    public bool isGrounded;
    [SerializeField]
    protected Transform[] groundPoints;

    public float groundRadius = 0.3f;
    public LayerMask whatIsGround;

    [SerializeField]
    protected int health;
    [SerializeField]
    public abstract bool isDead { get; }
    public abstract void Death();
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }

    [SerializeField]
    protected float moveSpeed;
    public abstract IEnumerator TakeDamage();

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "1")
        {
            MyRigidbody2D.velocity = Vector2.zero;

            if (IsFacingRight)
            {
                MyRigidbody2D.AddForce(new Vector2(-100, 0));
            }
            else
            {
                MyRigidbody2D.AddForce(new Vector2(100, 0));
            }
            StartCoroutine(TakeDamage());
        }
  
    }

    public void Flip()
    {
        //меняем направление движения персонажа
        IsFacingRight = !IsFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }


    public bool IsGrounded()
    {
        if (MyRigidbody2D.velocity.y<=0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

}
