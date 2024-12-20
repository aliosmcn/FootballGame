using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kale"))
        {
            Debug.Log("GOL");
        }
    }
}
