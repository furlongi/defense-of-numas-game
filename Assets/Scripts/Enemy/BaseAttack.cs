using System;
using UnityEngine;


public class BaseAttack : MonoBehaviour
{
    public float attackSpeed = 1f;
    
    private float _passedTime = 0f;
    
    private BaseEnemy _enemy;
    private EnemyChase _chase;
    private Player _player;

    private void Start()
    {
        _enemy = GetComponent<BaseEnemy>();
        _chase = GetComponent<EnemyChase>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        /* The enemy will do a melee attack at [attackSpeed] intervals.
         * _passedTime will keep track of the amount of time passed
         * until the enemy will inflict damage.
         */
        _passedTime += Time.deltaTime;
        if (_passedTime > attackSpeed)
        {
            _passedTime = 0;
            if (_chase.IsNear())
            {
                _player.Damage(_enemy.attack);
            }
        }
        
    }
}
