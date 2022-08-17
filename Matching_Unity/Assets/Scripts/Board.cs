using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update

    public int width, height;

    public GameObject bgTilePrefab;
    public Gem[] gems;
    public Gem[,] allGems;
    public MatchFinder matchFind;
    public enum BoardState { wait, move};
    public BoardState currentState = BoardState.move;
    public Gem bomb;
    public float bombChance = 2f;
    public RoundManager roundMan;

    private void Awake(){
        matchFind = FindObjectOfType<MatchFinder>();
    }
    void Start()
    {
        allGems = new Gem[width,height];
        SetUp();
        roundMan = FindObjectOfType<RoundManager>();
    }

    private void Update(){

       // matchFind.FindAllMatches();
       if(Input.GetKeyDown(KeyCode.S)){
        ShuffleBoard();
       }

    }

    private void SetUp()
    {
        for(int x =0 ; x<width;x++){
            for(int y=0;y<height;y++){
                Vector2 pos = new Vector2(x,y);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.name = "BG Tile - " + x +"," + y;

                int gemToUse = Random.Range(0,gems.Length);

                int iterations=0;
                while(MatchesAt(new Vector2Int(x,y), gems[gemToUse])&& iterations < 100){
                    gemToUse = Random.Range(0,gems.Length);
                    iterations++;
                }

                SpawnGem(new Vector2Int(x,y), gems[gemToUse]);
            }
        }
    }

    public void SpawnGem(Vector2Int pos,Gem gemToSpawn){
        if(Random.Range(0f, 100f)<bombChance){
            gemToSpawn = bomb;
        }
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y + height, 0), Quaternion.identity);
        StartCoroutine(SmoothLerp(1f,gem,pos));
        gem.transform.parent = this.transform;
        //gem.transform.position = new Vector3(pos.x, pos.y,0);
        gem.name = "Gem - "+ pos.x +"," + pos.y;
        allGems[pos.x,pos.y]=gem;
        gem.SetupGem(pos,this);
        
    }

    public IEnumerator SmoothLerp(float waitTime,Gem gemStartPosition, Vector2 posTarget){
        float elapsedTime =0;
        while(elapsedTime<waitTime){
            gemStartPosition.transform.position = Vector3.Lerp(gemStartPosition.transform.position, posTarget, (elapsedTime/waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gemStartPosition.transform.position = posTarget;
        yield return null;
        
    }

    private bool MatchesAt(Vector2Int posToCheck, Gem gemToCheck){

        if(posToCheck.x > 1){
            if(allGems[posToCheck.x-1, posToCheck.y].type == gemToCheck.type && allGems[posToCheck.x-2, posToCheck.y].type == gemToCheck.type){
                return true;
            }
        }
        if(posToCheck.y > 1){
            if(allGems[posToCheck.x, posToCheck.y-1].type == gemToCheck.type && allGems[posToCheck.x, posToCheck.y-2].type == gemToCheck.type){
                return true;
            }
        }

        return false;

    }

    public void ShuffleBoard(){
        if(currentState != BoardState.wait){
            currentState = BoardState.wait;
            List<Gem> gemsOnBoard = new List<Gem>();

            for(int x = 0; x<width; x++){
                for(int y = 0; y<height; y++){
                    gemsOnBoard.Add(allGems[x,y]);
                    allGems[x,y] = null;
                }
            }

             for(int x = 0; x<width; x++){
                for(int y = 0; y<height; y++){
                    int gemToUse = Random.Range(0,gemsOnBoard.Count);
                    int iterations = 0;
                     while(iterations < 100 && gemsOnBoard.Count>1 && MatchesAt(new Vector2Int(x,y), gemsOnBoard[gemToUse])){
                        gemToUse = Random.Range(0, gemsOnBoard.Count);
                        iterations++;
                    } 

                    gemsOnBoard[gemToUse].SetupGem(new Vector2Int(x,y), this);
                    allGems[x,y] = gemsOnBoard[gemToUse];
                    StartCoroutine(SmoothLerp(1f,allGems[x,y],new Vector2Int(x,y)));
                    gemsOnBoard.RemoveAt(gemToUse);
                }
            }

        } 
        StartCoroutine(matchFind.FillBoardCo());
    }


}
