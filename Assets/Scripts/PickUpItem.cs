using UnityEngine;


public class PickUpItem : MonoBehaviour
{
    public int collectedKeys = 0;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.StartsWith("Key"))
        {
            Destroy(other.gameObject);
            collectedKeys += 1;
            print(collectedKeys);
        }
    }
}
