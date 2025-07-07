using UnityEngine;

public class Cristais : MonoBehaviour
{
    public int pontos = 10; // quanto vale esse coletável

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Adiciona pontos ao sistema de pontuação
            GameManager.Instance.AdicionarPontos(pontos);

            // Desativa o coletável (ou destrói, mas prefira pooling)
            gameObject.SetActive(false);
        }
    }
}