using UnityEngine;


public class ExitPopUp : MonoBehaviour
{

    public HubManager manager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPart"))
        {
            manager.ShouldExitPrompt();
        }
    }
}
