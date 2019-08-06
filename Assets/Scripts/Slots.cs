using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //Variables
    private bool hovered;
	public bool empty;

	public GameObject item;
	public Texture itemIcon;

	private GameObject player;

    //Functions
    void Start(){

    	hovered= false;
    	player = GameObject.FindWithTag("Player");

    }

    void Update(){
    	if(item){
    		empty = false;
    		itemIcon = item.GetComponent<Item>().icon;
    		this.GetComponent<RawImage>().texture = itemIcon;
    	}else{
    		empty=true;
    		itemIcon = null;
    		this.GetComponent<RawImage>().texture = null;
    	}
    }

    public void OnPointerEnter(PointerEventData eventData){
    	hovered = true;
    }

     public void OnPointerExit(PointerEventData eventData){
    	hovered = false;
    }

    public void OnPointerClick(PointerEventData eventdata){
    	if(item){
    		Item thisItem = item.GetComponent<Item>(); //to make it easier to refer to the item scrip itself

    		//checking for item type
    		if(thisItem.type == "Water"){
    			player.GetComponent<Player>().Drink(thisItem.decreaseRate);
    			Destroy(item);
    		}

    		if(thisItem.type == "Weapon" && player.GetComponent<Player>().weaponEquipped == false){
    			thisItem.equipped = true;
    			item.SetActive(true);
    		}
    	}
    }
}
