using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float roundTime = 60f;
    private UIManager uiMan;

    // Start is called before the first frame update
    void Start()
    {
        uiMan = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(roundTime > 0){
            roundTime -= Time.deltaTime;
            if(roundTime <= 0){
                roundTime = 0;
                //do something or other
            }
        }

        uiMan.timeText.text = roundTime.ToString("0.0")+"s";

    }
}
