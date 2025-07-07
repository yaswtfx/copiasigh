using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platformPrefab; // Arraste o prefab da plataforma aqui no Inspector
    public float distanceY = 5f; // Distância fixa no eixo Y
    public int numberOfPlatforms = 50; // Número total de plataformas a serem criadas
    public float xMin = -10f; // Limite mínimo para a posição X
    public float xMax = 10f; // Limite máximo para a posição X

    void Start()
    {
        SpawnPlatforms();
    }

    void SpawnPlatforms()
    {
        float currentY = 0f; // Posição inicial no eixo Y

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            float randomX = Random.Range(xMin, xMax);
            Vector2 spawnPosition = new Vector2(randomX, currentY);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            currentY += distanceY; // Move a posição Y para a próxima plataforma
        }
    }
}
