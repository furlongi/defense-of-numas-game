using System;
using UnityEngine;


public class BaseAttack : MonoBehaviour
{
    public float attackSpeed = 1f;
    
    private float _passedTime = 0f;
    
    private BaseEnemy _enemy;
    private EnemyChase _chase;

    private void Start()
    {
        _enemy = GetComponent<BaseEnemy>();
        _chase = GetComponent<EnemyChase>();
        
    }

    private void FixedUpdate()
    {
        _passedTime += Time.deltaTime;
        if (_passedTime > attackSpeed)
        {
            _passedTime = 0;
            if (_chase.IsNear())
            {
                Debug.Log("Damage!");
            }
        }

    }
}
