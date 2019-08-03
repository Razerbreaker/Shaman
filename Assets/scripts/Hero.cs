using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : Abstruct
{
    private static Hero instance;

    public static Hero Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Hero>();
            }
            return instance;
        }
    }

    public bool Jump { get; set; }
    public bool Attack { get; set; }

    public override bool isDead
    {
        get
        {
            return health <= 0;
        }
    }
    [SerializeField]
    private PolygonCollider2D melee_attack_colider;
    [SerializeField]
    private PolygonCollider2D melee_attack_colider2;

    public float maxSpeed = 15f;
    public float maxSpeedInAir = 12f;
    public float currentSpeed;
    public float jumpForse = 1350f;
    public float move;
    public float xBound;
    public float yBound;

    public Vector2 reboundAtDamage;

    [SerializeField]
    private GameObject CheckPoint;

    private void Start()
    {
        health = int.Parse(DBmanager.ExecuteQueryWithAnswer($"SELECT healh FROM Player WHERE id_player=1"));
        MyAnimator = GetComponent<Animator>();
        MyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Handle_movement();
        Handle_flip();
    }

    private void Update()
    {
        if (isDead)
        {
            Death();
        }

        HandleInput();
        Update_parametrs();
    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Attack == false && MyRigidbody2D.velocity.y == 0)
        {
            MyAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.F) && Attack == false && MyRigidbody2D.velocity.y == 0)
        {
            MyAnimator.SetTrigger("melee attack1");
            Attack = true;
        }
        if (Input.GetKeyDown(KeyCode.G) && Attack == false && MyRigidbody2D.velocity.y == 0)
        {
            MyAnimator.SetTrigger("melee attack2");
            Attack = true;
        }
        if (Attack == false)
        {
            move = Input.GetAxis("Horizontal");
        }
    }

    private void Update_parametrs()
    {
        isGrounded = IsGrounded();
        MyAnimator.SetBool("Ground", isGrounded);
        MyAnimator.SetFloat("vSpeed", MyRigidbody2D.velocity.y);
        MyAnimator.SetFloat("speed", Mathf.Abs(move));
        ChangeSpeedInAir();
    }

    private void ChangeSpeedInAir()
    {
        if (!isGrounded)
        {
            currentSpeed = maxSpeedInAir;
        }
        else
        {
            currentSpeed = maxSpeed;
        }
    }

    private void Handle_movement()
    {
        MyRigidbody2D.velocity = new Vector2(move * currentSpeed, MyRigidbody2D.velocity.y);

        if (Jump && MyRigidbody2D.velocity.y == 0)
        {
            MyRigidbody2D.AddForce(new Vector2(0, jumpForse));
        }

    }

    private void Handle_flip()
    {
        if (!MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            if (move > 0 && !IsFacingRight)
                Flip();
            else if (move < 0 && IsFacingRight)
                Flip();
        }
    }

    public void Melee_attack()
    {
        melee_attack_colider.enabled = !melee_attack_colider.enabled;
    }

    public void Melee_attack2()
    {
        melee_attack_colider2.enabled = !melee_attack_colider2.enabled;
    }

    public int CheckAttackColliders()    // возвращает 0 если первый включен, 1 если включен второй, 2 если отключены оба
    {
        if (melee_attack_colider.enabled == true)
        {
            return 0;
        }
        if (melee_attack_colider2.enabled == true)
        {
            return 1;
        }
        return 2;
    }

    public override IEnumerator TakeDamage()
    {
        MyAnimator.SetTrigger("take damage");
        health -= 15;
        yield return null;
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy mushroom")
        {
            StartCoroutine(TakeDamage());

            MyRigidbody2D.velocity = Vector2.zero;


            if (other.GetComponentInParent<Enemy>().IsFacingRight)
            {
                reboundAtDamage = new Vector2(xBound, yBound);

            }
            else
            {
                reboundAtDamage = new Vector2(-xBound, yBound);

            }

            MyRigidbody2D.AddForce(reboundAtDamage);

        }

        if (other.tag == "Check point")
        {

            if (CheckPoint != null)
            {
                string nameOfThis = other.gameObject.name;    //получаем номер чекпоинта от соприкосновения
                nameOfThis = nameOfThis.Substring(13);
                int NumberOfThis = int.Parse(nameOfThis);

                string nameOfCurrent = CheckPoint.name;        //получаем номер текущего чекпоинта
                nameOfCurrent = nameOfCurrent.Substring(13);
                int NumberOfCurrent = int.Parse(nameOfCurrent);

                if (NumberOfThis > NumberOfCurrent)             // меняем чекпоинт, если дошли до нового
                {
                    CheckPoint = other.gameObject;
                }
            }
            else
            {
                CheckPoint = other.gameObject;
            }
        }

        if (other.tag == "Next level")
        {

            DBmanager.ExecuteQueryWithoutAnswer($"UPDATE Player SET healh=" + health);
            SceneManager.LoadScene("level-3", LoadSceneMode.Single);
        }

        if (other.tag == "Dead line")
        {
            health = 0;
        }
    }

    public override void Death()
    {
        health = 30;
        transform.position = CheckPoint.transform.position;
        MyRigidbody2D.velocity = Vector2.zero;
        MyRigidbody2D.AddForce(Vector2.zero);

    }
}