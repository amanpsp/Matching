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
    private MatchFinder matchFind;

    private void Awake(){
        matchFind = FindObjectOfType<MatchFinder>();
    }
    void Start()
    {
        allGems = new Gem[width,height];
        SetUp();
        
    }

    private void Update(){

        matchFind.FindAllMatches();

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

    private void SpawnGem(Vector2Int pos,Gem gemToSpawn){
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        gem.transform.parent = this.transform;
        gem.name = "Gem - "+ pos.x +"," + pos.y;
        allGems[pos.x,pos.y]=gem;
        gem.SetupGem(pos,this);
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
}
