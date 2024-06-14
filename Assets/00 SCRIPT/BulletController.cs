using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.y) > CONSTANT.SCREEN_HEIGHT / 2)
        {
            gameObject.SetActive(false);
        }
    }

}
