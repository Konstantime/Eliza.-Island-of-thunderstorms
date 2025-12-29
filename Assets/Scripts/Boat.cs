using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private GameObject ButtonFinishGame;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonFinishGame.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonFinishGame.SetActive(false);
        }
    }
}
