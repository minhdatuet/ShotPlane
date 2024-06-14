using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Singleton<BulletPool>
{

    [SerializeField] GameObject bulletPrefab;
    List<GameObject> bullets = new List<GameObject>();
    GameObject parentBullet;

    public GameObject GetObject()
    {
        foreach (GameObject b in bullets)
        {
            if (b.activeSelf) continue;
            return b;
        }

        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity, parentBullet.transform);
        bullets.Add(bullet);
        bullet.SetActive(false);
        return bullet;
    }

    void Start()
    {
        parentBullet = GameObject.Find("BulletList");
    }

}
