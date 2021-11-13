using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
To initiate (spawn) an Enemy, use this code structure:

public class Example : MonoBehavior {

    public GameObject enemy;

    void function() {
        GameObject g = Instantiate(enemy, transform.position, transform.rotate);
        g.GetComponent<BaseEnemy>().setHealth(20);
    }
}

GameObject is the base class of all instances. 

Since GameObject is not a BaseEnemy, you have to retrieve the script,
which is where GetComponent comes in. Once retrieved, you can edit
the attributes or call methods.

It is possible to use Resources.Load("Prefab") but it is not
recommended. If you do decide to do this, you need to make a
Assets/Resources folder (I believe it is like that).

Another way is with GameObject.Find("Name-of-Enemy-Prefab"), but 
requires the Enemy prefab to be loaded within the scene.

Alternatively (Unity's recommended way), you drag BaseEnemy into a variable
that will reference this from the editor. For example, in your spawner script,
add a "public BaseEnemy enemy" attribute. It will show up in the editor,
initially pointing to None. Drag the BaseEnemy prefab into this section.
If you need multiple types of enemies, use an array.
*/

public class BaseEnemy : MonoBehaviour
{

    public float health = 10;
    public float defense = 0;
    public float speed = 1;
    public float attack = 1;
    
    /* Range for enemy to stop following the player.
       Too low value and the enemy will end up inside the player.
       Too high and the enemy will stop too far away. 
       Should be used by the enemy movement script. */
    public float stopRange = 1.2f;

    /* Distance until it stops following player. < 1 for infinite */
    public float vision = -1;
    
    /* Radius to detect player. < 0 for infinite */
    public float detect = 5;
    
    /* If detect is infinite, it will ignore vision.
       If vision is infinite, but detect is not, then enemy will always chase
        once the player entered the detection zone.
    */
    
    /* Change in inspector if necessary */
    public float deathAnimationTimer = 1.1f;

    private Animator _animate;
    private bool _isDead = false;

    /* Gem to drop for loot*/
    public GameObject lootDrop;


    private void Start()
    {
        _animate = GetComponent<Animator>();
    }


    // Call this when the enemy will take damage
    public void Damage(float amount) {
        health -= Math.Max(amount - defense, 0);
        if (health <= 0)
        {
            // Prevent movement from affecting death animation
            _isDead = true;
            speed = 0;
            
            _animate.SetBool("isDead", true);
            _animate.speed = 1;
            
            // Wait for death animation to finish before destroying this
            IEnumerator toExecute = DestroyAfterAnimation(deathAnimationTimer);
            StartCoroutine(toExecute);
        }
    }

    private IEnumerator DestroyAfterAnimation(float deathTime)
    {
        yield return new WaitForSeconds(deathTime);
        KillEnemy();
    }
    
    public void KillEnemy()
    {
        Destroy(gameObject);
        DropLoot();
    }

    public void DropLoot()
    {
        Instantiate(lootDrop, new Vector3(transform.position.x, transform.position.y - 0.25F, transform.position.z), Quaternion.identity);

    }
    
    public void SetHealth(float amount) {
        health = amount;
    }

    public void SetSpeed(float amount) {
        speed = amount;
    }

    public void SetDefense(float amount) {
        defense = amount;
    }

    public void SetAttack(float amount) {
        attack = amount;
    }

    public void SetVision(float amount)
    {
        vision = amount;
    }

    public void SetDetect(float amount)
    {
        detect = amount;
    }

    public void SetAll(float hp, float spd, float atk, float def,
                        float vis, float detct) {
        health = hp;
        speed = spd;
        attack = atk;
        defense = def;
        vision = vis;
        detect = detct;
    }

    public void SetCombat(float hp, float atk, float def)
    {
        health = hp;
        attack = atk;
        defense = def;
    }

    public void SetAggro(float vis, float detct)
    {
        vision = vis;
        detect = detct;
    }

    public bool IsDead()
    {
        return _isDead;
    }
}
