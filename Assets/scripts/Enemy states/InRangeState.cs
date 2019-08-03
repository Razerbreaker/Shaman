using UnityEngine;

public class InRangeState : IEnemyState
{
    float jumpAttackTimer = 1.5f;
    float jumpAttackCooldown = 2;
    private Enemy enemy;


    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
        Attack();
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {


    }


    private void Attack()
    {
        //jumpAttackTimer += Time.deltaTime;
        float whenHeroLeftSide = 0;
        float whenHeroRightSide = 0;

        if (enemy.Target != null) {
            
         whenHeroLeftSide = enemy.transform.position.x - enemy.Target.transform.position.x;
         whenHeroRightSide = enemy.Target.transform.position.x - enemy.transform.position.x;
            
        }

        if (enemy.IsFacingRight && (whenHeroRightSide <= 5.5 && whenHeroRightSide >= 5))
        {
            //Debug.Log(whenHeroRightSide);
            enemy.canAttack = true;

        }
        else if (!enemy.IsFacingRight && (whenHeroLeftSide <= 5.5 && whenHeroLeftSide >= 5))
        {
            //Debug.Log(whenHeroLeftSide);
            enemy.canAttack = true;


        }
        //if (jumpAttackTimer >= jumpAttackCooldown)
        //{
        //    enemy.canAttack = true;
        //    jumpAttackTimer = 0;
        //}

        if (enemy.canAttack && enemy.isGrounded)
        {
            enemy.canAttack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}
