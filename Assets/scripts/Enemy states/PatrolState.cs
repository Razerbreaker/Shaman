using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;



    private float patrolTimer;
    private float patrolDuration = 8;


    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }


    public void Execute()
    {

        Patrol();
        enemy.Move();


        if (enemy.Target != null)
        {
            enemy.ChangeState(new InRangeState());
        }

    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Edge" && enemy.Target==null)
        {
            enemy.Flip();
        }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;


        if (patrolTimer > patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }

    }


}