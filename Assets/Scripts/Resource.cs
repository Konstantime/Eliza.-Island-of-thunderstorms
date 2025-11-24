using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourcesType nameResource;
    public void DestroyYourself()
    {
        Destroy(gameObject);
    }
}
