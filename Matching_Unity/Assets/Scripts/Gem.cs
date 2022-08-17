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
    public GameObject destroyEffect;

    public enum GemType { blue, green, red, yellow, purple, bomb};
    public GemType type;
    public bool isMatched;
    public int blastSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        originalGemVectorPos = new Vector3(posIndex.x, posIndex.y);//this.gameObject.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.Lerp(transform.position,posIndex, 3 *Time.deltaTime);
    }

    public void SetupGem(Vector2Int pos, Board theBoard){
        posIndex = pos;
        originalGemVectorPos = new Vector3(posIndex.x, posIndex.y);
        board = theBoard;
    }

    void OnMouseDown(){
        if(board.currentState == Board.BoardState.move && board.roundMan.roundTime > 0){
        isMouseGem = true;
        originalGemPos = posIndex;
        originalGemVectorPos = this.transform.position;
        offSet = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
        transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition)+offSet);
        }
    }

    void OnMouseDrag(){
        if(board.currentState == Board.BoardState.move){
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x,Input.mousePosition.y);
        Vector3 currentPosition = (Camera.main.ScreenToWorldPoint(currentScreenPoint)+offSet);
        transform.position = currentPosition;
        }
    }

    void OnMouseUp(){
        if(board.currentState == Board.BoardState.move){
        isMouseGem = false;
        transform.position = originalGemVectorPos;
        board.currentState = Board.BoardState.wait;
        board.matchFind.FindAllMatches();
        board.matchFind.DestroyMatches();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(isMouseGem == true)
        {
        newGemVectorPos = col.gameObject.GetComponent<Gem>().originalGemVectorPos;
        //col.gameObject.transform.position = originalGemVectorPos;
        StartCoroutine(board.SmoothLerp(.5f,col.gameObject.GetComponent<Gem>(), originalGemVectorPos));
        col.gameObject.GetComponent<Gem>().originalGemVectorPos = originalGemVectorPos;
        originalGemVectorPos = newGemVectorPos;
        SwapBoardIndex(col.GetComponent<Gem>());
        SwapGemPosIndex(col.GetComponent<Gem>());
        SwapGemNames(col.GetComponent<Gem>());
        //Debug.Log(board.allGems[posIndex.x, posIndex.y].GetComponent<Gem>().posIndex.x + " "+ board.allGems[posIndex.x, posIndex.y].GetComponent<Gem>().posIndex.y );
        }
    }

    private void SwapBoardIndex(Gem otherGem){
        Gem tempGem = otherGem;
        board.allGems[posIndex.x, posIndex.y] = otherGem;
        board.allGems[tempGem.posIndex.x, tempGem.posIndex.y] = this.GetComponent<Gem>();
        

    }

    private void SwapGemPosIndex(Gem otherGem){
        Vector2Int tempPosIndex = otherGem.posIndex;
        otherGem.posIndex = posIndex;
        posIndex = tempPosIndex;

    }

    private void SwapGemNames(Gem otherGem){
        string tempName = otherGem.name;
        otherGem.name = this.name;
        this.name = tempName;
    }
}
