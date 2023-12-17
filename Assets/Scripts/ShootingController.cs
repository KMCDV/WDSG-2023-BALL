using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float cameraAngleY = Camera.main.transform.eulerAngles.y;
            Quaternion direction = Quaternion.Euler(0, cameraAngleY, 0);
            Instantiate(bulletPrefab, transform.position, direction);
        }
        
    }
}
