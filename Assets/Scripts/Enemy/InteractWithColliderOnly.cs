using UnityEngine;

public class InteractWithColliderOnly : MonoBehaviour
{
    public string interactWith;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(interactWith))
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
    }
}