using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{

    public bool isMouseGem;
    private Vector3 offSet;
    //[HideInInspector]
    public Vector3 originalGemVectorPos;
    //[HideInInspector]
    public Vector3 newGemVectorPos;
    [HideInInspector]
    public Vector2Int originalGemPos;
    [HideInInspector]
    public Vector2Int newGemPos;
    [HideInInspector]
    public Vector2Int posIndex;
    [HideInInspector]
    public Board board;

    // Start is called before the first frame update
    void Start()
    {
        originalGemVectorPos = this.gameObject.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupGem(Vector2Int pos, Board theBoard){
        posIndex = pos;
        board = theBoard;
    }

    void OnMouseDown(){
        isMouseGem = true;
        originalGemPos = posIndex;
        originalGemVectorPos = this.transform.position;
        offSet = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
        transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition)+offSet);
    }

    void OnMouseDrag(){
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x,Input.mousePosition.y);
        Vector3 currentPosition = (Camera.main.ScreenToWorldPoint(currentScreenPoint)+offSet);
        transform.position = currentPosition;
    }

    void OnMouseUp(){
        isMouseGem = false;
        transform.position = originalGemVectorPos;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(isMouseGem == true)
        {
        newGemVectorPos = col.gameObject.GetComponent<Gem>().originalGemVectorPos;
        col.gameObject.transform.position = originalGemVectorPos;
        col.gameObject.GetComponent<Gem>().originalGemVectorPos = originalGemVectorPos;
        originalGemVectorPos = newGemVectorPos;
        }
    }
}
