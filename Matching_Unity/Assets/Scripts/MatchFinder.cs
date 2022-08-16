using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    private Board board;
    public List<Gem> currentMatches = new List<Gem>();

    private void Awake(){
        board = FindObjectOfType<Board>();
    } //Awake() gets called anytime an object is activated, better than start() here as awake() is called before start()

    public void FindAllMatches(){
        
        for( int x=0; x<board.width;x++)
        {
            for(int y=0; y<board.height;y++)
            {
                Gem currentGem = board.allGems[x,y];
                CheckHorizontalMatch(currentGem,x,y);
                CheckVerticalMatch(currentGem,x,y);
            }
        }
        if(currentMatches.Count > 0){
            currentMatches = currentMatches.Distinct().ToList();
        }
    }

    private void CheckHorizontalMatch(Gem currentGem, int x, int y){
                if(currentGem!=null&&x>0&&x<board.width-1){
                    Gem leftGem = board.allGems[x-1,y];
                    Gem rightGem = board.allGems[x+1,y];
                    if(leftGem!=null&&rightGem!=null){
                        if(leftGem.type == currentGem.type && rightGem.type == currentGem.type){
                            currentGem.isMatched = true;
                            leftGem.isMatched = true;
                            rightGem.isMatched = true; 
                            currentMatches.Add(currentGem);
                            currentMatches.Add(leftGem);
                            currentMatches.Add(rightGem);
                        }
                    }
                }
    }

    private void CheckVerticalMatch(Gem currentGem, int x, int y){
                if(currentGem!=null&&y>0&&y<board.height-1){
                    Gem downGem = board.allGems[x,y-1];
                    Gem upGem = board.allGems[x,y+1];
                    if(upGem!=null&&downGem!=null){
                        if(upGem.type == currentGem.type && downGem.type == currentGem.type){
                            currentGem.isMatched = true;
                            upGem.isMatched = true;
                            downGem.isMatched = true; 
                            currentMatches.Add(currentGem);
                            currentMatches.Add(upGem);
                            currentMatches.Add(downGem);
                        }
                    }
                }
    }

     public void DestroyMatches(){
        for(int i = 0; i<currentMatches.Count; i++){
            if(currentMatches[i] != null){
                DestroyMatchedGemAt(currentMatches[i].posIndex);
                
            }
        }
        currentMatches.Clear();
        StartCoroutine(DecreaseRowCo());

    }

    private IEnumerator DecreaseRowCo(){
        yield return new WaitForSeconds(.2f);

        MoveGemsDownAfterDestruction();
        StartCoroutine (FillBoardCo());
        
        
    }
    

    private void MoveGemsDownAfterDestruction(){
        int nullCounter =0;

        for( int x=0; x<board.width;x++)
        {
            for(int y=0; y<board.height;y++)
            {
                if(board.allGems[x,y]==null){
                    nullCounter++;
                } else if(nullCounter>0){
                    Vector3 posUpdate = new Vector3(0,nullCounter);
                    board.allGems[x,y].posIndex.y -= nullCounter;
                    board.allGems[x,y].originalGemVectorPos -= posUpdate;
                    Vector3 targetPos = board.allGems[x,y].transform.position;
                    targetPos -= posUpdate;
                    StartCoroutine(board.SmoothLerp(.5f,board.allGems[x,y],targetPos));
                    //board.allGems[x,y].transform.position -= posUpdate;
                    board.allGems[x,y-nullCounter] = board.allGems[x,y];
                    board.allGems[x,y] = null;
                    
                }                 
                
            }
            nullCounter = 0;
        }
    }

    private void DestroyMatchedGemAt(Vector2Int pos){
        if(board.allGems[pos.x,pos.y] != null){
            if(board.allGems[pos.x,pos.y].isMatched){
                Destroy(board.allGems[pos.x,pos.y].gameObject);
                board.allGems[pos.x,pos.y] = null;
            }
        }
    }

    private IEnumerator FillBoardCo(){
        yield return new WaitForSeconds(.5f);
        RefillBoard();

        yield return new WaitForSeconds(.5f);

        FindAllMatches();

        if(currentMatches.Count > 0){
            yield return new WaitForSeconds(1.5f);
            DestroyMatches();
        }else{
        yield return new WaitForSeconds(.5f);
        board.currentState = Board.BoardState.move;
        }
    }

    private void RefillBoard(){
        for( int x=0; x<board.width;x++)
        {
            for(int y=0; y<board.height;y++)
            {
                if(board.allGems[x,y]==null){
                int gemToUse = Random.Range(0, board.gems.Length);
                board.SpawnGem(new Vector2Int(x,y), board.gems[gemToUse]);
                }
            }
        }
        CheckMisplacedGems();
    }

    private void CheckMisplacedGems(){
        List<Gem> foundGems = new List<Gem>();
        foundGems.AddRange(FindObjectsOfType<Gem>());
        for(int x = 0; x<board.width;x++){
            for(int y = 0; y<board.height; y++){
                if(foundGems.Contains(board.allGems[x,y])){
                    foundGems.Remove(board.allGems[x,y]);
                }
            }
        }
        foreach(Gem g in foundGems){
            Destroy(g.gameObject);
        }


    }

}
