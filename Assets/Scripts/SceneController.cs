using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int enemyCount = 3;
    public int iguanaCount = 9;

    [SerializeField] private UIController ui;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject iguanaPrefab;
    private List<GameObject> enemies = new List<GameObject>();

    private List<GameObject> iguanas = new List<GameObject>();
    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private Vector3 altSpawnPoint = new Vector3(0, 0, -5);

    private void Awake()
    {
        Messenger.AddListener(GameEvents.PLAYER_DEAD, OnPlayerDead);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.PLAYER_DEAD, OnPlayerDead);
    }

    private void OnPlayerDead()
    {
        ui.ShowGameOverPopup();
    }


    // Update is called once per frame
    void Update()
    {
        int cleanIndex = -1;
        foreach (var enemy in enemies)
        {
            if (enemy == null)
            {
                cleanIndex = enemies.IndexOf(enemy);
            }
        }

        if (cleanIndex > -1)
        {
            enemies.RemoveAt(cleanIndex);
            cleanIndex = -1;
        }
        if (enemies.Count <= enemyCount)
        {
            GameObject enemyInst = Instantiate(enemyPrefab) as GameObject;
            enemyInst.transform.position = spawnPoint;
            float angle = Random.Range(0, 360);
            enemyInst.transform.Rotate(0, angle, 0);
            enemies.Add(enemyInst);
        }

        if (iguanas.Count <= iguanaCount)
        {
            GameObject iguanaInst = Instantiate(iguanaPrefab) as GameObject;
            iguanaInst.transform.position = altSpawnPoint;
            float angle = Random.Range(0, 360);
            iguanaInst.transform.Rotate(0, angle, 0);
            iguanas.Add(iguanaInst);
        }
    }
}
