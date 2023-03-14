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
    private TextMeshProUGUI countText;
    private TextMeshProUGUI staminaText;
   
    void Start()
    {
        countText = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();
        staminaText = GameObject.Find("StaminaText").GetComponent<TextMeshProUGUI>();
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
