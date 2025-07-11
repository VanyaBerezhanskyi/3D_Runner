using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _basicTile;
    [SerializeField] private Transform _obstacle;
    [SerializeField] private Transform _coin;
    [SerializeField] private int _initSpawnNum = 3;


    private Vector3 _startSpawn = new Vector3(0, 0, -1); // Спавни звідки починаємо спавнити тайли
    private Vector3 _nextTileSpawn;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.SPAWN_TRIGGER_ENTER, SpawnNextTile);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, StopGame);
        Messenger.AddListener(GameEvent.RETRY_BUTTON_PRESSED, RestartGame);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SPAWN_TRIGGER_ENTER, SpawnNextTile);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, StopGame);
        Messenger.RemoveListener(GameEvent.RETRY_BUTTON_PRESSED, RestartGame);
    }

    private void Start()
    {
        _nextTileSpawn = _startSpawn;

        for (int i = 0; i < _initSpawnNum; ++i)
        {
            SpawnNextTile();
        }
    }

    private void SpawnNextTile()
    {
        Transform nextTile = Instantiate(_basicTile, _nextTileSpawn, Quaternion.identity);

        _nextTileSpawn = nextTile.Find("Next Tile Spawn").transform.position;

        SpawnObstacle(nextTile);
        SpawnCoins(nextTile);
    }

    private void SpawnObstacle(Transform tile)
    {
        List<Transform> obstacleSpawns = new List<Transform>();

        foreach (Transform child in tile)
        {
            if (child.CompareTag("Obstacle Spawn"))
            {
                obstacleSpawns.Add(child);
            }
        }

        int randomIndex = Random.Range(0, obstacleSpawns.Count);
        Transform obstacleSpawn = obstacleSpawns[randomIndex];
        Transform newObstacle = Instantiate(_obstacle, obstacleSpawn.position, obstacleSpawn.rotation);

        newObstacle.SetParent(obstacleSpawn);
    }

    private void SpawnCoins(Transform tile)
    {
        List<List<Transform>> moneySpawns = new List<List<Transform>>(); // Оскільки у нас два ряди для монет, то ми використовуємо двохвимірний список
        moneySpawns.Add(new List<Transform>());
        moneySpawns.Add(new List<Transform>());
        int count = 0;

        foreach (Transform child in tile)
        {
            if (child.CompareTag("Money Spawn"))
            {
                count++;

                if (count < 4)
                    moneySpawns[0].Add(child);
                else
                    moneySpawns[1].Add(child);
            }
        }

        for (int i = 0; i < 2; ++i)
        {
            int randomIndex = Random.Range(0, moneySpawns[i].Count);
            Transform moneySpawn = moneySpawns[i][randomIndex];
            Transform newMoney = Instantiate(_coin, moneySpawn.position, moneySpawn.rotation);

            newMoney.SetParent(moneySpawn);
        }
    }

    private void StopGame() => Time.timeScale = 0;

    private void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Gameplay");
    }
}
