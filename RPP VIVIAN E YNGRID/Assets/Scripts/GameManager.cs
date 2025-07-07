using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI textoPontuacao;
    private int pontos = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AdicionarPontos(int valor)
    {
        pontos += valor;
        textoPontuacao.text = "Pontos: " + pontos;
    }
}
