using Unity.VisualScripting;
using UnityEngine;

public class Aim : MonoBehaviour
{
    
    [SerializeField] private Joystick joystick;
    [SerializeField] private Vector3 offset;
    private Vector2 moveInput;
    private Renderer myRenderer;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRenderer = gameObject.GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        moveInput = joystick.Direction.normalized;
        if( moveInput == Vector2.zero )
        {
            myRenderer.enabled = false;
            return;
        }
        myRenderer.enabled = true;
        transform.position = new Vector3 (player.transform.position.x + moveInput.x + offset.x, 
                                            player.transform.position.y + moveInput.y + offset.y);
    }
}
