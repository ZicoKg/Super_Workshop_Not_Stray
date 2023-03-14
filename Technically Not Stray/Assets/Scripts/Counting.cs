using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using TMPro;

public class Counting : MonoBehaviour
{
    // For UI
    private int count;
    private int stamina;
    public TextMeshPro countText;
    public TextMeshPro staminaText;
   
    void Start()
    {
        // For UI
        stamina = 100; 
        count = 10;
        setCountText();
        setStaminaText();
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();  
    }

    void setStaminaText()
    {
        staminaText.text = "Stamina: " + stamina.ToString();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Mouse"))
        {
            count--;
            stamina =+ 20;
            setCountText();
            setStaminaText();
            setStaminaText();
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            stamina =- 60; 
            setStaminaText();
        }
    }
}
