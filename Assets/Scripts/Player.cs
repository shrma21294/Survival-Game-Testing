using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variable
	public float maxHealth, maxThirst, maxHunger; //going to be more static
	public float thirstIncreaseRate, hungerIncreaseRate; //how much we are supposed to increase thirst and hunger by
	private float health, thirst, hunger; // how much of these the player currently have
	public bool dead;

    public float damage;
    public bool weaponEquipped;

    public static bool triggeringWithAI; // detecting if we are colliding with AI
    public static GameObject triggeringAI; //reference to  the AI

    //Functions
    void Start(){
    	health = maxHealth;
    }

    void Update(){
    	//thirst and hunger increase
    	if(!dead)
    	{
    		hunger += hungerIncreaseRate * Time.deltaTime;
    		thirst += thirstIncreaseRate * Time.deltaTime;
    	}

    	if(thirst >= maxThirst)
    		Die();

    	if(hunger >= maxHunger)
    		Die();	

        //Detecting and killing AI
        if(triggeringWithAI == true && triggeringAI){
            if(Input.GetMouseButtonDown(0)){
                Attack(triggeringAI);
            }
        }   

        if(!triggeringAI)
            triggeringWithAI =false; 

    }


    public void Die(){
    	dead = true;
    	print("Player has died");
    }

    public void Drink(float decreaseRate){
        thirst -= decreaseRate;
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag == "Animal"){
            {
                triggeringAI = other.gameObject;
                triggeringWithAI = true;
            }
        }
    }

    public void OnTriggerExit(Collider other){
        if(other.tag == "Animal"){
            {
                triggeringAI = null;
                triggeringWithAI = false;
            }
        }
    }

    public void Attack(GameObject target){
        if(target.tag == "Animal" && weaponEquipped){
            Animal animal = target.GetComponent<Animal>();
            animal.health -= damage;
        }

    }

}
