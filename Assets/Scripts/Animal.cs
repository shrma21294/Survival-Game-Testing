using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    //Variables
	private GameObject player;

	public float health;//health for the animal
	private bool dead;

	public int amountOfItems;
	public GameObject[] item;

	public float radius; //amount of radius animal can walk
	public float timer; //amount of the time we have the animal to cover certain amount of distance and then change the route;
	
	private Transform target;
	private NavMeshAgent agent;
	private float currentTimer; // will compete against timer

	private bool idle;
	public float idleTimer;
	private float currentIdleTimer;

	private Animation anime;

    //Functions
    void OnEnable(){
    	agent = GetComponent<NavMeshAgent>();
    	currentTimer = timer;

    	currentIdleTimer = idleTimer;
    	anime = GetComponent<Animation>();

    	player = GameObject.FindWithTag("Player");

    }

    void Update(){
    	currentTimer += Time.deltaTime; //increase current timer by every second
    	currentIdleTimer += Time.deltaTime;

    	if(currentIdleTimer >= idleTimer){
    		StartCoroutine("switchIdle");
    	}

    	if(currentTimer >= timer && !idle){
    		Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
    		agent.SetDestination(newPosition);
    		currentTimer = 0;
    	}

    	if(idle)
    		anime.CrossFade("idle");
    	else	
    		anime.CrossFade("walk");

    	if(health <= 0){
    		Die();
    	}	
    }

    IEnumerator switchIdle(){
    	idle = true;
    	yield return new WaitForSeconds(3);
    	currentIdleTimer = 0;
    	idle = false;
    }

    public void DropItems(){
    	for(int i = 0; i< amountOfItems; i++){
    		//for every item that we have, we are going to drop the item one by one and going to tranform the position 

    		Instantiate(item[i], transform.position, Quaternion.identity); 
    		break;
    	}
    }

     public void Die(){
    	DropItems();
    	Destroy(this.gameObject);
    }


    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask){
    	Vector3 randomDirection = Random.insideUnitSphere * distance;
    	randomDirection += origin;

    	NavMeshHit navHit;
    	NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

    	return navHit.position;
    }
}
