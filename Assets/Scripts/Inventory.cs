using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Variable
	public GameObject inventory; //Entire canvas
	public GameObject slotHolder;
    public GameObject itemManager;
	private bool inventoryEnabled;
	private bool itemAdded;

	private int slots; //calculating the amount of slots in the game
	private Transform[] slot; //to access all the slots individually

	private GameObject itemPickedUp;

    //Functions
    public void Start(){
    	//Slots beign detected
    	slots = slotHolder.transform.childCount; //give you number of slots
    	slot = new Transform[slots];
    	DetectInventorySlots();
 
    }

    public void Update(){
    	if(Input.GetKeyDown(KeyCode.I)){
    		inventoryEnabled = !inventoryEnabled;
    	}

    	if(inventoryEnabled){
    		inventory.GetComponent<Canvas>().enabled = true;
    	}else{
    		inventory.GetComponent<Canvas>().enabled = false;
    	}
    }


    public void OnTriggerEnter(Collider other){
    	if(other.tag == "Item"){
    		itemPickedUp = other.gameObject;
    		AddItem(itemPickedUp);
    	}
    }

    public void OnTriggerExit(Collider other){
    	if(other.tag == "Item"){
    		itemAdded = false;
    	}
    }


    public void AddItem(GameObject item){
    	for(int i= 0; i<slots;i++){
    		if(slot[i].GetComponent<Slots>().empty && itemAdded ==false){
    			slot[i].GetComponent<Slots>().item = itemPickedUp;
    			slot[i].GetComponent<Slots>().itemIcon = itemPickedUp.GetComponent<Item>().icon;

                item.transform.parent = itemManager.transform; //addin item to the itemManager
                item.transform.position = itemManager.transform.position;

                //changing transform in relative to the parent transform. Set the position of the relative to the child in the item manager
                item.transform.localPosition = item.GetComponent<Item>().position;
                item.transform.localEulerAngles = item.GetComponent<Item>().rotation;
                item.transform.localScale = item.GetComponent<Item>().scale;

                Destroy(item.GetComponent<Rigidbody>());

    			itemAdded =true;

                item.SetActive(false);
    		}
    	}
    }

    public void DetectInventorySlots(){

    	for(int i= 0; i<slots;i++){
    		slot[i] = slotHolder.transform.GetChild(i); //brings the inventory of each slot
    	}

    }
}
