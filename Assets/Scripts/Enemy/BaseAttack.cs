using System;
using UnityEngine;


public class BaseAttack : MonoBehaviour
{
    public float attackSpeed = 5f;
    public float attackRange = 3.7f;

    private float _passedTime = 0f;
    
    private BaseEnemy _enemy;
    private EnemyChasev2 _chase;
    private Player _player;
    private Animator _ani;

    private void Start()
    {
        _enemy = GetComponent<BaseEnemy>();
        _chase = GetComponent<EnemyChasev2>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        /* The enemy will do a melee attack at [attackSpeed] intervals.
         * _passedTime will keep track of the amount of time passed
         * until the enemy will inflict damage.
         */
        _passedTime += Time.deltaTime;
        if (!_enemy.IsDead() && _chase.DistanceToPlayer() <= attackRange)
        {
            if (_passedTime > attackSpeed)
            {
                _passedTime = 0;
                _ani.SetTrigger("isAttacking");
                _player.Damage(_enemy.attack);
            }
        }
    }
}
