using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{
    
    public static ArduinoReader Instance { get; private set; }
    private SerialPort sp;
    public string ssss;
    public static float fluxo;
    public static float dir = 500;
    public static int botao;

    void Awake()
    {
        // Garantir que só existe uma instância de ArduinoReader
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // Se já houver uma instância, destruir esta
        }
    }
    void Start()
    {
        sp = new SerialPort("COM3", 9600);
        try
        {
            sp.Open(); // Tente abrir a porta
            Debug.Log("Porta aberta com sucesso.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Erro ao abrir a porta: {e.Message}");
        }

        sp.ReadTimeout = 100; // Define um tempo limite de leitura
    }

    public void Update() {
            if (sp.IsOpen) {
                try {
                    string data = sp.ReadLine(); // Lê uma linha de dados
                    string[] values = data.Split(','); // Divide os valores
                    Debug.Log($"{values.Length}");
                    Debug.Log($"Dados recebidos: {data}");

                    if (values.Length >= 2) { 
                        fluxo = float.Parse(values[0]) / 100; // Converte o primeiro valor
                        dir = float.Parse(values[1]); // Converte o segundo valor
                        botao = int.Parse(values[2]);

                        // Aqui você pode usar os valores como quiser
                        Debug.Log($"Sensor 1: {fluxo}, Sensor 2: {dir}");
                        Debug.Log($"Sensor 1:");
                    }
                } catch (System.Exception) {
                    // Tratamento de exceções (se necessário)
                }
            }
    }

    
        private void OnApplicationQuit()
        {
            sp.Close(); // Fecha a porta ao sair
        }
    }

