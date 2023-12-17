using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
