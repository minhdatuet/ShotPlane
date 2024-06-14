using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesController : MonoBehaviour 
{
    private List<Vector3> squareSquadPos = new List<Vector3>();
    private List<Vector3> diamondSquadPos = new List<Vector3>();
    private List<Vector3> triangleSquadPos = new List<Vector3>();
    private List<Vector3> rectangleSquadPos = new List<Vector3>();
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject parentEnemies;
    private List<Vector3> currentFormation;
    private float formationChangeInterval = 5f;
    private int formationIndex = 0;

    void Start()
    {
        Vector3 startPos = new Vector3(0, 6, 0);
        parentEnemies = GameObject.Find("EnemyList");
        InitPos();
        for (int i = 0; i < CONSTANT.ENEMIES_QUANTITY; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, startPos, Quaternion.identity, parentEnemies.transform);
            enemies.Add(enemy);
        }
        currentFormation = squareSquadPos;
        StartCoroutine(MoveThroughFormations());
    }


    private void InitPos()
    {
        // Khởi tạo tọa độ cho đội hình hình vuông (4x4)
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                squareSquadPos.Add(new Vector3(i - 1.5f, j + 1.0f, 0));
            }
        }

        // Khởi tạo tọa độ cho đội hình hình thoi
        diamondSquadPos.Add(new Vector3(0, 4, 0)); // Hàng 1
        diamondSquadPos.Add(new Vector3(-1.5f, 3, 0)); // Hàng 2
        diamondSquadPos.Add(new Vector3(-0.5f, 3, 0));
        diamondSquadPos.Add(new Vector3(0.5f, 3, 0));
        diamondSquadPos.Add(new Vector3(1.5f, 3, 0));
        diamondSquadPos.Add(new Vector3(-2.5f, 2, 0)); // Hàng 3
        diamondSquadPos.Add(new Vector3(-1.5f, 2, 0));
        diamondSquadPos.Add(new Vector3(-0.5f, 2, 0));
        diamondSquadPos.Add(new Vector3(0.5f, 2, 0));
        diamondSquadPos.Add(new Vector3(1.5f, 2, 0));
        diamondSquadPos.Add(new Vector3(2.5f, 2, 0));
        diamondSquadPos.Add(new Vector3(-1.5f, 1, 0)); // Hàng 4
        diamondSquadPos.Add(new Vector3(-0.5f, 1, 0));
        diamondSquadPos.Add(new Vector3(0.5f, 1, 0));
        diamondSquadPos.Add(new Vector3(1.5f, 1, 0));
        diamondSquadPos.Add(new Vector3(0, 0, 0)); // Hàng 5

        // Khởi tạo tọa độ cho đội hình hình tam giác
        triangleSquadPos.Add(new Vector3(0, 3.5f, 0)); // Hàng 1
        triangleSquadPos.Add(new Vector3(-0.6f, 3, 0)); // Hàng 2
        triangleSquadPos.Add(new Vector3(0.6f, 3, 0));
        triangleSquadPos.Add(new Vector3(-1.2f, 2.5f, 0)); // Hàng 3
        triangleSquadPos.Add(new Vector3(1.2f, 2.5f, 0));
        triangleSquadPos.Add(new Vector3(-1.8f, 2, 0)); // Hàng 4
        triangleSquadPos.Add(new Vector3(1.8f, 2, 0));
        triangleSquadPos.Add(new Vector3(-2.4f, 1.5f, 0)); // Hàng 5
        triangleSquadPos.Add(new Vector3(-1.8f, 1.5f, 0));
        triangleSquadPos.Add(new Vector3(-1.2f, 1.5f, 0));
        triangleSquadPos.Add(new Vector3(-0.6f, 1.5f, 0));
        triangleSquadPos.Add(new Vector3(0, 1.5f, 0));
        triangleSquadPos.Add(new Vector3(0.6f, 1.5f, 0));
        triangleSquadPos.Add(new Vector3(1.2f, 1.5f, 0));
        triangleSquadPos.Add(new Vector3(1.8f, 1.5f, 0));
        triangleSquadPos.Add(new Vector3(2.4f, 1.5f, 0));

        // Khởi tạo tọa độ cho đội hình hình chữ nhật
        rectangleSquadPos.Add(new Vector3(-2.4f, 4, 0)); // Hàng 1
        rectangleSquadPos.Add(new Vector3(-1.6f, 4, 0));
        rectangleSquadPos.Add(new Vector3(-0.8f, 4, 0));
        rectangleSquadPos.Add(new Vector3(0, 4, 0));
        rectangleSquadPos.Add(new Vector3(0.8f, 4, 0));
        rectangleSquadPos.Add(new Vector3(1.6f, 4, 0));
        rectangleSquadPos.Add(new Vector3(2.4f, 4, 0));
        rectangleSquadPos.Add(new Vector3(-2.5f, 3, 0)); // Hàng 2
        rectangleSquadPos.Add(new Vector3(2.5f, 3, 0));
        rectangleSquadPos.Add(new Vector3(-2.4f, 2, 0)); // Hàng 3
        rectangleSquadPos.Add(new Vector3(-1.6f, 2, 0));
        rectangleSquadPos.Add(new Vector3(-0.8f, 2, 0));
        rectangleSquadPos.Add(new Vector3(0, 2, 0));
        rectangleSquadPos.Add(new Vector3(0.8f, 2, 0));
        rectangleSquadPos.Add(new Vector3(1.6f, 2, 0));
        rectangleSquadPos.Add(new Vector3(2.4f, 2, 0));
    }

    IEnumerator MoveThroughFormations()
    {
        List<List<Vector3>> formations = new List<List<Vector3>> { squareSquadPos, diamondSquadPos, triangleSquadPos, rectangleSquadPos };

        while (true)
        {
            currentFormation = formations[formationIndex];
            formationIndex = (formationIndex + 1) % formations.Count;
            yield return new WaitForSeconds(formationChangeInterval);
        }
    }

    void MoveToFormation()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i])
            {
                enemies[i].transform.position = Vector3.MoveTowards(enemies[i].transform.position, currentFormation[i], speed * Time.deltaTime);
            }
            
        }
    }

    void Update()
    {
        MoveToFormation();
    }
}
