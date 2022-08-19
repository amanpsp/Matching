using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    private Board board;
    public List<Gem> currentMatches = new List<Gem>();
    public List<Gem> bombMarks = new List<Gem>();
    public Queue<Gem> floodFill = new Queue<Gem>();

    private void Awake(){
        board = FindObjectOfType<Board>();
    } //Awake() gets called anytime an object is activated, better than start() here as awake() is called before start()

    public void FindAllMatches(){
        
        int matchNumber = 1;
        if(board.currentState = Board.BoardState.wait;){
            for( int x=0; x<board.width;x++)
            {
                for(int y=0; y<board.height;y++)
                {
                    Gem currentGem = board.allGems[x,y];
                    CheckHorizontalMatch(currentGem,x,y,matchNumber);
                     
                    matchNumber++;
                    
                }
            }
            //CheckMatchesAround();
        
        if(currentMatches.Count > 0){
            currentMatches = currentMatches.OrderBy(gem => gem.matchNumber).ToList();
            currentMatches = currentMatches.Distinct().ToList();
            //floodFill.Enqueue(currentMatches[0])
            //FloodCheckMatchNumbers(0);
            //currentMatches = currentMatches.OrderBy(gem => gem.matchNumber).ToList();
        }
        CheckForBombs();
        //StartCoroutine(board.matchFind.DestroyMatches());
        }
    }

    private int FloodCheckMatchNumbers(int currentPosition){
        //int currentPosition = 0;
        while(currentPosition<currentMatches.Count){
            Gem currentGem = currentMatches[currentPosition];
            //check in all 4 directions around the gem if there is a matching type
            //if there is a match, check if matchnumbers are the same, if not then set other gem match number to current gem matchnumber
            if(currentGem.posIndex.x+1 < board.height && currentGem.type == board.allGems[currentGem.posIndex.x+1,currentGem.posIndex.y].type){
                if(currentGem.matchNumber != board.allGems[currentGem.posIndex.x+1,currentGem.posIndex.y].matchNumber){
                    board.allGems[currentGem.posIndex.x+1,currentGem.posIndex.y].matchNumber = currentGem.matchNumber;
                    
                }
            }
            if(currentGem.posIndex.x-1 > 0 && currentGem.type == board.allGems[currentGem.posIndex.x-1,currentGem.posIndex.y].type){
                if(currentGem.matchNumber != board.allGems[currentGem.posIndex.x-1,currentGem.posIndex.y].matchNumber){
                    board.allGems[currentGem.posIndex.x-1,currentGem.posIndex.y].matchNumber = currentGem.matchNumber;
                }
            }
            if(currentGem.posIndex.y-1 > 0 && currentGem.type == board.allGems[currentGem.posIndex.x,currentGem.posIndex.y-1].type){
                if(currentGem.matchNumber != board.allGems[currentGem.posIndex.x,currentGem.posIndex.y-1].matchNumber){
                    board.allGems[currentGem.posIndex.x,currentGem.posIndex.y-1].matchNumber = currentGem.matchNumber;
                }
            }
            if(currentGem.posIndex.y+1 < board.width && currentGem.type == board.allGems[currentGem.posIndex.x,currentGem.posIndex.y+1].type){
                if(currentGem.matchNumber != board.allGems[currentGem.posIndex.x,currentGem.posIndex.y+1].matchNumber){
                    board.allGems[currentGem.posIndex.x,currentGem.posIndex.y+1].matchNumber = currentGem.matchNumber;
                }
            }

            currentPosition = FloodCheckMatchNumbers(currentPosition+1);
        }
        return currentPosition;

    }

    /* private void CheckMatchesAround(){
        int sweepCount = 0;
        Vector2Int start = new Vector2Int(0,0);
        Gem a = board.allGems[start.x, start.y];
        Gem b = null;
        Gem c = null;
        while(start.x<board.width){
            if(start.x + 1 < board.width){ b = board.allGems[start.x+1,start.y];}   
            if(start.x + 2 < board.width){ c = board.allGems[start.x+2, start.y];}              
            while(c!=null){
                if(a.type == b.type && c.type == a.type){
                    a.isMatched = b.isMatched = c.isMatched = true;
                    a.matchNumber = b.matchNumber = c.matchNumber = sweepCount;
                    currentMatches.Add(a);
                } else{
                    if(a.matchNumber == sweepCount){
                        currentMatches.Add(a);
                        if(b.matchNumber != sweepCount){
                            //CreateMatch(matchTiles, Match.Dir.HORIZ);
                            //matchTiles = new List<Tile>(); i dont get this part maybe its adding all the matches to a seperate list and indication the direction

                        }
                    }
                }
                if(start.x+3>=board.width){
                    if(a.matchNumber == sweepCount && b.matchNumber == sweepCount){
                        currentMatches.Add(b);
                        if(c.matchNumber == sweepCount){
                            currentMatches.Add(c);
                        }
                    }
                    if(currentMatches.Count>2){
                        //CreateMatch(matchTiles, Match.Dir.HORIZ);
                            //matchTiles = new List<Tile>(); i dont get this part maybe its adding all the matches to a seperate list and indication the direction 
                    }
                    a=b;
                    b=c;
                    if(b.posIndex.x+1<board.width){c = board.allGems[b.posIndex.x+1, b.posIndex.y];}
                    
                }
                start.x = start.x+1;
                if(start.x<board.width){a = board.allGems[start.x, start.y];}
                
            }
        }
            sweepCount++;

            bool junction = false;
            start.x =0; start.y = 0;
            a = board.allGems[start.x, start.y];
            while(a!=null){
                if(start.y + 1 < board.height){ b = board.allGems[start.x,start.y+1];}   
                if(start.y + 2 < board.height){ c = board.allGems[start.x, start.y+2];}
                while(c!=null){
                    if(a.type == b.type && c.type == a.type){
                    a.isMatched = b.isMatched = c.isMatched = true;
                    a.matchNumber = b.matchNumber = c.matchNumber = sweepCount;
                    currentMatches.Add(a);
                    if(a.isMatched == true){
                        a.junction = junction = true;
                    }
                    else{
                        if(a.matchNumber == sweepCount){
                            currentMatches.Add(a);
                            if(a.isMatched == true){
                                a.junction = junction = true;
                            }
                            if(b.matchNumber != sweepCount){
                                //CreateMatch(matchTiles, Match.Dir.HORIZ);
                                //matchTiles = new List<Tile>(); i dont get this part maybe its adding all the matches to a seperate list and indication the direction 
                                junction = false;
                            }
                        }
                    }
                    if(start.y+3 >=board.height){
                        if(a.matchNumber == sweepCount && b.matchNumber == sweepCount){
                            currentMatches.Add(b);
                            if(b.isMatched == true){
                                b.junction = junction = true;
                            }
                            if(c.matchNumber == sweepCount){
                                currentMatches.Add(c);
                                if(c.isMatched == true){
                                    c.junction = junction = true;
                                }
                            }
                        }
                        if(currentMatches.Count>2){
                            //CreateMatch(matchTiles, Match.Dir.HORIZ);
                            //matchTiles = new List<Tile>(); i dont get this part maybe its adding all the matches to a seperate list and indication the direction     
                            junction = false;
                        }
                    }
                    a=b;
                    b=c;
                    c = board.allGems[b.posIndex.x, b.posIndex.y+1];
                }
               
                start.x = start.x+1; start.y = start.y;
                a = board.allGems[start.x,start.y];
                

            }
        
        }
    } */
       
    
        

    public void CheckForBombs(){
        for(int i = 0; i<currentMatches.Count; i++){
            Gem gem = currentMatches[i];
            int x = gem.posIndex.x;
            int y = gem.posIndex.y;
            
            if(gem.posIndex.x>0){
                if(board.allGems[x-1, y] != null){
                    if(board.allGems[x-1,y].type == Gem.GemType.bomb){
                        MarkBombArea(new Vector2Int(x-1,y), board.allGems[x-1,y]);
                    }
                }
            }
            if(gem.posIndex.x<board.width-1){
                if(board.allGems[x+1, y] != null){
                    if(board.allGems[x+1,y].type == Gem.GemType.bomb){
                        MarkBombArea(new Vector2Int(x+1,y), board.allGems[x+1,y]);
                    }
                }
            }
            if(gem.posIndex.y>0){
                if(board.allGems[x, y-1] != null){
                    if(board.allGems[x,y-1].type == Gem.GemType.bomb){
                        MarkBombArea(new Vector2Int(x,y-1), board.allGems[x,y-1]);
                    }
                }
            }
            if(gem.posIndex.y<board.height-1){
                if(board.allGems[x, y+1] != null){
                    if(board.allGems[x,y+1].type == Gem.GemType.bomb){
                        MarkBombArea(new Vector2Int(x,y+1), board.allGems[x,y+1]);
                    }
                }
            }
        }
        StartCoroutine(board.matchFind.DestroyMatches());
    }

    public void MarkBombArea(Vector2Int bombPos, Gem theBomb){
        for(int x = bombPos.x - theBomb.blastSize; x<=bombPos.x+theBomb.blastSize;x++){
            for(int y = bombPos.y - theBomb.blastSize; y<= bombPos.y + theBomb.blastSize; y++){
                if(x>=0 && x<board.width && y>=0 && y<board.height){
                    if(board.allGems[x,y]!=null){
                        board.allGems[x,y].isMatched = true;
                        //currentMatches.Add(board.allGems[x,y]);
                        bombMarks.Add(board.allGems[x,y]);
                    }
                }
            }
        }

        //currentMatches = currentMatches.Distinct().ToList();
        bombMarks = bombMarks.Distinct().ToList();
    }

    private void CheckHorizontalMatch(Gem currentGem, int x, int y,int matchNumber){
        Gem leftGem = null;
        Gem rightGem = null; 
                if(currentGem!=null&&x<board.width){
                    if(x-1>=0){ leftGem = board.allGems[x-1,y];}
                    
                    if(x+1 < board.width){ rightGem = board.allGems[x+1,y];}
                    
                    
                    if(leftGem!=null&&rightGem!=null){
                        if(leftGem.type == currentGem.type && rightGem.type == currentGem.type){
                            currentGem.isMatched = true;
                            leftGem.isMatched = true;
                            rightGem.isMatched = true; 
                            currentMatches.Add(currentGem);
                            currentMatches.Add(leftGem);
                            currentMatches.Add(rightGem);
                            if(currentGem.matchNumber==0){
                                currentGem.matchNumber = matchNumber;
                                leftGem.matchNumber = matchNumber;
                                rightGem.matchNumber = matchNumber;
                            }else {
                                currentGem.matchNumber = leftGem.matchNumber;
                                rightGem.matchNumber = leftGem.matchNumber;
                            }
                            
                        }
                    }
                    CheckVerticalMatch(currentGem, x, y, matchNumber);
                }
    }

    private void CheckVerticalMatch(Gem currentGem, int x, int y, int matchNumber){
                Gem downGem = null;
                Gem upGem = null;
                if(currentGem!=null&&y<board.height){
                    if(y-1>=0){downGem = board.allGems[x,y-1];}
                    
                    if(y+1 < board.height){ upGem = board.allGems[x,y+1];}
                    
                    
                    if(upGem!=null&&downGem!=null){
                        if(upGem.type == currentGem.type && downGem.type == currentGem.type){
                            currentGem.isMatched = true;
                            upGem.isMatched = true;
                            downGem.isMatched = true; 
                            currentMatches.Add(currentGem);
                            currentMatches.Add(upGem);
                            currentMatches.Add(downGem);
                            if(currentGem.matchNumber==0){
                                currentGem.matchNumber = matchNumber;
                                upGem.matchNumber = matchNumber;
                                downGem.matchNumber = matchNumber;
                            }else {
                                currentGem.matchNumber = downGem.matchNumber;
                                upGem.matchNumber = downGem.matchNumber;
                            }
                        }
                    }
                }
    }
        //now with two different lists for matched gems and bombed gems i can loop through current matches and count how many of a match there is and then 
        //destroy them, then wait a little and start counting how many were matched for the next set and destroy those. will also be able to use the matchLength
        //variable to determine unique actions depending on how many of a gem type were matched at once
    public IEnumerator DestroyMatches(){
            
            if(currentMatches.Count!=0){
                int matchNumber = currentMatches[0].matchNumber;
            for(int i = 0; i < currentMatches.Count; i++){
                if(currentMatches[i].matchNumber == matchNumber){
                    DestroyMatchedGemAt(currentMatches[i].posIndex);
                }
                if(currentMatches[i].matchNumber != matchNumber){
                    
                    matchNumber = currentMatches[i].matchNumber;
                    yield return new WaitForSeconds(1f);
                    DestroyMatchedGemAt(currentMatches[i].posIndex);
                    
                }
            }
        }
        
        if(bombMarks.Count>=1){
        for(int x = 0; x<bombMarks.Count; x++){
            DestroyMatchedGemAt(bombMarks[x].posIndex);
        }
        bombMarks.Clear();
        }
        currentMatches.Clear();
         StartCoroutine(DecreaseRowCo());
    }

    /* private void StartSequentialDestroy(int matchLength, int currentMatchesPosition){
        for(int i = 0; i<matchLength;i++){
            if(currentMatches[i]!=null){
            DestroyMatchedGemAt(currentMatches[i].posIndex);
            } 
        }
        WaitingForTime(0f);

    } */
    /* private IEnumerator CoRoWait(float timeToWait, int i){
        yield return new WaitForSeconds(timeToWait);
        DestroyMatchedGemAt(currentMatches[i].posIndex);
    } */

    /* private void WaitingForTime(float time){
        while(time>0){
            time -= Time.deltaTime;
            if(time <= 0){
                time = 0;
            }
        }
    } */

    private IEnumerator DecreaseRowCo(){
        yield return new WaitForSeconds(.2f);
        //board.currentState = Board.BoardState.check;

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
                Instantiate(board.allGems[pos.x,pos.y].destroyEffect, new Vector2(pos.x,pos.y), Quaternion.identity);
                Destroy(board.allGems[pos.x,pos.y].gameObject);
                board.allGems[pos.x,pos.y] = null;
                //currentMatches.RemoveAt(0);
            }
        }
    }

    public IEnumerator FillBoardCo(){
        yield return new WaitForSeconds(.5f);
        RefillBoard();

        yield return new WaitForSeconds(.5f);

        FindAllMatches();

        if(currentMatches.Count > 0){
            yield return new WaitForSeconds(.5f);
            StartCoroutine(DestroyMatches());
        }//else{
        //yield return new WaitForSeconds(.5f);
        board.currentState = Board.BoardState.move;
        //}
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
