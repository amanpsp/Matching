using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
   // public TMP_Text sc;
    // Start is called before the first frame update
    void Start()
    {
        timeText= transform.Find("Time Remain Value").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }
}
