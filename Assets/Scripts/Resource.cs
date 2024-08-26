using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceType nameResource;
    public void DestroyYourself()
    {
        Destroy(gameObject);
    }
}
