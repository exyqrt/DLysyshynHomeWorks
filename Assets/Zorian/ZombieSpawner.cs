using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Не забудьте добавить этот импорт

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;        // Префаб зомби
    public Transform playerTransform;      // Ссылка на игрока
    public float spawnRadius = 35f;        // Радиус спауна зомби
    public float spawnInterval = 5f;       // Интервал между спаунами
    public int totalZombies = 51;          // Общее количество зомби
    public GameObject winText;             // Текст победы
    public TextMeshProUGUI killCounterText;  // TextMeshPro элемент для счетчика убийств

    private float nextSpawnTime;           // Время следующего спауна
    private int zombiesSpawned = 0;        // Количество заспауненных зомби
    private int zombiesKilled = 0;         // Количество убитых зомби
    private bool gameEnded = false;        // Флаг окончания игры

    private void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        winText.SetActive(false); // Скрыть текст победы в начале игры
        UpdateKillCounter();
    }

    private void Update()
    {
        if (!gameEnded && zombiesSpawned < totalZombies && Time.time >= nextSpawnTime)
        {
            SpawnZombie();
            zombiesSpawned++;
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnZombie()
    {
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        randomOffset.y = 0f;
        Vector3 spawnPosition = playerTransform.position + randomOffset;
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Зомби заспаунен в позиции: " + spawnPosition);
    }

    public void ZombieKilled()
    {
        zombiesKilled++;
        UpdateKillCounter();
        if (zombiesKilled >= totalZombies)
        {
            EndGame();
        }
    }

    private void UpdateKillCounter()
    {
        if (killCounterText != null)
        {
            killCounterText.text = zombiesKilled.ToString() + "/" + totalZombies.ToString();
        }
    }

    private void EndGame()
    {
        Debug.Log("Вы убили всех зомби! Конец игры.");
        winText.SetActive(true); // Показать текст победы
        Time.timeScale = 0f; // Остановить время
        gameEnded = true;
    }

    public void RestartGame()
    {
        zombiesSpawned = 0;
        zombiesKilled = 0;
        gameEnded = false;
        nextSpawnTime = Time.time + spawnInterval;
        winText.SetActive(false); // Скрыть текст победы в начале новой игры
        Time.timeScale = 1f; // Возобновить время
        UpdateKillCounter();
    }
}


