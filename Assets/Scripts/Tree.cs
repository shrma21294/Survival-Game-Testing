using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //Variables 
	public float health; //health of the tree

	public GameObject[] item;

	public static bool dead;//detecting tree if already dead or alive

    //Functions
    void Update(){
    	if(health <=0)
    		dead = true;


    	if(dead){
    		SpawnItems();
    	}
    }

    void SpawnItems(){
    	for(int i=0; i<item.Length; i++){

    		if(item[i] == null)
    			print("Item in the tree named "+ this.gameObject.name +"has not been set! Missing ID: "+i);

    		GameObject spawnItem = Instantiate(item[i], transform.position, Quaternion.identity);
    		Destroy(gameObject);
    	}
    }
}
