using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class FantinhaControllerKeyboard : MonoBehaviour
{
    public float velocidadeVertical;
    public float velocidadeLateral;
    public Rigidbody2D rig;
    public TextMeshProUGUI texto_Distancia;
    public Transform posicaoPlayer;
    public GameObject fireObj;

    private float fluxoAtual = 0f;
    private float fluxoAnterior = 0f;
    private bool podeSubir = false;
    private float posicaoInicialY;
    private float distanciaSubida;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        posicaoPlayer = GetComponent<Transform>();
        texto_Distancia.text = "Distancia: 0";

        posicaoInicialY = posicaoPlayer.position.y;
        distanciaSubida = 0f;
    }

    void Update()
    {
        Movimento();
        AtualizarDistancia();
    }

    void Movimento()
    {
        // --- Lê teclado ---
        bool teclaSubir = Input.GetKey(KeyCode.Space);  // fluxo simulando subir
        bool teclaDireita = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool teclaEsquerda = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        // --- Movimento lateral ---
        if (teclaDireita)
        {
            rig.velocity = new Vector2(velocidadeLateral, rig.velocity.y);
        }
        else if (teclaEsquerda)
        {
            rig.velocity = new Vector2(-velocidadeLateral, rig.velocity.y);
        }
        else
        {
            rig.velocity = new Vector2(0, rig.velocity.y); // parar horizontalmente se nada for pressionado
        }

        // --- Movimento vertical ---
        fluxoAnterior = fluxoAtual;
        fluxoAtual = teclaSubir ? 1f : 0f;

        if (fluxoAtual > fluxoAnterior)
        {
            podeSubir = true;
        }
        else if (fluxoAtual < fluxoAnterior)
        {
            podeSubir = false;
        }

        if (podeSubir)
        {
            fireObj.SetActive(true);
            rig.velocity = new Vector2(rig.velocity.x, velocidadeVertical);
        }
        else
        {
            fireObj.SetActive(false);
            rig.velocity = new Vector2(rig.velocity.x, Mathf.Max(rig.velocity.y, 0));
        }
    }

    void AtualizarDistancia()
    {
        float distanciaVertical = posicaoPlayer.position.y - posicaoInicialY;

        if (distanciaVertical > 0)
        {
            distanciaSubida = distanciaVertical;
        }

        texto_Distancia.text = $"Distancia: {distanciaSubida:F2}";
    }
}
