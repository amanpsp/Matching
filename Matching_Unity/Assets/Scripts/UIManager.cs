using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI somethingText;
   // public TMP_Text sc;
    public GameObject roundOverScreen;
   
    

    void Awake()//used to be Start
    {
        timeText= transform.Find("Time Remain Value").GetComponent<TextMeshProUGUI>();
        somethingText = transform.Find("Place Holder Value").GetComponent<TextMeshProUGUI>();
        roundOverScreen = transform.Find("Round Over Panel").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //
    }
}
