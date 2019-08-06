using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //Variables
	private GameObject player;

	public Texture icon;

	public string type;

	public float decreaseRate; //use this for decresing thirst and hunger

	public Vector3 position;
	public Vector3 rotation;
	public Vector3 scale;

	public bool pickedUp;
	public bool equipped;

    //Functions
    public void Start(){
    	player = GameObject.FindWithTag("Player");

    }

    public void Update(){
    	if(equipped){
    		if(Input.GetKeyDown(KeyCode.F)){
    			Unequip();
    		}
    	}
    }

    public void Unequip(){
        player.GetComponent<Player>().weaponEquipped = false;
    	equipped = false;
    	this.gameObject.SetActive(false);
    }
}
