using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    /*public GameObject platformPrefab; // Arraste o prefab da plataforma aqui no Inspector
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
    }*/

    public GameObject platformPrefab;
    public GameObject collectiblePrefab;
    public Transform player;
    public int poolSize = 10;              // Quantas plataformas na pool
    public float distanceY = 5f;          // Distância vertical entre plataformas
    public float xMin = -10f;
    public float xMax = 10f;
    public int initialPlatforms = 5;      // Quantas já criadas no início
    
    private List<GameObject> platformPool = new List<GameObject>();
    private List<GameObject> collectiblePool = new List<GameObject>();
    private float highestY;               // Y da plataforma mais alta

    void Start()
    {
        // Cria pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject plat = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
            plat.SetActive(false);
            platformPool.Add(plat);
        }

        highestY = 0f;
        
        // Cria pool de coletáveis
        for (int i = 0; i < poolSize; i++)
        {
            GameObject col = Instantiate(collectiblePrefab, Vector3.zero, Quaternion.identity);
            col.SetActive(false);
            collectiblePool.Add(col);
        }
    }

    void Update()
    {
        // Se o jogador está chegando perto da plataforma mais alta, gera outra
        if (player.position.y + (distanceY * 3) > highestY)
        {
            SpawnPlatformAt(highestY);
            highestY += distanceY;
        }
    }

    void SpawnPlatformAt(float y)
    {
        GameObject plat = GetPooledPlatform();
        float randomX = Random.Range(xMin, xMax);
        plat.transform.position = new Vector2(randomX, y);
        plat.SetActive(true);
        
        GameObject plat1 = GetPooledPlatform();
        float randomX1 = Random.Range(xMin, xMax);
        plat.transform.position = new Vector2(randomX, y);
        plat.SetActive(true);

        // Chance de spawnar coletável
        if (Random.value < 0.8f) // 80% de chance
        {
            GameObject coletavel = GetPooledCollectible();
            coletavel.transform.position = new Vector2(randomX, y + 1.5f); // 2 unidades acima da plataforma
            coletavel.SetActive(true);
        }
    }

    GameObject GetPooledPlatform()
    {
        foreach (GameObject plat in platformPool)
        {
            if (!plat.activeInHierarchy)
                return plat;
        }

        // Se não tiver nenhuma disponível, aumenta a pool
        GameObject extraPlat = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
        extraPlat.SetActive(false);
        platformPool.Add(extraPlat);
        return extraPlat;
    }
    
    GameObject GetPooledCollectible()
    {
        foreach (GameObject col in collectiblePool)
        {
            if (!col.activeInHierarchy)
                return col;
        }

        // se esgotar, cria mais
        GameObject extraCol = Instantiate(collectiblePrefab, Vector3.zero, Quaternion.identity);
        extraCol.SetActive(false);
        collectiblePool.Add(extraCol);
        return extraCol;
    }
}
