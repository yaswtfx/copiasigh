using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimer;
    public int duracao;
    private int duracaorestante;
    
    // Start is called before the first frame update
    void Start()
    {
        RodarCronometro(duracao);
    }

    // Update is called once per frame
    void RodarCronometro(int segundos)
    {
        duracaorestante = segundos;
        StartCoroutine(AtualizandoCronometro());
    }

    IEnumerator AtualizandoCronometro()
    {
        while (duracaorestante >= 0)
        {
            textTimer.text = $"{duracaorestante / 60:00} : {duracaorestante % 60:00}";
            yield return new WaitForSeconds(1f);
            duracaorestante--;
        }
        
        OnEnd();
    }

    private void OnEnd()
    {
        Debug.Log("ACABOU SE NAO MELHOROU MORREU");
    }
}
