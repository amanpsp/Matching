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

}
