using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public float fireRate = 0.1f;
    private float nextFire = 0f;

    void Update()
    {
        Move();
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        float halfWidth = CONSTANT.SCREEN_WIDTH / 2;
        float halfHeight = CONSTANT.SCREEN_HEIGHT / 2;
        float playerWidth = this.GetComponent<Renderer>().bounds.size.x;
        float playerHeight = this.GetComponent<Renderer>().bounds.size.y;

        //Giới hạn để nhân vật không đi ra ngoài màn hình
        newPosition.x = Mathf.Clamp(newPosition.x, -halfWidth + playerWidth/4, halfWidth - playerWidth/4);
        newPosition.y = Mathf.Clamp(newPosition.y, -halfHeight + playerHeight/2, halfHeight - playerHeight/2);

        transform.position = newPosition;
    }

    void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetObject();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true);
    }
}
