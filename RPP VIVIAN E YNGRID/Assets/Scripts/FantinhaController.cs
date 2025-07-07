using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class FantinhaController : MonoBehaviour
{
    public float velocidadeVertical;
    public float velocidadeLateral;
    public Rigidbody2D rig;
    private ArduinoReader ardu;
    public TextMeshProUGUI texto_Distancia;
    public Transform posicaoPlayer;
    public GameObject fireObj;
    private float fluxoAtual = 0f;
    private float fluxoAnterior = 0f;
    private float fluxoDoisCiclosAtras = 0f;
    private bool podeSubir = false;
    private float posicaoInicialY;  // Armazenar a posição inicial Y
    private float distanciaSubida;
    
    void Start()
    {
        ardu = GetComponent<ArduinoReader>();
        rig = GetComponent<Rigidbody2D>();
        posicaoPlayer = GetComponent<Transform>();
        texto_Distancia.text = "Distancia: 0";
        
        posicaoInicialY = posicaoPlayer.position.y;
        distanciaSubida = 0f;
        if (ardu == null)
        {
            Debug.LogError("ArduinoReader não encontrado! Certifique-se de que o script ArduinoReader está anexado ao mesmo GameObject.");
        }
    }

   
    void Update()
    {
        Movimento();
        AtualizarDistancia();
    }

    void Movimento()
    {
        float fluxoRecebido = ArduinoReader.fluxo;
        // Atualiza os valores para o próximo ciclo
        fluxoDoisCiclosAtras = fluxoAnterior;
        fluxoAnterior = fluxoAtual;
        fluxoAtual = fluxoRecebido;
        
        Debug.Log("DIR: " + ArduinoReader.dir);

        
        if (ArduinoReader.dir > 600)
        {
            rig.velocity = new Vector2(1 * velocidadeLateral, rig.velocity.y);
        }
        if (ArduinoReader.dir < 400)
        {
            rig.velocity = new Vector2(-1 * velocidadeLateral, rig.velocity.y);
        }

        
        if (fluxoAtual > fluxoAnterior)
        {
            podeSubir = true; // Permite subir
        }
        else if (fluxoAtual < fluxoAnterior)
        {
            podeSubir = false; // Bloqueia a subida
        }

        // Aplica o movimento se podeSubir for verdadeiro
        if (podeSubir)
        {
            fireObj.SetActive(true);
            rig.velocity = new Vector2(rig.velocity.x, 1 * velocidadeVertical);
        }
        else
        {
            fireObj.SetActive(false);
            rig.velocity = new Vector2(rig.velocity.x, Mathf.Max(rig.velocity.y, 0)); // Evita "pulso" de queda
        }
        
    }
    
    void AtualizarDistancia()
    {
        
        float distanciaVertical = posicaoPlayer.position.y - posicaoInicialY;
        
        // Se a posição Y aumentou, ou seja, o jogador subiu, somamos à distância total
        if (distanciaVertical > 0)
        {
            distanciaSubida = distanciaVertical;
        }

        // Atualiza o texto com a distância subida (formato com 2 casas decimais)
        texto_Distancia.text = $"Distancia: {distanciaSubida:F2}";  // F2 exibe 2 casas decimais
    }  
}

